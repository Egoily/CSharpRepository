using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace ee.Core.Utility
{
    public static class AssertExtessions
    {

        //public static T DeepCopyByReflection<T>(this T obj)
        //{

        //    if (obj == null || obj is string || obj.GetType().IsValueType)
        //    {
        //        return obj;
        //    }

        //    object retval = Activator.CreateInstance(obj.GetType());
        //    FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
        //    foreach (var field in fields)
        //    {
        //        try
        //        {
        //            field.SetValue(retval, DeepCopyByReflection(field.GetValue(obj)));
        //        }
        //        catch { }
        //    }

        //    return (T)retval;
        //}

        //public static object DeepCopyObject(this object obj)
        //{
        //    if (obj == null)
        //    {
        //        return null;
        //    }

        //    Type type = obj.GetType();

        //    if (type.IsValueType || type == typeof(string))
        //    {
        //        return obj;
        //    }
        //    else if (type.IsArray)
        //    {
        //        var array = obj as Array;
        //        Type elementType = type.Assembly.GetType(type.FullName.Replace("[]", string.Empty));

        //        Array copied = Array.CreateInstance(elementType, array.Length);
        //        for (int i = 0; i < array.Length; i++)
        //        {
        //            copied.SetValue(DeepCopyObject(array.GetValue(i)), i);
        //        }
        //        return Convert.ChangeType(copied, type);
        //    }
        //    else if (obj is IList)
        //    {
        //        var list = obj as IList;

        //        return null;
        //    }

        //    else if (obj is ArrayList)
        //    {
        //        var arrayList = obj as ArrayList;
        //        return null;
        //    }
        //    else if (type.Name == "ObservableCollection`1")
        //    {
        //        var builderType = typeof(ObservableCollection<>).MakeGenericType(type.GenericTypeArguments);
        //        return null;
        //    }
        //    else if (type.IsClass)
        //    {
        //        try
        //        {
        //            object toret = Activator.CreateInstance(obj.GetType());
        //            FieldInfo[] fields = type.GetFields(BindingFlags.Public |
        //                        BindingFlags.NonPublic | BindingFlags.Instance);
        //            foreach (FieldInfo field in fields)
        //            {
        //                object fieldValue = field.GetValue(obj);
        //                if (fieldValue == null)
        //                {
        //                    continue;
        //                }

        //                field.SetValue(toret, DeepCopyObject(fieldValue));
        //            }
        //            return toret;
        //        }
        //        catch
        //        {
        //            return null;
        //        }
        //    }
        //    else
        //    {
        //        throw new ArgumentException("Unknown type");
        //    }
        //}

        public static bool PropertiesAreEqual(this object self, object to, params string[] ignore)
        {
            return ObjectPropertiesAreEqual(self, to, ignore);
        }
        /// <summary>
        /// If objects are equal return true
        /// </summary>
        /// <param name="self"></param>
        /// <param name="to"></param>
        /// <param name="ignore"></param>
        private static bool ObjectPropertiesAreEqual(object self, object to, params string[] ignore)
        {
            #region Check nulls
            if (self == null && to == null)
            {
                return true;
            }

            if (self == null || to == null)
            {
                return false;
            }
            #endregion

            if (self is IList)
            {
                if (!CheckList(self, to))
                {
                    return false;
                }
            }
            else
            {
                Type typeSelf = self.GetType();
                List<string> ignoreList = new List<string>(ignore);
                foreach (PropertyInfo pi in typeSelf.GetProperties())
                {
                    if (ignoreList.Contains(pi.Name))
                    {
                        continue;
                    }

                    object selfValue = typeSelf.GetProperty(pi.Name).GetValue(self, null);
                    object toValue = typeSelf.GetProperty(pi.Name).GetValue(to, null);

                    #region Check nulls
                    if (selfValue == null && toValue == null)
                    {
                        continue;
                    }

                    if (selfValue == null || toValue == null)
                    {
                        return false;
                    }
                    #endregion

                    if (selfValue.GetType().IsPrimitive || selfValue is String || selfValue is DateTime)
                    {
                        if (selfValue != toValue && (!selfValue.Equals(toValue)))
                        {
                            return false;
                        }
                    }
                    else if (selfValue is IList)
                    {
                        if (((IList)selfValue).Count != ((IList)toValue).Count)
                        {
                            return false;
                        }



                        if (!CheckList(selfValue, toValue))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (!ObjectPropertiesAreEqual(selfValue, toValue))
                        {
                            return false;
                        }
                    }
                }

            }
            return true;
        }

        /// <summary>
        /// Check if the objects are equal lists 
        /// </summary>
        /// <param name="selfObject"></param>
        /// <param name="toObject"></param>
        private static bool CheckList(object selfObject, object toObject)
        {
            var selfList = (IList)selfObject;
            var toList = (IList)toObject;

            for (var pos = 0; pos < selfList.Count; pos++)
            {
                if (!ObjectPropertiesAreEqual(selfList[pos], toList[pos]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
