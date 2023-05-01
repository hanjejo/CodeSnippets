using System.ComponentModel.DataAnnotations;

namespace Sample.Users.DomainLayer
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
