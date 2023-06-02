namespace ETicaret.Categories.Settings
{
    public class CategoryDatabaseSettings : ICategoryDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
