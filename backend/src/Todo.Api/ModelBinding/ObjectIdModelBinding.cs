using Microsoft.AspNetCore.Mvc.ModelBinding;
using MongoDB.Bson;

namespace Todo.Api.ModelBinding;

public class ObjectIdModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        var modelName = bindingContext.ModelName;

        var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

        var value = valueProviderResult.FirstValue;

        if (string.IsNullOrEmpty(value))
        {
            return Task.CompletedTask;
        }

        if (!ObjectId.TryParse(value, out var id))
        {
            return Task.CompletedTask;
        }

        bindingContext.Result = ModelBindingResult.Success(id);

        return Task.CompletedTask;
    }
}