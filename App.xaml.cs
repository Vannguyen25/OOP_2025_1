using System.Windows;
using OOP_Semester.ViewModels;
using OOP_Semester.Views; // <--- QUAN TRỌNG: Phải có dòng này mới tìm thấy MainWindow

namespace OOP_Semester
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 1. Tạo MainWindow (Giờ nó đã tồn tại trong namespace OOP_Semester.Views)
            var mainWindow = new MainWindow();

            // 2. Tạo MainViewModel
            var mainViewModel = new MainViewModel();

            // 3. Gắn kết
            mainWindow.DataContext = mainViewModel;

            // 4. Hiện lên
            mainWindow.Show();
        }
    }
}