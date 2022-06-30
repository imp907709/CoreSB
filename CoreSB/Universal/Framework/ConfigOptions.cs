namespace CoreSB.Universal.Framework
{
    public class ConfigOptions
    {
        public ConnectionStringsOption ConnectionStrings { get; set; }
        public MongoConnectionStringOption MongoConnectionString { get; set; }
    }

    public class ConnectionStringsOption
    {
        
        public string ConfigString => "ConnectionStrings";

        public string DockerMsSQlCoreSBConnection { get; set; } = string.Empty;
        public string DockerPgSQlCoreSBConnection { get; set; } = string.Empty;
        public string DockerMsSQlNewOrderConnection { get; set; } = string.Empty;
        public string DockerPgSQlNewOrderConnection { get; set; } = string.Empty;

    }
    public class MongoConnectionStringOption
    {

        public string ConfigString => "MongoConnectionString";
        
        
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string BooksCollectionName { get; set; } = null!;
    }
}
