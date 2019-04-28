using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RolesController
    {
        [HttpGet]
        public ResultModel<List<RolesModel>> GetRoles()
        {
            return new ResultModel<List<RolesModel>>
            {
                Code = 20000,
                Data = new List<RolesModel>
                              {
                                  new RolesModel
                                  {
                                      Key = "",
                                      Name = "",
                                      Description = "",
                                      Routes = new List<RoutesModel>()
                                  }
                              }
            };
        }
    }
}