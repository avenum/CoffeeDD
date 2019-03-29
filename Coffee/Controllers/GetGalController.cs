using Coffee.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Coffee.Controllers
{
    public class GetGalController : ApiController
    {


        [HttpGet]
        public IHttpActionResult Search(string id)
        {
            var js = Json(Gal.SearchContacts(id));

            js.Request.Headers.Add("Access-Control-Allow-Origin", "*");
            return js;
        }

        public class Data
        {
            public string email { get; set; }
        }

        [HttpPost]
        public IHttpActionResult PostUser([FromBody] Data data)
        {

            return Json(Gal.GetPhotoContact(data.email));

        }






    }
}
