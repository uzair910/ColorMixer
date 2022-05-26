using NodeEditor.Components;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
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

        #region Toolbar controls and functions
        public Brush SelectedColor
        {
            get { return (Brush)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register("SelectedColor", typeof(Brush), typeof(MainWindow), new PropertyMetadata(Brushes.Transparent));

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

        private void colorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (colorPicker.SelectedColor.HasValue)
            {
                Color selectedColor = colorPicker.SelectedColor.Value;
                SelectedColor = new SolidColorBrush(selectedColor);
            }
        }

        private void AddToCanvasBtnClicked(object sender, RoutedEventArgs e)
        {
            AddNodeToCanvas();
        }


        #endregion

        #region Canvas Functions

        UIElement dragObject = null;
        Point offset;

        private void AddNodeToCanvas()
        {
            ColorNode node = new ColorNode();
            node.Color = SelectedColor;
            Canvas.SetLeft(node, 20);
            Canvas.SetTop(node, 20);
            node.PreviewMouseDown += Node_PreviewMouseDown;
            cvsBoard.Children.Add(node);
        }
        
        #region events
        private void Node_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.dragObject = sender as UIElement;
            this.offset = e.GetPosition(this.cvsBoard);
            this.offset.X -= Canvas.GetLeft(this.dragObject);
            this.offset.Y -= Canvas.GetTop(this.dragObject);
            this.cvsBoard.CaptureMouse();
        }
        private void cvsBoard_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (this.dragObject == null)
                return;
            var position = e.GetPosition(sender as IInputElement);
            Canvas.SetLeft(this.dragObject, position.X - this.offset.X);
            Canvas.SetTop(this.dragObject, position.Y - this.offset.Y);
        }

        private void cvsBoard_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.dragObject = null;
            this.cvsBoard.ReleaseMouseCapture();
        }

        #endregion

        #endregion

    }
}
