using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Semana7.Models;
using System.IO;

namespace Semana7
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registro : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        public Registro()
        {
            InitializeComponent();
            _connection = DependencyService.Get<DataBase>().GetConnection();
        }

        private void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var datosRegistro = new Estudiante { Nombre = txtNombre.Text, Usuario = txtUsuario.Text, Contrasena = txtContrasena.Text };
                _connection.InsertAsync(datosRegistro);
                Navigation.PushAsync(new Login());
            }
            catch (Exception ex)
            {

                DisplayAlert("Alerta", ex.Message, "Ok");
            }
        }
    }
}