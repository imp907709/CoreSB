﻿namespace CoreSB.Universal.Framework
{
    public class ConfigOptions
    {
        public ConnectionStringsOption ConnectionStrings { get; set; }
        public MongoOption MongoConnectionString { get; set; }
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
