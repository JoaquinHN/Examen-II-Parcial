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
            MostrarUsuario();

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
                sqlCommand.Parameters.AddWithValue("@nombreUs", txtusuario.Text);
                sqlCommand.Parameters.AddWithValue("@contrasenia", txtcontra.Text);
                sqlCommand.Parameters.AddWithValue("@correo", txtcorreo.Text);
                sqlCommand.Parameters.AddWithValue("@tipoUsuario", txttipo.Text);
                //Ejecutamos query de insersion
                sqlCommand.ExecuteNonQuery();
                //Limpiar el valor del texto en txtinformacion
                txtnombre.Text = string.Empty;
                txtapellido.Text = string.Empty;
                txtusuario.Text = string.Empty;
                txtcontra.Text = string.Empty;
                txtcorreo.Text = string.Empty;
                txttipo.Text = string.Empty;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlconnection.Close();
               
            }
        }

        private void BtnEliminarDatos_Click(object sender, RoutedEventArgs e)
        {
            if (lbUsuarios.SelectedValue == null)
                MessageBox.Show("Debes selecionar un Usuario");
            else
            {
                try
                {
                    string query = "DELETE FROM Usuarios.usuario WHERE id=@usId";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);
                    //Abrir la conexion
                    sqlconnection.Open();
                    //Agregar el parametro
                    sqlCommand.Parameters.AddWithValue("@usId", lbUsuarios.SelectedValue);
                    //Ejecutar un query scalar 
                    sqlCommand.ExecuteScalar();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    sqlconnection.Close();
                    MostrarUsuario(); 
                }
            }
        }

        public void MostrarUsuario()
        {
            try
            {
                string query = "SELECT * FROM Usuarios.usuario";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlconnection);
                using (sqlDataAdapter)
                {
                    // Objecto en C# que refleja una tabla de una BD
                    DataTable tablaUsuario = new DataTable();

                    // Llenar el objeto de tipo DataTable
                    sqlDataAdapter.Fill(tablaUsuario);

                    // ¿Cuál información de la tabla en el DataTable debería se desplegada en nuestro ListBox?
                    lbUsuarios.DisplayMemberPath = "nombre";
                   
                    // ¿Qué valor debe ser entregado cuando un elemento de nuestro ListBox es seleccionado?
                    lbUsuarios.SelectedValuePath = "id";
                    // ¿Quién es la referencia de los datos para el ListBox (popular)
                    lbUsuarios.ItemsSource = tablaUsuario.DefaultView;
                }

            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }
        }

        private void BtnActualizarDatos_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
