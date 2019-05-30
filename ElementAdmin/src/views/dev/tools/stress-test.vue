<template>
  <div class="app-container">
    <el-button type="primary" @click="start">主要按钮</el-button>
  </div>
</template>

<script>
import { start } from "@/api/stresstest";

export default {
  methods: {
    async start() {
      await start();
    }
  }
};
</script>

