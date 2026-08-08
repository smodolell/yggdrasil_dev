using Microsoft.AspNetCore.Components.Forms;
using Yggdrasil.Blazor.Exceptions;

namespace Yggdrasil.Blazor.Extensions;

public static class ValidationExtensions
{
    public static void MapErrors(this ValidationMessageStore messageStore,
                                 YggdrasilApiException exception,
                                 object model,
                                 EditContext editContext)
    {
        messageStore.Clear();

        foreach (var error in exception.ValidationErrors)
        {
            var fieldIdentifier = new FieldIdentifier(model, error.Key);
            foreach (var message in error.Value)
            {
                messageStore.Add(fieldIdentifier, message);
            }
        }

        editContext.NotifyValidationStateChanged();
    }
}