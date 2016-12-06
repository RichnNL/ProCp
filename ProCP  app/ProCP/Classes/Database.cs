using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Windows.Forms;

namespace ProCP.Classes
{
    class DatabaseConnection
    {
       
        //<summary>
        //Reads the connection information from file.
        //</summary>
        private String ConnectionInfo()
        {
            String cnx = null;
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

        public MySqlConnection connection;

        public DatabaseConnection()
        {
            connection = new MySqlConnection(ConnectionInfo());
        }



        private List<Crop> Crops;
        private List<SoilType> SoilTypes;
       
        // private List<Images> Images;
        // private List<Price> Prices;
        // private List<Weather> Weathers;
        // public Database(MYSQLConnection,String connectionInfo){}
       
        public void LoadSellPRices() { }
        public void LoadWeather() { }
        public void LoadImage() { }
        public Decimal GetBuyPrice(string CropName) { return 0; }
        public Decimal GetSellPrice(string CropName) { return 0; }
        
        public List<Crop> GetAllCrops() 
        {
            String sql = "SELECT * FROM crop_info";
            MySqlCommand command = new MySqlCommand(sql, connection);

            List<Crop> temp = new List<Crop>();

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                string cropName;
                int maturity;
                int waterMin;
                int waterMax;
                int thirst;
                int hunger;
                int temperature;
                String season;
                int yield;
                

                while (reader.Read())
                {
                    cropName = Convert.ToString(reader["Name"]);
                    maturity = Convert.ToInt32(reader["Maturity"]);
                    waterMin = Convert.ToInt32(reader["Water_Min"]);
                    waterMax = Convert.ToInt32(reader["Water_Max"]);
                    thirst = Convert.ToInt32(reader["Thirst"]);
                    hunger = Convert.ToInt32(reader["Nutrition_Need"]);
                    temperature = Convert.ToInt32(reader["Temperature"]);
                    season = Convert.ToString(reader["Season"]);
                    yield = Convert.ToInt32(reader["Yield"]);


                    temp.Add(new Crop(cropName, maturity, waterMin, waterMax, thirst, hunger, temperature, season, yield));

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return temp;
            
        }






        public Crop GetCrop(string name) { return null; }
        // public Weather GetWeather(string province,int season) { return null; }
        //public Images GetImage(string Cropname,int ImageNumber) { return null; }
        
        
        public List<SoilType>  GetAllSoilTypes()
        {
            String sql = "SELECT * FROM soil";
            MySqlCommand command = new MySqlCommand(sql, connection);

            List<SoilType> temp = new List<SoilType>();

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                String name;
                int max_water;
                int nutrients;

                while (reader.Read())
                {
                    name = Convert.ToString(reader["Type"]);
                    max_water = Convert.ToInt32(reader["Water_Retention"]);
                    nutrients = Convert.ToInt32(reader["Nutrients"]);

                    temp.Add(new SoilType(name, max_water, nutrients));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return temp;

            
        }
        





    }
}
