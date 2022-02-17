using GeekShopping.Web.Utils;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Models
{
    public class ProductModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        [ModelBinder(BinderType = typeof(DecimalModelBinder))]
        public decimal Price { get; set; }
        public string DescriptionAboutProduct { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
    }
}
