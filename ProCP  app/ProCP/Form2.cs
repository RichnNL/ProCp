using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProCP.Classes;


namespace ProCP
{
    public partial class Form2 : Form
    {
        
        
        RCAEA rcaea; 
        string province;

        public Form2()
        {
            InitializeComponent();
            
            province = "Drenthe";
            //defeault province for now;
            rcaea = new RCAEA(province);

            dateTimePicker1.MinDate = DateTime.Now;
            setMinMaxDates();
            AddSoilTypestoComboBox();
            RCAEA.simulation.OnDraw += new Simulation.DrawCropHandler(drawPictureBox);
  
            populateProvinceOption();
            setClickEventForPictureBoxes();
        }
        private void setMinMaxDates()
        {
            // fix min date 
           
            dateTimePicker2.MinDate = dateTimePicker1.Value.AddMonths(3);
            dateTimePicker2.MaxDate = dateTimePicker1.Value.AddMonths(36);
        }
        

        
        private void PictureBoxClicked(object sender, MouseEventArgs e)
        {
            PictureBox pbox = (PictureBox)sender;
            rcaea.componentSelected = "Strawberry";
            rcaea.selectedPlot = RCAEA.simulation.getPlot(pbox.Name);
            if (e.Button == MouseButtons.Left)
            {  
                if(rcaea.componentSelected != "" && rcaea.componentSelected != null)
                {
                    RCAEA.simulation.addCrop(RCAEA.simulation.database.GetCrop(rcaea.componentSelected), rcaea.selectedPlot);
                }
            }
            else if(e.Button == MouseButtons.Right)
            {
                RCAEA.simulation.removeCrop(rcaea.selectedPlot);
            }
            //if (RCAEA.simulation.GetNumber()>0)
            //{
            //    provinceCbx.Enabled = false;
            //    wateringCbx.Enabled = false;
            //    fertilizerCbx.Enabled = false;
            //}
            //else if ( RCAEA.simulation.GetNumber()==0)
            //{
            //    provinceCbx.Enabled = true;
            //    wateringCbx.Enabled = true;
            //    fertilizerCbx.Enabled = true;
            //}
                MessageBox.Show(pbox.Name);
            //rcaea.componentSelected = 
        }
        private void setClickEventForPictureBoxes()
        {
            foreach(Plot p in RCAEA.simulation.plots)
            {
                
                ((PictureBox)this.panel1.Controls[p.PlotId]).MouseClick += new MouseEventHandler((PictureBoxClicked));
            }
           
        }
        private void populateCropPanelSelection()
        {
            string[] all = RCAEA.simulation.database.getCropNamesBySeason("ALL");
            string[] spring = RCAEA.simulation.database.getCropNamesBySeason("SPRING");
            string[] summer = RCAEA.simulation.database.getCropNamesBySeason("SUMMER");
            string[] fall = RCAEA.simulation.database.getCropNamesBySeason("FALL");
            string[] winter = RCAEA.simulation.database.getCropNamesBySeason("WINTER");

            // Zisis finish 
        }

        private void AddSoilTypestoComboBox()
        {
            soilTypeCbx.Items.Clear();
            string[] array = RCAEA.simulation.database.getAllSoilTypeNames();
            foreach (var st in array)
            {
                this.soilTypeCbx.Items.Add(st);
            }

        }
    
        private void populateProvinceOption()
        { string[] array = RCAEA.simulation.database.getProvinceNames();
            provinceCbx.Items.Clear();
            foreach (var v in array)
            {
                this.provinceCbx.Items.Add(v);
            }
        }
        


        private void overviewTotalCostLbl_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void globalVariablesGroupBox_Enter(object sender, EventArgs e)
        {

        }

        private void yrSplitBtn_Click(object sender, EventArgs e)
        {

        }

        private void lettuceStripMenuItem_Click(object sender, EventArgs e)
        {
            yrSplitBtn.Image = lettuceMenuStripItem.Image;
            yrSplitBtn.Text = lettuceMenuStripItem.Text;
        }

        private void mushroomMenuStripItem_Click(object sender, EventArgs e)
        {
            yrSplitBtn.Image = mushroomMenuStripItem.Image;
            yrSplitBtn.Text = mushroomMenuStripItem.Text;
        }

        private void onionMenuStripItem_Click(object sender, EventArgs e)
        {
            yrSplitBtn.Image = onionMenuStripItem.Image;
            yrSplitBtn.Text = onionMenuStripItem.Text;
        }

        private void peasMenuStripItem_Click(object sender, EventArgs e)
        {
            yrSplitBtn.Image = peasMenuStripItem.Image;
            yrSplitBtn.Text = peasMenuStripItem.Text;
        }

        private void potatoMenuStripItem_Click(object sender, EventArgs e)
        {
            yrSplitBtn.Image = potatoMenuStripItem.Image;
            yrSplitBtn.Text = potatoMenuStripItem.Text;
        }
        private void CauliFlowerMenuStripItem_Click(object sender, EventArgs e)
        {
            springSplitBtn.Image = Cauliflower.Image;
            springSplitBtn.Text = Cauliflower.Text;
        }

        private void soilTypeLbl_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            //RCAEA.simulation.SetEndDate(dateTimePicker2.Value);
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //RCAEA.simulation.SetBeginDate(dateTimePicker1.Value);
            //MessageBox.Show("" + RCAEA.simulation.BeginDate.ToString());
            setMinMaxDates();
            
        }

        private void pb86_Click(object sender, EventArgs e)
        {

        }

        private void pb16_Click(object sender, EventArgs e)
        {

        }

        private void pb84_Click(object sender, EventArgs e)
        {

        }

        private void provinceCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            RCAEA.simulation.Province = provinceCbx.SelectedItem.ToString() ;
            MessageBox.Show(RCAEA.simulation.Province);
        }

        private void springSplitBtn_Click(object sender, EventArgs e)
        {

        }

        private void pb13_Click(object sender, EventArgs e)
        {

        }
        private void drawPictureBox(string pictureBox, string CropName, int ImageNumber)
        {
            if (String.IsNullOrEmpty(pictureBox) || String.IsNullOrEmpty(CropName) || ImageNumber == -1)
            {
                ((PictureBox)this.panel1.Controls[pictureBox]).Image = null;
            }
            else {
                ((PictureBox)this.panel1.Controls[pictureBox]).Image = RCAEA.simulation.database.GetImage(CropName, ImageNumber);
            }
        }
        private void CropFromPanelSelected()
        {
            //To do
            //rcaea.componentSelected = 
        }


        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void fertilizerCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            RCAEA.simulation.SetFertilizer(fertilizerCbx.SelectedItem.ToString());
            MessageBox.Show("Fertilizer set to " + RCAEA.simulation.Fertilizer.ToString());
        }

        private void wateringCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            RCAEA.simulation.Setwatering(wateringCbx.SelectedItem.ToString());
            MessageBox.Show("Watering set to " + RCAEA.simulation.Fertilizer.ToString());
        }
    }
}
