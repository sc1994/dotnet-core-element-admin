using System.Linq;
using System.Threading.Tasks;
using ElementAdmin.Domain.Aggregate.RolesRoot;
using ElementAdmin.Domain.Factories;
using ElementAdmin.Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;

namespace ElementAdmin.Application.Web.Controllers
{
    [Route("[controller]")]
    [Route("role")]
    [ApiController]
    public class RolesController : BaseController
    {
        private readonly IRolesFactory _roles;

        public RolesController(IRolesFactory roles)
        {
            _roles = roles;
        }

        [HttpGet("{key}")]
        public async Task<BaseResponse> GetRoleByKeyAsync(string key)
        {
            var result = await _roles.GetRoleTreesAsync();
            if (!result.Done)
            {
                return Bad(result.Message);
            }
            return Ok(result.Result.FirstOrDefault(x => x.Key == key));
        }

        [HttpGet]
        public async Task<BaseResponse> GetRolesAsync()
        {
            var result = await _roles.GetRoleTreesAsync();
            if (!result.Done)
            {
                return Bad(result.Message);
            }
            return Ok(result.Result);
        }

        [HttpPut("{key}")]
        public async Task<BaseResponse> EditRole(string key, RolesTreeAggRoot model)
        {
            var result = await _roles.EditRoleAsync(model);
            return Ok();
        }

        [HttpPost]
        public async Task<BaseResponse> AddRole(RolesTreeAggRoot model)
        {
            var result = await _roles.AddRoleAsync(model);
            return Ok();
        }
    }
}