using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace WebApi.Test;

public class CashFlowClassFixture(CustomWebApplicationFactory factory): IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    protected async Task<HttpResponseMessage> DoPost(string requestUri, object request, string token = "", string language= "")
    {
        AuthorizeRequest(token);
        SetLanguage(language);
        
        return await _client.PostAsJsonAsync(requestUri, request);
    }
    
    protected async Task<HttpResponseMessage> DoGet(string requestUri, string token, string language = "")
    {
        AuthorizeRequest(token);
        SetLanguage(language);
        
        return await _client.GetAsync(requestUri);
    }
    
    private void AuthorizeRequest(string token)
    {
        if (string.IsNullOrEmpty(token)) return;
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
    
    private void SetLanguage(string language)
    {
        if (string.IsNullOrEmpty(language)) return;
        _client.DefaultRequestHeaders.AcceptLanguage.Clear();
        _client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(language));
    }
}