namespace CoreSB.Universal.StartupConfigs
{
    public class ConfigOptions
    {
        public ConnectionStringsOption ConnectionStrings { get; set; }
        public MongoOption MongoConnectionString { get; set; }
    }
    
    public class ConnectionStrings
    {
        public string MsSQlCoreSBConnection { get; set; }
        public string DockerMsSQlCoreSBConnection { get; set; }
        public string PgSQlCoreSBConnection { get; set; }
    }
    public enum ContextType
    {
        SQL, SQLLite, InMemmory
    }
    public class RegistrationSettings
    {
        public ContextType ContextType { get; set; }
    }
    
    public class Variables
    {
        public static string context => "context";
        public static string ConnectionStrings => "ConnectionStrings";
        
        public static string SQL => "SQL";
        public static string SQLLite => "SQLLite";
        public static string InMemmory => "InMemmory";
        
        public static string MsSQlCoreSBConnection => "MsSQlCoreSBConnection";
        public static string DockerMsSQlCoreSBConnection => "DockerMsSQlCoreSBConnection";
        public static string PgSQlCoreSBConnection => "PgSQlCoreSBConnection";
        
        public static string ServiceInited => "Inited";
    }
  
    public class ConnectionStringsOption
    {
        
        public string ConfigString => "ConnectionStrings";

        public string DockerMsSQlCoreSBConnection { get; set; } = string.Empty;
        public string DockerPgSQlCoreSBConnection { get; set; } = string.Empty;
        public string DockerMsSQlNewOrderConnection { get; set; } = string.Empty;
        public string DockerPgSQlNewOrderConnection { get; set; } = string.Empty;

    }
    
    public class MongoOption
    {
        public string ConfigString => "Mongo";
        
        
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string CollectionName { get; set; } = null!;
    }
    
}
