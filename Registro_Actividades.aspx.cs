using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Registro_InfoToolsSV
{
    public partial class Registro_Actividades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblErrorContrasenia.Text = "";
            LeerDatos();
        }
        SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ConnectionString);

        void Limpiar()
        {
            tbNombres.Text = "";
            tbDescripcion.Text = "";
            tbFechaActividad.Text = "";
            tbUsuario.Text = "";
            tbConfirmarContrasenia.Text = "";
            tbContrasenia.Text = "";
            lblError.Text = "";
            lblErrorContrasenia.Text = "";
        }
        protected void BtnRegistrar_Click(Object sender, EventArgs e)
        {
            if (tbNombres.Text == "" || tbDescripcion.Text == "" || tbFechaActividad.Text == "" || tbUsuario.Text == "" || tbConfirmarContrasenia.Text == "" || tbContrasenia.Text == "")
            {
                lblError.Text = "Ningun campo puede quedar vacío!";
            }
            else
            {
                if (tbContrasenia.Text != tbConfirmarContrasenia.Text)
                {
                    lblErrorContrasenia.Text = "Las contrasenias no coinciden!";
                }
                else
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("select count(*) from Usuarios where Usuario='"+tbUsuario.Text+"'", conexion)
                    {
                        CommandType = System.Data.CommandType.Text
                    };
                    cmd.Parameters.AddWithValue("Usuario", tbUsuario.Text);
                    int usuario = Convert.ToInt32(cmd.ExecuteScalar());
                    string patron = "InfoToolsSV";
                    if (usuario < 1)
                    {
                        SqlCommand cmm = new SqlCommand("Insert into Usuarios values('"+tbNombres.Text+"','"+tbDescripcion.Text+"'," +
                            "'"+tbFechaActividad.Text+"','"+tbUsuario.Text+"',(EncryptByPassPhrase('"+patron+"','"+tbContrasenia.Text+"')))",conexion);
                        cmm.ExecuteNonQuery();
                        conexion.Close();
                        Limpiar();
                        LeerDatos();
                    }
                    else
                    {
                        lblError.Text = "El Usuario "+ tbUsuario.Text+" ya existe!";
                        tbUsuario.Text = "";
                    }    
                }
            }
        }

        void LeerDatos()
        {
            SqlCommand leerdatos = new SqlCommand("Select * from Usuarios",conexion);
            SqlDataAdapter da = new SqlDataAdapter(leerdatos);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvUsuarios.DataSource = dt;
            gvUsuarios.DataBind();
        }
    }
}