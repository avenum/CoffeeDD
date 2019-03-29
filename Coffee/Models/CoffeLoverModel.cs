using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coffee.Models
{
    public class CoffeLoverModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public int Count { get; set; }
        public bool PinExist { get; set; }

    }

    public class CoffeLoverPackage
    {
        public CoffeLoverPackage()
        {
            CoffeLovers = new List<CoffeLoverModel>();
        }
        public List<CoffeLoverModel> CoffeLovers { get; set; }
        public string DateTime { get; set; }
    }

    public class NewCoffeLover
    {
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Email { get; set; }
        public string PinHash { get; set; }

    }

    public class JsonResponse
    {
        public bool Success { get; set; }
        public object Result { get; set; }

        public JsonResponse(bool success, object result)
        {
            Success = success;
            Result = result;
        }
        public JsonResponse() { }
    }

    public class CoffeCounterRequest
    {
        public int id { get; set; }
        public string pin { get; set; }
    }

}