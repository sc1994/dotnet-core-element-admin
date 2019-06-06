<template>
  <div>
    <el-container style="height: 90vh;padding:10px">
      <el-aside width="490px" style="padding: 0px;margin-bottom: 0px;margin-top: 5px;">
        <el-card>
          <el-divider content-position="left">URL</el-divider>
          <el-input
            type="textarea"
            autosize
            placeholder="输入url。需要动态参数，请以<#value/>占位。"
            v-model="form.url"
          ></el-input>

          <el-divider content-position="left">Method</el-divider>
          <el-radio-group v-model="form.method">
            <el-radio-button label="get"></el-radio-button>
            <el-radio-button label="post"></el-radio-button>
            <el-radio-button label="put"></el-radio-button>
          </el-radio-group>

          <el-divider content-position="left">Headers</el-divider>
          <div
            v-for="item in form.headers"
            :key="'headers'+form.headers.indexOf(item)"
            style="margin-top: 6px;"
          >
            <el-input v-model="item.key" placeholder="Key" style="width:30%"></el-input>
            <el-input v-model="item.value" placeholder="以<#value/>占位。" style="width:40%"></el-input>
            <el-button-group style="margin-top: -2px;margin-left: 10px;">
              <el-button
                icon="el-icon-circle-plus-outline"
                type="success"
                plain
                @click="form.headers.push({})"
              ></el-button>
              <el-button
                icon="el-icon-remove-outline"
                type="warning"
                plain
                @click="form.headers.length > 1? (form.headers = form.headers.filter(x => x != item)): ''"
              ></el-button>
            </el-button-group>
          </div>

          <el-divider content-position="left">Cookics</el-divider>
          <div
            v-for="item in form.cookics"
            :key="'cookics'+form.cookics.indexOf(item)"
            style="margin-top: 6px;"
          >
            <el-input v-model="item.key" placeholder="Key" style="width:30%"></el-input>
            <el-input v-model="item.value" placeholder="以<#value/>占位。" style="width:40%"></el-input>
            <el-button-group style="margin-top: -2px;margin-left: 10px;">
              <el-button
                icon="el-icon-circle-plus-outline"
                type="success"
                plain
                @click="form.cookics.push({})"
              ></el-button>
              <el-button
                icon="el-icon-remove-outline"
                type="warning"
                plain
                @click="form.cookics.length > 1? (form.cookics = form.cookics.filter(x => x != item)): ''"
              ></el-button>
            </el-button-group>
          </div>

          <div v-if="form.method != 'get'">
            <el-divider content-position="left">Body</el-divider>
            <el-switch
              style="display: block"
              v-model="form.switchBody"
              active-color="#13ce66"
              inactive-color="#1890ff"
              active-text="键值"
              inactive-text="json"
            ></el-switch>
            <br>
            <div v-if="form.switchBody">
              <div v-for="item in form.body" :key="item.key" style="margin-top: 6px;">
                <el-input v-model="item.key" placeholder="Key" style="width:30%"></el-input>
                <el-input v-model="item.value" placeholder="以<#value/>占位。" style="width:40%"></el-input>
                <el-button-group style="margin-top: -2px;margin-left: 10px;">
                  <el-button
                    icon="el-icon-circle-plus-outline"
                    type="success"
                    plain
                    @click="form.body.push({})"
                  ></el-button>
                  <el-button
                    icon="el-icon-remove-outline"
                    type="warning"
                    plain
                    @click="form.body.length > 1 ? (form.body = form.body.filter(x => x != item)) : ''"
                  ></el-button>
                </el-button-group>
              </div>
            </div>
            <el-button
              v-else
              type="primary"
              icon="el-icon-search"
              @click="dialogBodyJson = true"
            >打开json编辑器</el-button>
          </div>

          <el-divider content-position="left">Params（注：每一组应为以上涉及到的全部参数）</el-divider>
          <el-button
            type="primary"
            icon="el-icon-search"
            @click="dialogDynamicJson = true"
          >打开json编辑器</el-button>
          <el-divider content-position="left">Assert</el-divider>
          <el-select v-model="form.assert" style="width:20%">
            <el-option label="包含" value="0"/>
            <el-option label="全等" value="1"/>
          </el-select>
          <el-input v-model="form.assertValue" placeholder="断言内容" style="width:78%"></el-input>
          <el-divider content-position="left">并发</el-divider>
          <el-form label-width="100px">
            <el-form-item label="线程数：">
              <el-input v-model="form.thread" type="number"></el-input>
            </el-form-item>
            <el-form-item label="循环次数：">
              <el-input v-model="form.cycle" type="number"></el-input>
            </el-form-item>
            <el-form-item label="间隔ms：">
              <el-input v-model="form.delay" type="number"></el-input>
            </el-form-item>
          </el-form>
          <el-divider content-position="left">其他</el-divider>
          <el-tooltip content="意味着将错乱使用动态参数的值，如果你的动态值是互相依赖的则不要使用这个属性" placement="top-start">
            <el-checkbox v-model="form.mixDynamic" label="混淆动态参数" border></el-checkbox>
          </el-tooltip>
          <br>
          <br>
          <div>
            <el-button type="primary" round>保 存</el-button>
            <el-button round>导 入</el-button>
            <el-button type="success" round @click="start" style="float: right" v-if="!running">
              开始
              <i class="el-icon-video-play"></i>
            </el-button>
            <el-button type="danger" round @click="abort" style="float: right" v-else>
              取消
              <i class="el-icon-video-pause"></i>
            </el-button>
          </div>
        </el-card>
      </el-aside>
      <el-container style="width:100%">
        <el-header style="height: 11vh;">
          <el-steps
            style="margin-left: 8%;margin-top: 18px;"
            align-center
            :space="200"
            :active="nexts.index"
            :finish-status="nexts.status"
          >
            <el-step title="参数验证"></el-step>
            <el-step title="初始化线程"></el-step>
            <el-step title="预热"></el-step>
            <el-step title="压测"></el-step>
            <el-step :title="stoped ? '已取消' : '全部完成'"></el-step>
          </el-steps>
          <el-progress
            :percentage="percentage"
            v-if="percentage > 0 && percentage < 100"
            :text-inside="true"
            :stroke-width="17"
            :status="stoped ? 'exception' : 'success'"
          ></el-progress>
        </el-header>
        <el-main style="padding: 0px;">
          <el-container style="height: 76vh;padding:0px 20px;padding-right: 0px;">
            <el-aside
              id="div_response"
              width="260px"
              style="padding:0px 10px;margin-bottom: 0px;background-color: #fff;"
            >
              <el-collapse style="border: 0px;" :value="responses.map(x=>x.index)">
                <el-collapse-item
                  v-for="response in responses"
                  :key="response.index"
                  :name="response.index"
                  style="padding-bottom: 10px;"
                >
                  <template slot="title">
                    <i
                      class="el-icon-error"
                      v-if="stoped && response.contents.filter(x=> x.type == 'success').length != form.cycle"
                      style="color:#ff4949;"
                    ></i>
                    <i class="el-icon-loading" v-else-if="!response.isDone"></i>
                    <i class="el-icon-check" v-else style="color:#67C23A;"></i>
                    &nbsp;&nbsp;
                    轮次：{{response.index + 1}}
                  </template>
                  <el-tag
                    v-for="content in response.contents"
                    :key="content.id"
                    :type="content.type"
                    style="margin-top:5px;margin-left:5px;"
                  >{{stoped && content.des == '请求中...' ? '已取消' : content.des}}</el-tag>
                </el-collapse-item>
              </el-collapse>
            </el-aside>
            <el-main>
              <el-table
                v-if="tableData.length>0"
                :data="tableData"
                border
                :summary-method="getAverage"
                show-summary
                style="width: 100%; margin-top: 0px"
              >
                <el-table-column prop="index" sortable label="轮次" width="90"></el-table-column>
                <el-table-column prop="count" label="已完成"></el-table-column>
                <el-table-column prop="average" sortable label="均值" width="110"></el-table-column>
                <el-table-column prop="fifty" sortable label="50线"></el-table-column>
                <el-table-column prop="ninetyFive" sortable label="95线"></el-table-column>
                <el-table-column prop="ninetyNine" sortable label="99线"></el-table-column>
                <el-table-column prop="min" sortable label="Min"></el-table-column>
                <el-table-column prop="max" sortable label="Max"></el-table-column>
                <el-table-column prop="error" label="Error"></el-table-column>
              </el-table>
            </el-main>
          </el-container>
        </el-main>
      </el-container>
    </el-container>
    <el-dialog
      v-el-drag-dialog
      :visible.sync="dialogBodyJson"
      title="编辑Body"
      @dragDialog="handleDrag"
    >
      <json-editor ref="jsonEditor" v-model="form.bodyJson"/>
    </el-dialog>
    <el-dialog
      v-el-drag-dialog
      :visible.sync="dialogDynamicJson"
      title="编辑动态参数"
      @dragDialog="handleDrag"
    >
      <json-editor ref="jsonEditor" v-model="form.dynamicJson"/>
    </el-dialog>
  </div>
