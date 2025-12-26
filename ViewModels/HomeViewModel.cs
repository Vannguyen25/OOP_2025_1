using System; // Để dùng String
using System.Windows.Input;
using OOP_Semester.Models;

namespace OOP_Semester.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly User _user;

        // --- 1. Quản lý View con ---
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        // --- 2. Thông tin User ---
        public string Username => _user.Name;

        // [MỚI] Xử lý AvatarUrl
        public string AvatarUrl
        {
            get
            {
                // A. Nếu trong DB chưa có avatar (null hoặc rỗng) -> Trả về ảnh mặc định
                if (string.IsNullOrEmpty(_user.Avatar))
                {
                    // Đảm bảo bạn có file này trong thư mục Images
                    return "/Images/System/DefaultAvatar.png";
                }

                // B. Nếu đã có đường dẫn (vd: "/Images/user1.png")
                // WPF thông minh tự hiểu đường dẫn tương đối nếu file là Resource
                return _user.Avatar;
            }
        }

        // --- 3. ViewModel con ---
        public TodayViewModel TodayVM { get; set; }

        // --- 4. Command ---
        public ICommand NavigateCommand { get; }

        public HomeViewModel(User user)
        {
            _user = user;

            TodayVM = new TodayViewModel();
            CurrentView = TodayVM;

            NavigateCommand = new RelayCommand(obj =>
            {
                switch (obj?.ToString()) // Thêm ? để tránh null crash
                {
                    case "Today": CurrentView = TodayVM; break;
                }
            });
        }
    }
}