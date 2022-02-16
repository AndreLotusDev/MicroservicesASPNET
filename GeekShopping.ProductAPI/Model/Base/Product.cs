using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.ProductAPI.Model.Base
{
    [Table("PRODUCT")]
    public class Product : BaseEntity
    {
        [Column("NAME")]
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Column("PRICE")]
        [Required]
        [Range(1, 10000)]
        public decimal Price { get; set; }

        [Column("DESCRIPTION_ABOUT_PRODUCT")]
        [Required]
        [StringLength(500)]
        public string DescriptionAboutProduct { get; set; }

        [Column("CATEGORY_NAME")]
        [StringLength(50)]
        [Required]
        public string CategoryName { get; set; }

        [Column("IMAGE_URL")]
        [StringLength(300)]
        public string ImageUrl { get; set; }
    }
}