</template>

<script>
import { start, abort } from "@/api/tools";
import { connection } from "@/utils/websocket";
import JsonEditor from "@/components/JsonEditor";
import elDragDialog from "@/directive/el-drag-dialog"; // base on element-ui
var tempResponses, timeSet;
const average = (arr, selector) => sum(arr, selector) / arr.length;
const sum = (arr, selector) => arr.map(selector).reduce((p, c) => p + c, 0);

export default {
  name: "stresstest",
  directives: { elDragDialog },
  components: { JsonEditor },
  data() {
    return {
      nexts: [],
      dialogBodyJson: false,
      dialogDynamicJson: false,
      form: {
        dynamicJson: [],
        bodyJson: {},
        headers: [{}],
        cookics: [{}],
        body: [{}],
        method: "get",
        url: "https://www.google.com/",
        assert: "0",
        assertValue: "name",
        thread: 10,
        cycle: 10,
        delay: 100
      },
      ws: {},
      percentage: 0,
      activeNames: [1],
      responses: [],
      resultCount: 0,
      tableData: [],
      running: false,
      stoped: false
    };
  },
  methods: {
    handleDrag() {},
    async start() {
      this.running = true;
      this.stoped = false;
      this.nexts = {
        index: -1,
        status: ""
      };
      tempResponses = [];
      this.percentage = 0;
      this.resultCount = 0;
      let that = JSON.parse(JSON.stringify(this.form));
      that.dynamicJson = JSON.stringify(this.form.dynamicJson);
      that.bodyJson = JSON.stringify(this.form.bodyJson);
      that.headers = this.form.headers.filter(x => x.key && x.value);
      that.cookics = this.form.cookics.filter(x => x.key && x.value);
      that.body = this.form.body.filter(x => x.key && x.value);
      that.connectedId = this.ws.id;
      await start(that);
      timeSet = setInterval(_ => {
        this.responses = JSON.parse(JSON.stringify(tempResponses));
        this.percentage = Number.parseInt(
          (this.resultCount / (this.form.thread * this.form.cycle)) * 100
        );
        let div = document.getElementById("div_response");
        div.scrollTop = div.scrollHeight;
        this.tableData = this.responses.map(x => {
          return {
            index: x.index,
            count: x.contents.filter(f => f.des != "请求中...").length,
            average: average(x.contents.filter(f => f.des != "请求中..."), s =>
              Number.parseFloat(s.des)
            ).toFixed(3),
            // average
            // fifty
            // ninetyFive
            // ninetyNine
            min: Math.min(
              ...x.contents
                .filter(f => f.des != "请求中...")
                .map(x => Number.parseFloat(x.des))
            ).toFixed(3),
            max: Math.max(
              ...x.contents
                .filter(f => f.des != "请求中...")
                .map(x => Number.parseFloat(x.des))
            ).toFixed(3)
            // error
          };
        });
      }, 300);
    },
    async abort() {
      await abort(this.ws.id);
      this.running = false;
      this.stoped = true;
      this.nexts.index = 4;
      this.nexts.status = "error";
      let div = document.getElementById("div_response");
      div.scrollTop = div.scrollHeight;
      clearInterval(timeSet);
    },
    onNext() {
      this.ws.connection.on("next", (index, status) => {
        this.nexts.index = index;
      });
    },
    onSending() {
      this.ws.connection.on("sending", (index, key) => {
        var response = tempResponses.find(x => x.index == index);
        var contents;
        if (!response) {
          response = {
            index: index,
            contents: [
              {
                id: key,
                type: "info",
                des: "请求中..."
              }
            ]
          };
          tempResponses.push(response);
          return;
        }
        response.contents.push({
          id: key,
          type: "info",
          des: "请求中..."
        });
      });
    },
    onResult() {
      this.ws.connection.on("result", (index, key, time) => {
        var response = tempResponses.find(x => x.index == index);
        if (response) {
          var content = response.contents.find(x => x.id == key);
          if (content) {
            content.type = "success";
            content.des = time.toFixed(3);
            this.resultCount++;
          }
          if (
            response.contents.filter(x => x.type == "success").length ==
            this.form.thread
          ) {
            response.isDone = true;
          }
        }
      });
    },
    onAssertError() {
      this.ws.connection.on("assertError", (index, key) => {});
    },
    onError() {
      this.ws.connection.on("error", (index, key) => {});
    },
    onOver() {
      this.ws.connection.on("over", _ => {
        this.nexts.index++;
        setTimeout(_ => {
          this.percentage = 100;
          this.responses = JSON.parse(JSON.stringify(tempResponses));
          let div = document.getElementById("div_response");
          div.scrollTop = div.scrollHeight;
          clearInterval(timeSet);
        }, 300);
        this.running = false;
        this.nexts.index++;
        this.nexts.status = "success";
      });
    },
    getAverage(param) {
      const { columns, data } = param;
      const sums = [];
      columns.forEach((column, index) => {
        if (index === 0) {
          sums[index] = "总均值";
          return;
        }
        if (index === 1) {
          sums[index] = "-";
          return;
        }
        const values = data
          .map(x => Number(x[column.property]))
          .filter(x => !isNaN(x) && x != Infinity && x != -Infinity);
        if (values.length > 0) {
          sums[index] = average(values, x => x).toFixed(3);
        } else {
          sums[index] = "-";
        }
      });

      return sums;
    }
  },
  async mounted() {
    this.ws = await connection("sth");
    this.onNext();
    this.onSending();
    this.onResult();
    this.onError();
    this.onOver();
  }
};
</script>

