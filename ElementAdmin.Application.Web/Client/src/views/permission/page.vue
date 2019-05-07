<template>
  <div class="app-container">
    <switch-roles @change="handleRolesChange" />
    <!-- <edit-role-tree></edit-role-tree> -->
  </div>
</template>

<script>
  import SwitchRoles from './components/SwitchRoles'
  // import EditRoleTree from './components/EditRoleTree'

  export default {
    name: 'PagePermission',
    components: { SwitchRoles },
    methods: {
      handleRolesChange() {
        this.$router.push({ path: '/permission/index?' + +new Date() })
      }
    }
  }
</script>
