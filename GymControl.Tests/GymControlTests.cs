using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Data.SqlClient;

namespace GymControl.Tests
{
    [TestClass]
    public  class GymControlTests
    {
        // Cadena de conexión para la base de datos de pruebas
        private static string connectionString = "data source=DESKTOP-3HP58V7\\SQLEXPRESS;initial catalog=GymControl;trusted_connection=true;";



        [TestMethod]
        public void ValidarEntrada_NoDebePermitirValoresVacios()
        {
            // Simulamos entrada vacía
           Console.SetIn(new System.IO.StringReader("\n"));
            string resultado = Program.ValidarEntrada();
            Assert.IsTrue(string.IsNullOrEmpty(resultado), "La entrada no puede estar vacía.");

            Console.SetIn(new StringReader("\nPrueba válida\n"));

            string resulta = Program.ValidarEntrada();

            Assert.AreEqual("Prueba válida", resulta);

        }


        [TestMethod]
        public void InsertarUsuario_DebeInsertarUsuarioEnLaBD()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "INSERT INTO Usuarios (id_usuario, nombre, apellido, fecha_nacimiento, genero, correo, telefono) " +
                               "VALUES (@id_usuario, @nombre, @apellido, @fecha_nacimiento, @genero, @correo, @telefono)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_usuario", "1234");
                    cmd.Parameters.AddWithValue("@nombre", "Juan");
                    cmd.Parameters.AddWithValue("@apellido", "Pérez");
                    cmd.Parameters.AddWithValue("@fecha_nacimiento", "1990-05-15");
                    cmd.Parameters.AddWithValue("@genero", "Masculino");
                    cmd.Parameters.AddWithValue("@correo", "juan@example.com");
                    cmd.Parameters.AddWithValue("@telefono", "5551234");

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    Assert.IsTrue(filasAfectadas > 0, "El usuario debería insertarse correctamente.");
                }
            }
        }


        [TestMethod]
        public void ModificarUsuario_DebeActualizarUsuarioEnLaBD()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "UPDATE Usuarios SET nombre = @nombre WHERE id_usuario = @id_usuario";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_usuario", "1234");
                    cmd.Parameters.AddWithValue("@nombre", "Carlos");

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    Assert.IsTrue(filasAfectadas > 0, "El usuario debería actualizarse correctamente.");
                }
            }
        }



        [TestMethod]
        public void InsertarMedidas_DebeInsertarMedidasEnLaBD()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "INSERT INTO Registro_Medidas (id_usuario, fecha_registro, biceps_derecho, biceps_izquierdo, pecho) " +
                               "VALUES (@id_usuario, @fecha_registro, @biceps_derecho, @biceps_izquierdo, @pecho)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_usuario", "1234");
                    cmd.Parameters.AddWithValue("@fecha_registro", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@biceps_derecho", 40);
                    cmd.Parameters.AddWithValue("@biceps_izquierdo", 39);
                    cmd.Parameters.AddWithValue("@pecho", 110);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    Assert.IsTrue(filasAfectadas > 0, "Las medidas deberían insertarse correctamente.");
                }
            }
        }



        [TestMethod]
        public void ModificarMedidas_DebeActualizarMedidasEnLaBD()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "UPDATE Registro_Medidas SET biceps_derecho = @biceps_derecho WHERE id_usuario = @id_usuario";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_usuario", "1234");
                    cmd.Parameters.AddWithValue("@biceps_derecho", 42);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    Assert.IsTrue(filasAfectadas > 0, "Las medidas deberían actualizarse correctamente.");
                }
            }
        }
    



    } // fin class 
} // fin namespace
