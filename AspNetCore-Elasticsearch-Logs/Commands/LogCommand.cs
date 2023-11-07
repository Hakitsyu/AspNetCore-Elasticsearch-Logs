using System.ComponentModel.DataAnnotations;

namespace AspNetCore_Elasticsearch_Logs.Dtos;

public class LogCommand
{
    [Required]
    public required string Description { get; set; }
    [Required]
    public DateTime At { get; set; }
    public Guid? ApplicationId { get; set; }
}