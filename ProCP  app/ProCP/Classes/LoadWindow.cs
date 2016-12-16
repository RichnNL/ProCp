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
                form.loadSimulation(simulationName);
                    this.Close();  
            }
        }
        private void loadSimulationName()
        {
            string[] names = null;
            names = form.loadSimulationNames();
            if(names != null)
            {
                foreach (string s in names)
                {
                    simulationList.Items.Add(s);
                }
                empty = false;
            }
            else
            {
                empty = true;
                simulationList.Items.Add("No Simulations Saved");
                return;
            }
            names = form.loadSimulationDescriptions();
            ListViewItem item = new ListViewItem();
            if(names != null)
            {
                foreach (string s in names)
                {
                    item.SubItems.Add(s);
                }
                simulationList.Items.Add(item);
            }
            else
            {
                simulationList.Columns.Remove(Description);
            }
            names = form.loadSimulationDates();
            ListViewItem item2 = new ListViewItem();
            if (names != null)
            {
                foreach (string s in names)
                {
                    item2.SubItems.Add(s);
                }
                simulationList.Items.Add(item2);
            }
            else
            {
                simulationList.Columns.Remove(Date);
            }
            names = form.loadSimulationCosts();
            ListViewItem item3 = new ListViewItem();
            if (names != null)
            {
                foreach (string s in names)
                {
                    item3.SubItems.Add(s);
                }
                simulationList.Items.Add(item3);
            }
            else
            {
                simulationList.Columns.Remove(Cost);
            }
            names = form.loadSimulationProfits();
            ListViewItem item4 = new ListViewItem();
            if (names != null)
            {
                foreach (string s in names)
                {
                    item4.SubItems.Add(s);
                }
                simulationList.Items.Add(item4);
            }
            else
            {
                simulationList.Columns.Remove(Profit);
            }


        }

      
        private void simulationList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem selectedItem = simulationList.SelectedItems[0];
            simulationName = selectedItem.SubItems[0].ToString();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
