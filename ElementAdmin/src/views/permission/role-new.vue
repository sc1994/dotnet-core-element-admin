<template>
  <div class="app-container">
    <el-button type="primary" @click="handleAddRole">添加角色</el-button>

    <el-table :data="rolesList" style="width: 100%;margin-top:30px;" border>
      <el-table-column align="center" label="Key" width="220">
        <template slot-scope="scope">{{ scope.row.roleKey }}</template>
      </el-table-column>
      <el-table-column align="center" label="名称" width="220">
        <template slot-scope="scope">{{ scope.row.name }}</template>
      </el-table-column>
      <el-table-column align="header-center" label="描述">
        <template slot-scope="scope">{{ scope.row.description }}</template>
      </el-table-column>
      <el-table-column align="center" label="操作">
        <template slot-scope="scope">
          <el-button type="primary" size="small" @click="handleEdit(scope)">编辑</el-button>
          <el-button type="danger" size="small" @click="handleDelete(scope)">删除</el-button>
        </template>
      </el-table-column>
    </el-table>

    <el-dialog :visible.sync="dialogVisible" :title="dialogType === 'edit' ? '编辑角色' : '添加角色'">
      <el-form :model="role" label-width="80px" label-position="left">
        <el-form-item label="Key">
          <el-input
            v-model="role.roleKey"
            placeholder="角色关键字，不可重复"
            :disabled="dialogType === 'edit'"
          />
        </el-form-item>
        <el-form-item label="名称">
          <el-input v-model="role.name" placeholder="角色名称"/>
        </el-form-item>
        <el-form-item label="描述">
          <el-input
            v-model="role.description"
            :autosize="{ minRows: 2, maxRows: 4 }"
            type="textarea"
            placeholder="描述"
          />
        </el-form-item>
        <el-form-item label="设置权限">
          <el-tree
            ref="tree"
            :check-strictly="checkStrictly"
            :data="routesData"
            :props="defaultProps"
            :default-checked-keys="role.routeKeys"
            show-checkbox
            node-key="routeKey"
            class="permission-tree"
          />
        </el-form-item>
      </el-form>
      <div style="text-align:right;">
        <el-button type="danger" @click="dialogVisible = false">取 消</el-button>
        <el-button type="primary" @click="confirmRole">确 定</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import path from "path";
import { deepClone } from "@/utils";
import {
  getRoles,
  addRole,
  deleteRole,
  updateRole,
  getRoutes
} from "@/api/role";

const defaultRole = {
  roleKey: "",
  name: "",
  description: "",
  routeKeys: []
};

