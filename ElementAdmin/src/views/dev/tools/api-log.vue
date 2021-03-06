<template>
  <div class="app-container">
    <el-row :gutter="30">
      <el-col :span="6">
        <el-form label-width="100px">
          <el-form-item label="方法名：">
            <el-input v-model="methodName"></el-input>
          </el-form-item>
        </el-form>
      </el-col>
      <el-col :span="8">
        <el-form label-width="100px">
          <el-form-item label="时间范围：">
            <el-date-picker
              v-model="timestamp"
              type="datetimerange"
              :picker-options="pickerOptions"
              range-separator="至"
              start-placeholder="开始日期"
              end-placeholder="结束日期"
              format="yyyy-MM-dd HH:mm:ss"
              value-format="yyyy-MM-dd HH:mm:ss"
              align="left"
            ></el-date-picker>
          </el-form-item>
        </el-form>
      </el-col>
      <el-col :span="6">
        <el-form label-width="100px">
          <el-form-item label="异常：">
            <el-checkbox v-model="onlyError" label="只看异常" border></el-checkbox>
          </el-form-item>
        </el-form>
      </el-col>
    </el-row>
    <el-form label-width="100px">
      <el-form-item label="刷新：">
        <el-checkbox v-model="autoRefresh" label="自动刷新" border></el-checkbox>
        <el-button type="primary" plain @click="search(1)">搜 索</el-button>
      </el-form-item>
    </el-form>
    <el-table
      :data="tableData"
      style="font-size: 12px;width: 100%"
      :row-class-name="tableRowClassName"
      v-loading="loading"
      row-key="id"
      lazy
      :load="expandChange"
    >
      <el-table-column label="时间" prop="time" width="230"></el-table-column>
      <el-table-column label="方法名" prop="method" width="220">
        <template slot-scope="props">
          <el-popover placement="top" effect="light" trigger="hover">
            <span>{{props.row.fullMethod}}</span>
            <el-button
              slot="reference"
              type="text"
              class="cut-out"
              @click="handleCopy(props.row.method,$event)"
            >{{props.row.method}}</el-button>
          </el-popover>
        </template>
      </el-table-column>
      <el-table-column label="入参" prop="params" width="450">
        <template slot-scope="props">
          <el-popover placement="top" effect="light" trigger="click">
            <div v-html="props.row.paramsHtml" style="max-width: 800px"></div>
            <el-button
              slot="reference"
              type="text"
              class="cut-out"
              @click="handleCopy(props.row.params,$event)"
            >{{props.row.params}}</el-button>
          </el-popover>
        </template>
      </el-table-column>
      <el-table-column label="返回值/异常堆栈" prop="returnValue">
        <template slot-scope="props">
          <el-popover placement="top" effect="light" trigger="click">
            <div v-html="props.row.returnValueHtml" style="max-width: 800px"></div>
            <el-button
              slot="reference"
              type="text"
              class="cut-out"
              @click="handleCopy(props.row.returnValue,$event)"
            >{{props.row.returnValue}}</el-button>
          </el-popover>
        </template>
      </el-table-column>
      <el-table-column label="耗时(ms)" prop="elapsed" width="120"></el-table-column>
    </el-table>
    <br>
    <div style="text-align: end;">
      <el-pagination
        background
        layout="total, prev, pager, next"
        :total="total"
        @current-change="search"
      ></el-pagination>
    </div>
  </div>
</template>

<script>
import clip from "@/utils/clipboard";
import { search, searchChild } from "@/api/tools";
var timeSet;

