using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.MainDb;

namespace Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoutesController
    {
        [HttpGet]
        public ResultModel<List<RoutesModel>> GetRoutes()
        {
            return new ResultModel<List<RoutesModel>>
            {
                Code = 20000,
                Data = new List<RoutesModel>
                               {
                    //new RoutesModel
                    //{
                    //    Path = "",
                    //    Children = new List<ChildrenItem>(), todo
                    //    Component = "",
                    //    Hidden = ""
                    //},
                    //new RoutesModel
                    //{
                    //    Path = "",
                    //    Children = new List<ChildrenItem>(),
                    //    Component = "",
                    //    Hidden = ""
                    //}
                }
            };
        }
    }
}