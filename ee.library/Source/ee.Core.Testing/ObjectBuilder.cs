using FizzWare.NBuilder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace ee.Core.Testing
{
    /// <summary>
    /// Creates an instance of an object 
    /// </summary>
    public class ObjectBuilder
    {
        /// <summary>
        /// Creates an instance of an object with type T, filling all the "primitive" properties (that means, Strings and DateTimes as well)
        /// and complex objects with null.
        /// </summary>
        /// <typeparam name="T">Type of object to be created</typeparam>
        /// <param name="nMembersInLists">By default is 2. Number of elements in the properties that are List</param>
        /// <returns>an object of type T filled</returns>
        public static T Create<T>(int nMembersInLists = 2)
        {
            var returnObject = Builder<T>.CreateNew().Build();

            foreach (PropertyInfo pi in typeof(T).GetProperties())
            {
                var type = pi.PropertyType;
                if (!IsCustomType(type))
                {
                    continue;
                }

                if (IsEnumerableType(type))
                {
                    if (typeof(IEnumerable).IsAssignableFrom(pi.PropertyType))
                    {

                        if (!pi.PropertyType.IsPrimitive)
                        {
                            if (!IsCustomType(type.GenericTypeArguments[0]))
                            {
                                var result = new List<string>(nMembersInLists);
                                for (int i = 0; i < nMembersInLists; i++)
                                {
                                    result.Add($"list item {i + 1}");
                                }
                                returnObject.GetType().GetProperty(pi.Name).SetValue(returnObject, result, null);
                            }
                            else
                            {

                                var builderType = typeof(FizzWare.NBuilder.Builder<>).MakeGenericType(type.GenericTypeArguments);

                                var createListOfSizeMethod = builderType.GetMethod("CreateListOfSize", new Type[] { typeof(int) });

                                var objectBuilder = createListOfSizeMethod.Invoke(builderType, new object[] { nMembersInLists });

                                //retrieve the "Build" method, which belongs to ObjectBuilder<T> class
                                var buildMethodInfo = objectBuilder.GetType().GetMethod("Build");

                                //finally, invoke "Build" from our ObjectBuilder<entityType> instance, which will give us... our entity !
                                var result = buildMethodInfo.Invoke(objectBuilder, null);

                                returnObject.GetType().GetProperty(pi.Name).SetValue(returnObject, result, null);
                            }
                        }

                    }
                }
                else
                {

                    var builder = new Builder();
                    var createNewMethodInfo = builder.GetType().GetMethod("CreateNew").MakeGenericMethod(new Type[] { type });

                    //invoke "CreateNew" from our builder instance which gives us an ObjectBuilder<T>, so now an ObjectBuilder<entityType> (well as an ISingleObjectBuilder<entityType>, but... who minds ;))
                    var objectBuilder = createNewMethodInfo.Invoke(builder, null);

                    //retrieve the "Build" method, which belongs to ObjectBuilder<T> class
                    var buildMethodInfo = objectBuilder.GetType().GetMethod("Build");

                    //finally, invoke "Build" from our ObjectBuilder<entityType> instance, which will give us... our entity !
                    var result = buildMethodInfo.Invoke(objectBuilder, null);

                    returnObject.GetType().GetProperty(pi.Name).SetValue(returnObject, result, null);


                }

            }

            return returnObject;
        }

        private static bool IsCustomType(Type type)
        {
            return (type != typeof(object) && Type.GetTypeCode(type) == TypeCode.Object);
        }
        private static bool IsEnumerableType(Type type)
        {
            return (type.GetInterface("IEnumerable") != null);
        }

    }
}
