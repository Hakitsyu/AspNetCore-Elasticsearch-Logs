namespace AspNetCore_Elasticsearch_Logs.Configurations;

public class ElasticsearchSettings
{
    public required string Url { get; set; }
    public required string User { get; set; }
    public required string Password { get; set; }
}