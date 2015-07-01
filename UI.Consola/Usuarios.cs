using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Logic;
using Business.Entities;

namespace UI.Consola
{
   public class Usuarios
    {
       public UsuarioLogic UsuarioNegocio { get; set; }

       public void menu() {

           UsuarioNegocio = new UsuarioLogic();

           Console.WriteLine("M E N U");
           Console.WriteLine("1 - Listado General");
           Console.WriteLine("2 - Consulta");
           Console.WriteLine("3 - Agregar");
           Console.WriteLine("4 - Modificar");
           Console.WriteLine("5 - Eliminar");
           Console.WriteLine("6 - Salir");

           int resp = 0;
           resp = int.Parse(Console.ReadLine());
           while (resp > 6 || resp < 1)
           {
               Console.WriteLine("INGRESAR OPCION VALIDA");
               resp = int.Parse(Console.ReadLine());
           }

           switch (resp){
               case 1: ListadoGeneral(); 
                   break;
               case 2: Consultar();;
                   break;
               case 3: Agregar();
                   break;
               case 4: Modificar();
                   break;
               case 5: Eliminar();
                   break;
               case 6: break;}
       }


            public void ListadoGeneral()
            {
                Console.Clear();
                foreach (Usuario usr in UsuarioNegocio.GetAll())
                {
                    MostrarDatos(usr);
                }
            }
       public void MostrarDatos (Usuario usr)
       {
           Console.WriteLine("Usuario: {0}", usr.ID);
           Console.WriteLine("\t\tNombre: {0}", usr.Nombre);
           Console.WriteLine("\t\tApellido: {0}", usr.Apellido);
           Console.WriteLine("\t\tNombre de Usuario: {0}", usr.NombreUsuario);
           Console.WriteLine("\t\tClave: {0}", usr.Clave);
           Console.WriteLine("\t\tEmail: {0}", usr.Email);
           Console.WriteLine("\t\tHabilitado: {0}", usr.Habilitado);
           Console.WriteLine();
           }

       public void Consultar()
       {
           try
           {
               Console.Clear();
               Console.WriteLine("Ingrese ID a buscar");
               int id = int.Parse(Console.ReadLine());
               MostrarDatos(UsuarioNegocio.GetOne(id));
           }
           catch (FormatException fe)
           {
               Console.WriteLine();
               Console.WriteLine("La ID ingresada debe ser un numero entero");
           }
           catch (Exception e)
           {
               Console.WriteLine();
               Console.WriteLine(e.Message);
           }
           finally
           {
               Console.WriteLine("Presione una tecla para continuar");
               Console.ReadKey();
           }
       }
       public void Agregar()
       {
           Usuario usuario = new Usuario();
           Console.Clear();
            Console.WriteLine("Ingrese nombre de un nuevo usuario");
            usuario.Nombre= Console.ReadLine();
            Console.WriteLine("Ingrese apellido de un nuevo usuario");
            usuario.Apellido = Console.ReadLine();
            Console.WriteLine("Ingrese nombre de usuario de un nuevo usuario");
            usuario.NombreUsuario = Console.ReadLine();
            Console.WriteLine("Ingrese clave de un nuevo usuario");
            usuario.Clave = Console.ReadLine();
            Console.WriteLine("Ingrese email de un nuevo usuario");
            usuario.Email = Console.ReadLine();
            Console.Write("Ingrese Habilitacion del Usuario (1-Si / Otro-No) : ");
            usuario.Habilitado = (Console.ReadLine() == "1");
            usuario.State = BusinessEntity.States.New;
            UsuarioNegocio.Save(usuario);
            Console.WriteLine();
            Console.WriteLine("ID: {0}", usuario.ID);

       }
       
       public void Modificar()
       {
           try
           {
               Console.Clear();
               Console.Write("Ingrese el ID del usuario a modificar: ");
               int ID = int.Parse(Console.ReadLine());
               Usuario usuario = UsuarioNegocio.GetOne(ID);
               Console.Write("Ingrese Nombre: ");
               usuario.Nombre = Console.ReadLine();
               Console.Write("Ingrese Apellido: ");
               usuario.Apellido = Console.ReadLine();
               Console.Write("Ingrese Nombre de Usuario: ");
               usuario.NombreUsuario = Console.ReadLine();
               Console.Write("Ingrese Clave: ");
               usuario.Clave = Console.ReadLine();
               Console.Write("Ingrese Email: ");
               usuario.Email = Console.ReadLine();
               Console.Write("Ingrese Habilitacion de Usuario (1-Si /otro-No: ");
               usuario.State = BusinessEntity.States.Modified;
               UsuarioNegocio.Save(usuario);

           }
           catch (FormatException fe)
           {
               Console.WriteLine();
               Console.WriteLine("La ID ingresada debe ser un numero entero");
           }
           catch (Exception e)
           {
               Console.WriteLine();
               Console.WriteLine(e.Message);
           }
           finally
           {
               Console.WriteLine("Presione una tecla para continuar");
               Console.ReadKey();
           }
       }


       public void Eliminar()
       {
           try
           {
               Console.Clear();
               Console.Write("Ingrese el ID del usuario a eliminar: ");
               int ID = int.Parse(Console.ReadLine());
               UsuarioNegocio.Delete(ID);
           }
         catch (FormatException fe)
           {
               Console.WriteLine();
               Console.WriteLine("La ID ingresada debe ser un numero entero");
           }
           catch (Exception e)
           {
               Console.WriteLine();
               Console.WriteLine(e.Message);
           }
           finally
           {
               Console.WriteLine("Presione una tecla para continuar");
               Console.ReadKey();
           }
       }


       
   
       }


    }

