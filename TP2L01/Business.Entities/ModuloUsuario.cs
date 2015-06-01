using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class ModuloUsuario : BusinessEntity
    {
        private int IdUsuario;
        private int IdModulo;
        private bool PermiteAlta;
        private bool PermiteBaja;
        private bool PermiteModificacion;
        private bool PermiteConsulta;
    }
}
