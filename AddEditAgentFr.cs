using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace realty
{
    public partial class AddEditAgentFr : Form
    {
        private  int operation ; // 0- add, 1- edit, delete
        private ClFunctions func;
        private List<IdNameStruct> agencies;
        private List<IdNameStruct> agents;
        public string Aid;
        public string Afio;
        public string Amail;
        public string Alogin;
        public string A_agency;
        public bool A_isadmin;
        public  string Aphone;
        public long terra_count;
        public long realt_count;
       
        public AddEditAgentFr()
        {
            InitializeComponent();
            
           
        }

        public AddEditAgentFr(int oper)
        {
            InitializeComponent();
            operation = oper;
            func = new ClFunctions();
            Aid="";
            Afio="";
            Amail="";
            Alogin="";
            A_agency="";
            A_isadmin=false;
            Aphone = "";
            terra_count = 0;
            realt_count = 0;
            
        }


        private void addAgent()
        {
            string fio=txbFIO.Text.Trim();
            string mail=txbMail.Text.Trim();
            string login=txbLogin.Text.Trim();
            string pass=txbPassword.Text.Trim();
            string agency=agencies[cbxAgency.SelectedIndex].id;
            string isadmin = "0";
            string phone = txbPhone.Text.Trim();
            if (chbxAdmin.Checked == true) isadmin = "1";
            if (fio == "" || login == "" || pass == "" || (agency == null || agency == "") || mail=="") { MessageBox.Show(" Не заполнены все поля"); return; }
            pass = func.GetMD5String(pass);
            string sql = "INSERT INTO agents  SET Ag_FIO='" + fio + "' , Ag_agency=" + agency + ", Ag_login='" + login + "'";
            sql += ", Ag_password ='" + pass + "', Ag_isadmin=" + isadmin + ", Ag_mail='"+mail+"'";
            if (phone != "") sql += ", Ag_phone ='" + phone+"'";
            string mesg= func.InsertToTable(sql);
            if (mesg != "") MessageBox.Show(" Возникла ошибка при добавлении агента в базу");
            else { MessageBox.Show("Изменения сохранены "); this.Close(); }
        }

        private void UpdateAgent()
        {
            if (Aid == "") return;
            string fio = txbFIO.Text.Trim();
            string mail = txbMail.Text.Trim();
            string login = txbLogin.Text.Trim();
            string pass = txbPassword.Text.Trim();
            string agency = agencies[cbxAgency.SelectedIndex].id;
            string isadmin = "0";
            string phone = txbPhone.Text.Trim();
            if (chbxAdmin.Checked == true) isadmin = "1";
            if (fio == "" || login == "" || (agency == null || agency == "") || mail == "") { MessageBox.Show(" Не заполнены все поля"); return; }
            pass = func.GetMD5String(pass);
            string sql = "UPDATE agents  SET Ag_FIO='" + fio + "' , Ag_agency=" + agency + ", Ag_login='" + login + "'";
            sql += ", Ag_isadmin=" + isadmin + ", Ag_mail='" + mail + "'";
            if (phone != "") sql += ", Ag_phone ='" + phone + "'";
            if (pass != "") sql += ", Ag_password ='" + pass + "'";
            sql += " WHERE Ag_id=" + Aid;
           
            string mesg = func.InsertToTable(sql);
            if (mesg != "") MessageBox.Show(" Возникла ошибка при редактировании агента");
            else { MessageBox.Show("Изменения сохранены "); this.Close(); }
        }


        private void AddEditAgentFr_Load(object sender, EventArgs e)
        {
            
            string msg="";
            agencies = func.getAgency(out msg);
            if (agencies == null || msg != "") { MessageBox.Show(msg); this.Close(); return; }
            int agscount = agencies.Count;
            if (agscount == 0) { MessageBox.Show("Нет агентств, агент не может быть добавлен"); this.Close(); return; }
           
            cbxAgency.Items.Clear();
            for (int i = 0; i < agscount; i++)
            {
                cbxAgency.Items.Add(agencies[i].name);
            }
            cbxAgency.SelectedIndex = 0;

            if (operation == 0)
            {
                Deletepanel.Visible = false;
            }
            else
            {
                if (operation == 1)
                {
                    Deletepanel.Visible = true;                    
                    cbxAgents.Items.Clear();
                    if (Afio != "") txbFIO.Text = Afio;
                    if (Aphone != "") txbPhone.Text = Aphone;
                    if (Amail != "") txbMail.Text = Aphone;
                    if (Alogin != "") txbLogin.Text = Alogin;
                    if (A_agency != "")
                    {
                        int k = agencies.FindIndex(n => n.id == A_agency);
                        if (k >= 0) cbxAgency.SelectedIndex = k;
                    }

                    chbxAdmin.Checked = A_isadmin;
                    if (terra_count == 0 && realt_count == 0) cbxAgents.Enabled = false;
                    else
                    {
                        if (A_agency != "")
                        {
                            msg="";
                            agents = func.getAgents(A_agency, out msg);
                            agscount=agents.Count;
                            if (msg != "" || agscount == 0) cbxAgents.Enabled = false;
                            else
                            {
                                cbxAgents.Enabled = true;
                                for (int i = 0; i < agscount; i++)
                                {
                                    cbxAgents.Items.Add(agents[i].name);//список агентов, для выбора нового при удалении текущего
                                }
                                cbxAgents.SelectedIndex = 0;
                            }

                        }
                    }
                }
               
            }


        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (operation == 0)
            {
                addAgent();
            }
            else
            {
                UpdateAgent();
            }
          
        }

        private void button1_Click(object sender, EventArgs e) //delete
        {
            if (MessageBox.Show("Удалить текущего агента?", "Предупреждение", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;

            if ((agents == null || agents.Count == 0) && (terra_count > 0 || realt_count > 0))
            {
                MessageBox.Show("Невозможно удаление  данного агента, за ним числятся объекты недвижимости");
            }
            else
            {
                string new_agent=agents[cbxAgents.SelectedIndex].id;
                string sql="", msg="";
                if (realt_count > 0)
                {
                    sql = "UPDATE realestate  SET Ragent_code=" + new_agent + "  WHERE Ragent_code=" + Aid;
                    msg=func.InsertToTable(sql);
                    if (msg != "") { MessageBox.Show("Возникла ошибка при назначении  объектов  агенту " + agents[cbxAgents.SelectedIndex].name + ", текущий агент не будет удален "); return; }
                }
                if (terra_count > 0)
                {
                    sql = "UPDATE terra  SET Tagent_code=" + new_agent + "  WHERE Tagent_code=" + Aid;
                    msg = func.InsertToTable(sql);
                    if (msg != "") { MessageBox.Show("Возникла ошибка при назначении  объектов  агенту " + agents[cbxAgents.SelectedIndex].name + ", текущий агент не будет удален "); return; }
                }

                sql = "DELETE FROM agents  WHERE Ag_id =" + Aid;
                msg = func.InsertToTable(sql);
                if (msg != "") { MessageBox.Show("Возникла ошибка при удалении агента"); }
                else { MessageBox.Show("Агент удален"); this.Close(); }
            }

        }
    }
}
