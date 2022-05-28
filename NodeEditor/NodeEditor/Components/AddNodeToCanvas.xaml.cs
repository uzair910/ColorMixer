using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NodeEditor.Components
{
    /// <summary>
    /// Interaction logic for AddNodeToCanvas.xaml
    /// </summary>
    public partial class AddNodeToCanvas : UserControl
    {

        public Brush ColorPicker
        {
            get { return (Brush)GetValue(ColorPickerProperty); }
            set { SetValue(ColorPickerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorPickerProperty =
            DependencyProperty.Register("ColorPicker", typeof(Brush), typeof(AddNodeToCanvas), new PropertyMetadata(Brushes.Transparent));

        public String HeaderText
        {
            get { return (String)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderTextProperty =
            DependencyProperty.Register("HeaderText", typeof(String), typeof(AddNodeToCanvas), new PropertyMetadata("Title"));


        public AddNodeToCanvas()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler AddToCanvasBtnClicked
        {
            add { AddToCanvasBtn.AddHandler(ButtonBase.ClickEvent, value); }
            remove { AddToCanvasBtn.AddHandler(ButtonBase.ClickEvent, value); }
        }
    }
}
