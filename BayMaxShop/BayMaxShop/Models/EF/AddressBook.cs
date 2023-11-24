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
        [StringLength(50, ErrorMessage = "Không được vượt quá 150 ký tự")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Dia chi không được để trống")]
        public string Address { get; set; }
        [Required(ErrorMessage = "So dien thoai không được để trống")]
        [StringLength(50, ErrorMessage = "Không được vượt quá 15 ký tự")]
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsDefault { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}