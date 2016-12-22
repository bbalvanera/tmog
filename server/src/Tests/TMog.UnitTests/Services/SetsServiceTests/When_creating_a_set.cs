using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TMog.Data;
using TMog.Entities;
using TMog.WowheadApi;

namespace TMog.UnitTests.Services.SetsServiceTests
{
    [TestClass]
    public class When_creating_set : SetsServiceTestHelper
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.Initialize(config => config.AddProfiles("TMog"));
        }

        [TestMethod]
        public async Task should_create_new_if_not_exists()
        {
            var data     = new List<Source>();
            var sources  = SetupDbSet(data);
            var database = new Mock<ITMogDatabase>();
            database.Setup(db => db.Sets.Find(It.IsAny<object>())).Returns((Set)null);
            database.Setup(db => db.Zones.Find(It.IsAny<object>())).Returns(new Zone { ZoneId = 1 });
            database.Setup(db => db.Sources).Returns(sources.Object);
            var subject = GetSubject(database.Object, SetupWowheadProvider());

            var result = await subject.Create(1119);

            Assert.AreEqual("Darkruned Battlegear", result.Name);
            Assert.AreEqual("XXXXXXXXXXXX", result.Slots);
            Assert.AreEqual(1119, result.SetId);
            database.Verify(db => db.Sets.Find(It.IsAny<object>()), Times.AtLeastOnce());
            database.Verify(db => db.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public async Task should_not_create_duplicates()
        {
            var database = new Mock<ITMogDatabase>();
            database.Setup(db => db.Sets.Find(It.IsAny<object>())).Returns(new Set { SetId = 1119 });
            var subject = GetSubject(database.Object);

            await subject.Create(1119);
            var result = await subject.Create(1119);

            Assert.AreEqual(1119, result.SetId);
            database.Verify(db => db.Sets.Find(It.IsAny<object>()), Times.AtLeastOnce());
            database.Verify(db => db.SaveChanges(), Times.Never());
        }

        [TestMethod]
        public async Task should_return_instance_after_create()
        {
            var database = new Mock<ITMogDatabase>();
            database.Setup(db => db.Sets.Find(It.IsAny<object>())).Returns(new Set { SetId = 1119 });
            var subject = GetSubject(database.Object);

            var result = await subject.Create(1119);

            Assert.AreEqual(1119, result.SetId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task shoud_throw_if_invalid_id()
        {
            var id = 0;
            var subject = GetSubject(new Mock<ITMogDatabase>().Object);

            await subject.Create(id);
        }



        private Mock<DbSet<T>> SetupDbSet<T>(List<T> data) where T : class
        {
            var datasource = data.AsQueryable();
            var dbSet      = new Mock<DbSet<T>>();

            dbSet.As<IQueryable<T>>().Setup(s => s.Provider).Returns(datasource.Provider);
            dbSet.As<IQueryable<T>>().Setup(s => s.Expression).Returns(datasource.Expression);
            dbSet.As<IQueryable<T>>().Setup(s => s.ElementType).Returns(datasource.ElementType);
            dbSet.As<IQueryable<T>>().Setup(s => s.GetEnumerator()).Returns(data.GetEnumerator());
            dbSet.Setup(s => s.Local).Returns(new ObservableCollection<T>());

            return dbSet;
        }

        private IWowheadProvider SetupWowheadProvider()
        {
            var wowheadSet = new Mock<IWowheadSet>();
            wowheadSet.Setup(set => set.WowheadSetId).Returns(1119);
            wowheadSet.Setup(set => set.Name).Returns("Darkruned Battlegear");
            wowheadSet.Setup(set => set.Items).Returns(new List<IWowheadItem>());

            var wowheadProvider = new Mock<IWowheadProvider>();
            wowheadProvider.Setup(wow => wow.GetSetById(It.IsAny<int>())).Returns(Task.FromResult(wowheadSet.Object));

            return wowheadProvider.Object;
        }
    }
}
