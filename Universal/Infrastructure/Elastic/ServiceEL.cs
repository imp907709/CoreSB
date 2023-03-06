using System;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

public class ServiceEL
{
    private ElasticsearchClient _client;

    public ServiceEL()
    {
        var settings = new ElasticsearchClientSettings(new Uri("https://localhost:9200"))
            //.CertificateFingerprint("<FINGERPRINT>")
            .Authentication(new BasicAuthentication("kibana_system", "zsxdcf"));

        _client = new ElasticsearchClient(settings);
    }

    public void Connect()
    {
        
    }
}
