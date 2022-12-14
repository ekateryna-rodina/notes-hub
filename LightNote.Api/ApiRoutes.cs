using System;
namespace LightNote.Api
{
	public class ApiRoutes
	{
		public const string BaseRoute = "api/v{version:apiVersion}/[controller]";

        public static class Identity {
			public const string Login = "login";
            public const string Register = "register";
        }
	}
}

