using System;
namespace LightNote.Api.Contracts.User.Response
{
	public class Subscription
	{
		public IEnumerable<BasicUserInfo> Following { get; set; } = default!;
		public IEnumerable<BasicUserInfo> Followers { get; set; } = default!;
	}
}

