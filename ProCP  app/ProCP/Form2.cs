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
        
        List<PictureBox> pb;
        Plot ptr;

       // DatabaseConnection dc;
//
        public Form2()
        {
           
           
            InitializeComponent();
            dateTimePicker1.MinDate = DateTime.Now;
            dateTimePicker2.MinDate = dateTimePicker1.Value.AddMonths(3);
            dateTimePicker2.MaxDate = dateTimePicker1.Value.AddMonths(36);

         //   dc = new DatabaseConnection();
           // dc.GetAllCrops();
            AddSoilTypestoComboBox();
            pb = new List<PictureBox>() { };
            AddPbToList();

            
            
            
        }
        public void AddPbToList()
        {
            foreach (PictureBox p in panel1.Controls)
            {
                pb.Add(p);
                p.Click += p_Click;
            }

        }

        void p_Click(object sender, EventArgs e)
        {
            PictureBox pbox = (PictureBox)sender;
            string str = yrSplitBtn.SplitMenuStrip.Text;
            if (!ptr.AddCrop(str, pbox.Name))
            {
                MessageBox.Show("alreadycultivated ");
}



        }

        public void AddSoilTypestoComboBox()
        {


            //foreach (var st in dc.GetAllSoilTypes())
            //{
            //    this.soilTypeCbx.Items.Add(st);
            //}

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
           

            dateTimePicker2.MinDate = dateTimePicker1.Value.AddMonths(3);
            dateTimePicker2.MaxDate = dateTimePicker1.Value.AddMonths(36);
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
    }
}
