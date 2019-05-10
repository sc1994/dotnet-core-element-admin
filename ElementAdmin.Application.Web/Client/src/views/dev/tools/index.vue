<template>
  <div class="app-container">
    <el-button type="primary" plain @click="initRoutesData">
      导入路由数据到数据库
    </el-button>
  </div>
</template>

<script>
import { initRoutesData } from "@/api/tools";
import { asyncRoutes } from "@/router";

export default {
  methods: {
    async initRoutesData() {
      await initRoutesData(asyncRoutes);
      this.$notify({
        title: "提示",
        message: "导入完成",
        duration: 5000
      });
    }
  }
};
</script>

