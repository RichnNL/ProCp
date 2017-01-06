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
            
            
            string sqlcosts = "INSERT INTO simulations (Name, Description, Province, BeginDate, EndDate,Settings, Cost, Profit, Binary_File) VALUES (@name, @description,@Province, @BeginDate, @EndDate, @Settings, @Cost, @Profit, @Binary_File)";
            string sql = "INSERT INTO simulations(Name, Description, Province, BeginDate, EndDate, Settings, Binary_File) VALUES (@Name, @Description, @Province, @BeginDate, @EndDate, @Settings, @Binary_File)";
            //if() set sql statement
            MySqlCommand command = new MySqlCommand();
            command.CommandText = sql;
            command.CommandType = System.Data.CommandType.Text;
            command.Connection = sqlconnection;
            command.Parameters.Add("@Name", MySqlDbType.VarChar).Value = simulation.SimulationName;
            command.Parameters.Add("@Description", MySqlDbType.VarChar).Value = simulation.SimulationDescription;
            command.Parameters.Add("@Province", MySqlDbType.VarChar).Value = simulation.Province;
            command.Parameters.Add("@BeginDate", MySqlDbType.Date).Value = simulation.BeginDate;
            command.Parameters.Add("@EndDate", MySqlDbType.Date).Value = simulation.EndDate;
            command.Parameters.Add("@Settings", MySqlDbType.VarChar).Value = simulation.Fertilizer + "," + simulation.Watering + "," + simulation.PlotSize.ToString();

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();

            //MemoryStream memStream = new MemoryStream();
            //StreamWriter sw = new StreamWriter(memStream);

            bf.Serialize(ms, simulation.getListOfPlots());
            command.Parameters.Add("@Binary_File", MySqlDbType.VarBinary, Int32.MaxValue).Value = ms.ToArray();


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
        
        public List<Plot>  LoadSimulation(string filename, out string[] details) {
            string sql = "SELECT * FROM simulations WHERE Name = @filename";
            MySqlCommand command = new MySqlCommand(sql, sqlconnection);
            command.Parameters.Add("@filename", MySqlDbType.VarChar).Value = filename;
            details = new string[5];
            List<Plot> loadedPlots = new List<Plot>();
            byte[] b;
            try
            {
                sqlconnection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    details[0] = Convert.ToString(reader["Name"]);
                    details[2] = Convert.ToString(reader["Province"]);
                    details[3] = Convert.ToString(reader["BeginDate"]);
                    details[4] = Convert.ToString(reader["EndDate"]);
                    details[1] = Convert.ToString(reader["Settings"]);
                    b = ((byte[])reader["Binary_File"]).ToArray();
                    loadedPlots = ByteArrayToSimulation(b);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading list");

            }
            finally
            {
                sqlconnection.Close();
            }
            return loadedPlots;
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
      
        private List<Plot> ByteArrayToSimulation(byte[] byteArray)
        {
            using (MemoryStream memoryStream = new MemoryStream(byteArray))
            {
                try
                {
                    BinaryFormatter binaryformatter = new BinaryFormatter();
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    var obj = binaryformatter.Deserialize(memoryStream);
                    return (List<Plot>)obj;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }

            }
        }
        public List<string[]> LoadSimulationDescriptions()
        {
            List<string[]> row = new List<string[]>();
            string[] column = new string[7];
            string sql = "SELECT * FROM simulations";
            MySqlCommand command = new MySqlCommand(sql, sqlconnection);
            try
            {
                sqlconnection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                 while (reader.Read())
                {
                    column[0] = Convert.ToString(reader["Name"]);
                    column[1] = Convert.ToString(reader["Description"]);
                    column[2] = Convert.ToString(reader["Province"]);
                    column[3] = Convert.ToString(reader["BeginDate"]);
                    column[4] = Convert.ToString(reader["EndDate"]);
                    column[5] = Convert.ToString(reader["Cost"]);
                    column[6] = Convert.ToString(reader["Profit"]);
                    row.Add(column.ToArray());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading list");
                
            }
            finally
            {
                sqlconnection.Close();
            }
            return row;
           
        }
        
        
      
    }
}
