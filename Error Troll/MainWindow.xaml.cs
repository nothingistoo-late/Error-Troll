using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Media;

namespace ErrorTroll
{
    public partial class MainWindow : Window
    {
        private int errorCount = 0;
        private const int maxErrors = 50; // Số lỗi tối đa (có thể tăng để che kín màn hình)
        private Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            ShowError();
        }

        private void ShowError()
        {
            MessageBoxResult result = MessageBox.Show("System failure detected!", "Critical Error",
                MessageBoxButton.OKCancel, MessageBoxImage.Error, MessageBoxResult.OK,
                MessageBoxOptions.DefaultDesktopOnly);

            if (result == MessageBoxResult.Cancel)
            {
                CreateErrorStorm();
            }
        }

        private async void CreateErrorStorm()
        {
            SystemSounds.Hand.Play(); // Phát âm thanh cảnh báo

            while (errorCount < maxErrors)
            {
                errorCount++;

                // Lấy vị trí random trên màn hình
                int screenWidth = (int)SystemParameters.PrimaryScreenWidth;
                int screenHeight = (int)SystemParameters.PrimaryScreenHeight;

                int posX = random.Next(0, screenWidth - 600);
                int posY = random.Next(0, screenHeight - 400);

                // Tạo cửa sổ lỗi
                Window errorWindow = new Window
                {
                    Title = "Critical Error!",
                    Width = 600,
                    Height = 400,
                    WindowStartupLocation = WindowStartupLocation.Manual,
                    Left = posX,
                    Top = posY,
                    Background = Brushes.Black,
                    Topmost = true, // Luôn nằm trên cùng
                    ResizeMode = ResizeMode.NoResize, // Không thay đổi kích thước
                    WindowStyle = WindowStyle.None // Ẩn nút đóng
                };

                // Thêm nội dung lỗi
                Grid grid = new Grid();
                errorWindow.Content = grid;

                TextBlock text = new TextBlock
                {
                    Text = "Your system is under attack!\n\nError Code: 0x800F\nPress ALT+F4 to escape!",
                    FontSize = 28,
                    Foreground = Brushes.Red,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                grid.Children.Add(text);

                errorWindow.Show();

                await Task.Delay(500); // Xuất hiện từ từ mỗi 0.5s
            }
        }
    }
}
