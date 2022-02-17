using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace GeekShopping.Web.Utils
{
    public class CustomBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {

            if(context.Metadata.BinderType == typeof(DecimalModelBinder))
            {
                return new BinderTypeModelBinder(typeof(DecimalModelBinder));
            }

            return null;
        }
    }
}
