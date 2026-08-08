using Microsoft.JSInterop;
using System.Net.Http.Headers;

namespace Yggdrasil.Blazor.Handlers;

//public class YggdrasilHeaderHandler(IAccessTokenProvider tokenProvider) : DelegatingHandler
//{
//    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
//    {
//        var tokenResult = await tokenProvider.RequestAccessToken();
//        if (tokenResult.TryGetToken(out var token))
//        {
//            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
//        }
//        return await base.SendAsync(request, cancellationToken);
//    }
//}

public class YggdrasilHeaderHandler : DelegatingHandler
{
    private readonly IJSRuntime _js;

    public YggdrasilHeaderHandler(IJSRuntime js)
    {
        _js = js;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _js.InvokeAsync<string>("localStorage.getItem", "authToken");
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
