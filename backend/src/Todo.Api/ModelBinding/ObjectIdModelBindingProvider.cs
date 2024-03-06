using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using MongoDB.Bson;

namespace Todo.Api.ModelBinding;

public class ObjectIdModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Metadata.ModelType == typeof(ObjectId)
            || context.Metadata.ModelType == typeof(ObjectId?))
        {
            return new BinderTypeModelBinder(typeof(ObjectIdModelBinder));
        }

        return null;
    }
}