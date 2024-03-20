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

namespace Zurich.BusinessLogic.Repository.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Point> PointRepository { get; }
        IRepository<Circle> CircleRepository { get; }
        IRepository<Rectangle> RectangleRepository { get; }
        IRepository<Square> SquareRepository { get; }
        Task<int> CompleteAsync();
    }
}
