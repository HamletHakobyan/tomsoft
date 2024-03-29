﻿using System;
using System.Diagnostics;
using System.Linq;
using Mediatek.Data.EntityFramework;
using Mediatek.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Mediatek.Data.EntityFramework
{
    [TestClass]
    public class Tests
    {
        private const string _storeConnectionString = @"Data Source=D:\Docs\Visual Studio 2010\Projects\Mediatek\Database\Mediatek.sdf";
        private const string _storeProviderName = "System.Data.SqlServerCe.3.5";

        [TestMethod]
        public void Test_OpenConnection()
        {
            using (MediatekContext context = MediatekContext.GetContext(_storeProviderName, _storeConnectionString))
            {
                context.Connection.Open();
            }
        }

        [TestMethod]
        public void Test_AddAndListRoles()
        {
            using (MediatekContext context = MediatekContext.GetContext(_storeProviderName, _storeConnectionString))
            {
                context.Connection.Open();

                using (var transaction = context.Connection.BeginTransaction())
                {
                    context.AddRole(new Role
                    {
                        Id = Guid.NewGuid(),
                        Name = "Director",
                        Description = "Movie director"
                    });

                    context.AddRole(new Role
                    {
                        Id = Guid.NewGuid(),
                        Name = "Author",
                        Description = "Book author"
                    });

                    context.AddRole(new Role
                    {
                        Id = Guid.NewGuid(),
                        Name = "Performer",
                        Description = "Performer of a music album"
                    });

                    context.SaveChanges();

                    foreach (var role in context.Roles)
                    {
                        Debug.WriteLine(string.Format("{0} - {1} - {2}", role.Id, role.Name, role.Description));
                    }

                    Role r = context.CreateObject<Role>();
                    r.Id = Guid.NewGuid();
                    r.Name = "Test";
                    context.AddRole(r);

                    context.SaveChanges();

                    Role r2 = context.Roles.SingleOrDefault(x => x.Id == r.Id);
                    Assert.AreSame(r, r2);

                    transaction.Rollback();
                }
            }
        }

        [TestMethod]
        public void Test_Count()
        {
            using (MediatekContext context = MediatekContext.GetContext(_storeProviderName, _storeConnectionString))
            {
                int count = context.Medias.Count();
                Assert.IsTrue(count > 0);
            }
        }
    }
}
