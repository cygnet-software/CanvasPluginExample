using System.Windows.Controls;
using Canvas.Helpers;

namespace Canvas.Controls.Example
{
    /// <summary>
    /// Interaction logic for ExampleControl.xaml
    /// </summary>
    public partial class ExampleControl : UserControl, ICanvasControlView
    {
        public ExampleControl()
        {
            InitializeComponent();
        }

        // implement ICanvasControlView by referencing its ViewModel
        public ICanvasControlViewModel ViewModel
        {
            get { return (ICanvasControlViewModel)DataContext; }
            set { DataContext = value; }
        }
    }
}
