using System.Windows;
using System.Windows.Input;
using OOP_Semester.Models;
using OOP_Semester.Repositories;

namespace OOP_Semester.ViewModels
{
    public class AuthViewModel : ViewModelBase
    {
        private readonly IUserRepository _userRepo;
        private readonly MainViewModel _mainViewModel;

        // --- 1. Các biến Binding ---
        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private bool _isRegisterMode;
        public bool IsRegisterMode
        {
            get => _isRegisterMode;
            set
            {
                if (SetProperty(ref _isRegisterMode, value))
                {
                    OnPropertyChanged(nameof(HeaderTitle));
                    OnPropertyChanged(nameof(SubmitButtonText));
                }
            }
        }

        public string HeaderTitle => IsRegisterMode ? "Tạo tài khoản mới" : "Xin chào! 👋";
        public string SubmitButtonText => IsRegisterMode ? "Đăng ký" : "Đăng nhập";

        // --- 2. Commands (Chỉ dùng cho việc chuyển Tab) ---
        public ICommand LoginTabCommand { get; }
        public ICommand RegisterTabCommand { get; }

        public AuthViewModel(IUserRepository userRepo, MainViewModel mainViewModel)
        {
            _userRepo = userRepo;
            _mainViewModel = mainViewModel;

            // Logic chuyển đổi qua lại giữa Login và Register
            LoginTabCommand = new RelayCommand(o => IsRegisterMode = false);
            RegisterTabCommand = new RelayCommand(o => IsRegisterMode = true);
        }

        // --- 3. HÀM QUAN TRỌNG ĐANG BỊ THIẾU ---
        // Đây chính là hàm mà AuthView.xaml.cs đang gọi. 
        // Nó nhận vào 2 chuỗi string thay vì object.
        public void HandleSubmit(string password, string confirmPass)
        {
            // Kiểm tra nhập liệu cơ bản
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo");
                return;
            }

            if (IsRegisterMode)
            {
                // --- LOGIC ĐĂNG KÝ ---
                if (password != confirmPass)
                {
                    MessageBox.Show("Mật khẩu xác nhận không khớp!", "Lỗi");
                    return;
                }

                var newUser = new Models.User
                {
                    Account = Username,
                    Password = password,
                    Role = UserRole.User // (Bỏ comment nếu model User có cột Role)
                };

                if (_userRepo.Register(newUser))
                {
                    MessageBox.Show("Đăng ký thành công! Hãy đăng nhập.");
                    IsRegisterMode = false; // Tự động chuyển về tab Đăng nhập
                }
                else
                {
                    MessageBox.Show("Tên tài khoản đã tồn tại!", "Lỗi");
                }
            }
            else
            {
                // --- LOGIC ĐĂNG NHẬP ---
                if (_userRepo.Login(Username, password))
                {
                    _mainViewModel.NavigateToHome();
                }
                else
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Lỗi");
                }
            }
        }
    }
}