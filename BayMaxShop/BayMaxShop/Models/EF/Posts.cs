using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BayMaxShop.Models.EF
{
    [Table("Posts")]
    public class Posts: CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage = "Bạn không để trống tiêu đề bài viết")]
        [StringLength(150)]
        public string PostName { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public int MenuId { get; set; }
        public bool IsActive { get; set; }
        public virtual Menu Menu { get; set; }
    }
}