using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Coffee.Models;

namespace Coffee.Controllers
{
    public class CoffeLoversController : ApiController
    {

        public IHttpActionResult Get()
        {
            var coffeLoverPack = new CoffeLoverPackage();
            coffeLoverPack.DateTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            using (CoffeDC dc = new CoffeDC())
            {
                foreach (var cl in dc.CoffeLovers.OrderBy(x => x.Name))
                {
                    var t = new CoffeLoverModel();
                    t.Name = cl.Name;
                    t.Photo = cl.Photo;
                    t.Id = cl.Id;
                    t.PinExist = !string.IsNullOrEmpty(cl.PinHash);

                    var ds = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
                    var de = ds.AddDays(7);

                    t.Count = cl.CoffeCounts.Count(x => x.DateTime >= ds && x.DateTime <= de);
                    coffeLoverPack.CoffeLovers.Add(t);
                }

            }

            return Json(new JsonResponse() { Success = true, Result = coffeLoverPack });


        }


        public IHttpActionResult Post([FromBody] NewCoffeLover man)
        {

            var res = new JsonResponse();
            using (CoffeDC dc = new CoffeDC())
            {
                if (dc.CoffeLovers.Any(x => x.Email == man.Email))
                {
                    res.Success = false;
                    res.Result = "Данный пользователь уже есть в системе";
                }
                else
                {
                    var t = new CoffeLover();
                    t.Email = man.Email;
                    t.Name = man.Name;
                    t.Photo = man.Photo;
                    t.PinHash = man.PinHash;

                    dc.CoffeLovers.Add(t);
                    dc.SaveChanges();
                    res.Success = true;
                    res.Result = t.Id;
                }

            }

            return Json(res);
        }
    }
}
