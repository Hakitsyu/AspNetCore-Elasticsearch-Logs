namespace AspNetCore_Elasticsearch_Logs.Elasticsearch.Indices;

public record Log(string Description, DateTime At)
{
    public Guid Id { get; } = Guid.NewGuid();
    public Guid? ApplicationId { get; set; }
}