using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Canvas.Data;
using Canvas.Helpers;
using System.Xml.Serialization;
using CygNet.Data.Core;
using CygNet.API.Cache;

namespace Canvas.Controls.Example
{
    public class ExampleCygNetAwareControlViewModel : CanvasCygNetDataControlBase, ICanvasControlViewModel, IPointCacheSubscriber
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

            Points.Clear();

            try
            {
                // determine which properties/values you want for the point
                PropertyIdList propertyList = new PropertyIdList
                {
                    CygNetCoreProperties.CygNetProperty.Value,
                    CygNetCoreProperties.CygNetProperty.FacilityDescription,
                };

                var pointTag = new PointTag(CygNetPointTag);
                Points.Add(new CoreCacheTag(pointTag), propertyList);
            }
            catch (Exception)
            {
                CurrentValue = "Invalid Point Tag";
            }
        }

        #endregion

        #region IPointCacheSubscriber Members

        // mark this as non-browsable so it doesn't show up in the Canvas property pane
        [XmlIgnore]
        [Browsable(false)]
        public PointSubscriptionList Points
        {
            get;
            set;
        }

        #endregion

        /// <summary>
        /// Use this property to configure which point we want to add to the point cache
        /// Note: in order to retrieve facility-based properties, you'll need to use a point
        /// tag format that explicitly includes the facility, such as SITE.SERVICE::FACILITY.UDC
        /// </summary>
        private string _cygNetPointTag;
        public string CygNetPointTag
        {
            get { return _cygNetPointTag; }
            set
            {
                _cygNetPointTag = value;
                HasChanged();
            }
        }

        /// <summary>
        /// We'll store the value of the configured point in this property.
        /// We don't want the user the change this directly, so we marked it as private
        /// by using Browsable(false)
        /// </summary>
        private string _currentValue = "[Value goes here]";
        [Browsable(false)]
        public string CurrentValue
        {
            get { return _currentValue; }
            set
            {
                _currentValue = value;
                HasChanged();
            }
        }

        private string _description = "[Description goes here]";
        [Browsable(false)]
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                HasChanged();
            }
        }

        #region CONSTRUCTOR

        public ExampleCygNetAwareControlViewModel()
        {
            ControlType = "CygNet Aware";
            ControlCategory = "Examples";

            // instantiate our Points list and connect its update event handler
            Points = new PointSubscriptionList
            {
                OnUpdate = OnPointUpdateHandler
            };
        }

        #endregion

        #region EVENT HANDLERS

        /// <summary>
        /// Called when a point we've subscribed to is updated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPointUpdateHandler(object sender, CoreCacheUpdateEventArgs e)
        {
            // This assumes that the first (and perhaps only) item in the subscription list
            // is the one that we want
            if (IsInRunMode && e.PropertyDataBlockList.Count > 0)
            {
                if (e.PropertyDataBlockList[0].PropertyId == CygNetCoreProperties.CygNetProperty.Value)
                {
                    CurrentValue = e.PropertyDataBlockList[0].StringValue;
                }
                if (e.PropertyDataBlockList[0].PropertyId == CygNetCoreProperties.CygNetProperty.FacilityDescription)
                {
                    Description = e.PropertyDataBlockList[0].StringValue;
                }
            }
        }

        #endregion
    }
}
