namespace Application.Common.Models.CrawlerService;

public class WorkerServiceTokenDto
{
    public string AccessToken { get; set; }

    public WorkerServiceTokenDto(string accessToken)
    {
        AccessToken = accessToken;
    }
}