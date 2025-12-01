using System.IO;
using System.Windows;
using System.Windows.Input;
using LinguaBender.Data;
using LinguaBender.Models;
using LinguaBender.Services;
using LinguaBender.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace LinguaBender.Views
{
    public partial class StudentsView : Window
    {
        private Cursor _customCursor;
        private Cursor _customPointerCursor;
        private string _userRole;
        private readonly User _currentUser;


        public static readonly RoutedEvent TunnelEvent = EventManager.RegisterRoutedEvent(
            "TunnelEvent", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(StudentsView));
                                                  //определяем тип обработчика (метод, который будет реагировать на событие) 

        public static readonly RoutedEvent BubbleEvent = EventManager.RegisterRoutedEvent(
            "BubbleEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(StudentsView));

        public static readonly RoutedEvent DirectEvent = EventManager.RegisterRoutedEvent(
            "DirectEvent", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(StudentsView));

        public StudentsView(User currentUser)
        {
            InitializeComponent();

            AddHandler(TunnelEvent, new RoutedEventHandler(OnTunnelEvent), true);  // Window             //подписка на событие
            DirectButton.AddHandler(DirectEvent, new RoutedEventHandler(OnDirectEvent));       // при возникновении события вызывается OnDirectEvent
            MyRoutedStack.AddHandler(BubbleEvent, new RoutedEventHandler(OnBubbleEvent), true);

            LoadCustomCursors();
            _currentUser = currentUser;
            _userRole = currentUser.Role.RoleName;
            ConfigureAccess();
        }
        private void ConfigureAccess()
        {
            if (_userRole == "user")
            {
                StudentEdit.IsEnabled = false;
                CourseEdit.IsEnabled = false;
            }
        }
        private void LoadCustomCursors()
        {
            try
            {
                string cursorPath = @"D:\УЧЁБА\2 курс\ООП\2\Лабораторные\LinguaBender\cursor.cur";
                string pointerPath = @"D:\УЧЁБА\2 курс\ООП\2\Лабораторные\LinguaBender\cursorPointer.cur";

                if (File.Exists(cursorPath))
                {
                    _customCursor = new Cursor(cursorPath);
                }

                if (File.Exists(pointerPath))
                {
                    _customPointerCursor = new Cursor(pointerPath);
                }

                if (_customCursor != null)
                {
                    this.Cursor = _customCursor;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки курсоров: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            if (_customPointerCursor != null)
            {
                this.Cursor = _customPointerCursor;
            }
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            if (_customCursor != null)
            {
                this.Cursor = _customCursor;
            }
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            var homeWindow = new MainWindow(_currentUser);
            homeWindow.Show();
            this.Close();
        }

        private void CoursesView_Click(object sender, RoutedEventArgs e)
        {
            var serviceProvider = ((App)Application.Current)._serviceProvider;

            //  Вот здесь — получаем ICourseService из контейнера
            var courseService = serviceProvider.GetRequiredService<ICourseService>();
            var viewModel = new CoursesViewModel(courseService);

            CoursesView coursesView = new CoursesView(_currentUser)
            {
                DataContext = viewModel
            };
            coursesView.Show();
            this.Close();
        }

        private void CourseEdit_Click(object sender, RoutedEventArgs e)
        {
            var serviceProvider = ((App)Application.Current)._serviceProvider;

            //  Вот здесь — получаем ICourseService из контейнера
            var courseService = serviceProvider.GetRequiredService<ICourseService>();
            var viewModel = new CoursesViewModel(courseService); //модель представления viewModel

            CoursesEdit coursesEdit = new CoursesEdit(_currentUser, serviceProvider)
            {
                DataContext = viewModel
            };
            coursesEdit.Show();
            this.Close();
        }

        private void StudentEdit_Click(object sender, RoutedEventArgs e)
        {
            StudentsEdit studentsEdit = new StudentsEdit(_currentUser);    
            studentsEdit.Show();
            this.Close();
        }


        private void TriggerButton_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Кнопка нажата! Это реакция на событие.", "Триггер", MessageBoxButton.OK, MessageBoxImage.Information);

            // Лог в файл
            File.AppendAllText("log.txt", $"{DateTime.Now}: TriggerButton was clicked.\n");
        }

        private void TriggerButton2_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы нажали на кнопку с DataTrigger!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ClearText_Click(object sender, RoutedEventArgs e)
        {
            TriggerButton2.Content = "";
        }

        //RoutedEvent   RoutedEvent   RoutedEvent
        private void RaiseTunnel(object sender, RoutedEventArgs e)
        {
            (sender as Button)?.RaiseEvent(new RoutedEventArgs(TunnelEvent));    //запускаем событие
        }

        private void RaiseDirect(object sender, RoutedEventArgs e)
        {
            (sender as Button)?.RaiseEvent(new RoutedEventArgs(DirectEvent));
        }

        private void RaiseBubble(object sender, RoutedEventArgs e)
        {
            (sender as Button)?.RaiseEvent(new RoutedEventArgs(BubbleEvent));
        }

         //обработчики
        private void OnTunnelEvent(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Tunnel Event: обработан в окне StudentsView и дальше спустился на кнопку");
        }

        private void OnBubbleEvent(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Bubble Event: поднялся от кнопки до StackPanel");
        }

        private void OnDirectEvent(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Direct Event: обработан только на кнопке");
        }


    }
}
