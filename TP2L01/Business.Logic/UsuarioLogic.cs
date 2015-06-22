using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Database;
using Business.Entities;

namespace Business.Logic
{
    public class UsuarioLogic : BusinessLogic 
    {
        private UsuarioAdapter UsuarioData { get; set; } 

        public UsuarioLogic()
        {
          UsuarioData = new UsuarioAdapter();
        }
        public Usuario GetOne(int id) {
            return UsuarioData.GetOne(id);
        }
        public List<Usuario> GetAll() {
            
            return UsuarioData.GetAll();
        }
        public void Save(Usuario u1) {
        UsuarioData.Save(u1); }
        public void Delete(int id) {
            UsuarioData.Delete(id);

        }

    }
}
