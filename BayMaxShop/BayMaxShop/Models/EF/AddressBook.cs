using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BayMaxShop.Models.EF
{
    [Table("AddressBook")]
    public class AddressBook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserID { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(50, ErrorMessage = "Không được vượt quá 50 ký tự")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Số điên thoại không được để trống")]
        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Số điện thoại phải có đủ 10 hay 11 số")]
        public string Phone { get; set; }
        [EmailAddress(ErrorMessage = "Địa chỉ Email không hợp lệ")]
        public string Email { get; set; }
        public bool IsDefault { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}