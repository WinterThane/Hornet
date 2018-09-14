using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

        public List<SpanishMaterial> SelectSpanishMaterials()
        {
            var query = "SELECT * FROM spanish_material WHERE material_group = 'B01' ORDER BY short_description ASC";
            var results = new List<SpanishMaterial>();
            
            if(OpenConnection() == true)
            {
                var cmd = new MySqlCommand(query, connection);
                var dataReader = cmd.ExecuteReader();

                while(dataReader.Read())
                {
                    results.Add(new SpanishMaterial
                    {
                        ShortDescription = dataReader["short_description"].ToString(),
                        CO2 = dataReader["co2"].ToString(),
                        CO2Units = dataReader["co2_units"].ToString()
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
    }
}
