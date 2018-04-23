using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Model.Models
{
    [Table("OnlineSupports")]
    public class OnlineSupport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Department { get; set; }

        public string Skype { get; set; }

        public string Facebook { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string Yahoo { get; set; }

        public bool? Status { get; set; }
    }
}
