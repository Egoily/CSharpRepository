using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ExtendedPictureBoxLib.Design
{
    /// <summary>
    /// Control inheriting from <see cref="CheckedListBox"/> to show and select values from a Flags enumeration.
    /// </summary>
    public partial class FlagCheckedListBox : CheckedListBox
    {
        #region (* Fields *)

        private bool _isUpdatingCheckStates = false;
        private Type _enumType;
        private Enum _enumValue;

        #endregion

        #region (* Constructors *)

        /// <summary>
        /// Creates a new instance.
        /// </summary>

        public FlagCheckedListBox()
        {
            InitializeComponent();
            this.CheckOnClick = true;
        }

        public FlagCheckedListBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        #endregion

        #region (* Overridden from CheckedListBox *)

        /// <summary>
        /// Handles the <see cref="CheckedListBox.ItemCheck"/> event.
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected override void OnItemCheck(ItemCheckEventArgs e)
        {
            base.OnItemCheck(e);

            if (_isUpdatingCheckStates)
                return;

            // Get the checked/unchecked item
            FlagCheckedListBoxItem item = Items[e.Index] as FlagCheckedListBoxItem;
            // Update other items
            UpdateCheckedItems(item, e.NewValue);
        }

        #endregion

        #region (* Public interface *)

        /// <summary>
        /// Adds a new item to the list.
        /// </summary>
        /// <param name="value">Value of the new item.</param>
        /// <param name="caption">Caption of the new item.</param>
        /// <returns>The new item.</returns>
        public FlagCheckedListBoxItem Add(int value, string caption)
        {
            FlagCheckedListBoxItem item = new FlagCheckedListBoxItem(value, caption);
            Items.Add(item);
            return item;
        }

        /// <summary>
        /// Adds an item to the list.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The item.</returns>
        public FlagCheckedListBoxItem Add(FlagCheckedListBoxItem item)
        {
            Items.Add(item);
            return item;
        }

        /// <summary>
        /// Gets the current bit value corresponding to all checked items
        /// </summary>
        /// <returns></returns>
        public int GetCurrentValue()
        {
            int sum = 0;

            for (int i = 0; i < Items.Count; i++)
            {
                FlagCheckedListBoxItem item = Items[i] as FlagCheckedListBoxItem;

                if (GetItemChecked(i))
                    sum |= item.Value;
            }

            return sum;
        }

        /// <summary>
        /// Gets or sets the current enumeration value.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Enum EnumValue
        {
            get
            {
                object e = Enum.ToObject(_enumType, GetCurrentValue());
                return (Enum)e;
            }
            set
            {
                Items.Clear();
                _enumValue = value; // Store the current enum value
                _enumType = value.GetType(); // Store enum type
                FillEnumMembers(); // Add items for enum members
                ApplyEnumValue(); // Check/uncheck items depending on enum value
            }
        }

        #endregion

        #region (* Privates *)

        private void UpdateCheckedItems(int value)
        {
            _isUpdatingCheckStates = true;

            // Iterate over all items
            for (int i = 0; i < Items.Count; i++)
            {
                FlagCheckedListBoxItem item = Items[i] as FlagCheckedListBoxItem;

                if (item.Value == 0)
                {
                    SetItemChecked(i, value == 0);
                }
                else
                {
                    // If the bit for the current item is on in the bitvalue, check it
                    if ((item.Value & value) == item.Value && item.Value != 0)
                        SetItemChecked(i, true);
                    else // Otherwise uncheck it
                        SetItemChecked(i, false);
                }
            }

            _isUpdatingCheckStates = false;
        }

        private void UpdateCheckedItems(FlagCheckedListBoxItem composite, CheckState cs)
        {
            // If the value of the item is 0, call directly.
            if (composite.Value == 0)
                UpdateCheckedItems(0);

            // Get the total value of all checked items
            int sum = 0;
            for (int i = 0; i < Items.Count; i++)
            {
                FlagCheckedListBoxItem item = Items[i] as FlagCheckedListBoxItem;

                // If item is checked, add its value to the sum.
                if (GetItemChecked(i))
                    sum |= item.Value;
            }

            // If the item has been unchecked, remove its bits from the sum
            if (cs == CheckState.Unchecked)
                sum = sum & (~composite.Value);
            else // If the item has been checked, combine its bits with the sum
                sum |= composite.Value;

            // Update all items in the checklistbox based on the final bit value
            UpdateCheckedItems(sum);
        }

        // Adds items to the checklistbox based on the members of the enum
        private void FillEnumMembers()
        {
            foreach (string name in Enum.GetNames(_enumType))
            {
                object val = Enum.Parse(_enumType, name);
                int intVal = (int)Convert.ChangeType(val, typeof(int));

                Add(intVal, name);
            }
        }

        private void ApplyEnumValue()
        {
            int intVal = (int)Convert.ChangeType(_enumValue, typeof(int));
            UpdateCheckedItems(intVal);
        }

        #endregion
    }
}