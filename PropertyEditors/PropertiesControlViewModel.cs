using Canvas.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Serialization;

namespace Canvas.Controls.Example.PropertyEditors
{
    public class MyClass
    {
        public string Bob { get; set; }
        public string Tom { get; set; }
        public bool Lou { get; set; }
    }

    public class PropertiesControlViewModel : CanvasControlViewModelBase,
        ICanvasControlViewModel, 
        ICanvasControlCustomPropertyEditorUser, 
        ICanvasControlCustomPropertyDataTemplateUser, 
        ICanvasCustomDropDownPropertyEditorUser, 
        INotifyPropertyChanged
    {
        [XmlIgnore]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public double DefaultWidth => 150;

        [XmlIgnore]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public double DefaultHeight => 200;

        [Browsable(false)]
        public string TypeID
        {
            get;
            set;
        }

        private MyClass _class;
        [CanvasPropertyEditorType(Helpers.Shared.CanvasPropertyEditorType.CustomDropDown)]
        public MyClass Class
        {
            get { return _class; }
            set
            {
                _class = value;
                HasChanged();
            }
        }

        private bool _boolean;
        public bool Boolean
        {
            get { return _boolean; }
            set
            {
                _boolean = value;
                HasChanged();
            }
        }

        private string _stringProperty;
        public string StringProperty
        {
            get { return _stringProperty; }
            set
            {
                _stringProperty = value;
                HasChanged();
            }
        }

        public enum ThisIsAnEnum
        {
            [Description("Description 1")]
            Option1 = 0,
            [Description("Description 2")]
            Option2,
            [Description("Description 3")]
            Option3,
            [Description("Description 4")]
            Option4
        }

        private ThisIsAnEnum _enum;
        public ThisIsAnEnum Enumeration
        {
            get { return _enum; }
            set
            {
                _enum = value;
                HasChanged();
            }
        }

        private string _color;
        [CanvasPropertyEditorType(Helpers.Shared.CanvasPropertyEditorType.ColorPicker)]
        public string Color
        {
            get { return _color; }
            set
            {
                _color = value;
                HasChanged();
            }
        }

        private string _service;
        [CanvasPropertyEditorType(Helpers.Shared.CanvasPropertyEditorType.CygNetSiteService)]
        public string Service
        {
            get { return _service; }
            set
            {
                _service = value;
                HasChanged();
            }
        }

        private string _facility;
        [CanvasPropertyEditorType(Helpers.Shared.CanvasPropertyEditorType.CygNetFacility)]
        public string Facility
        {
            get { return _facility; }
            set
            {
                _facility = value;
                HasChanged();
            }
        }

        private string _udc;
        [CanvasPropertyEditorType(Helpers.Shared.CanvasPropertyEditorType.CygNetUDC)]
        public string UDC
        {
            get { return _udc; }
            set
            {
                _udc = value;
                HasChanged();
            }
        }

        private string _customDialog;
        [CanvasPropertyEditorType(Helpers.Shared.CanvasPropertyEditorType.CustomDialog)]
        public string CustomDialog
        {
            get { return _customDialog; }
            set
            {
                _customDialog = value;
                HasChanged();
            }
        }

        private string _customTemplate;
        [CanvasPropertyEditorType(Helpers.Shared.CanvasPropertyEditorType.CustomDataTemplate)]
        public string CustomTemplate
        {
            get { return _customTemplate; }
            set
            {
                _customTemplate = value;
                HasChanged();
            }
        }

        #region ICanvasControlViewModel Members

        public event EventHandler Initialize;
        public event EventHandler Close;

        /// <summary>
        /// Invoke the Close event
        /// </summary>
        public void FireClose()
        {
            if (Close != null)
            {
                Close.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Invoke the Initialize event
        /// </summary>
        public void FireInitialize()
        {
            if (Initialize != null)
            {
                Initialize.Invoke(this, EventArgs.Empty);
            }
        }

        public void StartRunMode()
        {
            // nothing to do here yet, because this control doesn't do much
        }

        #endregion

        /// <summary>
        /// Called for properties with the editor type of CanvasPropertyEditorType.CustomDialog. Use to
        /// invoke a custom dialog for property configuration.
        /// Whatever you return will be displayed in the UI
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public string InvokeCustomPropertyEditorDialog(string propertyName)
        {
            string xml = string.Empty;
            if (propertyName == "CustomDialog")
            {
                CustomDialog dlg = new CustomDialog();

                dlg.ShowDialog();

                xml = dlg.GetStuff();
            }

            return xml;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void GetUserControlNameForDropDown(string propertyName, out string controlNamespace, out string controlAssembly, out string controlName, out string dataContext)
        {
            controlNamespace = string.Empty;
            controlAssembly = string.Empty;
            controlName = string.Empty;
            dataContext = string.Empty;

            if (propertyName == "Class")
            {
                controlNamespace = "Canvas.Controls.Example.PropertyEditors";
                controlAssembly = "Canvas.Controls.Example";
                controlName = "DropDownUserControl";
                dataContext = string.Empty;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public int GetDesiredControlWidth()
        {
            return 0;
        }

        #region DATA TEMPLATE

        private static DataTemplate GetCustomDataTemplate(string property)
        {
            string xaml =
                @"<DataTemplate
                    xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                    xmlns:telerik=""http://schemas.telerik.com/2008/xaml/presentation"">
                        <StackPanel Orientation=""Horizontal"">
                            <TextBlock HorizontalAlignment=""Stretch"" Text=""Custom Template: ""/>
                            <TextBox HorizontalAlignment=""Stretch"" Text=""{Binding ControlViewModel." + property + @", Mode=TwoWay}""/>
                        </StackPanel>
                  </DataTemplate>";

            return GetDataTemplateFromString(xaml);
        }

        private static DataTemplate GetDataTemplateFromString(string xaml)
        {
            StringReader stringReader = new StringReader(xaml);

            XmlReader xmlReader = XmlReader.Create(stringReader);

            return XamlReader.Load(xmlReader) as DataTemplate;
        }

        public DataTemplate GetCustomPropertyEditorDataTemplate(string propertyName)
        {
            if (propertyName == "CustomTemplate")
            {
                return GetCustomDataTemplate(propertyName);
            }

            return null;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string GetDisplayText(string propertyName)
        {
            return string.Empty;
        }

        #endregion

        public PropertiesControlViewModel()
        {
            ControlType = "PropertyEditors";
            ControlCategory = "Examples";

            Color = "Black";

            _class = new MyClass() { Bob = "Bob the string", Tom = "Tom the string", Lou = false };
        }
    }
}
