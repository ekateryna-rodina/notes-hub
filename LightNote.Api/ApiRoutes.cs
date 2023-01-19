using System;
using LightNote.Application.BusinessLogic.Notebook.Queries;

namespace LightNote.Api
{
    public class ApiRoutes
    {
        public const string BaseRoute = "api/v{version:apiVersion}/[controller]/";
        public static class Identity
        {
            public const string Login = "login";
            public const string Register = "register";
            public const string Refresh = "refresh";
            public const string Logout = "logout";
        }
        public static class Tag
        {
            public const string GetAll = "";
            public const string CreateMany = "";
            public const string GetByIds = "{ids}";
        }
    }
}

