# ElementAdmin

### 使用 dotnet core 作为后端搭建一个管理系统

> 前端

- 使用 [element-admin](https://github.com/PanJiaChen/vue-element-admin/)  作为页面

> 后端

- `dotnet core` 实现了一部分的接口
- `MySql` 存储数据
- `EF Core` 作为ORM
- `Serilog` 生产日志
- `Elasticsearch` 存储日志
- `AspectCore` 作为`DI`容器，利用它的`AOP`监控方法调用，还有实现了方法的调用权限验证
![image](https://raw.githubusercontent.com/sc1994/dotnet-core-element-admin/master/DocResource/1558955315.jpg)

> 环境依赖  
- [参考配置中用到的链接](https://github.com/sc1994/dotnet-core-element-admin/blob/master/ElementAdmin.Application/appsettings.Development.json)

> 使用

- 身份验证，设置参数`IdentityModel`的`Identity`特性，特性中会验证token值是否存在于`redis`。并且在特性中赋值`identity`

```Csharp
/// <summary>
/// 登出
/// </summary>
/// <param name="identity"></param>
/// <returns></returns>
Task<ApiResponse> LogoutAsync([Identity] IdentityModel identity = null);
```

- 方法调用追踪日志，下面的代码配置了启用范围，配置了那些命名空间下的方法会被记录。

```Csharp
public static void Register(IServiceContainer service)
{
    var logsNameSpace = new[]
    {
        "ElementAdmin.Application.Interface",
        "ElementAdmin.Domain.Interface"
    };
    service.Configure(configure =>
    {
        configure.Interceptors.AddTyped<LoggingAttribute>(predicates => logsNameSpace.Contains(predicates.DeclaringType.Namespace));
    });
    service.AddAspectScope();
}
```

> todo 