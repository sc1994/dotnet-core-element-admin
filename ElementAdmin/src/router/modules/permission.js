import Layout from '@/layout'

const permissionRoutes = {
  path: '/permission',
  component: Layout,
  redirect: '/permission/page',
  name: 'Permission',
  meta: {
    title: '角色权限',
    icon: 'lock'
  },
  children: [{
    path: 'role',
    component: () => import('@/views/permission/role-new'),
    name: 'RolePermission',
    meta: {
      title: '角色权限'
    }
  }]
}

export default permissionRoutes;
