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
            populateCropPanelSelection();
            InitializeProperties();
        }
        private void InitializeProperties()
        {
            this.wateringCbx.SelectedItem = wateringCbx.Items[1];
            this.fertilizerCbx.SelectedItem = fertilizerCbx.Items[1];
            this.soilTypeCbx.SelectedItem = soilTypeCbx.Items[0];
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
            if (RCAEA.simulation.getNumberOfCrops()>0)
            {
                provinceCbx.Enabled = false;
                wateringCbx.Enabled = false;
                fertilizerCbx.Enabled = false;
            }
            else if ( RCAEA.simulation.getNumberOfCrops()==0)
            {
                provinceCbx.Enabled = true;
                wateringCbx.Enabled = true;
                fertilizerCbx.Enabled = true;
            }
                MessageBox.Show(pbox.Name);
           
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

            yrMenuStrip.Items.Clear();
            springMenuStrip.Items.Clear();
            fallMenuStrip.Items.Clear();
            summerMenuStrip.Items.Clear();
            winterMenuStrip.Items.Clear();

            foreach(string s in all)
            {
                ToolStripMenuItem item = createStripMenuItem(s);
                yrMenuStrip.Items.Add(item);
                item.MouseDown += new MouseEventHandler(AllMenuItemClicked);
            }
            foreach (string s in winter)
            {
                ToolStripMenuItem item = createStripMenuItem(s);
                winterMenuStrip.Items.Add(item);
                item.MouseDown += new MouseEventHandler(WinterMenuItemClicked);
            }
            foreach (string s in spring)
            {
                ToolStripMenuItem item = createStripMenuItem(s);
                springMenuStrip.Items.Add(item);
                item.MouseDown += new MouseEventHandler(SpringMenuItemClicked);
            }
            foreach (string s in fall)
            {
                ToolStripMenuItem item = createStripMenuItem(s);
                fallMenuStrip.Items.Add(item);
                item.MouseDown += new MouseEventHandler(FallMenuItemClicked);
            }
            foreach (string s in summer)
            {
                ToolStripMenuItem item = createStripMenuItem(s);
                summerMenuStrip.Items.Add(item);
                item.MouseDown += new MouseEventHandler(SummerMenuItemClicked);
            }
        }
        private ToolStripMenuItem createStripMenuItem(string CropName)
        {
            ToolStripMenuItem SMI = new ToolStripMenuItem();
            SMI.Name = CropName;
            SMI.Text = CropName;
            return SMI;
        }

        private void AddSoilTypestoComboBox()
        {
            soilTypeCbx.Items.Clear();
            string[] array = RCAEA.simulation.database.getAllSoilTypeNames();
            foreach (var st in array)
            {
                this.soilTypeCbx.Items.Add(st);
            }
            this.soilTypeCbx.SelectedItem = this.soilTypeCbx.Items[0];

        }
        private void AllMenuItemClicked(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                ToolStripItem item = (ToolStripItem)sender;
                yrSplitBtn.Image = RCAEA.simulation.database.GetImage(item.Name, 3);
                yrSplitBtn.Text = item.Text;
                rcaea.componentSelected = item.Text;
            }
        }
        private void SummerMenuItemClicked(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                ToolStripItem item = (ToolStripItem)sender;
               summerSplitBtn.Image = RCAEA.simulation.database.GetImage(item.Name, 3);
               summerSplitBtn.Text = item.Text;
                rcaea.componentSelected = item.Text;
            }
        }
        private void WinterMenuItemClicked(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                ToolStripItem item = (ToolStripItem)sender;
                winterSplitBtn.Image = RCAEA.simulation.database.GetImage(item.Name, 3);
                winterSplitBtn.Text = item.Text;
                rcaea.componentSelected = item.Text;
            }
        }
        private void SpringMenuItemClicked(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                ToolStripItem item = (ToolStripItem)sender;
                springSplitBtn.Image = RCAEA.simulation.database.GetImage(item.Name, 3);
                springSplitBtn.Text = item.Text;
                rcaea.componentSelected = item.Text;
            }
        }
        private void FallMenuItemClicked(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                ToolStripItem item = (ToolStripItem)sender;
                fallSplitBtn.Image = RCAEA.simulation.database.GetImage(item.Name, 3);
                fallSplitBtn.Text = item.Text;
                rcaea.componentSelected = item.Text;
            }
        }


        private void populateProvinceOption()
        { string[] array = RCAEA.simulation.database.getProvinceNames();
            provinceCbx.Items.Clear();
            foreach (var v in array)
            {
                this.provinceCbx.Items.Add(v);
            }
            this.provinceCbx.SelectedItem = this.provinceCbx.Items[0];
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
           
       
        }

        private void mushroomMenuStripItem_Click(object sender, EventArgs e)
        {

        }

        private void onionMenuStripItem_Click(object sender, EventArgs e)
        {
          
        }

        private void peasMenuStripItem_Click(object sender, EventArgs e)
        {
        
        }

        private void potatoMenuStripItem_Click(object sender, EventArgs e)
        {
            
        }
        private void CauliFlowerMenuStripItem_Click(object sender, EventArgs e)
        {
        
        }

        private void soilTypeLbl_Click(object sender, EventArgs e)
        {
            if (RCAEA.simulation.getNumberOfCrops() == 0)
            {
                string position = rcaea.selectedPlot.PlotId;
                assignPlotSoil(position);
            }
            else if (rcaea.selectedPlot == null)
            {
                error("No plot was selected");
            }
            else
            {
                error(true);
            }
        }
        private void assignPlotSoil(string position)
        {
                Plot p = RCAEA.simulation.getPlot(position);
                p.setSoilType(RCAEA.simulation.database.getSoilType(soilTypeCbx.SelectedItem.ToString()));
                MessageBox.Show(p.plotWeeks[0].SoilNutrition.ToString());
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
            if (RCAEA.simulation.getNumberOfCrops() == 0)
            {


                RCAEA.simulation.Province = provinceCbx.SelectedItem.ToString();
                MessageBox.Show(RCAEA.simulation.Province);
            }
            else
            {
                error(true);
            }
        }

        private void springSplitBtn_Click(object sender, EventArgs e)
        {

        }
        private void error(string errorMessage)
        {
            MessageBox.Show(errorMessage);
        }
        private void error(bool cropAlreadySet)
        {
            MessageBox.Show("A crop has been set, please remove all crops in the Simulation before making adjustments");
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
