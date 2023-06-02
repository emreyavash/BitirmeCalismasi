namespace ETicaret.Categories.Settings
{
    public interface ICategoryDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
