using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    class DocenteCurso : BusinessEntity
    {
        public TiposCargo Cargo { get; set; }
        public int IDCurso { get; set; }
        public int IDDocente { get; set; }
    }

    public enum TiposCargo { }
}
