using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            Usuarios u = new Usuarios();
            u.menu();
            Console.ReadLine();
            
    

            // new Usuarios().Menu();
            //Esta escrito asi tp2 3.4 paso 9 pero da error.
        }
    }
}
