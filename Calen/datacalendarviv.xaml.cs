using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace Calen
{
    public partial class datacalendarviv : Page
    {
        public datacalendarviv()
        {
            InitializeComponent();
            DatePicker.SelectedDate = DateTime.Today;
            CreateUserControlsBasedOnDays();
        }

        private void nvpered_Click(object sender, RoutedEventArgs e)
        {
            ChangeMonth(1);
        }

        private void vnazad_Click(object sender, RoutedEventArgs e)
        {
            ChangeMonth(-1);
        }

        private void ChangeMonth(int months)
        {
            MyWrapPanel.Children.Clear();
            DatePicker.SelectedDate = DatePicker.SelectedDate.Value.AddMonths(months);
            CreateUserControlsBasedOnDays();
        }

        public void CreateUserControlsBasedOnDays()
        {
            DateTime selectedDate = DatePicker.SelectedDate.Value;
            int daysInMonth = DateTime.DaysInMonth(selectedDate.Year, selectedDate.Month);
            MyWrapPanel.Children.Clear();

            for (int i = 1; i <= daysInMonth; i++)
            {
                DateTime dateTime = new DateTime(selectedDate.Year, selectedDate.Month, i);
                var userControl = new Kartsiks
                {
                    DayBlock = { Text = i.ToString() },
                    sohrnDAteee = dateTime
                };

                AddItemImageToControl(userControl, dateTime);
                MyWrapPanel.Children.Add(userControl);
            }
        }

        private void AddItemImageToControl(Kartsiks control, DateTime date)
        {
            if (DayItems.days == null) return;

            var dailyItems = DayItems.days.FirstOrDefault(d => d.Day == date);
            if (dailyItems == null) return;

            var selectedItem = dailyItems.Items.FirstOrDefault(item => item.IsSelected);
            if (selectedItem != null)
            {
                BitmapImage image = new BitmapImage(new Uri(selectedItem.IconPath, UriKind.Relative));
                control.ItemImage.Source = image;
                control.ItemImage.Height = image.PixelHeight;
                control.ItemImage.Width = image.PixelWidth;
            }
        }

        private void nazad_MouseEnter(object sender, MouseEventArgs e)
        {
            Storyboard rotateAnimation = (Storyboard)FindResource("RotateAnimation");
            rotateAnimation.Begin(nazad, true);
        }

        private void nazad_MouseLeave(object sender, MouseEventArgs e)
        {
            Storyboard rotateAnimation = (Storyboard)FindResource("RotateAnimation");
            rotateAnimation.Stop(nazad);
        }

        private void Forward_MouseEnter(object sender, MouseEventArgs e)
        {
            Storyboard colorAnimation = (Storyboard)FindResource("ColorAnimation");
            colorAnimation.Begin(Forward, true);
        }

        private void Forward_MouseLeave(object sender, MouseEventArgs e)
        {
            Storyboard colorAnimation = (Storyboard)FindResource("ColorAnimation");
            colorAnimation.Stop(Forward);
        }
    }
}
