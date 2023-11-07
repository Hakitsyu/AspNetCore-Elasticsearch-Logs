using AspNetCore_Elasticsearch_Logs.Configurations;
using AspNetCore_Elasticsearch_Logs.Dtos;
using AspNetCore_Elasticsearch_Logs.Elasticsearch.Extensions.Shared;
using AspNetCore_Elasticsearch_Logs.Elasticsearch.Indices;
using AspNetCore_Elasticsearch_Logs.Elasticsearch.Repositories;
using Elastic.Transport;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var settings = builder.Configuration.GetSection("Elasticsearch").Get<ElasticsearchSettings>();

builder.Services.AddElasticsearch(settings.Url, cfg => cfg
    .Authentication(new BasicAuthentication(settings.User, settings.Password))
    .ServerCertificateValidationCallback((o, certificate, arg3, arg4) => true));

builder.Services.AddScoped<ILogsRepository, LogsRepository>();

var app = builder.Build();
app.MapPost("/", ([FromBody] IEnumerable<LogCommand> commands, [FromServices] ILogsRepository logsRepository) =>
{
    var logs = commands
        .Select(command => new Log(command.Description, command.At)
        {
            ApplicationId = command.ApplicationId
        });

    try
    {
        logsRepository.Bulk(logs);
        return Results.Ok();
    }
    catch
    {
        return Results.BadRequest();
    }
});

app.Run();