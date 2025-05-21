using Xunit;
using Primarie_Craiova.Controllers;
using Primarie_Craiova.Services.Interfaces;
using Primarie_Craiova.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;

namespace Primarie_Craiova.Tests.Controllers
{
    public class CitizensControllerTests
    {
        private readonly Mock<ICitizensService> _mockService;
        private readonly CitizensController _controller;

        public CitizensControllerTests()
        {
            _mockService = new Mock<ICitizensService>();
            _controller = new CitizensController(_mockService.Object);
        }

        [Fact]
        public async Task Index_ReturnsViewWithAllCitizens_WhenNoSearchName()
        {
            // Arrange
            var citizens = new List<Citizen> { new Citizen { CitizenID = 1, FullName = "Ion Popescu" } };
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(citizens);

            // Act
            var result = await _controller.Index(null) as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsAssignableFrom<List<Citizen>>(result.Model);
            Assert.Single(model);
        }

        [Fact]
        public async Task Index_ReturnsFilteredCitizens_WhenSearchNameIsProvided()
        {
            var citizens = new List<Citizen> { new Citizen { FullName = "Maria Ionescu" } };
            _mockService.Setup(s => s.SearchByNameAsync("Maria")).ReturnsAsync(citizens);

            var result = await _controller.Index("Maria") as ViewResult;

            Assert.NotNull(result);
            var model = Assert.IsAssignableFrom<List<Citizen>>(result.Model);
            Assert.Single(model);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenCitizenIsNull()
        {
            _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync((Citizen)null!);

            var result = await _controller.Details(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsViewWithCitizen_WhenFound()
        {
            var citizen = new Citizen { CitizenID = 1, FullName = "Ion Popescu" };
            _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(citizen);

            var result = await _controller.Details(1) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(citizen, result.Model);
        }

        [Fact]
        public void Create_Get_ReturnsView()
        {
            var result = _controller.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Create_Post_RedirectsToIndex_WhenModelStateIsValid()
        {
            var citizen = new Citizen { FullName = "Test" };

            var result = await _controller.Create(citizen) as RedirectToActionResult;

            _mockService.Verify(s => s.CreateAsync(citizen), Times.Once);
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Create_Post_ReturnsView_WhenModelStateIsInvalid()
        {
            _controller.ModelState.AddModelError("FullName", "Required");

            var citizen = new Citizen();

            var result = await _controller.Create(citizen);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Edit_Get_ReturnsNotFound_WhenCitizenIsNull()
        {
            _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync((Citizen)null!);

            var result = await _controller.Edit(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_Get_ReturnsViewWithCitizen_WhenFound()
        {
            var citizen = new Citizen { CitizenID = 1 };
            _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(citizen);

            var result = await _controller.Edit(1) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(citizen, result.Model);
        }

        [Fact]
        public async Task Edit_Post_ReturnsNotFound_WhenIdMismatch()
        {
            var citizen = new Citizen { CitizenID = 2 };

            var result = await _controller.Edit(1, citizen);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_Post_RedirectsToIndex_WhenUpdateSucceeds()
        {
            var citizen = new Citizen { CitizenID = 1 };
            _mockService.Setup(s => s.UpdateAsync(citizen)).ReturnsAsync(true);

            var result = await _controller.Edit(1, citizen) as RedirectToActionResult;

            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Edit_Post_ReturnsNotFound_WhenUpdateFails()
        {
            var citizen = new Citizen { CitizenID = 1 };
            _mockService.Setup(s => s.UpdateAsync(citizen)).ReturnsAsync(false);

            var result = await _controller.Edit(1, citizen);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_Get_ReturnsNotFound_WhenCitizenIsNull()
        {
            _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync((Citizen)null!);

            var result = await _controller.Delete(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_Get_ReturnsViewWithCitizen_WhenFound()
        {
            var citizen = new Citizen { CitizenID = 1 };
            _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(citizen);

            var result = await _controller.Delete(1) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(citizen, result.Model);
        }

        [Fact]
        public async Task DeleteConfirmed_RedirectsToIndex()
        {
            var result = await _controller.DeleteConfirmed(1) as RedirectToActionResult;

            _mockService.Verify(s => s.DeleteAsync(1), Times.Once);
            Assert.Equal("Index", result.ActionName);
        }
    }
        public class ComplaintsControllerTests
{
    private readonly Mock<IComplaintsService> _mockComplaintsService;
    private readonly Mock<ICitizensService> _mockCitizensService;
    private readonly ComplaintsController _controller;

    public ComplaintsControllerTests()
    {
        _mockComplaintsService = new Mock<IComplaintsService>();
        _mockCitizensService = new Mock<ICitizensService>();

        _controller = new ComplaintsController(
            _mockComplaintsService.Object,
            _mockCitizensService.Object
        );
    }

    [Fact]
    public async Task Index_ReturnsViewWithComplaints()
    {
        var complaints = new List<Complaint> { new Complaint { ComplaintID = 1 } };
        _mockComplaintsService.Setup(s => s.GetAllAsync()).ReturnsAsync(complaints);

        var result = await _controller.Index() as ViewResult;

        Assert.NotNull(result);
        var model = Assert.IsAssignableFrom<List<Complaint>>(result.Model);
        Assert.Single(model);
    }

    [Fact]
    public async Task Details_ReturnsNotFound_WhenComplaintIsNull()
    {
        _mockComplaintsService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync((Complaint)null!);

        var result = await _controller.Details(1);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Details_ReturnsView_WhenComplaintExists()
    {
        var complaint = new Complaint { ComplaintID = 1 };
        _mockComplaintsService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(complaint);

        var result = await _controller.Details(1) as ViewResult;

        Assert.NotNull(result);
        Assert.Equal(complaint, result.Model);
    }

    [Fact]
    public async Task Create_Get_ReturnsViewWithCitizenSelectList()
    {
        var citizens = new List<Citizen> { new Citizen { CitizenID = 1, FullName = "Test" } };
        _mockCitizensService.Setup(s => s.GetAllAsync()).ReturnsAsync(citizens);

        var result = await _controller.Create() as ViewResult;

        Assert.NotNull(result);
        Assert.True(_controller.ViewData.ContainsKey("CitizenID"));
    }

    [Fact]
    public async Task Create_Post_InvalidModel_ReturnsViewWithData()
    {
        _controller.ModelState.AddModelError("Title", "Required");
        var complaint = new Complaint { CitizenID = 1 };

        _mockCitizensService.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<Citizen>());

        var result = await _controller.Create(complaint) as ViewResult;

        Assert.NotNull(result);
        Assert.Equal(complaint, result.Model);
    }

    [Fact]
    public async Task Create_Post_ValidModel_RedirectsToIndex()
    {
        var complaint = new Complaint { ComplaintID = 1 };

        var result = await _controller.Create(complaint) as RedirectToActionResult;

        _mockComplaintsService.Verify(s => s.CreateAsync(complaint), Times.Once);
        Assert.Equal("Index", result.ActionName);
    }

    [Fact]
    public async Task Edit_Get_ReturnsNotFound_WhenComplaintIsNull()
    {
        _mockComplaintsService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync((Complaint)null!);

        var result = await _controller.Edit(1);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Edit_Get_ReturnsViewWithCitizenList()
    {
        var complaint = new Complaint { ComplaintID = 1, CitizenID = 2 };
        var citizens = new List<Citizen> { new Citizen { CitizenID = 2, FullName = "Test" } };

        _mockComplaintsService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(complaint);
        _mockCitizensService.Setup(s => s.GetAllAsync()).ReturnsAsync(citizens);

        var result = await _controller.Edit(1) as ViewResult;

        Assert.NotNull(result);
        Assert.Equal(complaint, result.Model);
    }

    [Fact]
    public async Task Edit_Post_IdMismatch_ReturnsNotFound()
    {
        var complaint = new Complaint { ComplaintID = 2 };

        var result = await _controller.Edit(1, complaint);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Edit_Post_InvalidModel_ReturnsView()
    {
        _controller.ModelState.AddModelError("Title", "Required");
        var complaint = new Complaint { ComplaintID = 1, CitizenID = 1 };
        _mockCitizensService.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<Citizen>());

        var result = await _controller.Edit(1, complaint);

        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public async Task Edit_Post_FailedUpdate_ReturnsNotFound()
    {
        var complaint = new Complaint { ComplaintID = 1 };
        _mockComplaintsService.Setup(s => s.UpdateAsync(complaint)).ReturnsAsync(false);

        var result = await _controller.Edit(1, complaint);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Edit_Post_SuccessfulUpdate_RedirectsToIndex()
    {
        var complaint = new Complaint { ComplaintID = 1 };
        _mockComplaintsService.Setup(s => s.UpdateAsync(complaint)).ReturnsAsync(true);

        var result = await _controller.Edit(1, complaint) as RedirectToActionResult;

        Assert.Equal("Index", result.ActionName);
    }

    [Fact]
    public async Task Delete_Get_ReturnsNotFound_WhenComplaintIsNull()
    {
        _mockComplaintsService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync((Complaint)null!);

        var result = await _controller.Delete(1);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Delete_Get_ReturnsView_WhenComplaintExists()
    {
        var complaint = new Complaint { ComplaintID = 1 };
        _mockComplaintsService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(complaint);

        var result = await _controller.Delete(1) as ViewResult;

        Assert.Equal(complaint, result.Model);
    }

    [Fact]
    public async Task DeleteConfirmed_FailedDeletion_ReturnsNotFound()
    {
        _mockComplaintsService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(false);

        var result = await _controller.DeleteConfirmed(1);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteConfirmed_SuccessfulDeletion_RedirectsToIndex()
    {
        _mockComplaintsService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(true);

        var result = await _controller.DeleteConfirmed(1) as RedirectToActionResult;

        Assert.Equal("Index", result.ActionName);
    }
}
}
