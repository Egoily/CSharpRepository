﻿using ee.iLawyer.DataAccessBuilder.Sqlite;
using ee.Core.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ee.iLawyer.SessionFactory.Sqlite.Tests
{
    [TestClass()]
    public class SessionFactoryTests
    {
        private static void Build()
        {
            SessionManager.Builder = new DataAccessBuilder.Sqlite.DataAccessBuilder();
            SessionManager.Builder.Build(@"..\..\..\..\03.Application\ee.iLawyer\bin\Debug\database.db");
        }
        [TestInitialize()]
        public void Initialize()
        {
            log4net.Config.XmlConfigurator.Configure();
            Build();
        }


    }
}