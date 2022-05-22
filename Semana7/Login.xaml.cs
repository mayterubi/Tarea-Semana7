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
    public partial class Login : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        public Login()
        {
            InitializeComponent();
            _connection = DependencyService.Get<DataBase>().GetConnection();
        }

        public static IEnumerable<Estudiante> Select_Where(SQLiteConnection db, string usuario, string contrasena)
        {
            return db.Query<Estudiante>("SELECT * FROM Estudiante WHERE Usuario = ? AND Contrasena = ?", usuario, contrasena);
        }

        private void btnIngresar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var dataBasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(dataBasePath);
                db.CreateTable<Estudiante>();

                IEnumerable<Estudiante> resultado = Select_Where(db, txtUsuario.Text, txtContrasena.Text);

                if (resultado.Count() > 0)
                    Navigation.PushAsync(new ConsultaRegistro());
                else
                    DisplayAlert("Alerta", "Usuario incorrecto", "Ok");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Registro());
        }
    }
}