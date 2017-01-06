namespace ProCP.Classes
{
    partial class LoadWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.OkButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.simulationList = new System.Windows.Forms.ListView();
            this.Simulation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Cost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Profit = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Province = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(330, 355);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(74, 60);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "Ok";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(635, 355);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(74, 60);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // simulationList
            // 
            this.simulationList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Simulation,
            this.Description,
            this.col2,
            this.col3,
            this.Province,
            this.Cost,
            this.Profit});
            this.simulationList.FullRowSelect = true;
            this.simulationList.Location = new System.Drawing.Point(12, 23);
            this.simulationList.MultiSelect = false;
            this.simulationList.Name = "simulationList";
            this.simulationList.Size = new System.Drawing.Size(1053, 305);
            this.simulationList.TabIndex = 3;
            this.simulationList.UseCompatibleStateImageBehavior = false;
            this.simulationList.View = System.Windows.Forms.View.Details;
            this.simulationList.SelectedIndexChanged += new System.EventHandler(this.simulationList_SelectedIndexChanged);
            // 
            // Simulation
            // 
            this.Simulation.Text = "Name";
            this.Simulation.Width = 120;
            // 
            // Description
            // 
            this.Description.Text = "Description";
            this.Description.Width = 312;
            // 
            // col2
            // 
            this.col2.Text = "BeginDate";
            this.col2.Width = 136;
            // 
            // Cost
            // 
            this.Cost.Text = "Cost";
            this.Cost.Width = 107;
            // 
            // Profit
            // 
            this.Profit.Text = "Profit";
            this.Profit.Width = 401;
            // 
            // col3
            // 
            this.col3.Text = "EndDate";
            this.col3.Width = 132;
            // 
            // Province
            // 
            this.Province.Text = "Province";
            this.Province.Width = 120;
            // 
            // LoadWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1094, 459);
            this.Controls.Add(this.simulationList);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.OkButton);
            this.Name = "LoadWindow";
            this.Text = "LoadWindow";
            this.Load += new System.EventHandler(this.LoadWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ListView simulationList;
        private System.Windows.Forms.ColumnHeader Simulation;
        private System.Windows.Forms.ColumnHeader Description;
        private System.Windows.Forms.ColumnHeader col2;
        private System.Windows.Forms.ColumnHeader Cost;
        private System.Windows.Forms.ColumnHeader Profit;
        private System.Windows.Forms.ColumnHeader col3;
        private System.Windows.Forms.ColumnHeader Province;
    }
}