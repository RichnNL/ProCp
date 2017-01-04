using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;

namespace ProCP.Classes
{
    class SimulationStorage
    {
        public bool changedSinceLastSave;
        private string connectionInfo;
        private MySqlConnection sqlconnection;
        public SimulationStorage(string connection)
        {
            this.connectionInfo = getConnectionInfo(connection);
            this.sqlconnection = new MySqlConnection(connectionInfo);
        }
       
        public bool SaveSimulation(Simulation simulation) {
            
            
            string sqlcosts = "INSERT INTO simulations (Name, Description, Province, BeginDate, EndDate, Cost, Profit, Binary_File) VALUES (@name, @description,@Province, @BeginDate, @EndDate, @Cost, @Profit, @Binary_File)";
            string sql = "INSERT INTO simulations(Name, Description, Province, BeginDate, EndDate, Binary_File) VALUES (@Name, @Description, @Province, @BeginDate, @EndDate, @Binary_File)";
            MySqlCommand command = new MySqlCommand();
            command.CommandText = sql;
            command.CommandType = System.Data.CommandType.Text;
            command.Connection = sqlconnection;
            command.Parameters.Add("@Name", MySqlDbType.VarChar).Value = simulation.SimulationName;
            command.Parameters.Add("@Description", MySqlDbType.VarChar).Value = simulation.SimulationDescription;
            command.Parameters.Add("@Province", MySqlDbType.VarChar).Value = simulation.Province;
            command.Parameters.Add("@BeginDate", MySqlDbType.Date).Value = simulation.BeginDate;
            command.Parameters.Add("@EndDate", MySqlDbType.Date).Value = simulation.EndDate;

            
            MemoryStream memStream = new MemoryStream();
            StreamWriter sw = new StreamWriter(memStream);

            sw.Write(simulation);
            command.Parameters.Add("@Binary_File", MySqlDbType.VarBinary, Int32.MaxValue).Value = memStream.GetBuffer(); ;
            

            //command.Parameters.AddWithValue("Cost", cost); 
            // command.Parameters.AddWithValue("Profit", profit);
            
            try
            {
                sqlconnection.Open();
                command.ExecuteNonQuery();
                changedSinceLastSave = true;
               
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                sqlconnection.Close();
            }
            
        }
        public string[] LoadSimulationNames()
        {
            List<string> list = new List<string>();
            string sql = "SELECT Name FROM simulations";
            MySqlCommand command = new MySqlCommand(sql, sqlconnection);
            try
            {
                sqlconnection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                string name;

                while (reader.Read())
                {
                    name = Convert.ToString(reader["Name"]);
                    list.Add(name);
                }
            }
            catch (Exception ex)
            {
               
            }
            if(list.Count() > 0)
            {
                return list.ToArray();
            }
            else
            {
                return null;
            }
           
        }
        public Simulation simulation LoadSimulation(string filename) {
            string sql = "SELECT Binary_File FROM simulations where Name = 'filename'";
            MySqlCommand command = new MySqlCommand(sql, sqlconnection);
           
                using (var sqlQueryResult = command.ExecuteReader())
                {
                     if (sqlQueryResult != null)
                     {
                        sqlQueryResult.Read();
                    byte[] plots = new byte[(sqlQueryResult.GetBytes(0, 0, null, 0, int.MaxValue))];
                    sqlQueryResult.GetBytes(0, 0, plots, 0, plots.Length);
                    return ByteArrayToSimulation(plots);
                        }
                     else
                     {
                       return null;
                      }
                }
              
        }
        
        public void somethingChanged()
        {
            changedSinceLastSave = false;
        }
        private string getConnectionInfo(string connection)
        {
            string cnx = null;
            try
            {
                
                using (StreamReader sr = new StreamReader(connection))
                {
                    cnx = sr.ReadToEnd();

                }
            }
            catch (IOException)
            {
                MessageBox.Show("Connection info could not be read");
            }

            return cnx;
        }
        private byte[] SimulationToByte(List<Plot> plots)
        {
            if(plots == null)
            {
                return null;
            }
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, plots);
                return ms.ToArray();
            }
        }
        private List<Plot> ByteArrayToSimulation(byte[] byteArray)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(byteArray, 0, byteArray.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return (List<Plot>)obj;
            }
        }
        public string[] LoadSimulationDescriptions()
        {
            List<string> list = new List<string>();
            string sql = "SELECT Description FROM simulations";
            MySqlCommand command = new MySqlCommand(sql, sqlconnection);
            try
            {
                sqlconnection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                string name;

                while (reader.Read())
                {
                    name = Convert.ToString(reader["Description"]);
                    list.Add(name);
                }
            }
            catch (Exception ex)
            {
                
            }
            if (list.Count() > 0)
            {
                return list.ToArray();
            }
            else
            {
                return null;
            }
        }
        public string[] LoadSimulationDates()
        {
            List<string> list = new List<string>();
            string sql = "SELECT Date FROM simulations";
            MySqlCommand command = new MySqlCommand(sql, sqlconnection);
            try
            {
                sqlconnection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                string name;

                while (reader.Read())
                {
                    name = Convert.ToString(reader["Date"]);
                    list.Add(name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Error Loading Simulation Dates");
            }
            if (list.Count() > 0)
            {
                return list.ToArray();
            }
            else
            {
                return null;
            }
        }
        public string[] LoadSimulationCosts()
        {
            List<string> list = new List<string>();
            string sql = "SELECT Cost FROM simulations";
            MySqlCommand command = new MySqlCommand(sql, sqlconnection);
            try
            {
                sqlconnection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                string name;

                while (reader.Read())
                {
                    name = Convert.ToString(reader["Cost"]);
                    list.Add(name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Error Loading Simulation Costs");
            }
            if (list.Count() > 0)
            {
                return list.ToArray();
            }
            else
            {
                return null;
            }
        }
        public string[] LoadSimulationProfit()
        {
            List<string> list = new List<string>();
            string sql = "SELECT Profit FROM simulations";
            MySqlCommand command = new MySqlCommand(sql, sqlconnection);
            try
            {
                sqlconnection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                string name;

                while (reader.Read())
                {
                    name = Convert.ToString(reader["Profit"]);
                    list.Add(name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Error Loading Simulation Profits");
            }
            if (list.Count() > 0)
            {
                return list.ToArray();
            }
            else
            {
                return null;
            }
        }

    }
}
