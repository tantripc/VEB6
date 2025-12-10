namespace MiddlewareTool.Common
{
    public sealed class AppMode
    {
        public enum Login
        {
            AD = 0,
            SP = 1,
            Local = 2,
            EOffice = 3,
            SSO = 4,
        }
        public enum Language
        {
            Vietnamese = 0,
            English = 1,
            French = 2,
            Chinese = 3,
            Japanese = 4,
            Russian = 5
        }
        public sealed class CachingAbsoluteExpiration
        {
            /// <summary>
            /// Default 1 day
            /// </summary>
            public static readonly DateTimeOffset Default = DateTime.Now.AddDays(1);
            /// <summary>
            /// None Expiration
            /// </summary>
            public static readonly DateTimeOffset NoneExpiration = DateTimeOffset.MaxValue;
        }
    }
}
