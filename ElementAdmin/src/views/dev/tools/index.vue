<template>
  <div class="app-container">
    <el-row :gutter="20">
      <el-col :span="12">
        <el-card class="box-card">
          <div slot="header" class="clearfix">
            <span>导入路由数据到数据库</span>
            <el-button style="float: right; padding: 3px 0" type="text" @click="initRoutesData">执行</el-button>
          </div>
          <el-tree :data="routeTreeData" :props="defaultTreeProps"></el-tree>
        </el-card>
      </el-col>
      <el-col :span="12">
        <el-card class="box-card">
          <div slot="header" class="clearfix">
            <span>生成Entities实现</span>
            <el-button
              style="float: right; padding: 3px 0"
              type="text"
              @click="initEntities"
              :disabled="selectEntities.length < 1"
            >执行</el-button>
          </div>
          <div v-for="item in entities" :key="item.key">
            <el-divider content-position="left">{{item.key}}</el-divider>
            <el-checkbox-group v-model="selectEntities">
              <el-checkbox
                v-for="x in item.value"
                :label="x"
                :key="x"
                border
                style="margin-bottom: 10px;"
              >{{x}}</el-checkbox>
            </el-checkbox-group>
          </div>
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script>
import { initRoutesData, initEntities, getEntities } from "@/api/tools";
import { asyncRoutes } from "@/router";

export default {
  data() {
    return {
      selectEntities: [],
      entities: [],
      defaultTreeProps: {
        children: "children",
        label: data => {
          return data.meta.title;
        }
      },
      routeTreeData: asyncRoutes.filter(x => !x.hidden)
    };
  },
  methods: {
    async initRoutesData() {
      await initRoutesData(asyncRoutes);
      this.$notify({
        title: "消息",
        message: "导入完成",
        duration: 5000
      });
    },
    async initEntities() {
      await initEntities(this.selectEntities);
      this.$notify({
        title: "消息",
        message: "生成完成",
        duration: 5000
      });
    },
    async getEntities() {
      return (await getEntities()).data;
    }
  },
  computed: {},
  async mounted() {
    this.entities = await this.getEntities();
  }
};
</script>

