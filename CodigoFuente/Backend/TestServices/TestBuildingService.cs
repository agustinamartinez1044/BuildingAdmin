using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services;
using IDataAccess;
using Domain;
using System.Linq.Expressions;
using IServices;

namespace TestServices
{
    [TestClass]
    public class TestBuildingService
    {
        BuildingService _buildingService;

        private Mock<ISessionService> _sessionServiceMock;
        private Mock<IGenericRepository<Building>> _buildingRepositoryMock;
        private Mock<IGenericRepository<Owner>> _ownerRepositoryMock;
        private Mock<IGenericRepository<Manager>> _managerRepositoryMock;
        private Mock<IGenericRepository<ConstructionCompany>> _constructionCompanyMock;

        private Building _building;
        private Apartment _apartment;
        private CompanyAdministrator _user;
        private Manager _manager;

        [TestInitialize]
        public void SetUp()
        {
            _buildingRepositoryMock = new Mock<IGenericRepository<Building>>(MockBehavior.Strict);

            _ownerRepositoryMock = new Mock<IGenericRepository<Owner>>(MockBehavior.Strict);
            _sessionServiceMock = new Mock<ISessionService>(MockBehavior.Strict);
            _constructionCompanyMock = new Mock<IGenericRepository<ConstructionCompany>>(MockBehavior.Strict);
            _managerRepositoryMock = new Mock<IGenericRepository<Manager>>(MockBehavior.Strict);

            _apartment = new Apartment()
            {
                Id = 1,
                Bathrooms = 2,
                DoorNumber = 3,
                Floor = 1,
                HasTerrace = false,
                Owner = new Owner() { Id = 1, Email = "owner@mail.com", Name = "Nombre", LastName = "Apellido" },
                Rooms = 2
            };

            _building = new Building()
            {
                Id = 1,
                Apartments = new List<Apartment> { _apartment },
                Address = "Rivera, 1111, Soca",
                ConstructionCompany = new ConstructionCompany { Name = "Construction" },
                Expenses = 2000,
                Location = "111,111",
                Name = "Edificio Las Delicias",
                Tickets = []
            };

            _user = new CompanyAdministrator()
            {
                Email = "company@admin.com",
                Name = "Admin",
                LastName = "Company",
                Password = "Test.1234!",
                ConstructionCompany = new ConstructionCompany { Name = "Test Construction Company" }
            };

            _manager = new Manager
            {
                Name = "Manager",
                LastName = "Manager",
                Email = "test@mail.com",
                Password = "Test.1234!",
                Buildings = new List<Building> { _building }
            };

        }

