// 开发demo相关的路由都在这

import Layout from "@/layout"

const devRoutes = {
  path: "/dev",
  component: Layout,
  redirect: "/dev/documentation/index",
  alwaysShow: true,
  name: "Developer",
  meta: {
    title: "Developer",
    icon: "bug"
  },
  children: [{
      path: "documentation/index",
      component: () => import("@/views/dev/documentation/index"),
      name: "Documentation",
      meta: {
        title: "文档",
        icon: "documentation"
      }
    },
    {
      path: "apilog",
      component: () => import("@/views/dev/api-log/index"),
      name: "ApiLog",
      meta: {
        title: "接口日志",
        icon: "education"
      }
    },
    {
      path: "tools",
      component: () => import("@/views/dev/tools/index"),
      name: "Tools",
      meta: {
        title: "工具集",
        icon: "form"
      }
    },
    {
      path: "guide/index",
      component: () => import("@/views/dev/guide/index"),
      name: "Guide",
      meta: {
        title: "引导页",
        icon: "guide",
        noCache: true
      }
    },
    {
      path: "profile/index",
      component: () => import("@/views/dev/profile/index"),
      name: "Profile",
      meta: {
        title: "简介",
        icon: "user",
        noCache: true
      }
    },
    {
      path: "iocn/index",
      component: () => import("@/views/dev/icons/index"),
      name: "Icons",
      meta: {
        title: "图标",
        icon: "icon",
        noCache: true
      }
    },
    {
      path: "permission",
      component: () => import("@/views/dev/permission/index"),
      redirect: "/dev/permission/page",
      name: "Permission Dev",
      meta: {
        title: "权限",
        icon: "lock",
      },
      children: [{
          path: "page",
          component: () => import("@/views/dev/permission/page"),
          name: "PagePermission",
          meta: {
            title: "页面权限",
          }
        },
        {
          path: "directive",
          component: () => import("@/views/dev/permission/directive"),
          name: "DirectivePermission",
          meta: {
            title: "权限指令"
          }
        }
      ]
    },
    {
      path: "error-log/log",
      component: () => import("@/views/dev/error-log/index"),
      name: "ErrorLog",
      meta: {
        title: "错误日志",
        icon: "bug"
      }
    },
    {
      path: "example",
      component: () => import("@/views/dev/example/index"),
      redirect: "/dev/example/create",
      name: "Example",
      meta: {
        title: "综合示例",
        icon: "example"
      },
      children: [{
          path: "create",
          component: () => import("@/views/dev/example/create"),
          name: "CreateArticle",
          meta: {
            title: "创建文章",
            icon: "edit"
          }
        },
        {
          path: "edit/:id(\\d+)",
          component: () => import("@/views/dev/example/edit"),
          name: "EditArticle",
          meta: {
            title: "编辑文章",
            noCache: true,
            activeMenu: "/example/list"
          },
          hidden: true
        },
        {
          path: "list",
          component: () => import("@/views/dev/example/list"),
          name: "ArticleList",
          meta: {
            title: "文章列表",
            icon: "list"
          }
        }
      ]
    },
    {
      path: "tab/index",
      component: () => import("@/views/dev/tab/index"),
      name: "Tab",
      meta: {
        title: "Tab",
        icon: "tab"
      }
    },
    {
      path: "error",
      component: () => import("@/views/dev/error-page/index"),
      redirect: "noRedirect",
      name: "ErrorPages",
      meta: {
        title: "错误页",
        icon: "404"
      },
      children: [{
          path: "401",
          component: () => import("@/views/dev/error-page/401"),
          name: "Page401",
          meta: {
            title: "401",
            noCache: true
          }
        },
        {
          path: "404",
          component: () => import("@/views/dev/error-page/404"),
          name: "Page404",
          meta: {
            title: "404",
            noCache: true
          }
        }
      ]
    },
    {
      path: "excel",
      component: () => import("@/views/dev/excel/index"),
      redirect: "/dev/excel/export-excel",
      name: "Excel",
      meta: {
        title: "Excel",
        icon: "excel"
      },
      children: [{
          path: "export-excel",
          component: () => import("@/views/dev/excel/export-excel"),
          name: "ExportExcel",
          meta: {
            title: "导出 Excel"
          }
        },
        {
          path: "export-selected-excel",
          component: () => import("@/views/dev/excel/select-excel"),
          name: "SelectExcel",
          meta: {
            title: "导出已选项"
          }
        },
        {
          path: "export-merge-header",
          component: () => import("@/views/dev/excel/merge-header"),
          name: "MergeHeader",
          meta: {
            title: "多级头"
          }
        },
        {
          path: "upload-excel",
          component: () => import("@/views/dev/excel/upload-excel"),
          name: "UploadExcel",
          meta: {
            title: "上传 Excel"
          }
        }
      ]
    },
    {
      path: "zip",
      component: () => import("@/views/dev/zip/index"),
      name: "ExportZip",
      meta: {
        title: "导出 Zip",
        icon: "zip"
      }
    },
    {
      path: "pdf",
      component: () => import("@/views/dev/pdf/index"),
      name: "PDF",
      meta: {
        title: "PDF",
        icon: "pdf"
      }
    },
    {
      path: "pdf/download",
      component: () => import("@/views/dev/pdf/download"),
      hidden: true,
      meta: {
        title: "PDF下载"
      }
    },
    {
      path: "theme",
      component: () => import("@/views/dev/theme/index"),
      name: "Theme",
      meta: {
        title: "主题",
        icon: "theme"
      }
    },
    {
      path: "clipboard",
      component: () => import("@/views/dev/clipboard/index"),
      name: "ClipboardDemo",
      meta: {
        title: "剪切板",
        icon: "clipboard"
      }
    },
    {
      path: "charts",
      component: () => import("@/views/dev/charts/index"),
      redirect: "noRedirect",
      name: "Charts",
      meta: {
        title: "图表",
        icon: "chart"
      },
      children: [{
          path: "keyboard",
          component: () => import("@/views/dev/charts/keyboard"),
          name: "KeyboardChart",
          meta: {
            title: "键盘图表",
            noCache: true
          }
        },
        {
          path: "line",
          component: () => import("@/views/dev/charts/line"),
          name: "LineChart",
          meta: {
            title: "折线图",
            noCache: true
          }
        },
        {
          path: "mix-chart",
          component: () => import("@/views/dev/charts/mix-chart"),
          name: "MixChart",
          meta: {
            title: "混合图表",
            noCache: true
          }
        }
      ]
    },
    {
      path: "components",
      component: () => import("@/views/dev/components-demo/index"),
      redirect: "noRedirect",
      name: "ComponentDemo",
      meta: {
        title: "组件",
        icon: "component"
      },
      children: [{
          path: "tinymce",
          component: () => import("@/views/dev/components-demo/tinymce"),
          name: "TinymceDemo",
          meta: {
            title: "富文本编辑器"
          }
        },
        {
          path: "markdown",
          component: () => import("@/views/dev/components-demo/markdown"),
          name: "MarkdownDemo",
          meta: {
            title: "Markdown"
          }
        },
        {
          path: "json-editor",
          component: () => import("@/views/dev/components-demo/json-editor"),
          name: "JsonEditorDemo",
          meta: {
            title: "JSON 编辑器"
          }
        },
        {
          path: "split-pane",
          component: () => import("@/views/dev/components-demo/split-pane"),
          name: "SplitpaneDemo",
          meta: {
            title: "SplitPane"
          }
        },
        {
          path: "avatar-upload",
          component: () => import("@/views/dev/components-demo/avatar-upload"),
          name: "AvatarUploadDemo",
          meta: {
            title: "头像上传"
          }
        },
        {
          path: "dropzone",
          component: () => import("@/views/dev/components-demo/dropzone"),
          name: "DropzoneDemo",
          meta: {
            title: "拖拽上传"
          }
        },
        {
          path: "sticky",
          component: () => import("@/views/dev/components-demo/sticky"),
          name: "StickyDemo",
          meta: {
            title: "Sticky"
          }
        },
        {
          path: "count-to",
          component: () => import("@/views/dev/components-demo/count-to"),
          name: "CountToDemo",
          meta: {
            title: "计数动画"
          }
        },
        {
          path: "mixin",
          component: () => import("@/views/dev/components-demo/mixin"),
          name: "ComponentMixinDemo",
          meta: {
            title: "小组件"
          }
        },
        {
          path: "back-to-top",
          component: () => import("@/views/dev/components-demo/back-to-top"),
          name: "BackToTopDemo",
          meta: {
            title: "返回顶部"
          }
        },
        {
          path: "drag-dialog",
          component: () => import("@/views/dev/components-demo/drag-dialog"),
          name: "DragDialogDemo",
          meta: {
            title: "拖拽 Dialog"
          }
        },
        {
          path: "drag-select",
          component: () => import("@/views/dev/components-demo/drag-select"),
          name: "DragSelectDemo",
          meta: {
            title: "拖拽 Select"
          }
        },
        {
          path: "dnd-list",
          component: () => import("@/views/dev/components-demo/dnd-list"),
          name: "DndListDemo",
          meta: {
            title: "拖拽 List"
          }
        },
        {
          path: "drag-kanban",
          component: () => import("@/views/dev/components-demo/drag-kanban"),
          name: "DragKanbanDemo",
          meta: {
            title: "拖拽看板"
          }
        }
      ]
    },
    {
      path: "table",
      component: () => import("@/views/dev/table/index"),
      redirect: "/dev/table/complex-table",
      name: "Table",
      meta: {
        title: "Table",
        icon: "table"
      },
      children: [{
          path: "dynamic-table",
          component: () => import("@/views/dev/table/dynamic-table/index"),
          name: "DynamicTable",
          meta: {
            title: "动态 Table"
          }
        },
        {
          path: "drag-table",
          component: () => import("@/views/dev/table/drag-table"),
          name: "DragTable",
          meta: {
            title: "拖拽 Table"
          }
        },
        {
          path: "inline-edit-table",
          component: () => import("@/views/dev/table/inline-edit-table"),
          name: "InlineEditTable",
          meta: {
            title: "行内编辑"
          }
        },
        {
          path: "complex-table",
          component: () => import("@/views/dev/table/complex-table"),
          name: "ComplexTable",
          meta: {
            title: "综合 Table"
          }
        }
      ]
    }
  ]
}

export default devRoutes;
