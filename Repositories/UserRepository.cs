using System.Linq;
using OOP_Semester.Data;
using OOP_Semester.Models; // Dùng Model User chuẩn

namespace OOP_Semester.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool Login(string username, string password)
        {
            // Sửa u.Account -> u.Username (tên cột chuẩn trong DB)
            return _context.Users.Any(u => u.Account == username && u.Password == password);
        }

        // Sửa tham số đầu vào: Nhận trực tiếp đối tượng User
        public bool Register(User user)
        {
            // Kiểm tra xem Username đã tồn tại chưa
            if (_context.Users.Any(u => u.Account == user.Account))
                return false;

            // Nếu user chưa có Role, gán mặc định
         
             user.Role = UserRole.User;
           

            // Lưu thẳng vào DB
            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }
    }
}