        [TestMethod]
        public void CreateBuilding()
        {
            _buildingRepositoryMock.Setup(r => r.Insert(It.IsAny<Building>())).Verifiable();
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);
            _buildingRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Building, bool>>>(), It.IsAny<List<string>>())).Returns((Building)null);
            _ownerRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Owner, bool>>>(), It.IsAny<List<string>>())).Returns(_apartment.Owner);

            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);

            var building = new Building
            {
                Name = "Edificio nuevo",
                Address = "Calle, 123, esquina",
                Location = "111,111",
                ConstructionCompany = new ConstructionCompany() { Name = "Construction" },
                Expenses = 1000,
                Apartments = new List<Apartment> { _apartment }
            };

            var result = _buildingService.CreateBuilding(building);

            _buildingRepositoryMock.VerifyAll();
            _sessionServiceMock.VerifyAll();
            _ownerRepositoryMock.VerifyAll();

            Assert.AreEqual(building, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateBuildingAlreadyExists()
        {
            _buildingRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Building, bool>>>(), It.IsAny<List<string>>())).Returns(_building);
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);

            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);

            var building = new Building
            {
                Name = "Edificio Las Delicias",
                Address = "Rivera, 1111, Soca",
                Location = "111,111",
                ConstructionCompany = new ConstructionCompany() { Name = "Construction" },
                Expenses = 1000,
                Apartments = new List<Apartment> { _apartment }
            };

            var result = _buildingService.CreateBuilding(building);
        }

        [TestMethod]
        public void DeleteBuilding()
        {
            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_manager);
            _buildingRepositoryMock.Setup(r => r.Delete(It.IsAny<Building>())).Verifiable();

            _buildingService.DeleteBuilding(1);

            _sessionServiceMock.VerifyAll();
            _buildingRepositoryMock.Verify(r => r.Delete(It.IsAny<Building>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteBuildingThatNotExist()
        {
            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_manager);
            _buildingRepositoryMock.Setup(r => r.Delete(It.IsAny<Building>())).Verifiable();

            _buildingService.DeleteBuilding(3);

            _sessionServiceMock.VerifyAll();
            _buildingRepositoryMock.Verify(r => r.Delete(It.IsAny<Building>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestDeleteBuildingNotManager()
        {
            var user = new Administrator();
            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(user);

            _buildingService.DeleteBuilding(1);

            _buildingRepositoryMock.VerifyAll();
        }


        [TestMethod]
        public void ModifyBuilding()
        {
            Building existingBuilding = new Building()
            {
                Id = 1,
                Address = "Rivera, 1111, Soca",
                ConstructionCompany = new ConstructionCompany() { Name = "Constructora" },
                Expenses = 2000,
                Location = "111,111",
                Name = "Edificio Las Delicias",
                Apartments = new List<Apartment>() { _apartment }
            };

            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_manager);
            _buildingRepositoryMock.Setup(r => r.Update(It.IsAny<Building>())).Verifiable();
            _ownerRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Owner, bool>>>(), It.IsAny<List<string>>())).Returns(_apartment.Owner);
            _constructionCompanyMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<ConstructionCompany, bool>>>(), It.IsAny<List<string>>())).Returns(existingBuilding.ConstructionCompany);

            Building modifiedBuilding = new Building()
            {
                Id = existingBuilding.Id,
                Address = existingBuilding.Address,
                ConstructionCompany = new ConstructionCompany() { Name = "New Constructora" },
                Expenses = 1000,
                Location = existingBuilding.Location,
                Name = existingBuilding.Name,
                Apartments = new List<Apartment>()
                {
                    new Apartment()
                    {
                        Owner = new Owner() { Email = "newOwner@test.com", Name = "Nuevo", LastName = "Owner" },
                        Id = 1
                    }
                }
            };

            var result = _buildingService.ModifyBuilding(1, modifiedBuilding);
            Assert.AreEqual(modifiedBuilding, result);
        }


        [TestMethod]
        public void ModifyBuildingWithExistingOwner()
        {
            var existingOwner = new Owner()
            {
                Id = 5,
                Email = "ownerViejo@mail.com",
                Name = "Due?o",
                LastName = "Owner"
            };
            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_manager);
            _buildingRepositoryMock.Setup(r => r.Update(It.IsAny<Building>())).Verifiable();
            _ownerRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Owner, bool>>>(), It.IsAny<List<string>>())).Returns(existingOwner);
            var modifiedBuilding = new Building();
            modifiedBuilding.Apartments.Add(new Apartment()
            {
                Owner = new Owner() { Email = "ownerViejo@mail.com", Name = "Due?o", LastName = "Nuevo " },
                Id = 1
            });

            var result = _buildingService.ModifyBuilding(1, modifiedBuilding);

            _buildingRepositoryMock.VerifyAll();
            _sessionServiceMock.VerifyAll();

            Assert.AreEqual(existingOwner, result.Apartments.First().Owner);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ModifyBuildingThatNotExist()
        {
            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_manager);
            _buildingRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns((Building)null);
            _buildingRepositoryMock.Setup(r => r.Update(It.IsAny<Building>())).Verifiable();
            var modifiedBuilding = new Building();

            _buildingService.ModifyBuilding(3, modifiedBuilding);

            _buildingRepositoryMock.VerifyAll();
            _sessionServiceMock.VerifyAll();
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestModifyBuildingNotManager()
        {
            var user = new Administrator();
            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(user);

            var modifiedBuilding = new Building();

            _buildingService.ModifyBuilding(1, modifiedBuilding);
        }

        [TestMethod]
        public void GetBuilding()
        {
            _buildingRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Building, bool>>>(), It.IsAny<List<string>>())).Returns(_building);
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(_user);
            _buildingRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Building, bool>>>(), It.IsAny<List<string>>())).Returns(_building);

            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);

            var result = _buildingService.Get();

            _buildingRepositoryMock.VerifyAll();
            _sessionServiceMock.VerifyAll();
            Assert.AreEqual(_building, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGetBuildingNotCompanyAdministrator()
        {
            var user = new Manager();
            _buildingRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Building, bool>>>(), It.IsAny<List<string>>())).Returns(_building);
            _sessionServiceMock.Setup(r => r.GetCurrentUser(It.IsAny<Guid?>())).Returns(user);
            _buildingRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Building, bool>>>(), It.IsAny<List<string>>())).Returns(_building);
            _managerRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Manager, bool>>>(), It.IsAny<List<string>>())).Returns(user);

            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);

            var result = _buildingService.Get();
        }

        [TestMethod]
        public void GetManagerName()
        {
            _managerRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Manager, bool>>>(), It.IsAny<List<string>>())).Returns(_manager);
            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);

            var result = _buildingService.GetManagerName(1);

            _managerRepositoryMock.VerifyAll();
            Assert.AreEqual(_manager.Name, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetManagerNameNotFound()
        {
            _managerRepositoryMock.Setup(r => r.GetByCondition(It.IsAny<Expression<Func<Manager, bool>>>(), It.IsAny<List<string>>())).Returns((Manager)null);
            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _sessionServiceMock.Object, _ownerRepositoryMock.Object, _managerRepositoryMock.Object, _constructionCompanyMock.Object);

            var result = _buildingService.GetManagerName(1);
        }
    }
}