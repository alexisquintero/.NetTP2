using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class Usuario : BusinessEntity
    {
        private string NombreUsuario;
        private string Clave;
        private string Nombre;
        private string Apellido;
        private string Email;
        private bool Habilitado;
    }
}
