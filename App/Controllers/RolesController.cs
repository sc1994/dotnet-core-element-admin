using System.Collections.Generic;
using System.Threading.Tasks;
using App;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.MainDb;
using Services.MainDb;
using System.Linq;
using Models.MainDbModel;
using Utils;

namespace Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RolesController : ControllerBaseExtend
    {
        private readonly IRolesService _service;
        private readonly IRoutesService _routesService;

        public RolesController(IRolesService service, IRoutesService routesService)
        {
            _service = service;

            _routesService = routesService;
        }

        [HttpGet]
        public async Task<ResultModel> GetRoles()
        {
            var roles = await _service.FindAsync(x => !string.IsNullOrWhiteSpace(x.Key));
            if ((roles?.Count ?? 0) < 3)
            {
                roles?.ToList().ForEach(x => _service.RemoveAsync(x));
                roles = new List<RolesModel>
                        {
                            new RolesModel
                            {
                                Key = "admin",
                                Name = "��������Ա",
                                Description = "��������ɶ�����ܿ���"
                            },
                            new RolesModel
                            {
                                Key = "editor",
                                Name = "�ɱ༭�ĳ�Ա",
                                Description = "����ܿ���һ���ְ�"
                            },
                            new RolesModel
                            {
                                Key = "visitor",
                                Name = "�۲���",
                                Description = "���Կ���һЩ��"
                            }
                        };
                await _service.AddRangeAsync(roles);
            }

            if (!(roles?.Any() ?? false))
            {
                return Bad("û���κ�����");
            }

            var result = roles.Select(Mapper.ToExtend<RolesView>).ToList();
            result.ForEach(x=>x.Routes);

            return Ok(result);
        }
    }
}