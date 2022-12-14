using System;
namespace LightNote.Api.Contracts.User.Response
{
	public class UserProfileResponse
	{
		public Guid Id { get; set; }
		public BasicUserInfo BasicUserInfo { get; set; } = new();
		public Subscription Subscription { get; set; }
	}
}

