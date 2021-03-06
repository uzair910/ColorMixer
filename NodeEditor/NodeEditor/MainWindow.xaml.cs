using NodeEditor.Components;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;

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



        public Brush MixedColor
        {
            get { return (Brush)GetValue(MixedColorProperty); }
            set { SetValue(MixedColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MixedColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MixedColorProperty =
            DependencyProperty.Register("MixedColor", typeof(Brush), typeof(MainWindow), new PropertyMetadata(Brushes.Transparent));



        private void SetRandomNodeColor()
        {
            Brush result = Brushes.Transparent;
            Random rnd = new Random();
            Type brushesType = typeof(Brushes);
            PropertyInfo[] properties = brushesType.GetProperties();
            int random = rnd.Next(properties.Length);
            result = (Brush)properties[random].GetValue(null, null);
            SelectedColor = result;

            // Delete this
            //random = rnd.Next(properties.Length);
            //result = (Brush)properties[random].GetValue(null, null);
            //MixedColor = result;

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
            // cannot pass dependency property as reference
            AddNodeToCanvas(SelectedColor);
        }


        #endregion

        #region Canvas Functions

        UIElement dragObject = null;
        UIElement sourceNode = null;
        UIElement targetNode = null; // for linking
        Point sourceOffset;


        private void AddNodeToCanvas(Brush color)
        {
            ColorNode node = new ColorNode();
            node.Color = color;
            Canvas.SetLeft(node, 20);
            Canvas.SetTop(node, 20);
            node.PreviewMouseDown += Node_PreviewMouseDown;
            cvsBoard.Children.Add(node);
        }

        #region events
        private void AddMixedColorToCanvasBtnClicked(object sender, RoutedEventArgs e)
        {
            //cannot pass dependency property as reference
            AddNodeToCanvas(MixedColor);
        }


        private void Node_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            // If left ctrl is pressed, that means we need to connect the objects.
            if (Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                if (this.sourceNode == null)
                    this.sourceNode = sender as UIElement;
                else
                    this.targetNode = sender as UIElement;

                if(this.targetNode != null && this.sourceNode != null)
                {
                    // lets connect the lines

                    ColorNode s= sourceNode as ColorNode;
                    Brush color1 = (sourceNode as ColorNode).Color;
                    Brush color2 = (targetNode as ColorNode).Color;



                }


                // Mixer
            }
            else
            {
                this.dragObject = sender as UIElement;
                this.sourceOffset = e.GetPosition(this.cvsBoard);
                this.sourceOffset.X -= Canvas.GetLeft(this.dragObject);
                this.sourceOffset.Y -= Canvas.GetTop(this.dragObject);
                this.cvsBoard.CaptureMouse();
            }
        }
        private void cvsBoard_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (this.dragObject == null)
                return;
            var position = e.GetPosition(sender as IInputElement);
            Canvas.SetLeft(this.dragObject, position.X - this.sourceOffset.X);
            Canvas.SetTop(this.dragObject, position.Y - this.sourceOffset.Y);
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
