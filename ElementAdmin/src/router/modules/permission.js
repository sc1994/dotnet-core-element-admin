import Layout from '@/layout'

const permissionRoutes = {
  path: '/permission',
  component: Layout,
  redirect: '/permission/page',
  name: 'Permission',
  meta: {
    title: '权限管理',
    icon: 'lock'
  },
  children: [{
    path: 'role',
    component: () => import('@/views/permission/role-new'),
    name: 'RolePermission',
    meta: {
      title: '角色管理'
    }
  }, {
    path: 'user',
    component: () => import('@/views/permission/user'),
    name: 'UserPermission',
    meta: {
      title: '用户管理'
    }
  }]
}

export default permissionRoutes;
