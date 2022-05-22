using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Semana7.Models;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Semana7
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Elemento : ContentPage
    {
        public int idSeleccionado;
        public string nombreSeleccionado;
        public string usuarioSeleccionado;
        public string contrasenaSeleccionado;
        private SQLiteAsyncConnection _connection;
        IEnumerable<Estudiante> _rUpdate;
        IEnumerable<Estudiante> _rDelete;
        public Elemento(int id, string nombre, string usuario, string contrasena)
        {
            InitializeComponent();
            idSeleccionado = id;
            nombreSeleccionado = nombre;
            usuarioSeleccionado = usuario;
            contrasenaSeleccionado = contrasena;
            txtNombre.Text = nombreSeleccionado;
            txtUsuario.Text = usuarioSeleccionado;
            txtContrasena.Text = contrasenaSeleccionado;
            _connection = DependencyService.Get<DataBase>().GetConnection();
        }

        public static IEnumerable<Estudiante> Delete(SQLiteConnection db, int id)
        {
            return db.Query<Estudiante>("DELETE FROM Estudiante WHERE Id = ?", id);
        }

        public static IEnumerable<Estudiante> Actualizar(SQLiteConnection db, int id, string nombre, string usuario, string contrasena)
        {
            return db.Query<Estudiante>("UPDATE Estudiante SET Nombre = ?, Usuario = ?, Contrasena = ? WHERE Id = ?", nombre, usuario, contrasena, id);
        }

        private void btnActualizar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var dataBasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(dataBasePath);
                _rUpdate = Actualizar(db, idSeleccionado, txtNombre.Text, txtUsuario.Text, txtContrasena.Text);
                DisplayAlert("Alerta", "Registro actualizado de manera exitosa", "Ok");
                Navigation.PushAsync(new ConsultaRegistro());
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var dataBasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(dataBasePath);
                _rDelete = Delete(db, idSeleccionado);
                DisplayAlert("Alerta", "Registro eliminado de manera exitosa", "Ok");
                Navigation.PushAsync(new ConsultaRegistro());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}