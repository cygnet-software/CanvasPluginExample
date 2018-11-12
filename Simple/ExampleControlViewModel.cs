using System;
using System.Collections.Generic;
using System.ComponentModel; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Canvas.Helpers;  // added for CanvasControlViewModelBase, ICanvasControlViewModel

namespace Canvas.Controls.Example
{
    public class ExampleControlViewModel : CanvasControlViewModelBase, ICanvasControlViewModel
    {
        // we don't need to customize this field in our view model, but in order to hide it at runtime, we need to define
        // it here and set Browsable to false
        [Browsable(false)]
        public string TypeID
        {
            get;
            set;
        }

        #region ICanvasControlViewModel Members

        [XmlIgnore]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public double DefaultWidth => 150;

        [XmlIgnore]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public double DefaultHeight => 22;

        public event EventHandler Initialize;
        public event EventHandler Close;

        /// <summary>
        /// Invoke the Close event
        /// </summary>
        public void FireClose()
        {
            FireEvent(Close);
        }

        /// <summary>
        /// Invoke the Initialize event
        /// </summary>
        public void FireInitialize()
        {
            FireEvent(Initialize);
        }

        public void StartRunMode()
        {
            // nothing to do here yet, because this control doesn't do much
            IsInRunMode = true;
        }

        #endregion

        public ExampleControlViewModel()
        {
            ControlType = "Basic Control";
            ControlCategory = "Examples";
        }
    }
}
