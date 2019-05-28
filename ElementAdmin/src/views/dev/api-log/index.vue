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
      <el-col :span="12">
        <el-form label-width="100px">
          <el-form-item label="时间范围：">
            <div class="block">
              <el-date-picker
                v-model="timestamp"
                type="datetimerange"
                :picker-options="pickerOptions"
                range-separator="至"
                start-placeholder="开始日期"
                end-placeholder="结束日期"
                format="yyyy-MM-dd HH:mm:ss"
                value-format="yyyy-MM-dd HH:mm:ss"
                align="right"
              ></el-date-picker>
            </div>
          </el-form-item>
        </el-form>
      </el-col>
    </el-row>
    <div style="text-align: end;">
      <el-button type="primary" plain @click="search(1)">搜 索</el-button>
    </div>
    <el-table :data="tableData" style="width: 100%" @expand-change="expandChange">
      <el-table-column type="expand">
        <template slot-scope="props">
          <el-table :data="props.row.tableData" style="width: 100%" border>
            <el-table-column label="时间" prop="time" width="200"></el-table-column>
            <el-table-column label="方法名" prop="method" width="220">
              <template slot-scope="props">
                <el-tooltip placement="top" effect="light">
                  <div slot="content">
                    <span>{{props.row.fullMethod}}</span>
                  </div>
                  <el-button
                    type="text"
                    class="cut-out"
                    @click="handleCopy(props.row.method,$event)"
                  >{{props.row.method}}</el-button>
                </el-tooltip>
              </template>
            </el-table-column>
            <el-table-column label="入参" prop="params" width="350">
              <template slot-scope="props">
                <el-tooltip placement="top" effect="light">
                  <div slot="content">
                    <pre v-html="props.row.paramsHtml"></pre>
                  </div>
                  <el-button
                    type="text"
                    class="cut-out"
                    @click="handleCopy(props.row.params,$event)"
                  >{{props.row.params}}</el-button>
                </el-tooltip>
              </template>
            </el-table-column>
            <el-table-column label="返回值" prop="returnValue">
              <template slot-scope="props">
                <el-tooltip placement="top" effect="light">
                  <div slot="content">
                    <pre v-html="props.row.returnValueHtml"></pre>
                  </div>
                  <el-button
                    type="text"
                    class="cut-out"
                    @click="handleCopy(props.row.returnValue,$event)"
                  >{{props.row.returnValue}}</el-button>
                </el-tooltip>
              </template>
            </el-table-column>
            <el-table-column label="耗时" prop="elapsed" width="120"></el-table-column>
          </el-table>
        </template>
      </el-table-column>
      <el-table-column label="时间" prop="time" width="200"></el-table-column>
      <el-table-column label="方法名" prop="method" width="220">
        <template slot-scope="props">
          <el-tooltip placement="top" effect="light">
            <div slot="content">
              <span>{{props.row.fullMethod}}</span>
            </div>
            <el-button
              type="text"
              class="cut-out"
              @click="handleCopy(props.row.method,$event)"
            >{{props.row.method}}</el-button>
          </el-tooltip>
        </template>
      </el-table-column>
      <el-table-column label="入参" prop="params" width="450">
        <template slot-scope="props">
          <el-tooltip placement="top" effect="light">
            <div slot="content">
              <pre v-html="props.row.paramsHtml"></pre>
            </div>
            <el-button
              type="text"
              class="cut-out"
              @click="handleCopy(props.row.params,$event)"
            >{{props.row.params}}</el-button>
          </el-tooltip>
        </template>
      </el-table-column>
      <el-table-column label="返回值" prop="returnValue">
        <template slot-scope="props">
          <el-tooltip placement="top" effect="light">
            <div slot="content">
              <pre v-html="props.row.returnValueHtml"></pre>
            </div>
            <el-button
              type="text"
              class="cut-out"
              @click="handleCopy(props.row.returnValue,$event)"
            >{{props.row.returnValue}}</el-button>
          </el-tooltip>
        </template>
      </el-table-column>
      <el-table-column label="耗时" prop="elapsed" width="120"></el-table-column>
    </el-table>
    <br>
    <div style="text-align: end;">
      <el-pagination background layout="prev, pager, next" :total="total" @current-change="search"></el-pagination>
    </div>
  </div>
</template>

<script>
import clip from "@/utils/clipboard";
import { search, searchChild } from "@/api/api-log";

export default {
  data() {
    return {
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
      tableData: []
    };
  },
  methods: {
    handleCopy(text, event) {
      clip(text, event);
    },
    async search(index) {
      var response = await search({
        size: 10,
        index: index,
        form: {
          methodName: this.methodName,
          timestamp: this.timestamp
        }
      });
      this.tableData = [];
      var data = JSON.parse(response.data);
      console.log(data);
      this.total = data.hits.total;
      data.hits.hits.forEach(x => {
        this.tableData.push(this.esToTable(x));
      });
    },
    async expandChange(row) {
      if (!row.tableData) {
        row.tableData = [];
        var response = await searchChild(row.tracerId);
        var data = JSON.parse(response.data);
        console.log(data);
        data.hits.hits.forEach(x => {
          row.tableData.push(this.esToTable(x));
        });
      }
    },
    esToTable(x) {
      var sum = 0;
      for (var p in x._source.fields.performance) {
        sum += x._source.fields.performance[p];
      }
      var paramsHtml = this.syntaxHighlight(x._source.fields.parameters);
      var returnValueHtml = this.syntaxHighlight(x._source.fields.return_value);

      return {
        time: new Date(
          (x._source.fields.start_timestamp - 621355968000000000) / 10000
        ).toISOString(),
        method: x._source.fields.method,
        fullMethod: x._source.fields.full_method,
        params: x._source.fields.parameters,
        paramsHtml: paramsHtml,
        elapsed: sum.toFixed(4),
        elapsedDetail: x._source.fields.performance,
        returnValue: x._source.fields.return_value,
        returnValueHtml: returnValueHtml,
        tracerId: x._source.fields.tracer_id
      };
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
  mounted() {
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
    start.setTime(start.getTime() - 3600 * 500);

    this.timestamp = [
      new Intl.DateTimeFormat("ZH-cn", options).format(start),
      new Intl.DateTimeFormat("ZH-cn", options).format(new Date())
    ];
    this.search(1);
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
  line-height: 30px;
  text-align: center;
  text-overflow: ellipsis;
  white-space: nowrap;
  overflow: hidden;
  display: block;
}
</style>



