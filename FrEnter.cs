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
    public partial class FrEnter : Form
    {
        private AdminStruct Current_admin;
        public FrEnter()
        {
            InitializeComponent();
        }

        private void btEnter_Click(object sender, EventArgs e)
        {
           string passw="";
           string login="";
           login=txbLogin.Text;
           passw=txbPassword.Text; 
        
            if (login!="" && passw!="")
            {   
                ClFunctions func = new ClFunctions();
                passw = func.GetMD5String(passw);
                string sql = "SELECT Ag_id,Ag_FIO, Ag_Agency, Ag_isadmin, Ag_mail FROM  agents WHERE Ag_login ='" + login + "' AND Ag_password='" + passw + "'";
              
                string message = "";
             
                DataTable dt = func.ReadFromTable(sql, out message);
                if (message == "" && dt.Rows.Count>0)
                {
                    Current_admin.id = dt.Rows[0][0].ToString();
                    Current_admin.FIO = dt.Rows[0][1].ToString();
                    Current_admin.agency = dt.Rows[0][2].ToString();
                    if (dt.Rows[0][3].ToString() == "1" || dt.Rows[0][3].ToString().ToLower() == "true")Current_admin.isadmin = true;
                    else     Current_admin.isadmin = false;
                    Current_admin.mail = dt.Rows[0][4].ToString();
                    this.Close();
                  
                }
                else { MessageBox.Show("Неверные входные данные..."); }
             
            }
        }

        public  AdminStruct showFrom()
        {
            this.ShowDialog();
            return Current_admin;
        }
    }
}
