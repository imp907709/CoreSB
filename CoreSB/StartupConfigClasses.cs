namespace StartupConfig
{
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
  
    
}
