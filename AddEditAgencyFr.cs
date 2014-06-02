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
    public partial class AddEditAgencyFr : Form
    {
        private ClFunctions func;
        private int operation;
        public string Aname;
        public string Address;
        public string Aphone;
        public string Aid;

        public AddEditAgencyFr()
        {
            InitializeComponent();
            func = new ClFunctions();
          
        }


        public AddEditAgencyFr(int oper)
        {
            InitializeComponent();
            func = new ClFunctions();
            operation = oper;
            Aname = "";
            Aphone = "";
            Address = "";
            Aid = "";
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            string address, name1, phone;
            string sqlQ = "", message,pass;

            long id=0;
            address = txbAddress.Text.Trim();
            name1 = txbName.Text.Trim();
            phone = txbPhone.Text.Trim();
          
            if (address != "" && name1 != "" && phone != "")
            {

                if (operation == 0) //если это операция добавления
                {
                    sqlQ = "INSERT INTO agency (Aname,Aaddress,APhone) VALUES ('" + name1 + "', '" + address + "', '" + phone + "')";
                    message = func.InsertToTable(sqlQ, out id);
                    if (id > 0)
                    {
                        pass = func.GetMD5String("1");
                        sqlQ = "INSERT INTO agents  (Ag_FIO, Ag_login, Ag_password, Ag_agency, Ag_isadmin) VALUES ('Админ', 'admin', '" + pass + "', " + id + ", 1)";
                        message = func.InsertToTable(sqlQ);
                        if (message == "")
                        {
                            MessageBox.Show("Агентство добавлено. \n Создан администратор агентства, логин = admmin,  пароль = admin123");
                            this.Close();
                        }
                        else MessageBox.Show("Возникла ошибка при сохранении данных");
                    }
                }

                if (operation == 1) //если это операция добавления
                {
                    double dbl;
                    if (Double.TryParse(Aid, out dbl))
                    {
                        message="";
                        sqlQ = "UPDATE agency SET Aname= '" + Aname + "', Aaddress='" + address + "', APhone='" + phone + "' WHERE Aid=" + Aid;
                        message = func.InsertToTable(sqlQ);
                        if(message=="") 
                        {
                            MessageBox.Show("Изменения сохранены");
                            this.Close();
                        }
                        else MessageBox.Show("Возникла ошибка при сохранении данных");
                    }
                    else MessageBox.Show("Возникла ошибка при сохранении данных");
                }


            }
            else
            {
                MessageBox.Show(" Заполните все поля");
            }


        }

        private void AddEditAgency_Load(object sender, EventArgs e)
        {
           if (operation==0)
           {
               txbAddress.Text="";
               txbName.Text="";
               txbPhone.Text="";
           }
           if (operation == 1)
           {
               if (Aid == "") return;
               else
               {
                   txbAddress.Text = Address;
                   txbName.Text = Aname;
                   txbPhone.Text = Aphone;
               }

           }
        }

        private void AddEditAgency_Shown(object sender, EventArgs e)
        {
            //if (EditId == 0)
            //{
            //    txbAddress.Text = "";
            //    txbName.Text = "";
            //    txbPhone.Text = "";
            //}
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

