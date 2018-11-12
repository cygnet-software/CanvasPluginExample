using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Canvas.Helpers;

namespace Canvas.Controls.Example
{
    /// <summary>
    /// Interaction logic for ExampleControlWithEvents.xaml
    /// </summary>
    public partial class ExampleControlWithEvents : UserControl, ICanvasControlView
    {
        public ExampleControlWithEvents()
        {
            InitializeComponent();
        }

        public ICanvasControlViewModel ViewModel
        {
            get { return (ICanvasControlViewModel)DataContext; }
            set { DataContext = value; }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            ((ExampleControlWithEventsViewModel)ViewModel).FireHyperlinkClick();
        }
    }
}
