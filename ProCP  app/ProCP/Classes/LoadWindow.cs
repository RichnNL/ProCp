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
    public partial class LoadWindow : Form
    {
        string simulationName;
        bool empty;
        Form2 form;
        public LoadWindow(Form2 form2)
        {
            this.form = form2;
            InitializeComponent();
            loadSimulationName();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(simulationName))
            {
                if (!form.simulationSaved())
                {
                    DialogResult result = MessageBox.Show("The current Simulation will be overwritten do you wish to save?","Warning", MessageBoxButtons.YesNo);
                    if(result == DialogResult.Yes)
                    {
                        form.save();
                        return;
                    }
                    else
                    {
                        form.loadSimulation(simulationName);
                    }
                }
                else
                {
                    form.loadSimulation(simulationName);
                }
                
                    this.Close();  
            }
        }
        private void loadSimulationName()
        {
            List<string[]> sim = form.loadSimulationDetails();
            if(sim.Count != 0)
            {
                foreach (string[] s in sim)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = s[0];
                    for (int i = 1; i < 7; i++)
                    {
                        if (String.IsNullOrEmpty(s[i]))
                        {
                            item.SubItems.Add("");
                        }
                        else
                        {
                            item.SubItems.Add(s[i]);
                        }
                    }
                    simulationList.Items.Add(item);

                }
            }
            else
            {
                simulationList.Items.Add("No Entries Found");
            }
            
            
            
        }

      
        private void simulationList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem selectedItem = simulationList.SelectedItems[0];
            simulationName = selectedItem.SubItems[0].Text.ToString();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
