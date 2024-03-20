/*
 * Alberto Blanco 2024 
 * hola@albertoblanco.dev
 * 
 */

using BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Zurich.DataAccess
{

    public class ShapesContext : DbContext
    {
        public DbSet<Point> Points { get; set; }
        public DbSet<Circle> Circles { get; set; }
        public DbSet<Rectangle> Rectangles { get; set; }
        public DbSet<Square> Squares { get; set; }

        public ShapesContext(DbContextOptions<ShapesContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }

}
