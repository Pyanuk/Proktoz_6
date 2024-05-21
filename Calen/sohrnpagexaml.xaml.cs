using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Calen
{
    public partial class sohrnpagexaml : Page
    {
        private MediaPlayer mediaPlayer;
        private DayItems dayItems = new DayItems
        {
            Items = new List<Item>
            {
                new Item { Name = "Вдумчивая", IconPath = "..\\..\\Resources\\IMG1.png", MusicPath = "..\\..\\Resources\\tr1.m4a" },
                new Item { Name = "Пьяница", IconPath = "..\\..\\Resources\\IMG2.png", MusicPath = "..\\..\\Resources\\tr2.mp3 " },
                new Item { Name = "Я фотограф", IconPath = "..\\..\\Resources\\IMG3.png", MusicPath = "..\\..\\Resources\\tr3.mp3" },
                new Item { Name = "Дед-инсайд", IconPath = "..\\..\\Resources\\IMG4.png", MusicPath = "..\\..\\Resources\\tr5.mp3" },
                new Item { Name = "Спящая", IconPath = "..\\..\\Resources\\IMG5.png", MusicPath = "..\\..\\Resources\\tr4.m4a" }
            }
        };

        private bool elementDate = true;
        private int Index = 0;

        public sohrnpagexaml(DateTime SelectedDate)
        {
            InitializeComponent();
            DateTextBlock.Text = SelectedDate.ToLongDateString();
            dayItems.Day = SelectedDate;

            foreach (var day in DayItems.days)
            {
                if (day.Day == SelectedDate)
                {
                    dayItems = day;
                    elementDate = false;
                    Index = DayItems.days.IndexOf(day);
                    break;
                }
            }

            GenerateItems();
        }

        private void GenerateItems()
        {
            ItemsStackPanel.Children.Clear();

            var title = new TextBlock
            {
                Text = "Какая ты сегодня?",
                TextAlignment = TextAlignment.Center,
                FontSize = 25
            };

            ItemsStackPanel.Children.Add(title);

            var wrapPanel = new WrapPanel
            {
                Orientation = Orientation.Horizontal
            };

            foreach (var item in dayItems.Items)
            {
                var itemPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(5)
                };

                var itemCheckBox = new CheckBox
                {
                    IsChecked = item.IsSelected,
                    Margin = new Thickness(30, 5, 5, 5),
                    Tag = item
                };
                itemCheckBox.Checked += CheckBox_Checked;
                itemCheckBox.Unchecked += CheckBox_Unchecked;

                var itemImage = new Image
                {
                    Source = new BitmapImage(new Uri(item.IconPath, UriKind.Relative)),
                    Margin = new Thickness(5),
                    Stretch = Stretch.Fill,
                    MaxWidth = 100,
                    MaxHeight = 100
                };

                var itemName = new TextBlock
                {
                    Text = item.Name,
                    FontSize = 16
                };

                itemPanel.Children.Add(itemCheckBox);
                itemPanel.Children.Add(itemImage);
                itemPanel.Children.Add(itemName);

                wrapPanel.Children.Add(itemPanel);
            }

            ItemsStackPanel.Children.Add(wrapPanel);
        }

        private void nazad_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.Content = mainWindow.datacalendarviv;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var item = (sender as CheckBox).Tag as Item;
            dayItems.Items[dayItems.Items.IndexOf(item)].IsSelected = true;

            if (!string.IsNullOrEmpty(item.MusicPath))
            {
                PlayMusic(item.MusicPath);
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var item = (sender as CheckBox).Tag as Item;
            dayItems.Items[dayItems.Items.IndexOf(item)].IsSelected = false;

            if (mediaPlayer != null)
            {
                mediaPlayer.Stop();
            }

        }

        private void PlayMusic(string audioPath)
        {
            mediaPlayer = new MediaPlayer();
            mediaPlayer.Open(new Uri(audioPath, UriKind.RelativeOrAbsolute));
            mediaPlayer.Play();
        }

        private void sohrn_Click(object sender, RoutedEventArgs e)
        {
            bool isSelected = dayItems.Items.Any(item => item.IsSelected);

            if (isSelected)
            {
                if (DayItems.days == null)
                {
                    DayItems.days = new List<DayItems>();
                }

                if (elementDate)
                {
                    DayItems.days.Add(dayItems);
                }
                else
                {
                    DayItems.days[Index] = dayItems;
                }
            }

     
            Storyboard moveUpAnimation = (Storyboard)FindResource("MoveUpAnimation");
            moveUpAnimation.Completed += MoveUpAnimation_Completed;
            moveUpAnimation.Begin(Save);

   
            Save.IsEnabled = false;
        }

        private void MoveUpAnimation_Completed(object sender, EventArgs e)
        {
           
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
            timer.Tick -= Timer_Tick;

           
            SaveData();
        }

        private void SaveData()
        {
            
            MessageBox.Show("Данные сохранены.");
            Save.IsEnabled = true; 

            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.Content = mainWindow.datacalendarviv;
        }
    }
}
