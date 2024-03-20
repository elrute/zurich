/*
 * Alberto Blanco 2024 
 * hola@albertoblanco.dev
 * 
 */


using BusinessLogic.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zurich.BusinessLogic.Repository.Abstract;
using Zurich.BusinessLogic.Services;
using Zurich.BusinessLogic.Services.Abstract; 

namespace Zurich.BusinessLogic.Tests {

    [TestClass]
    public class ShapeServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IScreenService> _mockScreenService;
        private readonly ShapeService _shapeService;

        public ShapeServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockScreenService = new Mock<IScreenService>();

            // If ShapeService takes more dependencies, mock and inject them as well
            _shapeService = new ShapeService(_mockUnitOfWork.Object, _mockScreenService.Object);
        }

        [TestMethod]
        public async Task GetAllShapesAsync_ReturnsShapes()
        {
            // Assuming ShapeService.GetAllShapesAsync aggregates shapes from all repositories
            var fakePoints = new List<Point> { new Point() };
            var fakeCircles = new List<Circle> { new Circle() };
            var fakeRectangles = new List<Rectangle> { new Rectangle() };
            var fakeSquares = new List<Square> { new Square() };

            _mockUnitOfWork.Setup(uow => uow.PointRepository.GetAllAsync()).ReturnsAsync(fakePoints);
            _mockUnitOfWork.Setup(uow => uow.CircleRepository.GetAllAsync()).ReturnsAsync(fakeCircles);
            _mockUnitOfWork.Setup(uow => uow.RectangleRepository.GetAllAsync()).ReturnsAsync(fakeRectangles);
            _mockUnitOfWork.Setup(uow => uow.SquareRepository.GetAllAsync()).ReturnsAsync(fakeSquares);

            var result = await _shapeService.ReadAllObjectsAsync();

            Assert.IsNotNull(result);
            // Further assertions can be made based on the expected structure of the result
        }

        [TestMethod]
        public async Task MoveShapeAsync_ChangesShapePosition()
        {
            // Setup initial state
            var shapeId = 1; // Example ID
            var offsetX = 5;
            var offsetY = 10;
            var fakeShape = new Circle { Id = shapeId, X = 20, Y = 30, Radius = 10 };

            _mockUnitOfWork.Setup(uow => uow.CircleRepository.GetByIdAsync(shapeId))
                           .ReturnsAsync(fakeShape);

            // Act: Move the shape
            await _shapeService.MoveAllObjectsAsync(offsetX, offsetY);

            // Assert
            Assert.AreEqual(25, fakeShape.X); // 20 + 5
            Assert.AreEqual(40, fakeShape.Y); // 30 + 10
            _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);

        }
    }

}