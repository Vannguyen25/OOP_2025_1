using System;
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

        // --- 2. Commands ---
        public ICommand LoginTabCommand { get; }
        public ICommand RegisterTabCommand { get; }

        public AuthViewModel(IUserRepository userRepo, MainViewModel mainViewModel)
        {
            _userRepo = userRepo;
            _mainViewModel = mainViewModel;

            LoginTabCommand = new RelayCommand(o => IsRegisterMode = false);
            RegisterTabCommand = new RelayCommand(o => IsRegisterMode = true);
        }

        // --- 3. OVERLOAD 1: XỬ LÝ ĐĂNG NHẬP (Login) ---
        public void HandleSubmit(string password)
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu!");
                return;
            }

            try
            {
                var user = _userRepo.Login(Username, password);

                if (user != null)
                {
                    // --- SỬA ĐỔI: Chuyển màn hình thay vì hiện MessageBox ---
                    // Giả sử MainViewModel có thuộc tính CurrentView để thay đổi giao diện
                    // Em có thể truyền 'user' vào HomeViewModel để hiển thị thông tin người dùng
                    _mainViewModel.NavigateToHome(user);
                }
                else
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message);
            }
        }

        // --- 4. OVERLOAD 2: XỬ LÝ ĐĂNG KÝ (Register) ---
        public void HandleSubmit(string password, string confirmPass)
        {
            if (password != confirmPass)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!");
                return;
            }

            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin đăng ký!");
                return;
            }

            try
            {
                var newUser = new User
                {
                    Account = Username,
                    Password = password,
                    Name = Username,
                    Role = UserRole.User,
                    CreatedAt = DateTime.Now,
                    GoldAmount = 0,
                    VacationMode = false,
                    Avatar = "Images\\System\\DefaultAvatar.png"
                };

                bool isSuccess = _userRepo.Register(newUser);

                if (isSuccess)
                {
                    MessageBox.Show("Đăng ký thành công! Vui lòng đăng nhập.");

                    // --- SỬA ĐỔI: Chuyển về Login nhưng GIỮ LẠI Username ---
                    IsRegisterMode = false;

                    // Dòng dưới đây đã bị xóa để Username vẫn hiện trên ô input
                    // Username = "";  <-- XÓA DÒNG NÀY
                }
                else
                {
                    MessageBox.Show("Tài khoản đã tồn tại hoặc lỗi tạo user.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng ký: " + ex.Message);
            }
        }
    }
}