using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class ConsultaRegistro : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<Estudiante> tablaEstudiantes;
        public ConsultaRegistro()
        {
            InitializeComponent();
            _connection = DependencyService.Get<DataBase>().GetConnection();
            Get();
        }

        public async void Get()
        {
            try
            {
                var resultado = await _connection.Table<Estudiante>().ToListAsync();
                tablaEstudiantes = new ObservableCollection<Estudiante>(resultado);
                listaUsuarios.ItemsSource = tablaEstudiantes;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void listaUsuarios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Estudiante)e.SelectedItem;
            var item = obj.Id.ToString();
            var id = int.Parse(item);
            var nombre = obj.Nombre.ToString();
            var usuario = obj.Usuario.ToString();
            var contrasena = obj.Contrasena.ToString();
            Navigation.PushAsync(new Elemento(id, nombre, usuario, contrasena));
        }
    }
}