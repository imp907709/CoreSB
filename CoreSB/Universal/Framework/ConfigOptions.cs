namespace CoreSB.Universal.Framework
{
    public class ConfigOptions
    {
        public ConnectionStringsOption ConnectionStrings { get; set; }
    }

    public class ConnectionStringsOption
    {
        
        public string ConnectionStrings => "ConnectionStrings";

        public string DockerMsSQlCoreSBConnection { get; set; } = string.Empty;
        public string DockerPgSQlCoreSBConnection { get; set; } = string.Empty;
        public string DockerMsSQlNewOrderConnection { get; set; } = string.Empty;
        public string DockerPgSQlNewOrderConnection { get; set; } = string.Empty;

    }
}
