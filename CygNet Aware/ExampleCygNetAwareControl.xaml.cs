using System.Windows.Controls;
using Canvas.Helpers;

namespace Canvas.Controls.Example
{
    /// <summary>
    /// Interaction logic for ExampleCygNetAwareControl.xaml
    /// </summary>
    public partial class ExampleCygNetAwareControl : UserControl, ICanvasControlView
    {
        public ExampleCygNetAwareControl()
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
