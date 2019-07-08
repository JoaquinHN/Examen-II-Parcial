using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
namespace ExamenIIParcialDanny
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        SqlConnection sqlconnection;

        public MainWindow()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ExamenIIParcialDanny.Properties.Settings.ERPConnectionString"].ConnectionString;
            sqlconnection = new SqlConnection(connectionString);
            InitializeComponent();


        }

        private void btnGuardar(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "INSERT INTO Usuarios.usuario(nombre,apellido,nombreUsuario,contrasenia,correo,tipoUsuario) VALUES(@nombre,@apellido,@nombreUs,@contrasenia,@correo,@tipoUsuario)";
                SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);
                //Abrir conexion
                sqlconnection.Open();
                //remplazar el parametro con su valor respectivo
                sqlCommand.Parameters.AddWithValue("@nombre", txtnombre.Text);
                sqlCommand.Parameters.AddWithValue("@apellido", txtapellido.Text);
                sqlCommand.Parameters.AddWithValue("@nombre", txtnombre.Text);
                //Ejecutamos query de insersion
                sqlCommand.ExecuteScalar();
                //Limpiar el valor del texto en txtinformacion
                txtInformacion.Text = string.Empty;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlconnection.Close();
                //Actualizar el Listbox de zoologicos
                MostrarZoologicos();
            }
        }
    }
}
