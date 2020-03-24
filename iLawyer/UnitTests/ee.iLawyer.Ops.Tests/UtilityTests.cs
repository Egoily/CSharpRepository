using ee.Core.Framework.Schema;
using ee.iLawyer.Ops.Contact.DTO.ViewObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ee.iLawyer.Ops.Tests
{
    [TestClass]
    public class UtilityTests
    {

        [TestInitialize()]
        public void Initialize()
        {

        }

        [TestMethod()]
        public void Test()
        {
            var a = new ObjectResponse<Court>()
            {
                Code = 0,
                Message = "this is message.",
                Object = new Court(),
            };

            object obj = a;

            var typeA = a.GetType();
            var typeObj = obj.GetType();

            var dataRes = obj.ToDataResponse();
        }

     
    }
}
