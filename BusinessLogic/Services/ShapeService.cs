/*
 * Alberto Blanco 2024 
 * hola@albertoblanco.dev
 * 
 */

using BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zurich.BusinessLogic.Repository.Abstract;
using Zurich.BusinessLogic.Services.Abstract;

namespace Zurich.BusinessLogic.Services
{
    public class ShapeService : IShapeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IScreenService _screen;

        public ShapeService(IUnitOfWork unitOfWork, IScreenService screen)
        {
            _unitOfWork = unitOfWork;
            _screen = screen;
        }

        public async Task<IEnumerable<Shape>> ReadAllObjectsAsync()
        {
            var points = await _unitOfWork.PointRepository.GetAllAsync();
            var circles = await _unitOfWork.CircleRepository.GetAllAsync();
            var rectangles = await _unitOfWork.RectangleRepository.GetAllAsync();
            var squares = await _unitOfWork.SquareRepository.GetAllAsync();

            return points.Select(p => p as Shape)
                .Concat(circles.Select(c => c as Shape))
                .Concat(rectangles.Select(c => c as Shape))
                .Concat(squares.Select(c => c as Shape)).ToList();            
        }

        public async Task MoveAllObjectsAsync(int offsetX, int offsetY)
        {
            // Move Points
            var points = await _unitOfWork.PointRepository.GetAllAsync();
            foreach (var point in points)
            {
                point.X += offsetX;
                point.Y += offsetY;
                _unitOfWork.PointRepository.Update(point);
            }

            // Move Circles
            var circles = await _unitOfWork.CircleRepository.GetAllAsync();
            foreach (var circle in circles)
            {
                circle.X += offsetX;
                circle.Y += offsetY;
                _unitOfWork.CircleRepository.Update(circle);
            }

            // Move Rectangles
            var rectangles = await _unitOfWork.RectangleRepository.GetAllAsync();
            foreach (var rectangle in rectangles)
            {
                rectangle.X1 += offsetX;
                rectangle.Y1 += offsetY;
                rectangle.X2 += offsetX;
                rectangle.Y2 += offsetY;
                _unitOfWork.RectangleRepository.Update(rectangle);
            }

            // Move Squares
            var squares = await _unitOfWork.SquareRepository.GetAllAsync();
            foreach (var square in squares)
            {
                square.X1 += offsetX;
                square.Y1 += offsetY;
                // Assuming squares are defined similarly to rectangles but with a side length
                _unitOfWork.SquareRepository.Update(square);
            }

            await _unitOfWork.CompleteAsync();
        }

        public async Task DisplayAllObjectsAsync()
        {
            // Display Points
            var points = await _unitOfWork.PointRepository.GetAllAsync();
            foreach (var point in points)
            {
                _screen.DrawPoint(point.X, point.Y);
            }

            // Display Circles
            var circles = await _unitOfWork.CircleRepository.GetAllAsync();
            foreach (var circle in circles)
            {
                _screen.DrawCircle(circle.X, circle.Y, circle.Radius);
            }

            // Display Rectangles
            var rectangles = await _unitOfWork.RectangleRepository.GetAllAsync();
            foreach (var rectangle in rectangles)
            {
                // Assuming top-left and bottom-right corner drawing for simplicity
                _screen.DrawLine(rectangle.X1, rectangle.Y1, rectangle.X2, rectangle.Y1); // Top edge
                _screen.DrawLine(rectangle.X2, rectangle.Y1, rectangle.X2, rectangle.Y2); // Right edge
                _screen.DrawLine(rectangle.X2, rectangle.Y2, rectangle.X1, rectangle.Y2); // Bottom edge
                _screen.DrawLine(rectangle.X1, rectangle.Y2, rectangle.X1, rectangle.Y1); // Left edge
            }

            // Display Squares (similar to rectangles)
            var squares = await _unitOfWork.SquareRepository.GetAllAsync();
            foreach (var square in squares)
            {
                // Simplified as a rectangle for display
                _screen.DrawLine(square.X1, square.Y1, square.X1 + square.Side, square.Y1);
                _screen.DrawLine(square.X1 + square.Side, square.Y1, square.X1 + square.Side, square.Y1 + square.Side);
                _screen.DrawLine(square.X1 + square.Side, square.Y1 + square.Side, square.X1, square.Y1 + square.Side);
                _screen.DrawLine(square.X1, square.Y1 + square.Side, square.X1, square.Y1);
            }
        }

    }

}
