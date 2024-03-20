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
using Zurich.DataAccess;

namespace DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShapesContext _context;

        public UnitOfWork(ShapesContext context)
        {
            _context = context;
            PointRepository = new Repository<Point>(_context);
            CircleRepository = new Repository<Circle>(_context);
            RectangleRepository = new Repository<Rectangle>(_context);
            SquareRepository = new Repository<Square>(_context);
        }

        public IRepository<Point> PointRepository { get; }
        public IRepository<Circle> CircleRepository { get; }
        public IRepository<Rectangle> RectangleRepository { get; }
        public IRepository<Square> SquareRepository { get; }

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
