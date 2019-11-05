using EgoDevil.Utilities.ThumbnailCreator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Globalization;

namespace EAlbums
{

    public interface IImageCircleRevolver
    {
        List<ImageCircle> Circles { get; set; }
        ImageCircleParameter CircleParameter { get; set; }
        ThumbElement SelectedObject { get; set; }

        void Load(List<string> filePaths);
        void Load(List<ThumbElement> thumbElements);

        void SetAngleOffset();

        void SetAlphaAccel(float alphaAccel);
        void SetFixedAlphaAccel(float alphaAccel);
        void SetPerspective(float perspective);

        void Refresh();

        void ClearHover();

        void DrawImages(Graphics g);

        bool SelectHoverItem(Point location);
        bool SelectHoverItem(string name);

        void SetRevolveType(RevolveTypes revolveType);
        void SetOrginalCenter(Point orginalCenter);
    }


    [TypeConverter(typeof(ImageCircleParameterConverter))]
    public class ImageCircleParameter
    {
        [DefaultValue(10)]
        public int MaxCircleCapacity { get; set; }
        [DefaultValue(360)]
        public int MaxCapacityInCircle { get; set; }
        [DefaultValue(100)]
        public int CircleVerInterval { get; set; }
        [DefaultValue(120)]
        public int MaxImageLength { get; set; }
        [DefaultValue(0.0F)]
        public float Alpha { get; set; }

        public Color BackgroundColor { get; set; }

        public Point OrginalCenter { get; set; }

        public Point Radius { get; set; }


        public ScalingOptions ScalingOption { get; set; }
        public Size DestinationSize { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", MaxCircleCapacity, MaxCapacityInCircle, CircleVerInterval, MaxImageLength, Alpha, BackgroundColor, OrginalCenter, Radius);  // <--¶ººÅ·Ö¸î
        }

        /// <summary>
        /// Gets a hash code for this instance.
        /// </summary>
        /// <returns>A hash code.</returns>
        public override int GetHashCode()
        {
            return MaxCircleCapacity.GetHashCode() ^ MaxCapacityInCircle.GetHashCode() ^ CircleVerInterval.GetHashCode() ^ MaxImageLength.GetHashCode()
                ^ Alpha.GetHashCode() ^ BackgroundColor.GetHashCode()
                ^ OrginalCenter.GetHashCode() ^ Radius.GetHashCode();
        }

        /// <summary>
        /// Determines whether this instance and another value are equal.
        /// </summary>
        /// <param name="obj">Another object.</param>
        /// <returns>
        /// True if obj is of type <see cref="PictureBoxState"/> and all properties are equal.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (!(obj is ImageCircleParameter))
                return false;

            return this == (ImageCircleParameter)obj;
        }
    }

    /// <summary>
    /// Designer converter class for <see cref="ImageCircleParameter"/> s.
    /// </summary>
    public class ImageCircleParameterConverter : ExpandableObjectConverter
    {
        #region Overridden from ExpandableObjectConverter

        /// <summary>
        /// Determines whether this converter can convert a <see cref="ImageCircleParameter"/> to a given
        /// type in the specified context.
        /// </summary>
        /// <param name="context">The formatting context.</param>
        /// <param name="destType">The type the conversion should result into.</param>
        /// <returns>True if the converter can handle the conversion, otherwise false.</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destType)
        {
            if (destType == typeof(InstanceDescriptor))
                return true;

            return base.CanConvertTo(context, destType);
        }

        /// <summary>
        /// Converts a specified value (which must be a <see cref="ImageCircleParameter"/>) into a given
        /// type un the specified context.
        /// </summary>
        /// <param name="context">The formatting context.</param>
        /// <param name="info">The culture under which the conversion should be performed.</param>
        /// <param name="value">Value to convert.</param>
        /// <param name="destType">The type the conversion should result into.</param>
        /// <returns>The converted value.</returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo info, object value, Type destType)
        {
            ImageCircleParameter para = (ImageCircleParameter)value;
            if (destType == typeof(InstanceDescriptor))
            {
                Type[] ctorTypes = new Type[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(float),
                                                  typeof(Color),  typeof(Point), typeof(Point) };
                object[] ctorParams = new object[] { para.MaxCircleCapacity, para.MaxCapacityInCircle, para.CircleVerInterval,para.MaxImageLength,
                    para.Alpha, para.BackgroundColor, para.OrginalCenter, para.Radius };
                return new InstanceDescriptor(typeof(ImageCircleParameter).GetConstructor(ctorTypes), ctorParams, true);
            }

            return base.ConvertTo(context, info, value, destType);
        }

        /// <summary>
        /// Creates a <see cref="ImageCircleParameter"/> instance from a collection of properties.
        /// </summary>
        /// <param name="context">The formatting context.</param>
        /// <param name="propertyValues">Collecion of properties.</param>
        /// <returns>A new <see cref="ImageCircleParameter"/> instance.</returns>
        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            return new ImageCircleParameter()
            {
                MaxCircleCapacity = (int)propertyValues["CircleCapacity"],
                MaxCapacityInCircle = (int)propertyValues["MaxCapacityInCircle"],
                CircleVerInterval = (int)propertyValues["CircleVerInterval"],
                MaxImageLength = (int)propertyValues["MaxImageLength"],
                Alpha = (float)propertyValues["Alpha"],
                BackgroundColor = (Color)propertyValues["BackgroundColor"],
                OrginalCenter = (Point)propertyValues["OrginalCenter"],
                Radius = (Point)propertyValues["Radius"],

            };
        }

        /// <summary>
        /// Gets whether <see cref="CreateInstance"/> is supported in the specified context.
        /// </summary>
        /// <param name="context">The formatting context.</param>
        /// <returns>True.</returns>
        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// Gets the properties associated with a <see cref="ImageCircleParameter"/>.
        /// </summary>
        /// <param name="context">The formatting context.</param>
        /// <param name="value">The value to obtain the properties from.</param>
        /// <param name="attributes">Array of attributes.</param>
        /// <returns>Collection of properties.</returns>
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            PropertyDescriptorCollection result = TypeDescriptor.GetProperties(typeof(ImageCircleParameter), attributes);
            return result;
        }

        /// <summary>
        /// Gets whether <see cref="GetProperties"/> is supported in the specified context.
        /// </summary>
        /// <param name="context">The formatting context.</param>
        /// <returns>True.</returns>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        #endregion
    }
















}