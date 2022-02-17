using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace GeekShopping.Web.Utils
{
    public class DecimalModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var values = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            try
            {
                decimal.TryParse(values.FirstValue, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out decimal result);
                bindingContext.Result = ModelBindingResult.Success(result);

            }
            catch (Exception ex)
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }
            

            return Task.CompletedTask;
        }
    }
}
