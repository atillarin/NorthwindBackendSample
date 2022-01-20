using Northwind.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Entity.Models
{
    public class User :EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage ="User ID required")]
        public int UserID { get; set; }
        [MaxLength(30)]
        public string UserName { get; set; }
        public string UserLastName { get; set; }
        public string UserCode { get; set; }
        public string Password { get; set; }
    }
}
