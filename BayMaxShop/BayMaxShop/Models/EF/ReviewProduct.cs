using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BayMaxShop.Models.EF
{
    [Table("Review")]
    public class ReviewProduct
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email không được để trống")]
        [StringLength(50, ErrorMessage = "Không được vượt quá 50 ký tự")]
        [EmailAddress(ErrorMessage = "Địa chỉ Email không hợp lệ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(50, ErrorMessage = "Không được vượt quá 50 ký tự")]
        public string FullName { get; set; }
        [StringLength(70, ErrorMessage = "Không được vượt quá 70 ký tự")]
        public string Content { get; set; }
        public int Rate { get; set; }
        public string Avatar { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual Product Product { get; set; }
    }
}