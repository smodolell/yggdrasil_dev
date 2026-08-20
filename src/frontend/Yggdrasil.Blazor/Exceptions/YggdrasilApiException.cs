using System.Net;
using System.Text.Json;
using Yggdrasil.Blazor.DTOs;

namespace Yggdrasil.Blazor.Exceptions;

public class YggdrasilApiException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public string Content { get; }
    // Diccionario de: Campo -> Lista de Errores
    public Dictionary<string, List<string>> ValidationErrors { get; } = new();
    // Lista cruda de errores tal como los devuelve la API (campo "errors")
    public List<string> Errors { get; } = new();
    // Mensaje de la API (campo "message")
    public string? ApiMessage { get; }

    public YggdrasilApiException(HttpStatusCode statusCode, string content)
        : base($"API Error: {statusCode}")
    {
        StatusCode = statusCode;
        Content = content;

        try
        {
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var apiResponse = JsonSerializer.Deserialize<ApiResponseDto<object>>(content, options);

            ApiMessage = apiResponse?.Message;

            if (apiResponse?.Errors != null)
            {
                Errors.AddRange(apiResponse.Errors);

                foreach (var error in apiResponse.Errors)
                {
                    var parts = error.Split(": ");
                    if (parts.Length > 1)
                    {
                        var key = parts[0].Trim();
                        var message = parts[1].Trim();

                        if (!ValidationErrors.ContainsKey(key))
                            ValidationErrors[key] = new List<string>();

                        ValidationErrors[key].Add(message);
                    }
                }
            }
        }
        catch { /* Fallback si el JSON no tiene el formato esperado */ }
    }
}

