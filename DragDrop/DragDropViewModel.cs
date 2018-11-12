using Canvas.Helpers;
using CygNet.Data.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace Canvas.Controls.Example.DragDrop
{
    public class DragDropViewModel : CanvasControlViewModelBase, 
        ICanvasControlViewModel,
        ICanvasControlDropDestination
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
        public double DefaultHeight => 40;

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

        public DragDropViewModel()
        {
            ControlType = "DragDrop";
            ControlCategory = "Examples";
        }


        #region DRAG-DROP

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool CanDrop(object payload)
        {
            return payload is PointTag draggedItem;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void OnDrop(object payload)
        {
            if (payload is PointTag point)
            {
                MessageBox.Show($"{point.GetPointTag()} was dropped on this control!");
            }
        }

        #endregion
    }
}
