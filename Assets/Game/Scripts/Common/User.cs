namespace Common
{
    public class User
    {
        public string Id;
        public readonly Inventory Inventory = new();
        public int CurrentLevel = 1;

        public void Load()
        {
            Inventory["melee"] = 10;
        }
    }
}