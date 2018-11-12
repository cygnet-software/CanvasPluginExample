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

namespace Canvas.Controls.Example.DragDrop
{
    /// <summary>
    /// Interaction logic for DragDrop.xaml
    /// </summary>
    public partial class DragDrop : UserControl, ICanvasControlView
    {
        public DragDrop()
        {
            InitializeComponent();
        }

        public ICanvasControlViewModel ViewModel
        {
            get { return (ICanvasControlViewModel)DataContext; }
            set { DataContext = value; }
        }
    }
}
