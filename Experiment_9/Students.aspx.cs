using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace Experiment_9
{
    public partial class Students : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStudents();
            }
        }

        private void LoadStudents()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PlacementDB"].ConnectionString;

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM Students";

                MySqlDataAdapter da = new MySqlDataAdapter(query, con);

                DataTable dt = new DataTable();

                da.Fill(dt);

                gvStudents.DataSource = dt;
                gvStudents.DataBind();
            }
        }
    }
}