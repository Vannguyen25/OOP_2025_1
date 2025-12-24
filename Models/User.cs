using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OOP_Semester.Models
{
    // 1. Định nghĩa Enum cho Role (khớp với giá trị enum trong MySQL)

    // 2. Class User ánh xạ bảng 'users'
    [Table("users")]
    public class User
    {
        [Key] // Khóa chính (Primary Key)
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Tự động tăng (AI)
        public int UserID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Account { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "enum('User','Admin')")] // Chỉ định kiểu enum trong DB
        public UserRole Role { get; set; } = UserRole.User; // Mặc định là User

        // tinyint(1) trong MySQL tự động map sang bool trong EF Core
        public bool VacationMode { get; set; }

        // Kiểu 'time' trong MySQL map sang 'TimeSpan' trong C#
        public TimeSpan? MorningTime { get; set; }
        public TimeSpan? AfternoonTime { get; set; }
        public TimeSpan? EveningTime { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [MaxLength(255)]
        public string? Avatar { get; set; }

        public int GoldAmount { get; set; }
    }
}