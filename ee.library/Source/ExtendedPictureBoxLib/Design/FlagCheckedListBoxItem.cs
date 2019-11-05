namespace ExtendedPictureBoxLib.Design
{
    /// <summary>
    /// Represents one item within a <see cref="FlagCheckedListBox"/>.
    /// </summary>
    public class FlagCheckedListBoxItem
    {
        #region Fields

        private int _value;
        private string _caption;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="value"><see cref="Value"/> of the new instance.</param>
        /// <param name="caption"><see cref="Caption"/> of the new instance</param>
        public FlagCheckedListBoxItem(int value, string caption)
        {
            _value = value;
            _caption = caption;
        }

        #endregion

        #region Public interface

        /// <summary>
        /// Return the enumeration value this item represents.
        /// </summary>
        public int Value
        {
            get { return _value; }
        }

        /// <summary>
        /// Returns the caption to be shown for this item.
        /// </summary>
        public string Caption
        {
            get { return _caption; }
        }

        /// <summary>
        /// Gets whether the value corresponds to a single bit being set.
        /// </summary>
        public bool IsFlag
        {
            get { return ((_value & (_value - 1)) == 0); }
        }

        /// <summary>
        /// Gets whether true if this value is a member of the composite bit value.
        /// </summary>
        /// <param name="composite"></param>
        /// <returns></returns>
        public bool IsMemberFlag(FlagCheckedListBoxItem composite)
        {
            return (IsFlag && ((_value & composite.Value) == _value));
        }

        #endregion

        #region Overridden from Object

        /// <summary>
        /// Return the <see cref="Caption"/>.
        /// </summary>
        /// <returns>The <see cref="Caption"/>.</returns>
        public override string ToString()
        {
            return _caption;
        }

        #endregion
    }
}