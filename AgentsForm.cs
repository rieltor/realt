using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
namespace realty
{
    public partial class AgentsForm : Form
    {

        private ClFunctions func;
        public AgentsForm()
        {
            InitializeComponent();
            func = new ClFunctions();
        }



        private void ShowAgents()
        {

            MySqlDataAdapter daRealty;

            string sql = "SELECT agents.Ag_id, agents.Ag_FIO, agents.Ag_phone, agents.Ag_mail, ";
            sql += " agency.Aname, agents.Ag_login, agents.Ag_isadmin,  (SELECT COUNT(realestate.Rid) FROM realestate WHERE realestate.Ragent_code = agents.Ag_id) as  count1 ,  (SELECT COUNT(terra.TId) FROM terra WHERE terra.Tagent_code = agents.Ag_id) as  count2 FROM   agents  LEFT JOIN  agency  ON   agents.Ag_agency=agency.Aid ";


            
            try
            {
                daRealty = new MySqlDataAdapter(sql, func.connStr);
                MySqlCommandBuilder cb = new MySqlCommandBuilder(daRealty);

                DataSet dsRealty = new DataSet();
                daRealty.Fill(dsRealty);

                dsRealty.Tables[0].Columns[1].Caption = "ФИО агента";
                dsRealty.Tables[0].Columns[1].ReadOnly = true;
                dsRealty.Tables[0].Columns[2].Caption = "Телефон";
                dsRealty.Tables[0].Columns[2].ReadOnly = true;
                dsRealty.Tables[0].Columns[3].Caption = "Почта";
                dsRealty.Tables[0].Columns[3].ReadOnly = true;
                dsRealty.Tables[0].Columns[4].Caption = "Агентство";
                dsRealty.Tables[0].Columns[4].ReadOnly = true;
                dsRealty.Tables[0].Columns[7].Caption = "Кол-во объектов недвижимости";
                dsRealty.Tables[0].Columns[7].ReadOnly = true;
                dsRealty.Tables[0].Columns[8].Caption = "Кол-во земельных участков";
                dsRealty.Tables[0].Columns[8].ReadOnly = true;
               

                try
                {

                    gridView1.Columns.Clear();
                    gridCtrAgents.DataSource = null;
                    gridCtrAgents.DataSource = dsRealty.Tables[0];
                    gridView1.Columns[0].Visible = false;
                    gridView1.Columns[5].Visible = false;
                    gridView1.Columns[6].Visible = false;
                  

                }

                finally
                {

                    gridCtrAgents.EndUpdate();

                }



            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }



        private void ShowAgency()
        {

            MySqlDataAdapter daRealty;

            string sql = "SELECT agency.*,  (SELECT COUNT(realestate.Rid) FROM   realestate, agents " +
                         "WHERE realestate.Ragent_code=agents.Ag_id  AND agency.Aid=agents.Ag_agency) as count1, " +
                         "(SELECT COUNT(terra.TId) FROM   terra, agents " +
                         "WHERE terra.Tagent_code=agents.Ag_id AND agency.Aid=agents.Ag_agency) as count2 FROM   agency  ";
        

            try
            {
                daRealty = new MySqlDataAdapter(sql, func.connStr);
                MySqlCommandBuilder cb = new MySqlCommandBuilder(daRealty);

                DataSet dsRealty = new DataSet();
                daRealty.Fill(dsRealty);

                dsRealty.Tables[0].Columns[1].Caption = "Название агентства";
                dsRealty.Tables[0].Columns[1].ReadOnly = true;
                dsRealty.Tables[0].Columns[2].Caption = "Адрес";
                dsRealty.Tables[0].Columns[2].ReadOnly = true;
                dsRealty.Tables[0].Columns[3].Caption = "Телефон";
                dsRealty.Tables[0].Columns[3].ReadOnly = true;
                dsRealty.Tables[0].Columns[4].Caption = "Кол-во объектов недвижимости";
                dsRealty.Tables[0].Columns[4].ReadOnly = true;
                dsRealty.Tables[0].Columns[5].Caption = "Кол-во земельных участков";
                dsRealty.Tables[0].Columns[5].ReadOnly = true;

                try
                {

                    gridView2.Columns.Clear();                 
                    gridCtrAgency.DataSource = null;
                    gridCtrAgency.DataSource = dsRealty.Tables[0];
                    gridView2.Columns[0].Visible = false;


                }

                finally
                {

                    gridCtrAgency.EndUpdate();

                }



            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void AgentsForm_Load(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 0;
            ShowAgents();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0) ShowAgents();
            if (xtraTabControl1.SelectedTabPageIndex == 1) ShowAgency();
        }

        private void simpleButton1_Click(object sender, EventArgs e) //добавить агентство
        {
            AddEditAgencyFr agForm = new AddEditAgencyFr(0);//0-add
            agForm.ShowDialog();
        }

        private void BtAddAgent_Click(object sender, EventArgs e) //добавить агента
        {
            AddEditAgentFr agntForm = new AddEditAgentFr(0); //0-add
            agntForm.ShowDialog();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
           
            if (gridView1.SelectedRowsCount == 0) return;
            Point pt = gridView1.GridControl.PointToClient(Control.MousePosition);
            GridHitInfo info = gridView1.CalcHitInfo(pt);
            if (!info.InRow && !info.InRowCell) return;

            //int[] rwind = gridView1.GetSelectedRows();
            double count = 0;
            string id;
            DataRowView rw = (DataRowView)gridView1.GetFocusedRow();
            id = rw[0].ToString();
            if (!Double.TryParse(id, out count)) return;
            AddEditAgentFr agntForm = new AddEditAgentFr(1); //1-edit, delete
            agntForm.Aid = id;
            agntForm.Afio = rw[1].ToString();
            agntForm.Aphone = rw[2].ToString();
            agntForm.Amail = rw[3].ToString();
            agntForm.A_agency = rw[4].ToString();
            agntForm.Alogin = rw[5].ToString();

            string isadm = rw[6].ToString();
            if (isadm == "1" || isadm.ToLower()=="true") agntForm.A_isadmin = true;
            else agntForm.A_isadmin = false;

            count = 0;
            if (Double.TryParse(rw[7].ToString(), out count)) agntForm.realt_count = (long)count;
            if (Double.TryParse(rw[8].ToString(), out count)) agntForm.terra_count = (long)count;

            agntForm.ShowDialog();
        }


       

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            if (gridView2.SelectedRowsCount == 0) return;
            Point pt = gridView2.GridControl.PointToClient(Control.MousePosition);
            GridHitInfo info = gridView2.CalcHitInfo(pt);
            if (!info.InRow && !info.InRowCell) return;

            //int[] rwind = gridView2.GetSelectedRows();
            double count = 0;
            string id;
            DataRowView rw = (DataRowView)gridView2.GetFocusedRow();
            id = rw[0].ToString();
            if (!Double.TryParse(id, out count)) return;
            AddEditAgencyFr agForm = new AddEditAgencyFr(1);// редактировать
            agForm.Aid = id;
            agForm.Aname = rw[1].ToString();
            agForm.Aphone = rw[3].ToString();
            agForm.Address = rw[2].ToString();
            agForm.ShowDialog();

        }

      

        private void AgentsForm_Activated(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0) ShowAgents();
            if (xtraTabControl1.SelectedTabPageIndex == 1) ShowAgency();
        }

       

    }
}
