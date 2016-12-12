using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

namespace ProCP.Classes
{
    class Database
    {
        public MySqlConnection connection;
        public List<Crop> Crops;
        public List<SoilType> SoilTypes;
        public List<Weather> weathers;
        public List<Price> prices;
        public List<Images> images;

        public Database(string StartingProvince)
        {
            string connectionInfo = getConnectionInfo();
            this.connection = new MySqlConnection(connectionInfo);

            images = new List<Images>();
            weathers = new List<Weather>();
            loadAllWeather(StartingProvince);
             SoilTypes = GetAllSoilTypes();
            Crops = LoadAllCrops();
            prices = LoadSellPrices();
            LoadImages();

        
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
       
        private List<Price> LoadSellPrices() {
            List<Price> temp = new List<Price>();

            String sql = "SELECT crop_info.Name, sell_prices.Price_Per_Unit, seed_cost.Cost_m2 FROM crop_info INNER JOIN sell_prices ON crop_info.CROP_ID=sell_prices.Crop_ID INNER JOIN seed_cost ON crop_info.CROP_ID=seed_cost.Crop_ID";
            MySqlCommand command = new MySqlCommand(sql, connection);

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                
                string cropName;
                decimal sellPrice;
                decimal buyPrice;

                while (reader.Read())
                {
                    cropName = Convert.ToString(reader["Name"]);
                    sellPrice = Convert.ToDecimal(reader["Price_Per_Unit"]);
                    buyPrice = Convert.ToDecimal(reader["Cost_m2"]);
                    temp.Add(new Price(cropName, sellPrice, buyPrice));
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
        private void LoadImages() {
            string sql = "SELECT Name,Image_0,Image_1,Image_2,Image_3 FROM crop_info;";


            MySqlCommand command = new MySqlCommand(sql, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            foreach(DataRow row in table.Rows)
            {
                string CropName = Convert.ToString(row["Name"]);
                Image Im1 = ConverttoImage((byte[])row["Image_0"]);
                Image Im2 = ConverttoImage((byte[])row["Image_1"]);
                Image Im3 = ConverttoImage((byte[])row["Image_2"]);
                Image Im4 = ConverttoImage((byte[])row["Image_3"]);
                images.Add(new Images(CropName, Im1, Im2, Im3, Im4));
            }



        }
        public decimal GetBuyPrice(string CropName) { return 0; }
        public decimal GetSellPrice(string CropName) { return 0; }
        
        private List<Crop> LoadAllCrops() 
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
                decimal waterMin;
                decimal waterMax;
                decimal thirst;
                decimal neededNutrition;
                decimal temperature;
                string season;
                int yield;
                

                while (reader.Read())
                {
                    cropName = Convert.ToString(reader["Name"]);
                    maturity = Convert.ToInt32(reader["Maturity"]);
                    waterMin = Convert.ToDecimal(reader["Water_Min"]);
                    waterMax = Convert.ToDecimal(reader["Water_Max"]);
                    thirst = Convert.ToDecimal(reader["Thirst"]);
                    neededNutrition = Convert.ToDecimal(reader["Nutrition_Need"]);
                    temperature = Convert.ToDecimal(reader["Temperature"]);
                    season = Convert.ToString(reader["Season"]);
                    yield = Convert.ToInt32(reader["Yield"]);


                    temp.Add(new Crop(cropName, maturity, waterMin, waterMax, thirst, neededNutrition, temperature, season, yield));
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

        public List<Crop> getAllCrops()
        {
            return this.Crops;
        }

        public Crop GetCrop(string name) { 
            foreach(Crop c in Crops)
            {
                if(c.GetCropName() == name)
                {
                    return c;
                }
            }
            return null;
        }
        public Weather GetWeather(string province, int month, int year)
        {
            foreach (Weather w in weathers)
            {
                if(w.GetProvince() == province && w.getMonth() == month && w.getYear() == year)
                {
                    return w;
                }
            }
            return null;
        }
        public SoilType GetSoilType(string SoilType)
        {
            foreach (SoilType s in SoilTypes)
            {
                if (s.GetName() == SoilType)
                {
                    return s;
                }
            }
            return null;
        }
        public Image GetImage(string Cropname,int ImageNumber) {
            foreach(Images i in images)
            {
                if (i.getCropName() == Cropname)
                {
                    return i.GetImage(ImageNumber);
                }
                else
                {
                    return null;
                }
            }
            return null;
        }
        public void loadAllWeather(string Province)
        {
            string sql = "SELECT * from weather where Province = \""+Province+"\"";
            MySqlCommand command = new MySqlCommand(sql, connection);

            List<Weather> temp = new List<Weather>();

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                decimal rainamount;
                decimal Temperature;
                string province;
                int Month;
                int Year;
                string date;

                while (reader.Read())
                {
                    province = Convert.ToString(reader["Province"]);
                    Temperature = Convert.ToDecimal(reader["Temp_Avg"]);
                    rainamount = Convert.ToDecimal(reader["Rain_Avg"]);
                    date = Convert.ToString(reader["Month_Year"]);

                    Year= Convert.ToInt32(date.Substring(0, 4));
                    Month = Convert.ToInt32(date.Substring(5, 2));
                    temp.Add(new Weather(province,Month,Year,rainamount,Temperature));

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

            weathers = temp;

        }

        private List<SoilType>  GetAllSoilTypes()
        {
            string sql = "SELECT * FROM soil";
            MySqlCommand command = new MySqlCommand(sql, connection);

            List<SoilType> temp = new List<SoilType>();

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                string name;
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
        
        private Image ConverttoImage(byte[] data)
        {
            using(MemoryStream ms = new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }
        }
        public SoilType getDefaultSoilType()
        {
            return SoilTypes[0];
        }
        public SoilType getSoilType(string SoilType)
        {
            foreach(SoilType st in SoilTypes)
            {
                if(st.GetName() == SoilType)
                {
                    return st;
                }
            }
            return null;
        }
        public string[] getAllSoilTypeNames()
        {
            List<string> names = new List<string>();
            foreach(SoilType s in SoilTypes)
            {
                names.Add(s.GetName());
            }
            return names.ToArray();
        }
        
        





    }
}
