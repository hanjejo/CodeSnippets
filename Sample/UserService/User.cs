using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.UserService
{
    // 유저 엔티티
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { set; get; }
        [Required]
        public int Age { set; get; }
    }
}