export default {
  data() {
    return {
      role: Object.assign({}, defaultRole),
      routes: [],
      rolesList: [],
      dialogVisible: false,
      dialogType: "new",
      checkStrictly: false,
      defaultProps: {
        children: "children",
        label: "name"
      }
    };
  },
  computed: {
    routesData() {
      return this.routes;
    }
  },
  created() {
    this.getRoutes();
    this.getRoles();
  },
  methods: {
    async getRoutes() {
      const res = await getRoutes();
      this.serviceRoutes = res.data;
      this.routes = res.data;
    },
    async getRoles() {
      const res = await getRoles();
      this.rolesList = res.data;
    },
    getCheckedKeys(data, keys, key) {
      var res = [];
      recursion(data, false);
      return res;
      function recursion(arr, isChild) {
        var aCheck = [];
        for (var i = 0; i < arr.length; i++) {
          var obj = arr[i];
          aCheck[i] = false;

          if (obj.children) {
            aCheck[i] = recursion(obj.children, true) ? true : aCheck[i];
            if (aCheck[i]) {
              res.push(obj[key]);
            }
          }

          for (var j = 0; j < keys.length; j++) {
            if (obj[key] == keys[j]) {
              aCheck[i] = true;
              if (res.indexOf(obj[key]) == -1) {
                res.push(obj[key]);
              }
              break;
            }
          }
        }
        if (isChild) {
          return aCheck.indexOf(true) != -1;
        }
      }
    },
    // Reshape the routes structure so that it looks the same as the sidebar
    generateRoutes(routes, basePath = "/") {
      const res = [];

      for (let route of routes) {
        // skip some route
        if (route.hidden) {
          continue;
        }

        const onlyOneShowingChild = this.onlyOneShowingChild(
          route.children,
          route
        );

        if (route.children && onlyOneShowingChild && !route.alwaysShow) {
          route = onlyOneShowingChild;
        }

        const data = {
          path: path.resolve(basePath, route.path),
          title: route.meta && route.meta.title
        };

        // recursive child routes
        if (route.children) {
          data.children = this.generateRoutes(route.children, data.path);
        }
        res.push(data);
      }
      return res;
    },
    generateArr(routes) {
      let data = [];
      routes.forEach(route => {
        data.push(route);
        if (route.children) {
          const temp = this.generateArr(route.children);
          if (temp.length > 0) {
            data = [...data, ...temp];
          }
        }
      });
      return data;
    },
    handleAddRole() {
      this.role = Object.assign({}, defaultRole);
      if (this.$refs.tree) {
        this.$refs.tree.setCheckedNodes([]);
      }
      this.dialogType = "new";
      this.dialogVisible = true;
    },
    handleEdit(scope) {
      if (this.$refs.tree) {
        this.$refs.tree.setCheckedNodes([]);
        this.role.routeKeys = [];
      }
      this.role = deepClone(scope.row);

      this.dialogType = "edit";
      this.dialogVisible = true;
    },
    handleDelete({ $index, row }) {
      this.$confirm("确定要删除这个角色吗？", "Warning", {
        confirmButtonText: "确 定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(async () => {
          await deleteRole(row.id);
          await this.getRoles();
          this.$message({
            type: "success",
            message: "Delete succed!"
          });
        })
        .catch(err => {
          console.error(err);
        });
    },
    generateTree(routes, basePath = "/", checkedKeys) {
      const res = [];

      for (const route of routes) {
        const routePath = path.resolve(basePath, route.path);

        // recursive child routes
        if (route.children) {
          route.children = this.generateTree(
            route.children,
            routePath,
            checkedKeys
          );
        }

        if (
          checkedKeys.includes(routePath) ||
          (route.children && route.children.length >= 1)
        ) {
          res.push(route);
        }
      }
      return res;
    },
    async confirmRole() {
      const isEdit = this.dialogType === "edit";

      const checkedKeys = this.getCheckedKeys(
        this.routes,
        this.$refs.tree.getCheckedKeys(),
        "routeKey"
      );

      var clone = deepClone(this.role);
      clone.routeKeys = checkedKeys;

      if (isEdit) {
        await updateRole(clone.roleKey, clone);
      } else {
        const { data } = await addRole(clone);
      }
      await this.getRoles();

      const { description, roleKey, name } = this.role;
      this.dialogVisible = false;
      this.$notify({
        title: "Success",
        dangerouslyUseHTMLString: true,
        message: `
            <div>Role Key: ${roleKey}</div>
            <div>Role Nmae: ${name}</div>
            <div>Description: ${description}</div>
          `,
        type: "success"
      });
    },
    // reference: src/view/layout/components/Sidebar/SidebarItem.vue
    onlyOneShowingChild(children = [], parent) {
      let onlyOneChild = null;
      const showingChildren = children.filter(item => !item.hidden);

      // When there is only one child route, the child route is displayed by default
      if (showingChildren.length === 1) {
        onlyOneChild = showingChildren[0];
        onlyOneChild.path = path.resolve(parent.path, onlyOneChild.path);
        return onlyOneChild;
      }

      // Show parent if there are no child route to display
      if (showingChildren.length === 0) {
        onlyOneChild = { ...parent, path: "", noShowingChildren: true };
        return onlyOneChild;
      }

      return false;
    }
  }
};
</script>

<style lang="scss" scoped>
.app-container {
  .roles-table {
    margin-top: 30px;
  }
  .permission-tree {
    margin-bottom: 30px;
  }
}
</style>
