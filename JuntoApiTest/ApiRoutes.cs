using System;
using System.Collections.Generic;
using System.Text;

namespace JuntoApi.Test
{
    public static class ApiRoutes
    {
        public static string _baseUrl { get; set; }

        public static class Users
        {
            private static readonly string _usersControllerUrl = string.Concat(_baseUrl, "users");

            public static readonly string Get = string.Concat(_usersControllerUrl, "");
            public static readonly string Post = string.Concat(_usersControllerUrl, "");
            public static readonly string Put = string.Concat(_usersControllerUrl, "");
            public static readonly string Delete = string.Concat(_usersControllerUrl, "");
            public static readonly string ChangePassword = string.Concat(_usersControllerUrl, "/changepassword");
        }

        public static class Auth
        {
            private static readonly string _usersControllerUrl = string.Concat(_baseUrl, "auth");

            public static readonly string Login = string.Concat(_usersControllerUrl, "/login");
            public static readonly string Public = string.Concat(_usersControllerUrl, "/public");
            public static readonly string Authenticated = string.Concat(_usersControllerUrl, "/authenticated");
        }
    }
}
