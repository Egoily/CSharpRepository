using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace ee.Core.Utility
{
    public static class Extessions
    {


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
