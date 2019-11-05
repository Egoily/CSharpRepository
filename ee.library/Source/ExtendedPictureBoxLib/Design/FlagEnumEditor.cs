using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ExtendedPictureBoxLib.Design
{
    /// <summary>
    /// Controls the design time editor for a flags enumerations.
    /// </summary>
    public class FlagEnumUIEditor : UITypeEditor
    {
        #region Fields

        private FlagCheckedListBox _listBox;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public FlagEnumUIEditor()
        {
            _listBox = new FlagCheckedListBox();
            _listBox.BorderStyle = BorderStyle.None;
        }

        #endregion

        #region Overridden from UITypeEditor

        /// <summary>
        /// Edits a value regarding a given service provider under a specified context.
        /// </summary>
        /// <param name="context">Context informations.</param>
        /// <param name="provider">Service provider.</param>
        /// <param name="value">Value to be edited.</param>
        /// <returns>The edited value.</returns>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context != null && context.Instance != null && provider != null)
            {
                IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (edSvc != null)
                {
                    Enum e = (Enum)Convert.ChangeType(value, context.PropertyDescriptor.PropertyType);
                    _listBox.EnumValue = e;
                    edSvc.DropDownControl(_listBox);
                    return _listBox.EnumValue;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the editor style.
        /// </summary>
        /// <param name="context">Context informations.</param>
        /// <returns></returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        #endregion
    }
}