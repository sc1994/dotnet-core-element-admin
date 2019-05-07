<template>
  <el-dialog
    :visible.sync="dialogVisible"
    :title="dialogType === 'edit' ? 'Edit Role' : 'New Role'"
  >
    <el-form :model="role" label-width="80px" label-position="left">
      <el-form-item label="Name">
        <el-input v-model="role.name" placeholder="Role Name" />
      </el-form-item>
      <el-form-item label="Desc">
        <el-input
          v-model="role.description"
          :autosize="{ minRows: 2, maxRows: 4 }"
          type="textarea"
          placeholder="Role Description"
        />
      </el-form-item>
      <el-form-item label="Menus">
        <el-tree
          :data="routes"
          show-checkbox
          node-key="id"
          :default-checked-keys="role.routeIds"
          :props="defaultProps"
        >
        </el-tree>
      </el-form-item>
    </el-form>
    <div style="text-align:right;">
      <el-button type="danger" @click="dialogVisible = false">
        {{ $t("permission.cancel") }}
      </el-button>
      <el-button type="primary" @click="confirmRole">
        {{ $t("permission.confirm") }}
      </el-button>
    </div>
  </el-dialog>
</template>

<script>
import { getRoutes } from "@/api/role";
import i18n from "@/lang";

export default {
  props: {
    role: {
      type: Object,
      required: true
    }
  },
  data() {
    return {
      routes: [],
      dialogVisible: false,
      dialogType: "new",
      defaultProps: {
        children: "children",
        label: "label"
      }
    };
  },
  created() {
    this.getRoutes();
  },
  methods: {
    async getRoutes() {
      const res = await getRoutes();
      this.serviceRoutes = res.data;
      this.routes = this.i18n(res.data);
    },
    i18n(routes) {
      const app = routes.map(route => {
        route.label = i18n.t(`route.${route.label}`);
        if (route.children) {
          route.children = this.i18n(route.children);
        }
        return route;
      });
      return app;
    }
  }
};
</script>
