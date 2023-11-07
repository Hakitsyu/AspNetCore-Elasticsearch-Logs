using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore_Elasticsearch_Logs.Elasticsearch.Extensions.Shared;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddElasticsearch(this IServiceCollection services, string url, Action<ElasticsearchClientSettings> configure)
    {
        var settings = new ElasticsearchClientSettings(new Uri(url));
        configure(settings);

        services.AddSingleton<IElasticsearchClientSettings>(settings);
        services.AddScoped(sp =>
        {
            var stg = sp.GetRequiredService<IElasticsearchClientSettings>();
            return new ElasticsearchClient(stg);
        });
        
        return services;
    }
}