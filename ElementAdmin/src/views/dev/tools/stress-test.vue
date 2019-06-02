<template>
  <div>
    <el-container>
      <el-aside width="490px" style="padding: 0px;height: 88vh;">
        <el-card class="box-card" style="min-height: 88vh">
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
            :key="form.headers.indexOf(item)"
            style="margin-top: 6px;"
          >
            <el-input
              v-model="item.key"
              placeholder="Key"
              style="width:30%"
            ></el-input>
            <el-input
              v-model="item.value"
              placeholder="以<#value/>占位。"
              style="width:40%"
            ></el-input>
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
            <br />
            <div v-if="form.switchBody">
              <div
                v-for="item in form.body"
                :key="item.key"
                style="margin-top: 6px;">
                <el-input
                  v-model="item.key"
                  placeholder="Key"
                  style="width:30%"
                ></el-input>
                <el-input
                  v-model="item.value"
                  placeholder="以<#value/>占位。"
                  style="width:40%"
                ></el-input>
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
                    @click="
                      form.body.length > 1
                        ? (form.body = form.body.filter(x => x != item))
                        : ''
                    "
                  ></el-button>
                </el-button-group>
              </div>
            </div>
            <el-button
              v-else
              type="primary"
              icon="el-icon-search"
              @click="dialogBodyJson = true"
              >打开json编辑器</el-button
            >
          </div>

          <el-divider content-position="left"
            >Params（注：每一组应为以上涉及到的全部参数）</el-divider
          >
          <el-button
            type="primary"
            icon="el-icon-search"
            @click="dialogDynamicJson = true"
            >打开json编辑器</el-button
          >
          <el-divider content-position="left">Assert</el-divider>
          <el-select v-model="form.assert" style="width:20%">
            <el-option label="包含" value="0" />
            <el-option label="全等" value="1" />
            </el-option>
          </el-select>
          <el-input
            v-model="form.assertValue"
            placeholder="断言内容"
            style="width:78%"
          ></el-input>
          <el-divider content-position="left">并发</el-divider>
          <el-form label-width="100px">
            <el-form-item label="线程数：">
              <el-input
                v-model="form.thread"
                type="number"
              ></el-input> </el-form-item
            ><el-form-item label="循环次数：">
              <el-input
                v-model="form.cycle"
                type="number"
              ></el-input> </el-form-item
            ><el-form-item label="间隔ms：">
              <el-input v-model="form.delay" type="number"></el-input>
            </el-form-item>
          </el-form>
          <el-divider content-position="left">其他</el-divider>
          <el-tooltip
            content="意味着将错乱使用动态参数的值，如果你的动态值是互相依赖的则不要使用这个属性"
            placement="top-start">
            <el-checkbox
              v-model="form.mixDynamic"
              label="混淆动态参数"
              border
            ></el-checkbox>
          </el-tooltip>

          <br />
          <br />
          <div>
            <el-button type="primary" round>保 存</el-button>
            <el-button round>导 入</el-button>
            <el-button type="success" round @click="start" style="float: right"
              >开始<i class="el-icon-caret-right"></i
            ></el-button>
          </div>
        </el-card>
      </el-aside>
      <el-container style="width:100%">
        <el-header style="te"
          ><el-steps
            style="margin-left: 10%;margin-top: 18px;"
            align-center
            :space="200"
            :active="nexts.length"
          >
            <el-step
              v-for="item in nexts"
              :key="item.msg"
              :title="item.msg"
              :status="item.status"
            ></el-step>
          </el-steps>
        </el-header>
        <el-main>
          <el-progress :percentage="percentage" v-if="percentage > 0"></el-progress>
          <div v-for="response in responses" :key="response.index">
            <el-divider content-position="left">轮次：{{response.index}}</el-divider>
            <el-popover 
              v-for="content in response.contents"
              :key="content.id"
              placement="top"
              width="600"
              trigger="click">
              <el-collapse v-model="content.showNames" accordion>
                <el-collapse-item title="url" :name="1">
                  {{content.show[0]}}
                </el-collapse-item>
                <el-collapse-item title="header" :name="2">
                  {{content.show[1]}}
                </el-collapse-item>
                <el-collapse-item title="body" :name="3" v-if="!content.show[0].startsWith('GET : ')">
                  {{content.show[2]}}
                </el-collapse-item>
                <el-collapse-item title="response/error" :name="4" v-if="content.showNames == 4">
                  {{content.show[3]}}
                </el-collapse-item>
              </el-collapse>
              <el-button size="mini" :type="content.type" slot="reference"  style="margin-top:5px;margin-left:5px;">{{content.des}}</el-button>
            </el-popover>
          </div>
        </el-main>
      </el-container>
    </el-container>
    <el-dialog
      v-el-drag-dialog
      :visible.sync="dialogBodyJson"
      title="编辑Body"
      @dragDialog="handleDrag"
    >
      <json-editor ref="jsonEditor" v-model="form.bodyJson" />
    </el-dialog>
    <el-dialog
      v-el-drag-dialog
      :visible.sync="dialogDynamicJson"
      title="编辑动态参数"
      @dragDialog="handleDrag"
    >
      <json-editor ref="jsonEditor" v-model="form.dynamicJson" />
    </el-dialog>
  </div>
