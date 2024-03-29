﻿using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;

namespace Webapp.APIs;
public abstract class IAPI
{
    private string BaseUrl { get; set; }
    protected HttpClient Client { get; set; }

    public IAPI(string? baseUrl)
    {
        BaseUrl = baseUrl ?? "";
        Client = new HttpClient();
    }

    public void AddToken(string? token)
    {
        if (token == null) return;

        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    protected Task<HttpResponseMessage> Get(string path)
    {
        return Client.GetAsync(BuildUrl(path));
    }

    protected Task<HttpResponseMessage> Post(string path, object bodyParams)
    {
        string url = BuildUrl(path);

        return Client.PostAsync(url, MountBodyContent(bodyParams));
    }

    protected Task<HttpResponseMessage> Put(string path, object bodyParams)
    {
        string url = BuildUrl(path);

        return Client.PutAsync(url, MountBodyContent(bodyParams));
    }

    protected Task<HttpResponseMessage> Delete(string path)
    {
        string url = BuildUrl(path);

        return Client.DeleteAsync(url);
    }

    protected string BuildUrl(string path)
    {
        return $"{BaseUrl}{path}";
    }

    #region private

    private HttpContent MountBodyContent(object? bodyParams)
    {
        var body = new StringContent(
            JsonConvert.SerializeObject(bodyParams),
            Encoding.UTF8,
            MediaTypeNames.Application.Json
        );

        return body;
    }

    #endregion
}
