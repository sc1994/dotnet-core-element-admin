<template>
  <div style="height: 90vh">
    <split-pane split="vertical" @resize="resize" :default-percent="30">
      <template slot="paneL">
        <el-card class="box-card" style="min-height: 90vh;">
          <el-button
            style="position: absolute;right: 14px;top: 14px;"
            type="success"
            icon="el-icon-caret-right"
            circle
          ></el-button>
          <br>

          <el-divider content-position="left">URL</el-divider>
          <el-input
            type="textarea"
            autosize
            placeholder="输入url。需要动态参数，请以{#value/}占位。"
            v-model="form.url"
          ></el-input>

          <el-divider content-position="left">Method</el-divider>
          <el-radio-group v-model="form.method">
            <el-radio-button label="get"></el-radio-button>
            <el-radio-button label="post"></el-radio-button>
            <el-radio-button label="put"></el-radio-button>
          </el-radio-group>

          <el-divider content-position="left">Headers</el-divider>
          <div v-for="item in form.headers" :key="item.key" style="margin-top: 6px;">
            <el-input v-model="item.key" placeholder="Key" style="width:30%"></el-input>
            <el-input v-model="item.value" placeholder="以{#value/}占位。" style="width:40%"></el-input>
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
                @click="form.headers.length > 1 ? form.headers = form.headers.filter(x=>x != item) : ''"
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
                <el-input v-model="item.value" placeholder="以{#value/}占位。" style="width:40%"></el-input>
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
                    @click="form.body.length > 1 ? form.body = form.body.filter(x=>x != item) : ''"
                  ></el-button>
                </el-button-group>
              </div>
            </div>
            <el-button
              v-else
              type="primary"
              icon="el-icon-search"
              @click="dialogBodyJson=true;"
            >打开json编辑器</el-button>
          </div>

          <el-divider content-position="left">动态参数（注：每一组应为以上涉及到的全部参数）</el-divider>
          <el-button type="primary" icon="el-icon-search" @click="dialogDynamicJson=true;">打开json编辑器</el-button>
          <br>
          <br>
          <div style="text-align: center;">
            <el-button type="primary" round>保 存</el-button>
            <el-button round>导 入</el-button>
          </div>
        </el-card>
      </template>
      <template slot="paneR">
        <split-pane split="horizontal">
          <template slot="paneL">
            <div class="top-container"/>
          </template>
          <template slot="paneR">
            <div class="bottom-container"/>
          </template>
        </split-pane>
      </template>
    </split-pane>
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
import { start } from "@/api/stresstest";
import { connection } from "@/utils/websocket";
import splitPane from "vue-splitpane";
import JsonEditor from "@/components/JsonEditor";
import elDragDialog from "@/directive/el-drag-dialog"; // base on element-ui

export default {
  name: "stresstest",
  directives: { elDragDialog },
  components: { splitPane, JsonEditor },
  data() {
    return {
      dialogBodyJson: false,
      dialogDynamicJson: false,
      form: {
        dynamicJson: [],
        bodyJson: {},
        headers: [{}],
        body: [{}]
      },
      ws: {}
    };
  },
  methods: {
    handleDrag() {
      this.$refs.select.blur();
    },
    async start() {
      await start();
    },
    resize() {
      console.log("resize");
    }
  },
  async mounted() {
    this.ws = await connection("sth");
  }
};
</script>

<style  scoped>
.components-container {
  position: relative;
  height: 100vh;
}

.right-container {
  background-color: #fce38a;
  height: 200px;
}

.top-container {
  background-color: #fce38a;
  width: 100%;
  height: 100%;
}

.bottom-container {
  width: 100%;
  background-color: #95e1d3;
  height: 100%;
}
</style>


