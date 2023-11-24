using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BayMaxShop.Models.EF
{
    [Table("Brand")]
    public class Brand
    {
        public Brand() 
        {
            this.Products = new HashSet<Product>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage ="Không được để trống tên thương hiệu")]
        [StringLength(50)]
        public string BrandName { get; set; }
        [Required]
        [StringLength(150)]
        public string Alias { get; set; }
        [StringLength(250)]
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}