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
//
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
        }
        private void setMinMaxDates()
        {
            // fix min date 
           
            dateTimePicker2.MinDate = dateTimePicker1.Value.AddMonths(3);
            dateTimePicker2.MaxDate = dateTimePicker1.Value.AddMonths(36);
        }
        

        void p_Click(object sender, EventArgs e)
        {
            PictureBox pbox = (PictureBox)sender;
            string str = yrSplitBtn.SplitMenuStrip.Text;
//            if (!ptr.AddCrop(str, pbox.Name))
//            {
//                MessageBox.Show("alreadycultivated ");
//}



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

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

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
            
        }

        private void springSplitBtn_Click(object sender, EventArgs e)
        {

        }

        private void pb13_Click(object sender, EventArgs e)
        {

        }
        private void drawPictureBox(string pictureBox, string CropName, int ImageNumber)
        {
            ((PictureBox)this.panel1.Controls[pictureBox]).Image = RCAEA.simulation.database.GetImage(CropName, ImageNumber);
           
        }
       
    }
}
