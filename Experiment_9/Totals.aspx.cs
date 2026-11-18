using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Experiment_9
{

    public partial class Totals : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTotal();
            }
        }

        private void LoadTotal()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PlacementDB"].ConnectionString;

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string query = @"
                SELECT CompanyName, COUNT(*) AS StudentPlaced, AVG(PackageLPA) AS AveragePackage
                FROM Placements
                GROUP BY CompanyName;

                SELECT
                    COUNT(*) AS TotalPlacements,
                    COUNT(DISTINCT CompanyName) AS TotalCompanies,
                    AVG(PackageLPA) AS OverallAverage
                FROM Placements;
            ";

                MySqlCommand cmd = new MySqlCommand(query, con);

                con.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();

                    dt.Load(reader);

                    gvTotals.DataSource = dt;
                    gvTotals.DataBind();

                    if (reader.NextResult() && reader.Read())
                    {
                        lblTotalPlacements.Text = reader["TotalPlacements"].ToString();

                        lblTotalCompanies.Text = reader["TotalCompanies"].ToString();

                        decimal average = reader["OverallAverage"] == DBNull.Value
                            ? 0
                            : Convert.ToDecimal(reader["OverallAverage"]);

                        lblOverallAverage.Text = average.ToString("0.00");
                    }
                }
            }
        }
    }
}