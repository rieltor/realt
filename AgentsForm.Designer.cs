namespace realty
{
    partial class AgentsForm
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
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gridCtrAgents = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtAddAgent = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.gridCtrAgency = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.BtAddAgency = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCtrAgents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.xtraTabPage3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCtrAgency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(868, 387);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage3});
            this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl1_SelectedPageChanged);
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.panel2);
            this.xtraTabPage1.Controls.Add(this.panel1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(862, 359);
            this.xtraTabPage1.Text = "Информация по агентам";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gridCtrAgents);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 58);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(862, 301);
            this.panel2.TabIndex = 1;
            // 
            // gridCtrAgents
            // 
            this.gridCtrAgents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCtrAgents.Location = new System.Drawing.Point(0, 0);
            this.gridCtrAgents.MainView = this.gridView1;
            this.gridCtrAgents.Name = "gridCtrAgents";
            this.gridCtrAgents.Size = new System.Drawing.Size(862, 301);
            this.gridCtrAgents.TabIndex = 1;
            this.gridCtrAgents.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridCtrAgents;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtAddAgent);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(862, 58);
            this.panel1.TabIndex = 0;
            // 
            // BtAddAgent
            // 
            this.BtAddAgent.Location = new System.Drawing.Point(11, 11);
            this.BtAddAgent.Name = "BtAddAgent";
            this.BtAddAgent.Size = new System.Drawing.Size(130, 38);
            this.BtAddAgent.TabIndex = 1;
            this.BtAddAgent.Text = "Добавить агента";
            this.BtAddAgent.Click += new System.EventHandler(this.BtAddAgent_Click);
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.panel4);
            this.xtraTabPage3.Controls.Add(this.panel3);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(855, 355);
            this.xtraTabPage3.Text = "Информация по агентствам";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.gridCtrAgency);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 57);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(855, 298);
            this.panel4.TabIndex = 1;
            // 
            // gridCtrAgency
            // 
            this.gridCtrAgency.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCtrAgency.Location = new System.Drawing.Point(0, 0);
            this.gridCtrAgency.MainView = this.gridView2;
            this.gridCtrAgency.Name = "gridCtrAgency";
            this.gridCtrAgency.Size = new System.Drawing.Size(855, 298);
            this.gridCtrAgency.TabIndex = 1;
            this.gridCtrAgency.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridCtrAgency;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.DoubleClick += new System.EventHandler(this.gridView2_DoubleClick);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.BtAddAgency);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(855, 57);
            this.panel3.TabIndex = 0;
            // 
            // BtAddAgency
            // 
            this.BtAddAgency.Location = new System.Drawing.Point(11, 10);
            this.BtAddAgency.Name = "BtAddAgency";
            this.BtAddAgency.Size = new System.Drawing.Size(130, 38);
            this.BtAddAgency.TabIndex = 0;
            this.BtAddAgency.Text = "Добавить агентство";
            this.BtAddAgency.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // AgentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 387);
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "AgentsForm";
            this.Text = "Агенты и Агентства";
            this.Activated += new System.EventHandler(this.AgentsForm_Activated);
            this.Load += new System.EventHandler(this.AgentsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCtrAgents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.xtraTabPage3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCtrAgency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraGrid.GridControl gridCtrAgents;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private DevExpress.XtraGrid.GridControl gridCtrAgency;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.Panel panel3;
        private DevExpress.XtraEditors.SimpleButton BtAddAgency;
        private DevExpress.XtraEditors.SimpleButton BtAddAgent;
    }
}