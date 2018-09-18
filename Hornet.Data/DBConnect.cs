using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Windows;

namespace Hornet.Data
{
    public class DBConnect
    {
        private readonly MySqlConnection connection;

        public DBConnect()
        {
            connection = new MySqlConnection(Config.connectionString);
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password.");
                        break;
                }
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        // return all materials
        public List<SpanishMaterial> SelectSpanishMaterials(string groupParam, bool nullCO2)
        {
            string query = BuildQuery(groupParam, nullCO2);

            var results = new List<SpanishMaterial>();

            if (OpenConnection() == true)
            {
                var cmd = new MySqlCommand(query, connection);
                var dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    results.Add(new SpanishMaterial
                    {
                        MaterialGroup = dataReader["material_group"].ToString(),
                        ShortDescription = dataReader["short_description"].ToString(),
                        CO2 = dataReader["co2"].ToString() + " " + dataReader["co2_units"].ToString(),
                        MaterialPicture = dataReader["picture"].ToString(),
                        Consumption = dataReader["energy_consumption"].ToString() + " " + dataReader["energy_units"].ToString(),
                        RawMaterial = dataReader["raw_material"].ToString() + " " + dataReader["raw_material_units"].ToString(),
                        PostRecycling = dataReader["post_recycling"].ToString() + " " + dataReader["post_recycling_units"].ToString(),
                        PreRecycling = dataReader["pre_recycling"].ToString(),
                        CompanyName = dataReader["company_name"].ToString(),
                        CompanyPicture = dataReader["company_picture"].ToString(),
                        Price = dataReader["price"].ToString() + " " + dataReader["price_units"].ToString(),
                        LongDescription = dataReader["long_description"].ToString(),
                        LifeCycleScope = dataReader["life_cycle_scope"].ToString(),
                        WebPageLing = dataReader["web_link"].ToString()
                    });
                }

                dataReader.Close();
                CloseConnection();

                return results;
            }
            else
            {
                return results;
            }
        }

        private static string BuildQuery(string groupParam, bool nullCO2)
        {
            var query = "SELECT * FROM spanish_material";
            if (nullCO2)
            {
                if (groupParam == "")
                {
                    query += " ORDER BY short_description ASC";
                }
                else
                {
                    query += " WHERE material_group = '" + groupParam + "' ORDER BY material_group ASC";
                }
            }
            else
            {
                if (groupParam == "")
                {
                    query += " WHERE co2 IS NOT NULL ORDER BY short_description ASC";
                }
                else
                {
                    query += " WHERE material_group = '" + groupParam + "' AND co2 IS NOT NULL ORDER BY material_group ASC";
                }
            }

            return query;
        }

        // return distinct material groups
        public List<string> SelectSpanishMaterialsGroups()
        {
            var query = "SELECT DISTINCT(material_group) FROM spanish_material ORDER BY material_group ASC";
            var results = new List<string>();

            if (OpenConnection() == true)
            {
                var cmd = new MySqlCommand(query, connection);
                var dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    results.Add(dataReader["material_group"] + "");
                }

                dataReader.Close();
                CloseConnection();

                return results;
            }
            else
            {
                return results;
            }
        }
    }
}
