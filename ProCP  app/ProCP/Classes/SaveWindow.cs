using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProCP.Classes
{
    public partial class SaveWindow : Form
    {
        Form2 form;

        public SaveWindow(Form2 form)
        {
            InitializeComponent();
            this.form = form;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(saveText.Text)){
                form.setSimulationNameandDecription(saveText.Text,descriptionText.Text);
                        this.Close();
                
                }
            else
            {
                MessageBox.Show("Please Enter a Filename");
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
