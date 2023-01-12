namespace LightNote.Api.Extensions
{
    public static class HttpContextExtensions
    {
        public static Guid GetCurrentUserId(this HttpContext context)
        {
            Guid userId = Guid.Empty;
            Guid.TryParse(context.User.FindFirst("UserProfileId")?.Value, out userId);
            return userId;
        }
        public static Guid GetIdentityId(this HttpContext context)
        {
            Guid userId = Guid.Empty;
            Guid.TryParse(context.User.FindFirst("IdentityId")?.Value, out userId);
            return userId;
        }
    }
}