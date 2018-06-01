using Floodr.Instagram.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Floodr.Controllers
{
    public class InstagramController : Controller
    {
        string baseAuthorizeURL = "https://api.instagram.com/oauth/authorize/";
        string acessTokenURL = "https://api.instagram.com/oauth/access_token";
        string mediaRecentURL = "https://api.instagram.com/v1/users/self/media/recent/";

        /*GET: Instagram
         *<summary> Starts Oauth flow to get acess token and this flows finished with getStream method
         */
        public ActionResult Index()
        {
            string redirectURI = Url.Action("exchangeCode",null,null, this.Request.Url.Scheme);
            string AuthorizationURI = baseAuthorizeURL +"?client_id=" + WebConfigurationManager.AppSettings["InstagramClientID"] + "&redirect_uri=" + redirectURI + "&response_type=code";
            return Redirect(AuthorizationURI);
        }


        /* GET
         * Called by Instagram,
         * Redirects with AcessToken to GetStream Action
         */ 
        public ActionResult ExchangeCode(string code)
        {
            Uri accessTokenUri = new Uri(acessTokenURL);
            string accessToken = "";

            using (var client = new WebClient())
            {
                var data = new NameValueCollection();
                data["client_id"] = WebConfigurationManager.AppSettings["InstagramClientId"];
                data["client_secret"] = WebConfigurationManager.AppSettings["InstagramSecretId"];
                data["grant_type"] = "authorization_code";
                data["redirect_uri"] = Url.Action("exchangeCode",null,null, this.Request.Url.Scheme);;
                data["code"] = code;

                var response = client.UploadValues(accessTokenUri, "POST", data);
                Stream stream = new MemoryStream(response);

                using (var reader = new StreamReader(stream))
                {
                    var jObject = Newtonsoft.Json.Linq.JObject.Parse(reader.ReadLine());
                    accessToken = jObject["access_token"].ToString();
                }

                return RedirectToAction("GetStream", new {AccessToken = accessToken});
            }
        }


        public ActionResult GetStream(string AccessToken)
        {
            if (AccessToken == null)
            {
                return Content("No Acess Token Retrieved");
            }

            Uri instagramMediaRecent = new Uri(mediaRecentURL+"?access_token=" + AccessToken);


            using (var client = new WebClient())
            {
                var response = client.OpenRead(instagramMediaRecent);

                using (var reader = new StreamReader(response))
                {
                    var jObject = Newtonsoft.Json.JsonConvert.DeserializeObject<InstagramRootObject>(reader.ReadLine());
                    ViewBag.ResponseObject=jObject;
                }

            }

            return View();
        }

    }
}