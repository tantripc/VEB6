namespace MiddlewareTool.Common
{
    public sealed class AppMenu
    {
        public enum Status : byte
        {
            AllowAnonymous = 0,//public and not menu
            Private = 1, //private menu
            Public = 2 //public menu
        }
        public enum Action : byte
        {
            None = 0,
            Index = 1,
            Insert = 2,
            Update = 3,
            Delete = 4,
            Detail = 5,
            API = 6,
            Import = 7,
            Export = 8
        }
        public enum Role : byte
        {
            Menu = 0,
            Action = 1
        }
    }
}
