using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floodr.Instagram.Models
{
    //http://json2csharp.com/ this can also be used. But no need for unnecessary fields right?

        public class User
        {
            public string id { get; set; }
            public string full_name { get; set; }
            public string profile_picture { get; set; }
            public string username { get; set; }
        }

        public class StandardResolution
        {
            public int width { get; set; }
            public int height { get; set; }
            public string url { get; set; }
        }

        public class Images
        {
            public StandardResolution standard_resolution { get; set; }
        }

        public class From
        {
            public string id { get; set; }
            public string full_name { get; set; }
            public string profile_picture { get; set; }
            public string username { get; set; }
        }

        public class Caption
        {
            public string id { get; set; }
            public string text { get; set; }
            public string created_time { get; set; }
            public From from { get; set; }
        }

        public class Likes
        {
            public int count { get; set; }
        }

        public class Comments
        {
            public int count { get; set; }
        }

        public class InstagramPost
        {
            public string id { get; set; }
            public User user { get; set; }
            public Images images { get; set; }
            public string created_time { get; set; }
            public Caption caption { get; set; }
            public bool? user_has_liked { get; set; }
            public Likes likes { get; set; }
            public List<object> tags { get; set; }
            public string filter { get; set; }
            public Comments comments { get; set; }
            public string type { get; set; }
            public string link { get; set; }
            public object attribution { get; set; }
            public List<object> users_in_photo { get; set; }
            public List<object> carousel_media { get; set; }
        }


        public class InstagramRootObject
        {            
            public List<InstagramPost> data { get; set; }

        }

    }