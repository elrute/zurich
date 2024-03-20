/*
 * Alberto Blanco 2024 
 * hola@albertoblanco.dev
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zurich.BusinessLogic.Services.Abstract
{
    public interface IScreenService
    {
        void DrawLine(int x1, int y1, int x2, int y2);
        void DrawCircle(int x, int y, int radius);
        void DrawPoint(int x, int y);
    }

}
