import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

/* Layout */
import Layout from '@/layout'


export const constantRoutes = [{
    path: '/redirect',
    component: Layout,
    hidden: true,
    children: [{
      path: '/redirect/:path*',
      component: () => import('@/views/redirect/index')
    }]
  },
  {
    path: '/login',
    component: () => import('@/views/login/index'),
    hidden: true
  },
  {
    path: '/auth-redirect',
    component: () => import('@/views/login/auth-redirect'), // todo
    hidden: true
  },
  {
    path: '/404',
    component: () => import('@/views/dev/error-page/404'),
    hidden: true
  },
  {
    path: '/401',
    component: () => import('@/views/dev/error-page/401'),
    hidden: true
  },
  {
    path: '/',
    component: Layout,
    redirect: '/dashboard',
    children: [{
      path: 'dashboard',
      component: () => import('@/views/dashboard/index'),
      name: 'Dashboard',
      meta: {
        title: 'Dashboard',
        icon: 'dashboard',
        affix: true
      }
    }]
  }
]

/**
 * asyncRoutes
 * the routes that need to be dynamically loaded based on user roles
 */
export const asyncRoutes = [{
    path: '/permission',
    component: Layout,
    redirect: '/permission/page',
    name: 'Permission',
    meta: {
      title: 'Permission',
      icon: 'lock',
      roles: ['admin', 'editor'] // you can set roles in root nav
    },
    children: [{
      path: 'role',
      component: () => import('@/views/permission/role-new'),
      name: 'RolePermission',
      meta: {
        title: 'Role Permission',
        roles: ['admin']
      }
    }]
  },
  {
    path: '/dev',
    component: Layout,
    redirect: '/dev/documentation/index',
    alwaysShow: true,
    name: 'Developer',
    meta: {
      title: 'Developer',
      icon: 'bug'
    },
    children: [{
        path: 'documentation/index',
        component: () => import('@/views/dev/documentation/index'),
        name: 'Documentation',
        meta: {
          title: 'Documentation',
          icon: "documentation"
        }
      },
      {
        path: 'guide/index',
        component: () => import('@/views/dev/guide/index'),
        name: 'Guide',
        meta: {
          title: 'Guide',
          icon: 'guide',
          noCache: true
        }
      },
      {
        path: 'profile/index',
        component: () => import('@/views/dev/profile/index'),
        name: 'Profile',
        meta: {
          title: 'Profile',
          icon: 'user',
          noCache: true
        }
      },
      {
        path: 'iocn/index',
        component: () => import('@/views/dev/icons/index'),
        name: 'Icons',
        meta: {
          title: 'Icons',
          icon: 'icon',
          noCache: true
        }
      },
      {
        path: 'permission',
        component: () => import('@/views/dev/permission/index'),
        redirect: '/dev/permission/page',
        name: 'Permission Dev',
        meta: {
          title: 'Permission Dev',
          icon: 'lock',
        },
        children: [{
            path: 'page',
            component: () => import('@/views/dev/permission/page'),
            name: 'PagePermission',
            meta: {
              title: 'Page Permission',
            }
          },
          {
            path: 'directive',
            component: () => import('@/views/dev/permission/directive'),
            name: 'DirectivePermission',
            meta: {
              title: 'Directive Permission'
            }
          }
        ]
      },
      {
        path: 'error-log/log',
        component: () => import('@/views/dev/error-log/index'),
        name: 'ErrorLog',
        meta: {
          title: 'Error Log',
          icon: 'bug'
        }
      },
      {
        path: 'example',
        component: () => import('@/views/dev/example/index'),
        redirect: '/dev/example/create',
        name: 'Example',
        meta: {
          title: 'Example',
          icon: 'example'
        },
        children: [{
            path: 'create',
            component: () => import('@/views/dev/example/create'),
            name: 'CreateArticle',
            meta: {
              title: 'Create Article',
              icon: 'edit'
            }
          },
          {
            path: 'edit/:id(\\d+)',
            component: () => import('@/views/dev/example/edit'),
            name: 'EditArticle',
            meta: {
              title: 'Edit Article',
              noCache: true,
              activeMenu: '/example/list'
            },
            hidden: true
          },
          {
            path: 'list',
            component: () => import('@/views/dev/example/list'),
            name: 'ArticleList',
            meta: {
              title: 'Article List',
              icon: 'list'
            }
          }
        ]
      },
      {
        path: 'tab/index',
        component: () => import('@/views/dev/tab/index'),
        name: 'Tab',
        meta: {
          title: 'Tab',
          icon: 'tab'
        }
      },
      {
        path: 'error',
        component: () => import('@/views/dev/error-page/index'),
        redirect: 'noRedirect',
        name: 'ErrorPages',
        meta: {
          title: 'Error Pages',
          icon: '404'
        },
        children: [{
            path: '401',
            component: () => import('@/views/dev/error-page/401'),
            name: 'Page401',
            meta: {
              title: '401',
              noCache: true
            }
          },
          {
            path: '404',
            component: () => import('@/views/dev/error-page/404'),
            name: 'Page404',
            meta: {
              title: '404',
              noCache: true
            }
          }
        ]
      },
      {
        path: 'excel',
        component: () => import('@/views/dev/excel/index'),
        redirect: '/dev/excel/export-excel',
        name: 'Excel',
        meta: {
          title: 'Excel',
          icon: 'excel'
        },
        children: [{
            path: 'export-excel',
            component: () => import('@/views/dev/excel/export-excel'),
            name: 'ExportExcel',
            meta: {
              title: 'Export Excel'
            }
          },
          {
            path: 'export-selected-excel',
            component: () => import('@/views/dev/excel/select-excel'),
            name: 'SelectExcel',
            meta: {
              title: 'Export Selected'
            }
          },
          {
            path: 'export-merge-header',
            component: () => import('@/views/dev/excel/merge-header'),
            name: 'MergeHeader',
            meta: {
              title: 'Merge Header'
            }
          },
          {
            path: 'upload-excel',
            component: () => import('@/views/dev/excel/upload-excel'),
            name: 'UploadExcel',
            meta: {
              title: 'Upload Excel'
            }
          }
        ]
      },
      {
        path: 'zip',
        component: () => import('@/views/dev/zip/index'),
        name: 'ExportZip',
        meta: {
          title: 'Export Zip',
          icon: 'zip'
        }
      },
      {
        path: 'pdf',
        component: () => import('@/views/dev/pdf/index'),
        name: 'PDF',
        meta: {
          title: 'PDF',
          icon: 'pdf'
        }
      },
      {
        path: 'pdf/download',
        component: () => import('@/views/dev/pdf/download'),
        hidden: true
      },
      {
        path: 'theme',
        component: () => import('@/views/dev/theme/index'),
        name: 'Theme',
        meta: {
          title: 'Theme',
          icon: 'theme'
        }
      },
      {
        path: 'clipboard',
        component: () => import('@/views/dev/clipboard/index'),
        name: 'ClipboardDemo',
        meta: {
          title: 'Clipboard',
          icon: 'clipboard'
        }
      },
      {
        path: 'charts',
        component: () => import('@/views/dev/charts/index'),
        redirect: 'noRedirect',
        name: 'Charts',
        meta: {
          title: 'Charts',
          icon: 'chart'
        },
        children: [{
            path: 'keyboard',
            component: () => import('@/views/dev/charts/keyboard'),
            name: 'KeyboardChart',
            meta: {
              title: 'Keyboard Chart',
              noCache: true
            }
          },
          {
            path: 'line',
            component: () => import('@/views/dev/charts/line'),
            name: 'LineChart',
            meta: {
              title: 'Line Chart',
              noCache: true
            }
          },
          {
            path: 'mix-chart',
            component: () => import('@/views/dev/charts/mix-chart'),
            name: 'MixChart',
            meta: {
              title: 'Mix Chart',
              noCache: true
            }
          }
        ]
      },
      {
        path: 'components',
        component: () => import('@/views/dev/components-demo/index'),
        redirect: 'noRedirect',
        name: 'ComponentDemo',
        meta: {
          title: 'Components',
          icon: 'component'
        },
        children: [{
            path: 'tinymce',
            component: () => import('@/views/dev/components-demo/tinymce'),
            name: 'TinymceDemo',
            meta: {
              title: 'Tinymce'
            }
          },
          {
            path: 'markdown',
            component: () => import('@/views/dev/components-demo/markdown'),
            name: 'MarkdownDemo',
            meta: {
              title: 'Markdown'
            }
          },
          {
            path: 'json-editor',
            component: () => import('@/views/dev/components-demo/json-editor'),
            name: 'JsonEditorDemo',
            meta: {
              title: 'JSON Editor'
            }
          },
          {
            path: 'split-pane',
            component: () => import('@/views/dev/components-demo/split-pane'),
            name: 'SplitpaneDemo',
            meta: {
              title: 'SplitPane'
            }
          },
          {
            path: 'avatar-upload',
            component: () => import('@/views/dev/components-demo/avatar-upload'),
            name: 'AvatarUploadDemo',
            meta: {
              title: 'Upload'
            }
          },
          {
            path: 'dropzone',
            component: () => import('@/views/dev/components-demo/dropzone'),
            name: 'DropzoneDemo',
            meta: {
              title: 'Dropzone'
            }
          },
          {
            path: 'sticky',
            component: () => import('@/views/dev/components-demo/sticky'),
            name: 'StickyDemo',
            meta: {
              title: 'Sticky'
            }
          },
          {
            path: 'count-to',
            component: () => import('@/views/dev/components-demo/count-to'),
            name: 'CountToDemo',
            meta: {
              title: 'Count To'
            }
          },
          {
            path: 'mixin',
            component: () => import('@/views/dev/components-demo/mixin'),
            name: 'ComponentMixinDemo',
            meta: {
              title: 'Component Mixin'
            }
          },
          {
            path: 'back-to-top',
            component: () => import('@/views/dev/components-demo/back-to-top'),
            name: 'BackToTopDemo',
            meta: {
              title: 'Back To Top'
            }
          },
          {
            path: 'drag-dialog',
            component: () => import('@/views/dev/components-demo/drag-dialog'),
            name: 'DragDialogDemo',
            meta: {
              title: 'Drag Dialog'
            }
          },
          {
            path: 'drag-select',
            component: () => import('@/views/dev/components-demo/drag-select'),
            name: 'DragSelectDemo',
            meta: {
              title: 'Drag Select'
            }
          },
          {
            path: 'dnd-list',
            component: () => import('@/views/dev/components-demo/dnd-list'),
            name: 'DndListDemo',
            meta: {
              title: 'Dnd List'
            }
          },
          {
            path: 'drag-kanban',
            component: () => import('@/views/dev/components-demo/drag-kanban'),
            name: 'DragKanbanDemo',
            meta: {
              title: 'Drag Kanban'
            }
          }
        ]
      },
      {
        path: 'table',
        component: () => import('@/views/dev/table/index'),
        redirect: '/dev/table/complex-table',
        name: 'Table',
        meta: {
          title: 'Table',
          icon: 'table'
        },
        children: [{
            path: 'dynamic-table',
            component: () => import('@/views/dev/table/dynamic-table/index'),
            name: 'DynamicTable',
            meta: {
              title: 'Dynamic Table'
            }
          },
          {
            path: 'drag-table',
            component: () => import('@/views/dev/table/drag-table'),
            name: 'DragTable',
            meta: {
              title: 'Drag Table'
            }
          },
          {
            path: 'inline-edit-table',
            component: () => import('@/views/dev/table/inline-edit-table'),
            name: 'InlineEditTable',
            meta: {
              title: 'Inline Edit'
            }
          },
          {
            path: 'complex-table',
            component: () => import('@/views/dev/table/complex-table'),
            name: 'ComplexTable',
            meta: {
              title: 'Complex Table'
            }
          }
        ]
      }
    ]
  },
  // 404 page must be placed at the end !!!
  {
    path: '*',
    redirect: '/404',
    hidden: true
  }
]


const createRouter = () => new Router({
  // mode: 'history', // require service support
  scrollBehavior: () => ({
    y: 0
  }),
  routes: constantRoutes
})

const router = createRouter()

// Detail see: https://github.com/vuejs/vue-router/issues/1234#issuecomment-357941465
export function resetRouter() {
  const newRouter = createRouter()
  router.matcher = newRouter.matcher // reset router
}

export default router
