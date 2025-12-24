using System.Windows;
using System.Windows.Controls;
using OOP_Semester.ViewModels; // <--- QUAN TRỌNG: Phải có dòng này mới hiểu AuthViewModel là gì

namespace OOP_Semester.Views
{
    public partial class AuthView : UserControl
    {
        public AuthView()
        {
            InitializeComponent();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            // 1. Kiểm tra xem DataContext có đúng là AuthViewModel không
            if (this.DataContext is AuthViewModel viewModel)
            {
                // 2. Lấy mật khẩu từ giao diện (XAML phải đặt x:Name="txtPassword")
                // Kiểm tra null để tránh lỗi crash app
                string pass = txtPassword != null ? txtPassword.Password : "";
                string confirmPass = txtConfirmPassword != null ? txtConfirmPassword.Password : "";

                // 3. Gọi hàm xử lý bên ViewModel
                // LƯU Ý: Bên AuthViewModel bắt buộc phải có hàm HandleSubmit nhé
                viewModel.HandleSubmit(pass, confirmPass);

                // 4. Xóa mật khẩu sau khi bấm (Logic bảo mật/UI)
                if (!viewModel.IsRegisterMode && txtPassword != null)
                {
                    txtPassword.Clear();
                }
            }

        }
    }
}