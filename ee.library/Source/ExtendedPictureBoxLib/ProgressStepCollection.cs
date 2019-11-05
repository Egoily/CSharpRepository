using System.Collections;

namespace ExtendedPictureBoxLib
{
    /// <summary>
    /// A typesafe collection class for <see cref="ProgressStep"/> instances.
    /// </summary>
    public class ProgressStepCollection : CollectionBase
    {
        #region Constructors

        /// <summary>
        /// Creats a new empty instance.
        /// </summary>
        public ProgressStepCollection()
        {
        }

        #endregion

        #region Public interface

        /// <summary>
        /// Adds a <see cref="ProgressStep"/> to the end of the collection.
        /// </summary>
        /// <param name="progressStep">Step to be added.</param>
        public void Add(ProgressStep progressStep)
        {
            base.InnerList.Add(progressStep);
        }

        /// <summary>
        /// Removes a <see cref="ProgressStep"/> from the collection.
        /// </summary>
        /// <param name="progressStep">Step to be removed.</param>
        public void Remove(ProgressStep progressStep)
        {
            base.InnerList.Remove(progressStep);
        }

        /// <summary>
        /// Gets a <see cref="ProgressStep"/> from a specified position.
        /// </summary>
        public ProgressStep this[int index]
        {
            get { return (ProgressStep)base.InnerList[index]; }
        }

        #endregion
    }
}