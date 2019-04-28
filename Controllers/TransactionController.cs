using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionController
    {
        [HttpGet("list")]
        public ResultModel<PageModel<TransactionModel>> TransactionList()
        {
            return new ResultModel<PageModel<TransactionModel>>
            {
                Code = 20000,
                Data = new PageModel<TransactionModel>
                {
                    Total = 1,
                    Items = new List<TransactionModel>
                            {
                                new TransactionModel
                                {
                                    Timestamp = (long)(DateTime.Now - DateTime.MinValue).TotalSeconds,
                                    Status = "Status",
                                    OrderNo = "OrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNo",
                                    Price = 1.1,
                                    Username = "Username"
                                }
                            }
                }
            };
        }
    }
}