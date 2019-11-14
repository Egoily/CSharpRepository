using ee.Core.Testing;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ee.Core.Framework.Tests
{
    [TestClass]
    public class JsonMarkBigObjectConverterTest
    {
        private static readonly JsonSerializerSettings JsonSetting = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            Converters = new List<JsonConverter>() { new JsonMarkBigObjectConverter() }
        };

        [TestInitialize()]
        public void Initialize()
        {

        }


        [TestMethod]
        public void CanCreateMoqTest()
        {
            // Arrange
            Mock<User> mock = new Mock<User>();
            mock.SetupAllProperties();
            // Assert
            Assert.IsNotNull(mock.Object);
        }


        [TestMethod]
        public void TestMethod1()
        {
            var request = ObjectBuilder.Create<User>();

            var normalJson = JsonConvert.SerializeObject(request);

            System.Diagnostics.Debug.WriteLine($"Normal Json: {normalJson}");

            var markJson = JsonConvert.SerializeObject(request, JsonSetting);
            System.Diagnostics.Debug.WriteLine($"Mark   Json: {markJson}");
        }

        [TestMethod]
        public void GetStaticMethodTest()
        {
            var builderType = typeof(FizzWare.NBuilder.Builder<>);
            builderType = builderType.MakeGenericType(new Type[] { typeof(Role) });
            var methods = builderType.GetMethods();
            var createListOfSizeMethods = methods.Where(x => x.Name == "CreateListOfSize");
            var createListOfSizeMethod = builderType.GetMethod("CreateListOfSize", new Type[] { typeof(int) });



            //invoke "CreateNew" from our builder instance which gives us an ObjectBuilder<T>, so now an ObjectBuilder<entityType> (well as an ISingleObjectBuilder<entityType>, but... who minds ;))
            var objectBuilder = createListOfSizeMethod.Invoke(builderType, new object[] { 2 });

            //retrieve the "Build" method, which belongs to ObjectBuilder<T> class
            var buildMethodInfo = objectBuilder.GetType().GetMethod("Build");

            //finally, invoke "Build" from our ObjectBuilder<entityType> instance, which will give us... our entity !
            var result = buildMethodInfo.Invoke(objectBuilder, null);
        }


        [TestMethod]
        public void CreateStringMethod()
        {
            var request = Builder<string>.CreateListOfSize(2).Build();

            var normalJson = JsonConvert.SerializeObject(request);

            Console.WriteLine($"Normal Json: {normalJson}");

            var markJson = JsonConvert.SerializeObject(request, JsonSetting);
            Console.WriteLine($"Mark Json: {markJson}");
        }
    }


















    public class User
    {

        //public virtual List<Role> Roles { get; set; }


        public virtual List<string> Resources { get; set; }

        public User()
        {

        }
    }
    public class Role
    {

        public virtual int Id { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary>
        ///  名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }

        public virtual Role Parent { get; set; }
        public virtual List<Role> Children { get; set; }

        public Role()
        {

        }
    }

}
