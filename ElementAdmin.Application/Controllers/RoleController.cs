using System.Net;
using System;
using System.Threading.Tasks;
using ElementAdmin.Application.Model;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using static ElementAdmin.Application.Model.ApiResponse;
using ElementAdmin.Application.Model.Role;
using ElementAdmin.Application.Interface;

namespace ElementAdmin.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoleController
    {
        private readonly IRoleService _role;

        public RoleController(IRoleService role)
        {
            _role = role;
        }

        [HttpGet]
        public async Task<ApiResponse> GetRoles()
        {
            return await _role.GetRolesAsync();
        }

        [HttpPost]
        public async Task<ApiResponse> AddRole(RoleModel model)
        {
            return await _role.AddRoleAsync(model);
        }

        [HttpPut("{key}")]
        public async Task<ApiResponse> UpdateRole(RoleModel model)
        {
            return await _role.UpdateRoleAsync(model);
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> DeleteRole(long id)
        {
            return await _role.DeleteRoleAsync(id);
        }

        [HttpGet("getroutes")]
        public async Task<ApiResponse> GetRoutes()
        {
            return await _role.GetRoutesAsync();
        }
    }
}