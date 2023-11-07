using AspNetCore_Elasticsearch_Logs.Elasticsearch.Indices;

namespace AspNetCore_Elasticsearch_Logs.Elasticsearch.Repositories;

public interface ILogsRepository
{
    void Bulk(IEnumerable<Log> logs);
}