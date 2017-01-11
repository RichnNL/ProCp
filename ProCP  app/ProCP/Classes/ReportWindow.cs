﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ProCP.Classes
{
    public partial class ReportWindow : Form
    {
        Form2 form;
        public ReportWindow(Form2 form)
        {
            InitializeComponent();
            this.form = form;

            string b=form.SendReport();
            richTextBox1.Text = b;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string filename;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                filename = saveFileDialog.FileName;
                richTextBox1.SaveFile(filename + ".txt", RichTextBoxStreamType.PlainText);
            }
        }

        private void ReportWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
