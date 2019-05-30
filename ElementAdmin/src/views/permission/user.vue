<template>
  <div class="app-container">
    <el-form :inline="true" :model="form" class="demo-form-inline" label-width="120px">
      <el-form-item label="用户名/昵称：">
        <el-input v-model="form.user" placeholder="用户名/昵称" style="width:255px;"></el-input>
      </el-form-item>
      <el-form-item label="注册时间：">
        <div class="block">
          <el-date-picker
            v-model="form.timestamp"
            type="datetimerange"
            :picker-options="pickerOptions"
            range-separator="至"
            format="yyyy-MM-dd HH:mm"
            value-format="yyyy-MM-dd HH:mm"
            align="right"
          ></el-date-picker>
        </div>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @click="onSubmit">查 询</el-button>
      </el-form-item>
    </el-form>
    <el-table :data="tableData" border style="width: 100%">
      <el-table-column prop="date" label="头像" width="100"></el-table-column>
      <el-table-column prop="date" label="用户名/昵称" width="230"></el-table-column>
      <el-table-column prop="date" label="描述"></el-table-column>
      <el-table-column prop="date" label="角色" width="200"></el-table-column>
      <el-table-column prop="date" label="注册时间" width="180"></el-table-column>
      <el-table-column prop="date" label="上一次登录时间" width="180"></el-table-column>
      <el-table-column prop="date" label="操作" width="180"></el-table-column>
    </el-table>
  </div>
</template>

<script>
export default {
  data() {
    return {
      pickerOptions: {
        shortcuts: [
          {
            text: "最近两小时",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 3);
              picker.$emit("pick", [start, end]);
            }
          },
          {
            text: "最近十小时",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 3);
              picker.$emit("pick", [start, end]);
            }
          },
          {
            text: "最近一天",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24);
              picker.$emit("pick", [start, end]);
            }
          },
          {
            text: "最近三天",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 3);
              picker.$emit("pick", [start, end]);
            }
          }
        ]
      },
      form: {
        user: "",
        timestamp: []
      },
      tableData: []
    };
  },
  methods: {
    onSubmit() {
      console.log("submit!");
    }
  }
};
</script>