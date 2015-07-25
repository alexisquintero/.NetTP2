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
using System.Text.RegularExpressions;

namespace UI.Desktop
{
    public partial class UsuarioDesktop : ApplicationForm
    {
        public UsuarioDesktop()
        {
            InitializeComponent();
        }

        public Usuario UsuarioActual;

        public UsuarioDesktop(ModoForm modo)
            : this()
        {
            //Internamete debe setear a ModoForm en el modo enviado, este constructor servirá para las altas.
        }

        public UsuarioDesktop(int ID, ModoForm modo)
            : this()
        {
            //En este nuevo constructor seteamos el modo que ha sido especificado en el parámetro e instanciamos un nuevo objeto de UsuarioLogic y
            //utilizamos el método GetOne para recuperar la entidad Usuario. Entonces la asignamos a la propiedad UsuarioActual e invocaremos al método
            //MapearDeDatos.            UsuarioLogic ul = new UsuarioLogic();
            UsuarioActual = ul.GetOne(ID);
            this.MapearDeDatos();
        }

        public virtual void MapearDeDatos() {

            // txtClave, txtConfirmarClave
            this.txtID.Text = this.UsuarioActual.ID.ToString();
            this.chkHabilitado.Checked = this.UsuarioActual.Habilitado;
            this.txtNombre.Text = this.UsuarioActual.Nombre;
            this.txtApellido.Text = this.UsuarioActual.Apellido;
            this.txtEmail.Text = this.UsuarioActual.Email;
            this.txtUsuario.Text = this.UsuarioActual.NombreUsuario;
//            this.txtClave.Text = "******";

            //Dentro del mismo método setearemos el texto del botón Aceptar en función del Modo del formulario de esta forma 
            //Alta o Modificación Guardar 
            //Baja Eliminar 
            //Consulta Aceptar

//TODO: Arreglar función con enum.
            int i = 5;
            switch( i)
            {
                case 0:
                    this.btnAceptar.Name = "Guardar";
                    break;
                case 1:
                    this.btnAceptar.Name = "Eliminar";
                    break;
                case 2:
                    this.btnAceptar.Name = "Guardar";
                    break;
                default:
                    //this.btnAceptar.Name = "Aceptar";
                    break;
            }
        
        }
        public virtual void MapearADatos() {

//TODO: arreglar, enum            if(Modo del formulario es Alta){
                UsuarioActual = new Usuario();
//TODO: getid de algun lado, método de clase?                UsuarioActual.ID = 0; 
//            }

//TODO: arreglar, enum            if(Modo del formulario es Alta){
                UsuarioActual.ID = int.Parse(this.txtID.Text);
//            }

            this.UsuarioActual.Habilitado = this.chkHabilitado.Checked;
            this.UsuarioActual.Nombre = this.txtNombre.Text;
            this.UsuarioActual.Apellido = this.txtApellido.Text;
            this.UsuarioActual.Email = this.txtEmail.Text;
            this.UsuarioActual.NombreUsuario = this.txtUsuario.Text;

//TODO: arreglar, enum            this.UsuarioActual.State = ModoForm;

        
        }
        public virtual void GuardarCambios() {

            MapearADatos();

            UsuarioLogic uL = new UsuarioLogic();

            uL.Save(UsuarioActual);
        
        }
        public virtual bool Validar() {

            bool valido = false;

            if (this.txtID.Text == "" || this.txtApellido.Text == "" || this.txtNombre.Text == "" || this.txtEmail.Text == "" || this.txtUsuario.Text == "" ||
                this.txtClave.Text == "" || this.txtConfirmarClave.Text == "")
            {
//TODO: agregar parámetros                this.Notificar();
//                valido = false;
            };

            if (this.txtClave.Text.Length < 8)
            {
//TODO: agregar parámetros                this.Notificar();
//                valido = false;
            }

            if(this.txtClave.Text != this.txtConfirmarClave.Text )
            {
//TODO: agregar parámetros                this.Notificar();
//                valido = false;
            }

            string rgxPattern = "^[^\\r\\n\\t\\f@ ]+(@)\\S(\\.)[a-zA-Z]{3}((\\.)[a-zA-Z]{2})?$" ; 
            Regex rgx = new Regex(rgxPattern, RegexOptions.IgnoreCase);  
            MatchCollection matches;
            string[] cadenas = this.txtEmail.Text.Split('@');

//Método 1, con regex

            matches = rgx.Matches(this.txtEmail.Text);
            if (matches.Count == 1)
            {
                valido = true;
            }
            else   // email invalido
            {
//TODO: agregar parámetros                this.Notificar();
//                valido = false;
            }


 //Método 2, sin regex
 /*           if (cadenas.Length == 2)  // 1 solo @
            {
                if(cadenas[0].Length > 0)  // no empieza con @
                {
                    cadenas = cadenas[1].Split('.');
                    if(cadenas.Length == 2)  // 1 punto despues del @
                    {
                        if(cadenas[1].Length == 3)  // 3 caracteres despues del punto; .com, .edu, etc.
                        {
                            valido = true;
                        }
                    }
                    if(cadenas.Length == 3)  // 2 puntos despues del @
                    {
                        if (cadenas[1].Length == 3)  // 3 caracteres despues del primer punto; .com, .edu, etc.
                        {
                            if (cadenas[2].Length == 2)  // 2 caracteres despues del segundo punto; .ar, .it, etc.
                            {
                                valido = true;
                            }
                        }
                    }
                }
            }
*/
   
            return valido;        
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                GuardarCambios();
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();  
            /* "20.También creamos el manejador del evento clic para el botón salir y allí
utilizamos el método"         no dice nada más */
        }
    


        private void tsbNuevo_Click_1(object sender, EventArgs e)
        {
            UsuarioDesktop formUsuario = new UsuarioDesktop(ApplicationForm.ModoForm.Alta);
            formUsuario.ShowDialog();
            this.Listar();  
        }

        private void Listar()
        {
//TODO: Hacer el método Listar, refresca la grilla
        }

        private void tsbModificar_Click(object sender, EventArgs e)
        {
            this.MapearDeDatos();
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            this.MapearDeDatos();
        }

//TODO:
/*24.De manera similar llamar al Formulario crear los manejadores de eventos
para el Editar y el Eliminar. Utilizando el constructor de
UsuarioDesktop que requiere enviar el ID y el Modo
Para obtener el ID podemos hacerlo de la siguiente forma
int ID = ((Business.Entities.Usuario)this.dgvUsuarios.SelectedRows[0].DataBoundItem).ID;
Para poder utilizar dicha línea de código debemos:
• Asegurarnos que haya una fila seleccionada (controlando que
this.dgvUsuarios.SelectedRows tenga elementos dentro)
• Permitir que se selecciones una única fila (Propiedad MultiSelect de
la grilla en false)
• Que al hacer clic en una celda se seleccione una fila entera
(Propiedad SelectionMode de la grilla en FullRowSelect)*/

//TODO: vincular grilla con un List<Usuario> = devolución del getAll()

    }
}
