using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace Experiment_9
{
    public partial class Placements : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPlacements();
            }
        }

        private void LoadPlacements()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PlacementDB"].ConnectionString;

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string query = @"
                    SELECT
                        P.PlacementId, S.StudentName, S.Branch, P.CompanyName, P.PackageLPA
                    FROM Placements P
                    INNER JOIN Students S ON P.StudentId = S.StudentId
                ";

                MySqlDataAdapter da = new MySqlDataAdapter(query, con);

                DataTable dt = new DataTable();

                da.Fill(dt);

                gvPlacements.DataSource = dt;
                gvPlacements.DataBind();
            }
        }
    }
}