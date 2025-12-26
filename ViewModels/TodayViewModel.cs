using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using OOP_Semester.Models; // Nếu em chưa có Model Habit thì tạm thời bỏ qua namespace này

namespace OOP_Semester.ViewModels
{
    // Class phụ để hiển thị từng dòng thói quen trong list
    public class HabitItemDisplay : ViewModelBase
    {
        public string Title { get; set; }
        public string Subtitle { get; set; } // Ví dụ: "07:00 AM • 5km"
        public string Icon { get; set; } // Dùng Emoji tạm hoặc PackIcon sau này
        public string ColorHex { get; set; } // Màu nền icon

        private bool _isCompleted;
        public bool IsCompleted
        {
            get => _isCompleted;
            set => SetProperty(ref _isCompleted, value);
        }

        // Dành cho loại thói quen đếm số (như uống nước)
        private int _currentCount;
        public int CurrentCount
        {
            get => _currentCount;
            set => SetProperty(ref _currentCount, value);
        }
        public int TargetCount { get; set; }
        public bool IsCounterType { get; set; } // True nếu là dạng đếm (2/8 cốc)
    }

    public class TodayViewModel : ViewModelBase
    {
        // --- 1. Thông tin Header ---
        public string Greeting { get; set; } = "Good Morning, Alex! ☀️";
        public string StreakMessage { get; set; } = "Bạn đang giữ chuỗi 5 ngày liên tiếp 🔥";
        public int DailyProgressPercent { get; set; } = 65; // 65%
        public int Coins { get; set; } = 150;

        // --- 2. Thông tin Challenge (Thẻ màu cam) ---
        public string ChallengeName { get; set; } = "7 Days of Yoga";
        public string ChallengeProgressText { get; set; } = "Day 3 / 7 Complete";
        public int ChallengePercent { get; set; } = 42; // (3/7)*100

        // --- 3. Danh sách thói quen ---
        public ObservableCollection<HabitItemDisplay> Habits { get; set; }

        // --- 4. Commands ---
        public ICommand CompleteHabitCommand { get; }
        public ICommand IncrementCounterCommand { get; }
        public ICommand DecrementCounterCommand { get; }

        public TodayViewModel()
        {
            // Khởi tạo dữ liệu mẫu (Mock Data) giống hệt HTML em gửi
            Habits = new ObservableCollection<HabitItemDisplay>
            {
                new HabitItemDisplay
                {
                    Title = "Morning Run",
                    Subtitle = "07:00 AM • 5km Goal",
                    Icon = "🏃",
                    ColorHex = "#E3F2FD", // Xanh nhạt
                    IsCompleted = false
                },
                new HabitItemDisplay
                {
                    Title = "Hydration",
                    Subtitle = "Uống đủ 8 cốc nước",
                    Icon = "💧",
                    ColorHex = "#E0F7FA", // Cyan nhạt
                    IsCounterType = true,
                    CurrentCount = 2,
                    TargetCount = 8
                },
                new HabitItemDisplay
                {
                    Title = "Read 10 Pages",
                    Subtitle = "Completed at 8:30 AM",
                    Icon = "📖",
                    ColorHex = "#F3E5F5", // Tím nhạt
                    IsCompleted = true
                },
                new HabitItemDisplay
                {
                    Title = "Meditation",
                    Subtitle = "15 Minutes • Afternoon",
                    Icon = "🧘",
                    ColorHex = "#E8EAF6", // Indigo nhạt
                    IsCompleted = false
                }
            };

            // Logic xử lý khi click checkbox
            CompleteHabitCommand = new RelayCommand(obj =>
            {
                if (obj is HabitItemDisplay item)
                {
                    // Logic update xuống DB sẽ viết ở đây
                    // item.IsCompleted đã tự đổi do Binding
                    MessageBox.Show($"Đã check thói quen: {item.Title}");
                }
            });

            // Logic tăng giảm số lượng (nước uống)
            IncrementCounterCommand = new RelayCommand(obj =>
            {
                if (obj is HabitItemDisplay item && item.CurrentCount < item.TargetCount)
                    item.CurrentCount++;
            });

            DecrementCounterCommand = new RelayCommand(obj =>
            {
                if (obj is HabitItemDisplay item && item.CurrentCount > 0)
                    item.CurrentCount--;
            });
        }
    }
}