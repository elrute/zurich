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

namespace Zurich.BusinessLogic.Services.Abstract
{
    public interface IShapeService
    {
        Task<IEnumerable<Shape>> ReadAllObjectsAsync();
        Task MoveAllObjectsAsync(int offsetX, int offsetY);
        Task DisplayAllObjectsAsync();

    }
}
