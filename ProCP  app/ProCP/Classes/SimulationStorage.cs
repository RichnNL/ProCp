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
        public MySqlConnection connection;
        private string ConnectionInfo;
        public  SimulationStorage(string connection) {
            string connectionInfo = getConnectionInfo();
        }
        public bool SaveSimulation(string description) {
            
            string sql = "INSERT INTO simulations (Name, Description, Date, Cost, Profit, Binary_File) VALUES (@name, @description, @Date, @Cost, @Profit, @Binary_File)";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("name", RCAEA.simulation.SimulationName);
            command.Parameters.AddWithValue("description", description);
            command.Parameters.AddWithValue("Date", RCAEA.simulation.DateToString(RCAEA.simulation.BeginDate));
            //command.Parameters.AddWithValue("Cost", cost); 
            // command.Parameters.AddWithValue("Profit", profit);
            Simulation sim = 
            var bite = SimulationToByte(sim);
            command.Parameters.AddWithValue("Binary_File", bite);
            try
            {
                command.ExecuteNonQuery();
                changedSinceLastSave = true;
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }
        public string[] LoadSimulationNames()
        {
            List<string> list = new List<string>();
            string sql = "SELECT Name FROM simulations";
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
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
        public Simulation LoadSimulation(string filename) {
            string sql = "SELECT Binary_File FROM simulations where Name = 'filename'";
            MySqlCommand command = new MySqlCommand(sql, connection);
           
                using (var sqlQueryResult = command.ExecuteReader())
                {
                     if (sqlQueryResult != null)
                     {
                        sqlQueryResult.Read();
                    byte[] simulation = new byte[(sqlQueryResult.GetBytes(0, 0, null, 0, int.MaxValue))];
                    sqlQueryResult.GetBytes(0, 0, simulation, 0, simulation.Length);
                    return ByteArrayToSimulation(simulation);
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
        private string getConnectionInfo()
        {
            string cnx = null;
            try
            {
                
                using (StreamReader sr = new StreamReader("connection.ini"))
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
        private byte[] SimulationToByte(Simulation simulation)
        {
            if(simulation == null)
            {
                return null;
            }
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, simulation);
                return ms.ToArray();
            }
        }
        private Simulation ByteArrayToSimulation(byte[] byteArray)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(byteArray, 0, byteArray.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return (Simulation)obj;
            }
        }
        public string[] LoadSimulationDescriptions()
        {
            List<string> list = new List<string>();
            string sql = "SELECT Description FROM simulations";
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
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
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
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
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
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
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
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
