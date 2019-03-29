using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Coffee.Models;

namespace Coffee.Controllers
{
    public class AddCoffeeController : ApiController
    {
        [HttpPost]
        public IHttpActionResult GetAddCoffee([FromBody] CoffeCounterRequest ccr)
        {
            var res = new JsonResponse();

            using (var dc = new CoffeDC())
            {
                var cl = dc.CoffeLovers.FirstOrDefault(x => x.Id == ccr.id);

                if (string.IsNullOrEmpty(cl.PinHash))
                {
                    cl.PinHash = ccr.pin;
                }

                if (cl.PinHash == ccr.pin)
                {
                    cl.CoffeCounts.Add(new CoffeCount() { DateTime = DateTime.Now });
                    dc.SaveChanges();
                    res.Success = true;

                }
                else
                {
                    res.Success = false;
                    res.Result = "Не верно набран пин код";
                }

            }



            return Json(res);
        }
    }
    public class DellCoffeeController : ApiController
    {
        [HttpPost]
        public IHttpActionResult GetDellCoffee([FromBody] CoffeCounterRequest ccr)
        {
            var res = new JsonResponse();

            using (var dc = new CoffeDC())
            {
                var cl = dc.CoffeLovers.FirstOrDefault(x => x.Id == ccr.id);
                var d = cl.CoffeCounts.OrderByDescending(x => x.Id).FirstOrDefault();
                if (d != null)
                {
                    dc.CoffeCounts.Remove(d);
                    dc.SaveChanges();
                }
                else
                {
                    dc.CoffeLovers.Remove(cl);
                    dc.SaveChanges();
                }
            }

            res.Success = true;


            return Json(res);
        }


    }



}
