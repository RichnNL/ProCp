﻿using System;
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
        bool internalChange;
        public Form2()
        {
            InitializeComponent();
            
            province = "Drenthe";
            //defeault province for now;
            rcaea = new RCAEA(province);
            internalChange = false;
            dateTimePicker1.MinDate = new DateTime(2015, 1, 1);
            setMinMaxDates();
            AddSoilTypestoComboBox();
            rcaea.simulation.OnDraw += new Simulation.DrawCropHandler(drawPictureBox);
            rcaea.simulation.SimulationChangedEvent += new Simulation.SimulationChangedHandler(hostsimulationChanged);
  

            populateProvinceOption();
            setClickEventForPictureBoxes();
            populateCropPanelSelection();
            InitializeProperties();
            plotInfoLstbx.Items.Add("No Plot Selected");
        }
        private void InitializeProperties()
        {
            this.wateringCbx.SelectedItem = wateringCbx.Items[0];
            this.fertilizerCbx.SelectedItem = fertilizerCbx.Items[0];
            this.soilTypeCbx.SelectedItem = soilTypeCbx.Items[0];
        }
        private void setMinMaxDates()
        {
           
            dateTimePicker2.MinDate = dateTimePicker1.Value.AddMonths(3);
            dateTimePicker2.MaxDate = dateTimePicker1.Value.AddMonths(36);
        }
        private void showCropInfo()
        {

        }
        private void PictureBoxSingleClicked(object sender, MouseEventArgs e)
        {
            PictureBox pbox = (PictureBox)sender;
            if(rcaea.selectedPlot == rcaea.simulation.getPlot(pbox.Name))
            {
                rcaea.selectedPlot = null;
                soilTypeCbx.Enabled = false;
                rcaea.submitChange = false;
                plotInfoLstbx.Items.Clear();
                plotInfoLstbx.Items.Add("No Plot Selected");
            }
            else
            {
                rcaea.submitChange = false;
                rcaea.selectedPlot = rcaea.simulation.getPlot(pbox.Name);
                soilTypeCbx.Enabled = true;
                string soiltype = rcaea.selectedPlot.getSoilType();
                for(int i = 0; i < soilTypeCbx.Items.Count; i++)
                {
                    string value = soilTypeCbx.GetItemText(soilTypeCbx.Items[i]);
                    if(value == soiltype)
                    {
                        soilTypeCbx.SelectedIndex = i;
                        break;
                    }
                }
                rcaea.submitChange = true;
                FillPlotInfo(rcaea.selectedPlot);
            }
            
            

        }
        private void PictureBoxDoubleClicked(object sender, MouseEventArgs e)
        {
            PictureBox pbox = (PictureBox)sender;
            
            rcaea.selectedPlot = rcaea.simulation.getPlot(pbox.Name);
            if (e.Button == MouseButtons.Left)
            {  
                if(rcaea.componentSelected != "" && rcaea.componentSelected != null)
                {
                    rcaea.simulation.addCrop(rcaea.simulation.database.GetCrop(rcaea.componentSelected), rcaea.selectedPlot);
                    FillPlotInfo(rcaea.selectedPlot);
                }
            }
            else if(e.Button == MouseButtons.Right)
            {
                rcaea.simulation.removeCrop(rcaea.selectedPlot);
                FillPlotInfo(rcaea.selectedPlot);
            }
            if (rcaea.simulation.getNumberOfCrops()>0)
            {
                provinceCbx.Enabled = false;
                wateringCbx.Enabled = false;
                fertilizerCbx.Enabled = false;
            }
            else if ( rcaea.simulation.getNumberOfCrops()==0)
            {
                provinceCbx.Enabled = true;
                wateringCbx.Enabled = true;
                fertilizerCbx.Enabled = true;
            }

           
        }
        private void setClickEventForPictureBoxes()
        {
            foreach(Plot p in rcaea.simulation.plots)
            {
                
                ((PictureBox)this.panel1.Controls[p.PlotId]).MouseClick += new MouseEventHandler((PictureBoxSingleClicked));
                ((PictureBox)this.panel1.Controls[p.PlotId]).MouseDoubleClick += new MouseEventHandler((PictureBoxDoubleClicked));
            }
           
        }
        private void populateCropPanelSelection()
        {
            string[] all = rcaea.simulation.database.getCropNamesBySeason("ALL");
            string[] spring = rcaea.simulation.database.getCropNamesBySeason("SPRING");
            string[] summer = rcaea.simulation.database.getCropNamesBySeason("SUMMER");
            string[] fall = rcaea.simulation.database.getCropNamesBySeason("FALL");
            string[] winter = rcaea.simulation.database.getCropNamesBySeason("WINTER");

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
            string[] array = rcaea.simulation.database.getAllSoilTypeNames();
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
                yrSplitBtn.Image = rcaea.simulation.database.GetImage(item.Name, 3);
                yrSplitBtn.Text = item.Text;
                rcaea.componentSelected = item.Text;
            }
        }
        private void SummerMenuItemClicked(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                ToolStripItem item = (ToolStripItem)sender;
               summerSplitBtn.Image = rcaea.simulation.database.GetImage(item.Name, 3);
               summerSplitBtn.Text = item.Text;
                rcaea.componentSelected = item.Text;
            }
        }
        private void WinterMenuItemClicked(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                ToolStripItem item = (ToolStripItem)sender;
                winterSplitBtn.Image = rcaea.simulation.database.GetImage(item.Name, 3);
                winterSplitBtn.Text = item.Text;
                rcaea.componentSelected = item.Text;
            }
        }
        private void SpringMenuItemClicked(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                ToolStripItem item = (ToolStripItem)sender;
                springSplitBtn.Image = rcaea.simulation.database.GetImage(item.Name, 3);
                springSplitBtn.Text = item.Text;
                rcaea.componentSelected = item.Text;
            }
        }
        private void FallMenuItemClicked(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                ToolStripItem item = (ToolStripItem)sender;
                fallSplitBtn.Image = rcaea.simulation.database.GetImage(item.Name, 3);
                fallSplitBtn.Text = item.Text;
                rcaea.componentSelected = item.Text;
            }
        }


        private void populateProvinceOption()
        { string[] array = rcaea.simulation.database.getProvinceNames();
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
           
        }
        private void assignPlotSoil(Plot p)
        {
                if(rcaea.selectedPlot != null || rcaea.submitChange)
                 {
                p.setSoilType(rcaea.simulation.database.getSoilType(soilTypeCbx.SelectedItem.ToString()));

                FillPlotInfo(rcaea.selectedPlot);
                
            }
                

                
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (!internalChange)
            {
                setEndDate(dateTimePicker2.Value);
            }

            

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (!internalChange)
            {
                setBeginDate(dateTimePicker1.Value);
            }
            
        }
        private void setBeginDate(DateTime date)
        {
            if (!internalChange)
            {
                rcaea.simulation.SetBeginDate(date);
                
            }
            else
            { 
                dateTimePicker1.Value = date;
            }
            
            setMinMaxDates();
            startDateLabel.Text = rcaea.simulation.BeginDate.ToString("yyyy-MM-dd");
        }
        private void setEndDate(DateTime date)
        {
            if (!internalChange)
            {
                rcaea.simulation.SetEndDate(date);
            }
            else
            {
                dateTimePicker2.MinDate = new DateTime(2015, 1, 1);
                dateTimePicker2.Value = date;
            }

            setMinMaxDates();
            EndDateLabel.Text = rcaea.simulation.EndDate.ToString("yyyy-MM-dd");
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
            if (!internalChange)
            {
                setProvince(provinceCbx.SelectedItem.ToString());
            }
                
        }
        private void setProvince(string province)
        {
            if (rcaea.simulation.getNumberOfCrops() == 0 || internalChange)
            {
                if (!internalChange)
                {
                    rcaea.simulation.Province = province;
                }
                else
                {
                    for(int i = 0; i < provinceCbx.Items.Count; i++)
                    {
                        if(provinceCbx.Items[i].ToString() == province)
                        {
                            this.provinceCbx.SelectedItem = provinceCbx.Items[i];
                            break;
                        }
                    }
                    
                }
                

                if (rcaea.selectedPlot != null)
                {
                    FillPlotInfo(rcaea.selectedPlot);
                }

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
                if (((PictureBox)this.panel1.Controls[pictureBox]).BackColor == Color.Red)
                {
                    ((PictureBox)this.panel1.Controls[pictureBox]).BackColor = Color.Peru;
                }
                ((PictureBox)this.panel1.Controls[pictureBox]).Image = null;
            }
            else if(ImageNumber == 5)
            {
                ((PictureBox)this.panel1.Controls[pictureBox]).BackColor = Color.Red;
            }
            else
            {
                if (((PictureBox)this.panel1.Controls[pictureBox]).BackColor == Color.Red)
                {
                    ((PictureBox)this.panel1.Controls[pictureBox]).BackColor = Color.Peru;
                }
                
                ((PictureBox)this.panel1.Controls[pictureBox]).Image = rcaea.simulation.database.GetImage(CropName, ImageNumber);
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void fertilizerCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!internalChange)
            {
                setFertilizer(fertilizerCbx.SelectedItem.ToString());
            }
          


        }
        private void setFertilizer(string fertilizer)
        {
            if (rcaea.simulation.getNumberOfCrops() == 0 || internalChange)
            {

                if (!internalChange)
                {
                    rcaea.simulation.SetFertilizer(fertilizer);
                }
                else
                {
                    for (int i = 0; i < fertilizerCbx.Items.Count; i++)
                    {
                        if (fertilizerCbx.Items[i].ToString() == fertilizer)
                        {
                            this.fertilizerCbx.SelectedItem = fertilizerCbx.Items[i];
                            break;
                        }
                    }
                }
                if (rcaea.selectedPlot != null)
                {
                    FillPlotInfo(rcaea.selectedPlot);
                }
            }
            else
            {
                error(true);
            }
        }

        private void wateringCbx_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!internalChange)
            {
                setWatering(wateringCbx.SelectedItem.ToString());
            }

        }
        private void setWatering(string watering)
        {
            if (rcaea.simulation.getNumberOfCrops() == 0 || internalChange)
            {

                if (!internalChange)
                {
                    rcaea.simulation.Setwatering(watering);
                }
                else
                {
                    for (int i = 0; i < wateringCbx.Items.Count; i++)
                    {
                        if (wateringCbx.Items[i].ToString() == watering)
                        {
                            this.wateringCbx.SelectedItem = wateringCbx.Items[i];
                            break;
                        }
                    }
                }
               
                if (rcaea.selectedPlot != null)
                {
                    FillPlotInfo(rcaea.selectedPlot);
                }
            }
            else
            {
                error(true);
            }
        }

        private void soilTypeCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rcaea.submitChange && rcaea.selectedPlot != null)
            {
                assignPlotSoil(rcaea.selectedPlot);
            }
        }
        private void FillPlotInfo(Plot p)
        {
            plotInfoLstbx.Items.Clear();
            plotInfoLstbx.Items.Add("Plot Summary");
            plotInfoLstbx.Items.Add(p.PlotId + ":  SoilType: " + p.getSoilType());
            if(p.getNumberOfCrops() != 0)
            {
                plotInfoLstbx.Items.Add("Total Crops Set in Plot" + p.getNumberOfCrops().ToString());
                plotInfoLstbx.Items.Add("Crops Harvested: " + p.getNumberOfHarvestedCrops().ToString());
            }
            else
            {
                plotInfoLstbx.Items.Add("No Crops Set in Plot");
            }
            
            plotInfoLstbx.Items.Add("------------------");
            if (p.getNumberOfCrops() != 0)
            {
                if (p.plotWeeks[rcaea.simulation.CurrentWeek].isEmpty)
                {
                    string[] Crops = p.getAllCropNamesInPlotWithStartEndDates();
                    foreach(string s in Crops)
                    {
                        plotInfoLstbx.Items.Add(s);
                    }
                }
                else
                {
                    CropData currentPlot = p.GetCurrentCropData();
                    plotInfoLstbx.Items.Add("Currently Selected Crop");
                    plotInfoLstbx.Items.Add("Set at " + rcaea.simulation.DateToString(currentPlot.GetBeginDate()));
                    plotInfoLstbx.Items.Add("Matured at: " + rcaea.simulation.DateToString(currentPlot.GetEndDate()));
                    plotInfoLstbx.Items.Add("Current Status: ");
                    plotInfoLstbx.Items.Add(currentPlot.getHealth());
                }
                

            }
        }
        private void saveAs()
        {
            SaveWindow save = new SaveWindow(this);
            save.Show();
        }
        public void save()
        {
            if (rcaea.SimulationNameIsSet())
            {
                if (rcaea.SaveSimulation())
                {

                    error("Save Succesful");
                }
                else
                {
                    error("Save unsucessful");
                }
            }
            else
            {
                saveAs();
            }
          
        }
        private void load()
        {
            LoadWindow load = new LoadWindow(this);
            load.Show();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveAs();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            load();
        }
        private void newSimulation()
        {
            province = "Drenthe";
            rcaea.resetSimulation();
            }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newSimulation();
        }
        public void setSimulationNameandDecription(string name, string description)
        {
            rcaea.setSimulationName(name);
            if (!String.IsNullOrEmpty(description)){
                rcaea.setSimulationDescription(description);
            }
            if (rcaea.SaveSimulation())
            {

                error("Save Succesful");
            }
            else
            {
                error("Save unsucessful");
            }
        }
        public void loadSimulation(string simulationName)
        {
            if (rcaea.loadSimulation(simulationName))
            {
                error("Load Successful");
            }
            else
            {
                error("Load unsuccessful");
            }
        }
        public List<string[]> loadSimulationDetails()
        {
            return rcaea.simulationStorage.LoadSimulationDescriptions();
        }
      

        private void playBtn_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// Asking user for confirmation whenever going to exit
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (rcaea.simulationStorage.changedSinceLastSave)
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to save before Exit?", "Exit", MessageBoxButtons.YesNoCancel);
                if (dialogResult == DialogResult.Yes)
                {
                    e.Cancel = true;
                    save();
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (dialogResult == DialogResult.No)
                {
                    e.Cancel = false;
                }
            }
            else
            {
                e.Cancel = false;
            }
          
        }
        public bool simulationSaved()
        {
            if (rcaea.simulationStorage.changedSinceLastSave)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void hostsimulationChanged(string change, string value)
        {
            internalChange = true;
            if (change == "BeginDate")
            {
                DateTime date = Convert.ToDateTime(value);
                setBeginDate(date);
            }
            else if (change == "EndDate")
            {
                DateTime date = Convert.ToDateTime(value);
                setEndDate(date);
            }
            else if (change == "Province")
            {
                setProvince(value);
            }
            else if(change == "Reset")
            {
                InitializeProperties();
            }
            else if(change == "Fertilizer")
            {
                setFertilizer(value);
            }
            else if (change == "Watering")
            {
                setWatering(value);
            }
            else if(change == "PlotSize")
            {
                setPlotSize(Convert.ToInt32(value));
            }
            internalChange = false;
        }

        private void plotSizeNmr_ValueChanged(object sender, EventArgs e)
        {
            if (!internalChange)
            {
                setPlotSize(Convert.ToInt32(plotSizeNmr.Value));
            }
        }
        private void setPlotSize(int size)
        {
           
          if(rcaea.simulation.getNumberOfCrops() == 0 || internalChange)
            {
                if (!internalChange)
                {
                    rcaea.simulation.PlotSize = size;
                }
                else
                {
                    plotSizeNmr.Value = size;
                }

                if (rcaea.selectedPlot != null)
                {
                    FillPlotInfo(rcaea.selectedPlot);
                }
            }
            else
            {
                error(true);
            }
        }
    }
}
