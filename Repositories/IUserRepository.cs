using OOP_Semester.Models;

namespace OOP_Semester.Repositories
{
    public interface IUserRepository
    {
        bool Login(string username, string password);
        bool Register(User user);
    }
}