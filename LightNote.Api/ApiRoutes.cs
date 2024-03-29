﻿using System;
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
        public static class Reference
        {
            public const string Create = "";
            public const string GetAllByNotebookId = "{notebookId}";
            public const string GetById = "{id}";
            public const string Update = "{id}";
            public const string Delete = "{id}";
        }
        public static class Notebook
        {
            public const string Create = "";
            public const string GetAll = "";
            public const string GetById = "{id}";
            public const string Update = "{id}";
            public const string Delete = "{id}";
        }
        public static class SlipNote
        {
            public const string Create = "";
            public const string GetAllByNotebookId = "{id}";
            public const string GetById = "{id}";
            public const string Update = "{id}";
            public const string Delete = "{id}";
        }
        public static class PermanentNote
        {
            public const string Create = "";
            public const string GetAllByNotebookId = "{id}";
            public const string GetById = "{id}";
            public const string Update = "{id}";
            public const string Delete = "{id}";
        }
        public static class Insight
        {
            public const string Create = "";
            public const string GetAllByNotebookId = "{id}";
            public const string GetById = "{id}";
            public const string Update = "{id}";
            public const string Delete = "{id}";
        }
        public static class Question
        {
            public const string Create = "";
            public const string GetAllByNotebookId = "{id}";
            public const string GetById = "{id}";
            public const string Update = "{id}";
            public const string Delete = "{id}";
        }
    }
}

