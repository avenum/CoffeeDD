using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Coffee.Models
{
    public class Gal
    {
        const string GalLogin = "digdes\\Sokolov.sur";
        const string GalPassword = "";
        const string GalUrl = "http://tlssrv.digdes.com:8088/cert-gal/ContactService.svc";

        public class GetContactsResultClass
        {
            public ResultResolvedContact GetContactsResult { get; set; }
        }
        public class GetPhotoContactsResultClass
        {
            public ResultResolvedContact GetPhotoContactsResult { get; set; }
        }
        public class ResultResolvedContact
        {
            public List<ResolvedContact> Contacts { get; set; }
            public bool IsLastPage { get; set; }
            public string ErrorMessage { get; set; }
            public int ContactsCacheCount { get; set; }
            public int ContactsResultCount { get; set; }
            public double TimeWork { get; set; }
        }
        public class ResolvedContact
        {
            public string Id { get; set; }
            public string DisplayName { get; set; }
            public string EMail { get; set; }
            public string Certificates { get; set; }
            public string Mobile { get; set; }
            public string Phone { get; set; }
            public string Photo { get; set; }
            public string Position { get; set; }
            public string Department { get; set; }
            public string Address { get; set; }
            public string Created { get; set; }
            public string Changed { get; set; }
        }
        public static List<ResolvedContact> SearchContacts(string text)
        {
            var str = string.Format("{0}/getcontacts?c={1}&u={2}&p={3}", GalUrl, text, GalLogin, GalPassword);

            ServicePointManager
    .ServerCertificateValidationCallback +=
    (sender, cert, chain, sslPolicyErrors) => true;

            var wc = new WebClient();
            wc.Encoding = System.Text.Encoding.UTF8;
            var _json = wc.DownloadString(str);

            var GetContactsResult = JsonConvert.DeserializeObject<GetContactsResultClass>(_json);

            return GetContactsResult.GetContactsResult.Contacts;
        }
        public static ResolvedContact GetPhotoContact(string email)
        {
            var str = string.Format("{0}/GetPhotoContacts?e={1}&u={2}&p={3}", GalUrl, email, GalLogin, GalPassword);

            ServicePointManager
.ServerCertificateValidationCallback +=
(sender, cert, chain, sslPolicyErrors) => true;


            var wc = new WebClient();
            wc.Encoding = System.Text.Encoding.UTF8;
            var _json = wc.DownloadString(str);

            var GetContactsResult = JsonConvert.DeserializeObject<GetPhotoContactsResultClass>(_json);

            return GetContactsResult.GetPhotoContactsResult.Contacts.FirstOrDefault();
        }

    }


}