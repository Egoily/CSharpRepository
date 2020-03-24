using ee.Core.NhDataAccess;
using ee.Core.Logging;
using ee.iLawyer.Ops.Contact.Args;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using ee.Core.Net;

namespace ee.iLawyer.Ops.Tests
{
    [TestClass]
    public class SystemManagementTests
    {
        private ILawyerService service;

        private IList<Contact.DTO.ViewObjects.SystemManagement.PermissionModule> permissionModules;
        private IList<Contact.DTO.ViewObjects.SystemManagement.Role> roles;
        private static void Build()
        {
            SessionManager.Builder = new DataAccessBuilder.SqlServer.DataAccessBuilder();
            SessionManager.Builder.Build();
        }
        [TestInitialize()]
        public void Initialize()
        {
            Logger.Configure("ee.iLawyer.Ops.Tests");
            Build();
            service = new ILawyerService();

            permissionModules = service.GetPermissionModules(new RequestBase()).QueryList.ToList();
            roles = service.GetRoles(new GetRolesRequest()).QueryList.ToList();
        }



        [TestMethod()]
        public void Register()
        {
            var request = new RegisterRequest()
            {
                UserName = "Test",
                Password = "Test",
                PhoneNo = "123456789",
                RoleIds = new List<int> { 2 },
            };
            var response = service.Register(request);

            Assert.AreEqual(0, response.Code);
        }


        [TestMethod()]
        public void Login()
        {
            var request = new LoginRequest()
            {
                LoginName = "Test",
                Password = "Test"
            };
            var response = service.Login(request);

            Assert.AreEqual(0, response.Code);
        }

        [TestMethod()]
        public void Grant_Increase()
        {

            var user = service.Login(new LoginRequest() { LoginName = "Test", Password = "Test" });



            var request = new GrantRequest()
            {
                UserId = user.Object.Id,
                Pattern = Contact.OperatePattern.Increase,
                RoleIds = roles.Where(x => x.Code == "Assistant").Select(x => x.Id),
                PermissionIds = permissionModules.Take(4).Select(x => x.Id),
                PermissionRestrictionIds = permissionModules.Take(3).Select(x => x.Id),

            };
            var response = service.Grant(request);

            Assert.AreEqual(0, response.Code);
        }
        [TestMethod()]
        public void Grant_Decrease()
        {
            var user = service.Login(new LoginRequest() { LoginName = "Test", Password = "Test" });
            var request = new GrantRequest()
            {
                UserId = user.Object.Id,
                Pattern = Contact.OperatePattern.Decrease,
                RoleIds = new List<int> { 3 },
                PermissionIds = permissionModules.Skip(1).Take(3).Select(x => x.Id),
                PermissionRestrictionIds = permissionModules.Skip(1).Take(2).Select(x => x.Id),

            };
            var response = service.Grant(request);

            Assert.AreEqual(0, response.Code);
        }
        [TestMethod()]
        public void Grant_Hybrid()
        {
            var user = service.Login(new LoginRequest() { LoginName = "Test", Password = "Test" });
            var request = new GrantRequest()
            {
                UserId = user.Object.Id,
                Pattern = Contact.OperatePattern.Hybrid,
                RoleIds = new List<int> { 3 },
                PermissionIds = new List<string> { permissionModules[2].Id, permissionModules[4].Id, permissionModules[6].Id, permissionModules[8].Id },
                PermissionRestrictionIds = new List<string> { permissionModules[1].Id, permissionModules[3].Id, permissionModules[4].Id, permissionModules[5].Id },

            };
            var response = service.Grant(request);

            Assert.AreEqual(0, response.Code);
        }
        [TestMethod()]
        public void Grant_Hybrid_Clear()
        {
            var user = service.Login(new LoginRequest() { LoginName = "Test", Password = "Test" });
            var request = new GrantRequest()
            {
                UserId = user.Object.Id,
                Pattern = Contact.OperatePattern.Hybrid,
            };
            var response = service.Grant(request);

            Assert.AreEqual(0, response.Code);
        }
    }
}
