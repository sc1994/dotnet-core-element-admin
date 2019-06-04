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
            <el-button type="success" round @click="start" style="float: right">
              开始
              <i class="el-icon-caret-right"></i>
            </el-button>
          </div>
        </el-card>
      </el-aside>
      <el-container style="width:100%">
        <el-header style="height: 11vh;">
          <el-steps
            style="margin-left: 10%;margin-top: 18px;"
            align-center
            :space="200"
            :active="nexts.length"
          >
            <el-step v-for="item in nexts" :key="item.msg" :title="item.msg" :status="item.status"></el-step>
          </el-steps>
          <el-progress
            :percentage="percentage"
            v-if="percentage > 0"
            :text-inside="true"
            :stroke-width="17"
          ></el-progress>
        </el-header>
        <el-main style="padding: 0px;">
          <el-container style="height: 76vh;padding:0px 20px;">
            <el-aside
              id="div_response"
              width="300px"
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
                    <i class="el-icon-loading" v-if="!response.isDone"></i>
                    <i class="el-icon-check" style="color:#67C23A;" v-else></i>
                    &nbsp;&nbsp;
                    轮次：{{response.index + 1}}
                  </template>
                  <el-tag
                    v-for="content in response.contents"
                    :key="content.id"
                    :type="content.type"
                    style="margin-top:5px;margin-left:5px;"
                  >{{content.des}}</el-tag>
                </el-collapse-item>
              </el-collapse>
            </el-aside>
            <el-main>Main</el-main>
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
import { start } from "@/api/tools";
import { connection } from "@/utils/websocket";
import JsonEditor from "@/components/JsonEditor";
import elDragDialog from "@/directive/el-drag-dialog"; // base on element-ui
var tempResponses, timeSet;

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
        url: "http://10.101.72.6:9222/",
        assert: "0",
        assertValue: "name",
        thread: 10,
        cycle: 30,
        delay: 100
      },
      ws: {},
      percentage: 0,
      activeNames: [1],
      responses: [],
      resultCount: 0
    };
  },
  methods: {
    handleDrag() {},
    async start() {
      this.nexts = [];
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
      }, 300);
    },
    onNext() {
      this.ws.connection.on("next", (msg, status) => {
        if (status != "error") {
          this.nexts.push({
            msg,
            status: "success"
          });
        } else {
          this.nexts.push({
            msg,
            status: "error"
          });
        }
        if (msg == "预热完毕") {
          this.nexts.push({
            msg: "开始压测",
            status: "process "
          });
        }
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
        var first = this.nexts.find(x => x.msg == "开始压测");
        if (first) {
          first.msg = "压测完毕";
          first.status = "success";
        }
        setTimeout(_ => {
          this.percentage = 100;
          this.responses = JSON.parse(JSON.stringify(tempResponses));
          let div = document.getElementById("div_response");
          div.scrollTop = div.scrollHeight;
          clearInterval(timeSet);
        }, 300);
      });
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
