using System;
using System.Collections.Generic;
using App;
using Microsoft.AspNetCore.Mvc;
using Models.MainDb;

namespace Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionController
    {
        [HttpGet("list")]
        public Response<Page<TransactionModel>> TransactionList()
        {
            return new Response<Page<TransactionModel>>
            {
                Code = 20000,
                Data = new Page<TransactionModel>
                {
                    Total = 1,
                    Items = new List<TransactionModel>
                            {
                                new TransactionModel
                                {
                                    Timestamp = (long)(DateTime.Now - DateTime.MinValue).TotalSeconds,
                                    Status = "Status",
                                    OrderNo = "OrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNoOrderNo",
                                    Price = 1.1M,
                                    Username = "Username"
                                }
                            }
                }
            };
        }
    }
}