</template>

<script>
import { start } from "@/api/tools";
import { connection } from "@/utils/websocket";
import JsonEditor from "@/components/JsonEditor";
import elDragDialog from "@/directive/el-drag-dialog"; // base on element-ui

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
        body: [{}],
        method: "get",
        url: "",
        assert: "0"
      },
      ws: {},
      percentage: 0,
      activeNames: [1],
      responses: [],
      resultCount: []
    };
  },
  methods: {
    handleDrag() {
    },
    async start() {
      this.nexts = [];
      this.responses = [];
      this.percentage = 0;
      this.resultCount = [];
      let that = JSON.parse(JSON.stringify(this.form));
      that.dynamicJson = JSON.stringify(this.form.dynamicJson);
      that.bodyJson = JSON.stringify(this.form.bodyJson);
      that.headers = this.form.headers.filter(x => x.key && x.value);
      that.body = this.form.body.filter(x => x.key && x.value);
      that.connectedId = this.ws.id;
      await start(that);
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
      this.ws.connection.on("sending", (index, key, item) => {
        var response = this.responses.find(x => x.index == index);
        var contents;
        if (!response) {
          response = {
            index: index,
            contents: [
              {
                id: key,
                type: "info",
                des: "请求中...",
                showNames: 1,
                show: [
                  `${item.method} : ${item.url}`,
                  JSON.stringify(item.headers),
                  JSON.stringify(item.body)
                ]
              }
            ]
          };
          this.responses.push(response);
          return;
        }
        response.contents.push({
          id: key,
          type: "info",
          des: "请求中...",
          showNames: 1,
          show: [
            `${item.method} : ${item.url}`,
            JSON.stringify(item.headers),
            JSON.stringify(item.body)
          ]
        });
      });
    },
    onResult() {
      this.ws.connection.on("result", (index, key, result) => {
        var response = this.responses.find(x => x.index == index);
        if (response) {
          var content = response.contents.find(x => x.id == key);
          if (content) {
            content.type = "success";
            content.des = "已完成";
            content.showNames = 4;
            content.show.push(result);
            this.resultCount.push(1);
            this.percentage = Number.parseInt(
              (this.resultCount.length / (this.form.thread * this.form.cycle)) *
                100
            );
          }
        }
      });
    },
    onAssertError() {
      this.ws.connection.on("assertError", (index, key, response) => {});
    },
    onError() {
      this.ws.connection.on("error", (index, key, ex) => {});
    },
    onOver() {
      this.ws.connection.on("over", _ => {
        var first = this.nexts.find(x => x.msg == "开始压测");
        if (first) {
          first.msg = "压测完毕";
          first.status = "success";
        }
        this.percentage = 100;
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