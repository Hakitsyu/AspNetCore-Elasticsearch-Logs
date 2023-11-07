using AspNetCore_Elasticsearch_Logs.Elasticsearch.Indices;
using Elastic.Clients.Elasticsearch;

namespace AspNetCore_Elasticsearch_Logs.Elasticsearch.Repositories;

public class LogsRepository : ILogsRepository
{
    private readonly ElasticsearchClient _client;
    
    public LogsRepository(ElasticsearchClient client)
    {
        _client = client;
    }

    public void Bulk(IEnumerable<Log> logs)
    {
        if (logs == null)
            throw new ArgumentNullException(nameof(logs));
        
        var response = _client.Bulk(c => c
            .Index("search-logs")
            .IndexMany(logs));

        var hasException = response.TryGetOriginalException(out Exception? ex);
        if (hasException && ex != null)
            throw ex;
    }
}