using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business.Logic;
using Business.Entities;

namespace UI.Desktop
{
    public partial class UsuarioDesktop : ApplicationForm
    {
        public UsuarioDesktop()
        {
            InitializeComponent();
        }
        public UsuarioDesktop(ModoForm modo):this()
        {
            this.modo = modo;
        }
        public UsuarioDesktop(int ID, ModoForm modo)
            : this()
        {
            this.modo = modo;
            UsuarioLogic usuarioLogic = new UsuarioLogic();
            this.UsuarioActual = usuarioLogic.GetOne(ID);
            this.MapearDeDatos();
        }
        public Usuario UsuarioActual;
        new public virtual void MapearDeDatos() //El new fue agregado para que no muestre el
                                                // "Hides from inherited member" 
        {
            this.txtID.Text = this.UsuarioActual.ID.ToString();
            this.chkHabilitado.Checked = this.UsuarioActual.Habilitado;
            this.txtNombre.Text = this.UsuarioActual.Nombre;
            this.txtApellido.Text = this.UsuarioActual.Apellido;
            this.txtEmail.Text = this.UsuarioActual.Email;
            this.txtUsuario.Text = this.UsuarioActual.NombreUsuario;

            switch (this.modo)
            {
                case ModoForm.Alta: this.btnAceptar.Text = "Guardar"; break;
                case ModoForm.Modificacion: this.btnAceptar.Text = "Guardar"; break;
                case ModoForm.Baja: this.btnAceptar.Text = "Eliminar"; break;
                case ModoForm.Consulta: this.btnAceptar.Text = "Aceptar"; break;
            }
        }
        new public virtual void MapearADatos()
        {
            if (this.modo.Equals(ModoForm.Alta))
            {
                this.UsuarioActual = new Usuario();
            }
            if (this.modo.Equals(ModoForm.Alta) || this.modo.Equals(ModoForm.Modificacion))
            {
                if(this.modo.Equals(ModoForm.Modificacion))
                {
                       this.UsuarioActual.ID = int.Parse(this.txtID.Text);
                }
                this.UsuarioActual.Habilitado = this.chkHabilitado.Checked;
                this.UsuarioActual.Nombre = this.txtNombre.Text;
                this.UsuarioActual.Apellido = this.txtApellido.Text;
                this.UsuarioActual.Email = this.txtEmail.Text;
                this.UsuarioActual.NombreUsuario = this.txtUsuario.Text;
            }
            switch (this.modo)
            {
                case ModoForm.Alta: this.UsuarioActual.State = BusinessEntity.States.New; break;
                case ModoForm.Modificacion:this.UsuarioActual.State = BusinessEntity.States.Modified; break;
                case ModoForm.Baja: this.UsuarioActual.State = BusinessEntity.States.Deleted; break;
                case ModoForm.Consulta: this.UsuarioActual.State = BusinessEntity.States.Unmodified; break;
            }
        }
        new public virtual void GuardarCambios()
        {
            this.MapearADatos();
            UsuarioLogic usuarioLogic = new UsuarioLogic();
            usuarioLogic.Save(this.UsuarioActual);
        }
        new public virtual bool Validar() 
        {
            if(this.txtID.Text.Equals("")){
                this.Notificar("ID vacía", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(this.txtNombre.Text.Equals("")){
                this.Notificar("Nombre vacío", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(this.txtApellido.Text.Equals("")){
                this.Notificar("Apellido vacío", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(this.txtEmail.Text.Equals("")){
                this.Notificar("Email vacío", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (this.txtUsuario.Text.Equals(""))
            {
                this.Notificar("Usuario vacío", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (this.txtClave.Text.Equals(""))
            {
                this.Notificar("Clave vacía", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (this.txtConfirmarClave.Text.Equals(""))
            {
                this.Notificar("Confirmar clave vacía", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (this.txtClave.Text.Length < 8)
            {
                this.Notificar("La clave debe tener 8 caracteres como mínimo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(this.txtClave.Text != this.txtConfirmarClave.Text)
            {
                this.Notificar("Las claves no coinciden", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //TODO: validar email
            return true;                                       
        }

        private void lblHabilitado_Click(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (this.Validar()) 
            {
                this.GuardarCambios();
                Close();
            }             
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
