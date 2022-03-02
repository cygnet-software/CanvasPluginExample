using Canvas.Helpers;
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

namespace Canvas.Controls.Example.PropertyEditors
{
    /// <summary>
    /// Interaction logic for PropertiesControl.xaml
    /// </summary>
    public partial class PropertiesControl : UserControl, ICanvasControlView
    {
        public PropertiesControl()
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
