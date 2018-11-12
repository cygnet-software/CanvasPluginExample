using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Canvas.Helpers;

namespace Canvas.Controls.Example
{
    public class ExampleControlWithEventsViewModel : CanvasControlViewModelBase, ICanvasControlViewModel
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

        public void FireClose()
        {
            FireEvent(Close);
        }

        public void FireInitialize()
        {
            FireEvent(Initialize);
        }

        public void StartRunMode()
        {
            IsInRunMode = true;
        }

        #endregion

        private string _hyperlinkText;
        [Browsable(true)]
        [Display(Name = "Hyperlink Text", Description = "The text that will appear on the hyperlink", GroupName = "Hyperlink Properties")]
        public string HyperlinkText
        {
            get { return _hyperlinkText; }
            set
            {
                _hyperlinkText = value;
                HasChanged();
            }
        }

        /// <summary>
        /// A custom event that can be referenced from a screen's script.
        /// </summary>
        [Browsable(true)]
        public event EventHandler HyperlinkClick;

        /// <summary>
        /// Function to invoke our custom event. This will be called from the code-behind our View.
        /// See ExampleControlWithEvents.xaml.cs
        /// </summary>
        public void FireHyperlinkClick()
        {
            // use FireEvent() from CanvasControlViewModelBase to ensure property error handling if the screen's script
            // throws an unhandled exception
            FireEvent(HyperlinkClick);
        }

        #region CONSTRUCTOR

        public ExampleControlWithEventsViewModel()
        {
            ControlType = "Events";
            ControlCategory = "Examples";

            HyperlinkText = "Hyperlink";
        }

        #endregion
    }
}
