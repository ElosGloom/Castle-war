namespace Common
{
    public static class User
    {
        public static string Id;
        public static readonly Inventory Inventory = new();
        public static int CurrentLevel
        {
            get;
            set;
        }
    }
}