export default {
  data() {
    return {
      autoRefresh: false,
      pickerOptions: {
        shortcuts: [
          {
            text: "最近半小时",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 500);
              picker.$emit("pick", [start, end]);
            }
          },
          {
            text: "最近一小时",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000);
              picker.$emit("pick", [start, end]);
            }
          },
          {
            text: "最近三小时",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 3);
              picker.$emit("pick", [start, end]);
            }
          },
          {
            text: "最近八小时",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 8);
              picker.$emit("pick", [start, end]);
            }
          }
        ]
      },
      total: 0,
      methodName: "",
      timestamp: [],
      tableData: [],
      onlyError: false,
      loading: false,
      pageIndex: 0
    };
  },
  methods: {
    handleCopy(text, event) {
      clip(text, event);
    },
    async search(index) {
      this.pageIndex = index;
      this.loading = true;
      var response = await search({
        size: 10,
        index: index,
        form: {
          methodName: this.methodName,
          timestamp: this.timestamp,
          onlyError: this.onlyError
        }
      });
      this.tableData = [];
      var data = JSON.parse(response.data);
      console.log(data);
      this.total = data.hits.total;
      data.hits.hits.forEach(x => {
        let temp = this.esToTable(x);
        temp.hasChildren = true;
        this.tableData.push(temp);
      });
      this.loading = false;
    },
    async expandChange(tree, treeNode, resolve) {
      let that = [];
      let response = await searchChild(tree.tracerId);
      let data = JSON.parse(response.data);
      console.log(data);
      data.hits.hits.forEach(x => {
        that.push(this.esToTable(x));
      });
      resolve(that);
    },
    esToTable(x) {
      var sum = 0;
      for (var p in x._source.fields.performance) {
        sum += x._source.fields.performance[p];
      }
      var paramsHtml = this.syntaxHighlight(x._source.fields.parameters);
      var returnValueHtml;
      var returnValue;
      if (x._source.fields.error) {
        returnValue = x._source.fields.error;
        returnValueHtml = x._source.fields.error;
      } else {
        returnValue = x._source.fields.return_value;
        var returnValueHtml = this.syntaxHighlight(
          x._source.fields.return_value
        );
      }

      return {
        id: x._id,
        time: new Date(
          (x._source.fields.start_timestamp - 621355968000000000) / 10000
        ).toISOString(),
        method: x._source.fields.method,
        fullMethod: x._source.fields.full_method,
        params: x._source.fields.parameters,
        paramsHtml: paramsHtml,
        elapsed: sum.toFixed(4),
        elapsedDetail: x._source.fields.performance,
        returnValue: returnValue,
        returnValueHtml: returnValueHtml,
        tracerId: x._source.fields.tracer_id,
        error: x._source.fields.error
      };
    },
    tableRowClassName({ row, index }) {
      if (row.error) {
        return "warning-row";
      }
      if (!row.hasChildren) {
        return "children-row";
      }
      return "";
    },
    syntaxHighlight(json) {
      if (typeof json != "string") {
        json = JSON.stringify(json, undefined, 2);
      } else {
        json = JSON.stringify(JSON.parse(json), undefined, 2);
      }
      json = json
        .replace(/&/g, "&")
        .replace(/</g, "<")
        .replace(/>/g, ">");
      return json.replace(
        /("(\\u[a-zA-Z0-9]{4}|\\[^u]|[^\\"])*"(\s*:)?|\b(true|false|null)\b|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?)/g,
        function(match) {
          var cls = "number";
          if (/^"/.test(match)) {
            if (/:$/.test(match)) {
              cls = "key";
            } else {
              cls = "string";
            }
          } else if (/true|false/.test(match)) {
            cls = "boolean";
          } else if (/null/.test(match)) {
            cls = "null";
          }
          return '<span class="' + cls + '">' + match + "</span>";
        }
      );
    }
  },
  watch: {
    autoRefresh(val) {
      if (val) {
        timeSet = setInterval(() => {
          if (this.timestamp && this.timestamp.length == 2) {
            if (new Date() - new Date(this.timestamp[1]) < 10000) {
              var options = {
                year: "numeric",
                month: "numeric",
                day: "numeric",
                hour: "numeric",
                minute: "numeric",
                second: "numeric",
                hour12: false
              };
              var start = new Date();
              start.setTime(
                start.getTime() -
                  (new Date(this.timestamp[1]) - new Date(this.timestamp[0]))
              );
              this.timestamp = [
                new Intl.DateTimeFormat("ZH-cn", options).format(start),
                new Intl.DateTimeFormat("ZH-cn", options).format(new Date())
              ];
            }
          }
          this.search(this.pageIndex);
        }, 3000);
      } else {
        clearInterval(timeSet);
      }
    }
  },
  mounted() {
    this.search(1);
  },
  destroyed() {
    clearInterval(timeSet);
  }
};
</script>

<style>
pre {
  outline: 1px solid #ccc;
  padding: 5px;
  margin: 5px;
}

.string {
  color: green;
}

.number {
  color: darkorange;
}

.boolean {
  color: blue;
}

.null {
  color: magenta;
}

.key {
  color: red;
}

.cut-out {
  overflow: hidden;
}

.el-table .warning-row {
  background: rgb(253, 226, 226);
}

.el-table .children-row {
  background: rgb(225, 243, 216);
}

.el-button--medium {
  font-size: 12px !important;
}
</style>



