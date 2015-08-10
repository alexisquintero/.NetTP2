﻿using System;
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

            this.modo = modo;
            this.renombrarBotones();
        }

        public UsuarioDesktop(int ID, ModoForm modo)
            : this()
        {
            //En este nuevo constructor seteamos el modo que ha sido especificado en el parámetro e instanciamos un nuevo objeto de UsuarioLogic y
            //utilizamos el método GetOne para recuperar la entidad Usuario. Entonces la asignamos a la propiedad UsuarioActual e invocaremos al método
            //MapearDeDatos.            UsuarioLogic ul = new UsuarioLogic();
            UsuarioActual = ul.GetOne(ID);
            this.MapearDeDatos();
            this.renombrarBotones();
        }

        public override void MapearDeDatos()
        {

            // txtClave, txtConfirmarClave
            this.txtID.Text = this.UsuarioActual.ID.ToString();
            this.chkHabilitado.Checked = this.UsuarioActual.Habilitado;
            this.txtNombre.Text = this.UsuarioActual.Nombre;
            this.txtApellido.Text = this.UsuarioActual.Apellido;
            this.txtEmail.Text = this.UsuarioActual.Email;
            this.txtUsuario.Text = this.UsuarioActual.NombreUsuario;
//            this.txtClave.Text = "******";
        
        }
        public override void MapearADatos() {

           
            if(modo == ModoForm.Alta){
                UsuarioActual = new Usuario();
            }
           
            if(modo != ModoForm.Alta){
                UsuarioActual.ID = int.Parse(this.txtID.Text);
            }

            this.UsuarioActual.Habilitado = this.chkHabilitado.Checked;
            this.UsuarioActual.Nombre = this.txtNombre.Text;
            this.UsuarioActual.Apellido = this.txtApellido.Text;
            this.UsuarioActual.Email = this.txtEmail.Text;
            this.UsuarioActual.NombreUsuario = this.txtUsuario.Text;

            switch(this.modo)
            {
                case ModoForm.Alta:
                    this.UsuarioActual.State = BusinessEntity.States.New;
                    break;
                case ModoForm.Baja:
                    this.UsuarioActual.State = BusinessEntity.States.Deleted;
                    break;
                case ModoForm.Modificaion:
                    this.UsuarioActual.State = BusinessEntity.States.Modified;
                    break;
                case ModoForm.Consulta:
                    this.UsuarioActual.State = BusinessEntity.States.Unmodified;
                    break;
            }

        
        }
        public override void GuardarCambios()
        {

            MapearADatos();

            UsuarioLogic uL = new UsuarioLogic();

            uL.Save(UsuarioActual);
        
        }
        public override bool Validar()
        {

            bool valido = false;

            if (this.txtID.Text == "" || this.txtApellido.Text == "" || this.txtNombre.Text == "" || this.txtEmail.Text == "" || this.txtUsuario.Text == "" ||
                this.txtClave.Text == "" || this.txtConfirmarClave.Text == "")
            {
                this.Notificar("Existen uno o más campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valido = false;
            };

            if (this.txtClave.Text.Length < 8)
            {
                this.Notificar("La contraseña debe tener por lo menos 8 caracteres", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valido = false;
            }

            if (this.txtClave.Text != this.txtConfirmarClave.Text)
            {
                this.Notificar("Las claves son distintas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valido = false;
            }
/*
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
                this.Notificar("Email inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valido = false;
            }

*/
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
            this.txtID.Enabled = true;
        }

        private void Listar() 
        {
            UsuarioLogic ul = new UsuarioLogic();

            try
            {
                this.dgvUsuarios.DataSource = ul.GetAll();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }

        }

        private void tsbModificar_Click(object sender, EventArgs e)
        {
            if (this.dgvUsuarios.SelectedRows.Count == 1)
            {
                int ID = ((Business.Entities.Usuario)this.dgvUsuarios.SelectedRows[0].DataBoundItem).ID;
                UsuarioDesktop formUsuario = new UsuarioDesktop(ID, ModoForm.Modificaion);
                this.MapearDeDatos();
                this.Listar();
            }
            else
            {
                this.Notificar("Ningún elemento seleccionado", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
            }
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (this.dgvUsuarios.SelectedRows.Count == 1)
            {
                int ID = ((Business.Entities.Usuario)this.dgvUsuarios.SelectedRows[0].DataBoundItem).ID;
                UsuarioDesktop formUsuario = new UsuarioDesktop(ID, ModoForm.Baja);
                this.MapearDeDatos();
                this.Listar();
            }
            else
            {
                this.Notificar("Ningún elemento seleccionado", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
            }
            
        }

        private void renombrarBotones(){
            switch (this.modo)
            {
                case ModoForm.Alta:
                    this.btnAceptar.Name = "Guardar";
                    break;
                case ModoForm.Baja:
                    this.btnAceptar.Name = "Eliminar";
                    break;
                case ModoForm.Modificaion:
                    this.btnAceptar.Name = "Guardar";
                    break;
                default:
                    //this.btnAceptar.Name = "Aceptar";
                    break;
            }
        }
    }
}
