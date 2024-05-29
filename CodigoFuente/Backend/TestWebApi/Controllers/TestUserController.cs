using Domain;
using IServices;
using WebApi.Controllers;
using Moq;
using DTO.In;
using Microsoft.AspNetCore.Mvc;
using DTO.Out;

namespace TestWebApi;

[TestClass]
public class TestUserController
{
    private Mock<IUserServices> _userServiceMock;

    [TestInitialize]
    public void SetUp()
    {
        _userServiceMock = new Mock<IUserServices>(MockBehavior.Strict);
    }

    [TestMethod]
    public void TestCreateAdministrator()
    {
        Administrator admin = new Administrator
        {
            Id = 1,
            Name = "John",
            LastName = "Doe",
            Email = "test@test.com",
            Password = "Prueba.1234"
        };

        var administratorCreateModel = new AdministratorCreateInput
        {
            Name = "John",
            LastName = "Doe",
            Email = "test@test.com",
            Password = "Prueba.1234"
        };

        _userServiceMock.Setup(r => r.CreateAdministrator(It.IsAny<Administrator>())).Returns(admin);


        var userController = new UserController(_userServiceMock.Object);

        var result = userController.CreateAdministrator(administratorCreateModel);
        var okResult = result as OkObjectResult;
        var administratorModel = okResult.Value as AdministratorOutput;

        var expectedContent = new AdministratorOutput(admin);

        _userServiceMock.VerifyAll();
        Assert.AreEqual(administratorModel, expectedContent);
    }

    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public void TestCreateAdministratorWithNoBody()
    {
        AdministratorCreateInput administratorCreateModel = null;
        _userServiceMock.Setup(r => r.CreateAdministrator(It.IsAny<Administrator>())).Throws(new NullReferenceException());

        var userController = new UserController(_userServiceMock.Object);

        var result = userController.CreateAdministrator(administratorCreateModel);
        var okResult = result as OkObjectResult;
        var administratorModel = okResult.Value as AdministratorOutput;

        _userServiceMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestCreateAdministratorWithNoName()
    {
        Administrator admin = new Administrator
        {
            LastName = "Doe",
            Email = "test@tes.com"
        };

        AdministratorCreateInput administratorCreateModel = new AdministratorCreateInput
        {
            LastName = admin.LastName,
            Email = admin.Email,
            Password = "Prueba.1234"
        };

        var userController = new UserController(_userServiceMock.Object);

        var result = userController.CreateAdministrator(administratorCreateModel);
        var okResult = result as OkObjectResult;
        var administratorModel = okResult.Value as AdministratorOutput;
        _userServiceMock.VerifyAll();

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestCreateAdministratorWithNoLastName()
    {
        Administrator admin = new Administrator
        {
            Name = "John",
            Email = "test@test.com"
        };

        AdministratorCreateInput administratorCreateModel = new AdministratorCreateInput
        {
            Name = admin.Name,
            Email = admin.Email
        };

        var userController = new UserController(_userServiceMock.Object);

        var result = userController.CreateAdministrator(administratorCreateModel);

        _userServiceMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestCreateAdministratorWithNoEmail()
    {
        Administrator admin = new Administrator
        {
            Name = "John",
            LastName = "Doe"
        };

        AdministratorCreateInput administratorCreateModel = new AdministratorCreateInput
        {
            Name = admin.Name,
            LastName = admin.LastName
        };

        var userController = new UserController(_userServiceMock.Object);

        var result = userController.CreateAdministrator(administratorCreateModel);

        _userServiceMock.VerifyAll();

    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestCreateAdministratorWithNoPassword()
    {
        Administrator admin = new Administrator
        {
            Name = "John",
            LastName = "Doe",
            Email = "test@test.com",
        };

        AdministratorCreateInput administratorCreateModel = new AdministratorCreateInput
        {
            Name = admin.Name,
            LastName = admin.LastName,
            Email = admin.Email
        };

        var userController = new UserController(_userServiceMock.Object);

        var result = userController.CreateAdministrator(administratorCreateModel);

        _userServiceMock.VerifyAll();
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public void TestCreateMaintenanceOperator()
    {
        MaintenanceOperator maintenanceOperator = new MaintenanceOperator
        {
            Id = 1,
            Name = "John",
            LastName = "Doe",
            Email = "test@test.com",
            Password = "Prueba.1234",
            Building = new Building { Id = 1, Name = "Building" }
        };

        var maintenanceOperatorCreateModel = new MaintenanceOperatorCreateInput
        {
            Name = maintenanceOperator.Name,
            LastName = maintenanceOperator.LastName,
            Email = maintenanceOperator.Email,
            Password = maintenanceOperator.Password,
            BuildingId = maintenanceOperator.Building.Id
        };

        _userServiceMock.Setup(r => r.CreateMaintenanceOperator(It.IsAny<MaintenanceOperator>())).Returns(maintenanceOperator);


        var userController = new UserController(_userServiceMock.Object);

        var result = userController.CreateMaintenanceOperator(maintenanceOperatorCreateModel);
        var okResult = result as OkObjectResult;
        var maintenanceOperatorModel = okResult.Value as MaintenanceOperatorOutput;

        var expectedContent = new MaintenanceOperatorOutput(maintenanceOperator);



        _userServiceMock.VerifyAll();
        Assert.AreEqual(maintenanceOperatorModel, expectedContent);
    }

    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public void TestCreateMaintenanceOperatorWithNoBody()
    {
        MaintenanceOperatorCreateInput maintenanceOperatorCreateModel = null;

        var userController = new UserController(_userServiceMock.Object);

        var result = userController.CreateMaintenanceOperator(maintenanceOperatorCreateModel);

        _userServiceMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestCreateMaintenanceOperatorWithNoName()
    {
        var maintenanceOperator = new MaintenanceOperator
        {
            LastName = "Doe",
            Email = "test@test.com"
        };

        MaintenanceOperatorCreateInput maintenanceOperatorCreateModel = new MaintenanceOperatorCreateInput
        {
            LastName = maintenanceOperator.LastName,
            Email = maintenanceOperator.Email,
            BuildingId = 1,
            Password = "Pass123.!"
        };

        var userController = new UserController(_userServiceMock.Object);

        var result = userController.CreateMaintenanceOperator(maintenanceOperatorCreateModel);

        _userServiceMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestCreateMaintenanceOperatorWithNoLastName()
    {
        var maintenanceOperator = new MaintenanceOperator
        {
            Name = "John",
            Email = "test@test.com"
        };

        MaintenanceOperatorCreateInput maintenanceOperatorCreateModel = new MaintenanceOperatorCreateInput
        {
            Name = maintenanceOperator.Name,
            Email = maintenanceOperator.Email,
            BuildingId = 1,
            Password = "Pass123.!"
        };

        var userController = new UserController(_userServiceMock.Object);

        var result = userController.CreateMaintenanceOperator(maintenanceOperatorCreateModel);

        _userServiceMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestCreateMaintenanceOperatorWithNoEmail()
    {
        _userServiceMock.Setup(r => r.CreateMaintenanceOperator(It.IsAny<MaintenanceOperator>())).Throws(new ArgumentNullException());
        var maintenanceOperator = new MaintenanceOperator
        {
            Name = "John",
            LastName = "Doe"
        };

        MaintenanceOperatorCreateInput maintenanceOperatorCreateModel = new MaintenanceOperatorCreateInput
        {
            Name = maintenanceOperator.Name,
            LastName = maintenanceOperator.LastName,
            BuildingId = 1,
            Password = "Pass123.!"
        };

        var userController = new UserController(_userServiceMock.Object);

        var result = userController.CreateMaintenanceOperator(maintenanceOperatorCreateModel);

        _userServiceMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestCreateMaintenanceOperatorWithNoBuilding()
    {
        _userServiceMock.Setup(r => r.CreateMaintenanceOperator(It.IsAny<MaintenanceOperator>())).Throws(new ArgumentNullException());
        var maintenanceOperator = new MaintenanceOperator
        {
            Name = "John",
            LastName = "Doe",
            Email = "test@test.com",
            Password = "Prueba.1234",
        };

        MaintenanceOperatorCreateInput maintenanceOperatorCreateModel = new MaintenanceOperatorCreateInput
        {
            Name = maintenanceOperator.Name,
            LastName = maintenanceOperator.LastName,
            Email = maintenanceOperator.Email,
            Password = maintenanceOperator.Password,
        };

        var userController = new UserController(_userServiceMock.Object);

        var result = userController.CreateMaintenanceOperator(maintenanceOperatorCreateModel);

        _userServiceMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestCreateMaintenanceOperatorWithNoPassword()
    {
        var maintenanceOperator = new MaintenanceOperator
        {
            Name = "John",
            LastName = "Doe",
            Email = "test@test.com",
            Building = new Building()
        };

        MaintenanceOperatorCreateInput maintenanceOperatorCreateModel = new MaintenanceOperatorCreateInput
        {
            Name = maintenanceOperator.Name,
            LastName = maintenanceOperator.LastName,
            Email = maintenanceOperator.Email,
            BuildingId = maintenanceOperator.Building.Id,
        };

        var userController = new UserController(_userServiceMock.Object);

        var result = userController.CreateMaintenanceOperator(maintenanceOperatorCreateModel);

        _userServiceMock.VerifyAll();
    }

    [TestMethod]
    public void TestDeleteManager()
    {
        int id = 1;

        _userServiceMock.Setup(r => r.DeleteManager(id));

        var userController = new UserController(_userServiceMock.Object);

        var result = userController.DeleteManager(id);

        _userServiceMock.VerifyAll();

        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestDeleteManagerNotFound()
    {
        int id = 1;

        _userServiceMock.Setup(r => r.DeleteManager(id)).Throws(new ArgumentOutOfRangeException());

        var userController = new UserController(_userServiceMock.Object);

        var result = userController.DeleteManager(id);

        _userServiceMock.VerifyAll();
    }

    [TestMethod]
    public void TestCreateCompanyAdministrator()
    {

        CompanyAdministrator companyAdministrator = new CompanyAdministrator
        {
            Id = 1,
            Name = "John",
            LastName = "Doe",
            Email = "john@doe.com",
            Password = "Prueba.1234",
            ConstructionCompany = new ConstructionCompany { Id = 1, Name = "Company" }
        };

        var companyAdministratorCreateModel = new CompanyAdministratorCreateInput
        {
            Name = companyAdministrator.Name,
            LastName = companyAdministrator.LastName,
            Email = companyAdministrator.Email,
            Password = companyAdministrator.Password
        };

        _userServiceMock.Setup(r => r.CreateCompanyAdministrator(It.IsAny<CompanyAdministrator>())).Returns(companyAdministrator);

        var userController = new UserController(_userServiceMock.Object);

        var result = userController.CreateCompanyAdministrator(companyAdministratorCreateModel);
        var okResult = result as OkObjectResult;
        var companyAdministratorModel = okResult.Value as CompanyAdministratorOutput;
        Assert.IsNotNull(companyAdministratorModel);
        var expectedContent = new CompanyAdministratorOutput(companyAdministrator);

        _userServiceMock.VerifyAll();
        Assert.AreEqual(companyAdministratorModel, expectedContent);
    }

    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public void TestCreateCompanyAdministratorWithNoBody()
    {
        CompanyAdministratorCreateInput companyAdministratorCreateModel = null;

        var userController = new UserController(_userServiceMock.Object);

        var result = userController.CreateCompanyAdministrator(companyAdministratorCreateModel);

        _userServiceMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TestCreateCompanyAdministratorWithaUthorizedCurrentUser()
    {
        Manager manager = new Manager
        {
            Id = 1,
            Name = "John",
            Email = "john@doe.com",
            Password = "Prueba.1234"
        };

        CompanyAdministrator companyAdministrator = new CompanyAdministrator
        {
            Id = 1,
            Name = "Test",
            LastName = "Admin",
            Email = "test@admin.com",
            Password = "Prueba.1234",
            ConstructionCompany = new ConstructionCompany { Id = 1, Name = "Company" }
        };

        CompanyAdministratorCreateInput companyAdministratorCreateModel = new CompanyAdministratorCreateInput { Name = companyAdministrator.Name, LastName = companyAdministrator.LastName, Email = companyAdministrator.Email, Password = companyAdministrator.Password};

        _userServiceMock.Setup(r => r.CreateCompanyAdministrator(It.IsAny<CompanyAdministrator>())).Throws(new InvalidOperationException("Current user is not authorized to create a company administrator"));

        var userController = new UserController(_userServiceMock.Object);

        var result = userController.CreateCompanyAdministrator(companyAdministratorCreateModel);
        _userServiceMock.VerifyAll();
           
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestCreateCompanyAdministratorWithNoName()
    {
        CompanyAdministrator companyAdministrator = new CompanyAdministrator
        {
            Id = 1,
            LastName = "Doe",
            Email = "john@doe.com",
            Password = "Prueba.1234"
        };

        CompanyAdministratorCreateInput companyAdministratorCreateModel = new CompanyAdministratorCreateInput
        {
            LastName = companyAdministrator.LastName,
            Email = companyAdministrator.Email,
            Password = companyAdministrator.Password
        };

        var userController = new UserController(_userServiceMock.Object);

        var result = userController.CreateCompanyAdministrator(companyAdministratorCreateModel);
        _userServiceMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestCreateCompanyAdministratorWithExistingUser()
    {
        CompanyAdministrator companyAdministrator = new CompanyAdministrator
        {
            Id = 1,
            Name = "John",
            LastName = "Doe",
            Email = "john@doe.com",
            Password = "Prueba.1234"
        };

        CompanyAdministratorCreateInput companyAdministratorCreateModel = new CompanyAdministratorCreateInput
        {
            Name = companyAdministrator.Name,
            LastName = companyAdministrator.LastName,
            Email = companyAdministrator.Email,
            Password = companyAdministrator.Password
        };

        _userServiceMock.Setup(r => r.CreateCompanyAdministrator(It.IsAny<CompanyAdministrator>())).Throws(new ArgumentException("User already exist"));

        var userController = new UserController(_userServiceMock.Object);

        var result = userController.CreateCompanyAdministrator(companyAdministratorCreateModel);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TestCreateCompanyAdministratorWithNoConstructionCompany()
    {
        CompanyAdministrator companyAdministrator = new CompanyAdministrator
        {
            Id = 1,
            Name = "John",
            LastName = "Doe",
            Email = "john@doe.com",
            Password = "Prueba.1234",
            ConstructionCompany = new ConstructionCompany { Id = 1, Name = "Company" }
        };

        CompanyAdministratorCreateInput companyAdministratorCreateModel = new CompanyAdministratorCreateInput
        {
            Name = companyAdministrator.Name,
            LastName = companyAdministrator.LastName,
            Email = companyAdministrator.Email,
            Password = companyAdministrator.Password
        };

        _userServiceMock.Setup(r => r.CreateCompanyAdministrator(It.IsAny<CompanyAdministrator>())).Throws(new InvalidOperationException("Current user does not have a construction company"));

        var userController = new UserController(_userServiceMock.Object);

        var result = userController.CreateCompanyAdministrator(companyAdministratorCreateModel);
        Assert.IsNotNull(result);

    }
}