using System;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace NodeEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetRandomNodeColor();
        }

        private void SetRandomNodeColor()
        {
            Brush result = Brushes.Transparent;
            Random rnd = new Random();
            Type brushesType = typeof(Brushes);
            PropertyInfo[] properties = brushesType.GetProperties();
            int random = rnd.Next(properties.Length);
            result = (Brush)properties[random].GetValue(null, null);
            SelectedColor = result;
        }

        private void cp_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (colorPicker.SelectedColor.HasValue)
            {
                Color selectedColor = colorPicker.SelectedColor.Value;
                SelectedColor = new SolidColorBrush(selectedColor);
            }
        }



        public Brush SelectedColor
        {
            get { return (Brush)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register("SelectedColor", typeof(Brush), typeof(MainWindow), new PropertyMetadata(Brushes.Transparent));




    }
}
