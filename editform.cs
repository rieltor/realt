using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace realty
{
    public partial class editform : Form
    {
        static public Photos dbvals;
        static public Comments dbcomm;
        DBWrap dbw;
        static public ArrayList comments;
        ClFunctions func;
        estaterecord record;
        public bool newrecord = false;
        //int operation;
        
        public editform(string id, bool rent, bool isadmin)
        {
            InitializeComponent();
            dbvals = new Photos("http://tamarrra.net/img1/esterne201755292010175841_big.jpg;http://tamarrra.net/img1/esterne201755302010175843_big.jpg;http://tamarrra.net/img1/esterne201755312010175845_big.jpg");
            dbw = new DBWrap();
            estaterecord record = dbw.ReadEstate(id);
            dbw.LoadAddressBase();
            dbcomm = new Comments();
            newrecord = true;
            dbvals.LoadImagesFromURLs();

            rieltorname.Text = record.Ragent_code;
            buildingtype.Text = record.Rhouse_type;
            material.Text = record.Rwalling_type;
            planning.Text = record.Rroom_layout;
            phone.Text = record.Rtelephone;
            condition.Text = record.Rcondition2;
            maindoor.Text = record.Rfront_door_type;
            ownertype.Text = record.Rproperty_type;
            windows.Text = record.Rwindow_material;
            doors.Text = record.Rinterior_door_type;
            canalization.Text = record.Rsewerage;
            tubes.Text = record.Rtubing_material;
            code.Text = record.Restate_type;
            label89.Text = record.Roperation;
            label91.Text = record.Rstatus;           

            dateTimePicker3.Value = record.Rdate_add;
            dateTimePicker4.Value = record.Rdate_change;
            dateTimePicker6.Value = record.Rtrade_date;
                        
            dateTimePicker3.Enabled = false;
            dateTimePicker4.Enabled = false;
            dateTimePicker6.Enabled = false;
           
            floor.Text += record.Rfloor.ToString();
            rooms.Text = record.Rroom_quantity.ToString();
            totalfloor.Text += record.Rnumber_of_storeys.ToString();
            label70.Text = record.Rlevel_quantity.ToString();

            totalarea.Text = record.Rtotal_floor_space.ToString() + " м2";
            livingarea.Text = record.Rfloor_space.ToString() + " м2";
            hallarea.Text = record.Rhall_floor_space + " м2";
            kitchenarea.Text = record.Rkitchen_floor_space + " м2";
            high.Text = record.Rceiling_height + " м";

            district.Text = record.Rdistrict_region + " ," + record.Rcity;
            if (record.Rblock_no == "")
            { street.Text = record.Rstreet + " ," + func.Decrypt(record.Rhouse_no, func.fkey); }
            else { street.Text = record.Rstreet + " ," + func.Decrypt(record.Rhouse_no, func.fkey) + "/" + func.Decrypt(record.Rblock_no, func.fkey); }
            orient.Text = func.Decrypt(record.Rflat_no, func.fkey);           

            if (record.Rhaggle == 1) { checkBox47.Checked = true; }
            if (record.Ruse_for_office == 1) { checkBox48.Checked = true; }
            if (record.Rparking == 1) { checkBox45.Checked = true; }
            if (record.Routhouse_legality == 1) { checkBox46.Checked = true; }
            if (record.RFridge == 1) { checkBox26.Checked = true; }

            if (rent)
            {
                checkBox46.Visible = false;
                checkBox26.Visible = true;
                checkBox46.Enabled = false;
                checkBox26.Enabled = true;
                checkBox26.Left = 530;
                checkBox26.Top = 51;
            }
            else
            {
                checkBox46.Visible = true;
                checkBox26.Visible = false;
                checkBox46.Enabled = true;
                checkBox26.Enabled = false;
                checkBox46.Left = 530;
                checkBox46.Top = 51;
            }

            bath.Text = record.Rbathroom_note;
            kitchen.Text = record.Rkitchen_note;
            backroom.Text = record.Rbackrooms;
            stuff.Text = record.Rnote;

            description.Text = record.Rdescription_detail;

            gas.Text = record.Rgas_supply;
            warm.Text = record.Rheat_supply;
            water.Text = record.Rwater_supply;
            balcon.Text = record.Rbalcony;
            logja.Text = record.Rloggia;
            lootype.Text = record.Rbathroom;

            price.Text = record.Rprice.ToString();
            if (rent)
            {
                dayprice.Text = record.Rrent_price_day.ToString();
                monthprice.Text = record.Rrent_price_month.ToString();
            }
            else
            {
                dayprice.Visible = false;
                monthprice.Visible = false;
                label76.Visible = false;
                label77.Visible = false;
            }
            label81.Text = record.Rtrade_price.ToString();
            //item.Ragent_code = result.Rows[0][48].ToString();            

            listBox2.Items.Clear();
            listView1.Items.Clear();

            for (int i = 0; i < dbvals.photolist.Count; i++)
            { listBox2.Items.Add(dbvals.photolist[i]); }
            listBox2.DisplayMember = "Name";
            if (listBox2.Items.Count > 0) { listBox2.SelectedIndex = 1; }
            pictureEdit1.Image = (dbvals.photolist[1] as Img).Picture;

            simpleButton3.Visible = false;
            simpleButton4.Visible = false;
            simpleButton8.Visible = false;
            checkBox25.Visible = false;
            textBox21.Enabled = false;

            addphotos.Visible = false;
            simpleButton7.Visible = false;

        }
        public editform(string id, bool rent)
        {
            InitializeComponent();
            dbvals = new Photos("http://tamarrra.net/img1/esterne201755292010175841_big.jpg;http://tamarrra.net/img1/esterne201755302010175843_big.jpg;http://tamarrra.net/img1/esterne201755312010175845_big.jpg");
            dbw = new DBWrap();
            estaterecord record = dbw.ReadEstate(id);
            dbw.LoadAddressBase();
            dbcomm = new Comments();
            newrecord = true;
            dbvals.LoadImagesFromURLs();

            foreach (string value in dbw.balconyValues)
                balcony.Properties.Items.Add(value, CheckState.Unchecked, true);
            foreach (string value in dbw.loggiaValues)
                loggia.Properties.Items.Add(value, CheckState.Unchecked, true);

            // Specify the separator character.
            balcony.Properties.SeparatorChar = ',';
            loggia.Properties.SeparatorChar = ',';

            comboBox5.Items.AddRange(dbw.housetypeValues);
            for (int i = 0; i < dbw.housetypeValues.Length;i++ )
                if (record.Rhouse_type == dbw.housetypeValues[i]) { comboBox5.SelectedIndex = i; i = dbw.housetypeValues.Length; }
            
            comboBox6.Items.AddRange(dbw.wallValues);
            for (int i = 0; i < dbw.wallValues.Length; i++)
                if (record.Rwalling_type == dbw.wallValues[i]) { comboBox6.SelectedIndex = i; i = dbw.wallValues.Length; }
            
            comboBox7.Items.AddRange(dbw.roomlayoutValues);
            for (int i = 0; i < dbw.roomlayoutValues.Length; i++)
                if (record.Rroom_layout == dbw.roomlayoutValues[i]) { comboBox7.SelectedIndex = i; i = dbw.roomlayoutValues.Length; }
            
            comboBox14.Items.AddRange(dbw.phoneValues);
            for (int i = 0; i < dbw.phoneValues.Length; i++)
                if (record.Rtelephone == dbw.phoneValues[i]) { comboBox14.SelectedIndex = i; i = dbw.phoneValues.Length; }
            
            comboBox12.Items.AddRange(dbw.conditionValues);
            for (int i = 0; i < dbw.conditionValues.Length; i++)
                if (record.Rcondition2 == dbw.conditionValues[i]) { comboBox12.SelectedIndex = i; i = dbw.conditionValues.Length; }
            
            comboBox8.Items.AddRange(dbw.maindoorValues);
            for (int i = 0; i < dbw.maindoorValues.Length; i++)
                if (record.Rfront_door_type == dbw.maindoorValues[i]) { comboBox8.SelectedIndex = i; i = dbw.maindoorValues.Length; }
            
            comboBox13.Items.AddRange(dbw.propertyValues);
            for (int i = 0; i < dbw.propertyValues.Length; i++)
                if (record.Rproperty_type == dbw.propertyValues[i]) { comboBox13.SelectedIndex = i; i = dbw.propertyValues.Length; }

            comboBox9.Items.AddRange(dbw.windowValues);
            for (int i = 0; i < dbw.windowValues.Length; i++)
                if (record.Rwindow_material == dbw.windowValues[i]) { comboBox9.SelectedIndex = i; i = dbw.windowValues.Length; }
                        
            comboBox10.Items.AddRange(dbw.doorValues);
            for (int i = 0; i < dbw.doorValues.Length; i++)
                if (record.Rinterior_door_type == dbw.doorValues[i]) { comboBox10.SelectedIndex = i; i = dbw.doorValues.Length; }
            
            comboBox11.Items.AddRange(dbw.sewerageValues);
            for (int i = 0; i < dbw.sewerageValues.Length; i++)
                if (record.Rsewerage == dbw.sewerageValues[i]) { comboBox11.SelectedIndex = i; i = dbw.sewerageValues.Length; }
            
            comboBox16.Items.AddRange(dbw.tubeValues);
            for (int i = 0; i < dbw.tubeValues.Length; i++)
                if (record.Rtubing_material == dbw.tubeValues[i]) { comboBox16.SelectedIndex = i; i = dbw.tubeValues.Length; }
            
            comboBox19.Items.AddRange(dbw.estateValues);
            for (int i = 0; i < dbw.estateValues.Length; i++)
                if (record.Restate_type == dbw.estateValues[i]) { comboBox19.SelectedIndex = i; i = dbw.estateValues.Length; }

            comboBox17.Items.AddRange(dbw.operationValues);
            for (int i = 0; i < dbw.operationValues.Length; i++)
                if (record.Roperation == dbw.operationValues[i]) { comboBox17.SelectedIndex = i; i = dbw.operationValues.Length; }

            comboBox18.Items.AddRange(dbw.stateValues);
            for (int i = 0; i < dbw.stateValues.Length; i++)
                if (record.Rstatus == dbw.stateValues[i]) { comboBox18.SelectedIndex = i; i = dbw.stateValues.Length; }

            dateTimePicker2.Value = record.Rdate_add;
            dateTimePicker1.Value = record.Rdate_change;
            dateTimePicker5.Value = record.Rtrade_date;
            
            textBox9.Text = record.Rfloor.ToString();
            textBox8.Text = record.Rroom_quantity.ToString();
            textBox10.Text = record.Rnumber_of_storeys.ToString();
            textBox11.Text = record.Rlevel_quantity.ToString();

            textBox12.Text = record.Rtotal_floor_space.ToString();
            textBox13.Text = record.Rfloor_space.ToString();
            textBox14.Text = record.Rhall_floor_space;
            textBox15.Text = record.Rkitchen_floor_space;
            textBox16.Text = record.Rceiling_height;

            comboBox2.DataSource = dbw.district;
            comboBox2.DisplayMember = dbw.district.Columns[1].ColumnName;
            comboBox3.DataSource = dbw.town;
            comboBox3.DisplayMember = dbw.town.Columns[1].ColumnName;
            comboBox4.DataSource = dbw.street;
            comboBox4.DisplayMember = dbw.street.Columns[0].ColumnName;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 2;
            comboBox4.SelectedIndex = 0;
            comboBox2.BindingContext = this.BindingContext;
            comboBox3.BindingContext = this.BindingContext;
            comboBox4.BindingContext = this.BindingContext;

            for (int i = 0; i < dbw.district.Rows.Count; i++ )
                if (dbw.district.Rows[i][0].ToString() == record.Rdistrict_region) { comboBox2.SelectedIndex = i; i = dbw.district.Rows.Count; }

            for (int i = 0; i < dbw.town.Rows.Count; i++)
                if (dbw.town.Rows[i][0].ToString() == record.Rcity) { comboBox3.SelectedIndex = i; i = dbw.town.Rows.Count; }

            int street_count = dbw.street.Rows.Count;
            for (int i = 0; i < street_count; i++)
                if (dbw.street.Rows[i][1].ToString() == record.Rstreet) { comboBox4.SelectedIndex = i; i = dbw.street.Rows.Count; }

            if (record.Rblock_no != "") { textBox3.Text = String.Concat(func.Decrypt(record.Rhouse_no, func.fkey), "/", func.Decrypt(record.Rblock_no, func.fkey)); } else { textBox3.Text = func.Decrypt(record.Rhouse_no, func.fkey); }
            textBox4.Text = func.Decrypt(record.Rflat_no, func.fkey);
                        
            if (record.Rhaggle == 1) {checkBox2.Checked = true;}
            if (record.Ruse_for_office == 1) {checkBox1.Checked = true;}
            if (record.Rparking == 1) {checkBox4.Checked = true;}
            if (record.Routhouse_legality == 1) {checkBox3.Checked = true;}
            if (record.RFridge == 1) {freezer.Checked = true; }
                               
            textBox20.Text = record.Rbathroom_note;
            textBox19.Text = record.Rkitchen_note;
            textBox18.Text = record.Rbackrooms;
            textBox22.Text = record.Rnote;
            textBox17.Text = record.Rdescription_detail;

            if (record.Rbalcony.IndexOf("Застеклен") > -1) {balcony.Properties.Items[0].CheckState = CheckState.Checked;}
            if (record.Rbalcony.IndexOf("Не застеклен")>-1) {balcony.Properties.Items[1].CheckState = CheckState.Checked;}
            if (record.Rbalcony.IndexOf("Зарешечен") > -1) {balcony.Properties.Items[2].CheckState = CheckState.Checked; }

            if (record.Rloggia.IndexOf("Застеклена") > -1) { loggia.Properties.Items[0].CheckState = CheckState.Checked; }
            if (record.Rloggia.IndexOf("Не застеклена") > -1) { loggia.Properties.Items[1].CheckState = CheckState.Checked; }
            if (record.Rloggia.IndexOf("Зарешечена") > -1) { loggia.Properties.Items[2].CheckState = CheckState.Checked; }

            if (record.Rgas_supply.IndexOf("природный") > -1) { checkBox5.Checked = true; }
            if (record.Rgas_supply.IndexOf("баллонный") > -1) { checkBox6.Checked = true; }
            if (record.Rgas_supply.IndexOf("электроплита") > -1) { checkBox7.Checked = true; }
            if (record.Rgas_supply.IndexOf("нет") > -1) { checkBox8.Checked = true; }

            if (record.Rheat_supply.IndexOf("электрическое") > -1) { checkBox17.Checked = true; }
            if (record.Rheat_supply.IndexOf("автономное") > -1) { checkBox18.Checked = true; }
            if (record.Rheat_supply.IndexOf("нет") > -1) { checkBox19.Checked = true; }
            if (record.Rheat_supply.IndexOf("ТЭЦ") > -1) { checkBox20.Checked = true; }

            if (record.Rbathroom.IndexOf("электрическое") > -1) { checkBox17.Checked = true; }
            if (record.Rbathroom.IndexOf("автономное") > -1) { checkBox18.Checked = true; }
            if (record.Rbathroom.IndexOf("Отсутствует") > -1) { checkBox21.Checked = true; }
            if (record.Rbathroom.IndexOf("Только") > -1) { checkBox22.Checked = true; }
            if (record.Rbathroom.IndexOf("Совмещенный") > -1) { checkBox16.Checked = true; }
            if (record.Rbathroom.IndexOf("Раздельный") > -1) { checkBox15.Checked = true; }

            if (record.Rwater_supply.IndexOf("(водопровод)") > -1) { checkBox12.Checked = true; }
            if (record.Rwater_supply.IndexOf("(колодец)") > -1) { checkBox11.Checked = true; }
            if (record.Rwater_supply.IndexOf("(АГВ)") > -1) { checkBox10.Checked = true; }
            if (record.Rwater_supply.IndexOf("(ТЭЦ)") > -1) { checkBox14.Checked = true; }
            if (record.Rwater_supply.IndexOf("нет") > -1) { checkBox9.Checked = true; }
            if (record.Rwater_supply.IndexOf("(бойлер)") > -1) { checkBox13.Checked = true; }

            textBox5.Text = record.Rprice.ToString();
            textBox6.Text = record.Rrent_price_day.ToString();
            textBox7.Text = record.Rrent_price_month.ToString();
            textBox23.Text = record.Rtrade_price.ToString();
            //item.Ragent_code = result.Rows[0][48].ToString();            
            
            listBox2.Items.Clear();
            listView1.Items.Clear();

            if (rent)
            {
                textBox6.Enabled = true;
                textBox7.Enabled = true;

                checkBox3.Visible = false;
                checkBox3.Enabled = false;
                freezer.Left = 497;
                freezer.Top = 551;
                freezer.Visible = true;
                freezer.Enabled = true;
            }
            else
            {
                textBox6.Enabled = false;
                textBox7.Enabled = false;

                checkBox3.Visible = true;
                checkBox3.Left = 497;
                checkBox3.Top = 551;
                checkBox3.Enabled = true;
                freezer.Visible = false;
                freezer.Enabled = false;
            }
                        
            for (int i = 0; i < dbvals.photolist.Count; i++)
            { listBox2.Items.Add(dbvals.photolist[i]); }
            listBox2.DisplayMember = "Name";
            if (listBox2.Items.Count > 0) { listBox2.SelectedIndex = 1; }
            pictureEdit1.Image = (dbvals.photolist[1] as Img).Picture;
            
        }
        public editform()
        {
            InitializeComponent();
            dbvals = new Photos("");
            dbw = new DBWrap();
            record = new estaterecord();
            dbw.LoadAddressBase();
            dbcomm = new Comments();
            newrecord = true;
            dbvals.LoadImagesFromURLs();            
            
            foreach (string value in dbw.balconyValues)
                balcony.Properties.Items.Add(value, CheckState.Unchecked, true);
            foreach (string value in dbw.loggiaValues)
                loggia.Properties.Items.Add(value, CheckState.Unchecked, true);

            comboBox5.Items.AddRange(dbw.housetypeValues);
            comboBox5.SelectedIndex = 12;
            comboBox6.Items.AddRange(dbw.wallValues);
            comboBox6.SelectedIndex = 1;
            comboBox7.Items.AddRange(dbw.roomlayoutValues);
            comboBox7.SelectedIndex = 4;
            comboBox14.Items.AddRange(dbw.phoneValues);
            comboBox14.SelectedIndex = 0;
           // comboBox15.Items.AddRange(roomlayoutValues);
           //  comboBox15.SelectedIndex = 4;
            comboBox12.Items.AddRange(dbw.conditionValues);
            comboBox12.SelectedIndex = 1;
            comboBox8.Items.AddRange(dbw.maindoorValues);
            comboBox8.SelectedIndex = 1;
            comboBox13.Items.AddRange(dbw.propertyValues);
            comboBox13.SelectedIndex = 7;
            comboBox9.Items.AddRange(dbw.windowValues);
            comboBox9.SelectedIndex = 4;
            comboBox10.Items.AddRange(dbw.doorValues);
            comboBox10.SelectedIndex = 0;
            comboBox11.Items.AddRange(dbw.sewerageValues);
            comboBox11.SelectedIndex = 0;
            comboBox16.Items.AddRange(dbw.tubeValues);
            comboBox16.SelectedIndex = 2;
            comboBox19.Items.AddRange(dbw.estateValues);
            comboBox19.SelectedIndex = 0;
            comboBox17.Items.AddRange(dbw.operationValues);
            comboBox17.SelectedIndex = 0;
            comboBox18.Items.AddRange(dbw.stateValues);
            comboBox18.SelectedIndex = 0;

            textBox9.Text = "1";
            textBox8.Text = "1";
            textBox10.Text = "1";
            textBox11.Text = "1";

            textBox12.Text = "0";
            textBox13.Text = "0";
            textBox14.Text = "0";
            textBox15.Text = "0";
            textBox16.Text = "2.5";


            // Specify the separator character.
            balcony.Properties.SeparatorChar = ',';
            loggia.Properties.SeparatorChar = ',';
            listBox2.Items.Clear();
            for (int i = 0; i < dbvals.photolist.Count;i++)
            { listBox2.Items.Add(dbvals.photolist[i]); }
            listBox2.DisplayMember = "Name";
            if (listBox2.Items.Count > 0) { listBox2.SelectedIndex = 1; }
            if (dbvals.photolist.Count>0) pictureEdit1.Image = (dbvals.photolist[0] as Img).Picture;
            comboBox2.DataSource = dbw.district;
            comboBox2.DisplayMember = dbw.district.Columns[1].ColumnName;
            comboBox3.DataSource = dbw.town;
            comboBox3.DisplayMember = dbw.town.Columns[1].ColumnName;
            comboBox4.DataSource = dbw.street;
            comboBox4.DisplayMember = dbw.street.Columns[0].ColumnName;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 2;
            comboBox4.SelectedIndex = 0;
            comboBox2.BindingContext = this.BindingContext;
            comboBox3.BindingContext = this.BindingContext;
            comboBox4.BindingContext = this.BindingContext;

            
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex>=0) 
            {
            //listBox2.Enabled = false;            
            pictureEdit1.Image = (dbvals.photolist[listBox2.SelectedIndex] as Img).Picture;
            LinkLabel.Link link = new LinkLabel.Link();
            link.LinkData = (dbvals.photolist[listBox2.SelectedIndex] as Img).Url;
            linkLabel1.Links.Clear();
            linkLabel1.Links.Add(link);
            linkLabel1.Text = (dbvals.photolist[listBox2.SelectedIndex] as Img).Description;
            //listBox2.Enabled = true;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != listBox2.Items.Count - 1)
            {
                listBox2.SelectedIndex++;
            }
            else
            {
                listBox2.SelectedIndex = 0;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != 0)
            {
                listBox2.SelectedIndex--;
            }
            else
            {
                listBox2.SelectedIndex = listBox2.Items.Count - 1;
            }
        }

        private void addphotos_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog1.CheckFileExists == false)
                {
                    MessageBox.Show("Файл не выбран");
                    return;
                }
                foreach (string flname in openFileDialog1.FileNames)
                {
                    Img photo = new Img();
                    photo.IsNew = true;
                    photo.Name = Path.GetFileName(flname);
                    photo.Picture = Image.FromFile(flname);
                    photo.Description = "Новое изображение";
                    photo.Url = DateTime.UtcNow.GetHashCode().ToString();
                    photo.Url= "image_"+ photo.Url + photo.Name;
                    photo.OriginalPath = flname;
                    dbvals.photolist.Add(photo);
                    listBox2.Items.Add(photo);
                }               
            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
           System.Uri ftpuri;

           if (MessageBox.Show("Подтвердите удаление", "Операция", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
           {
               func = new ClFunctions();
                   for (int i = listBox2.SelectedIndices.Count-1; i >= 0; i--)
                   {
                       //MessageBox.Show(listBox2.Items[listBox2.SelectedIndices[i]] as Img).Name);
                       ftpuri = new Uri("ftp://turangaleela:Ardonskaya209@tamarrra.net:21//www/img1/" + (listBox2.Items[listBox2.SelectedIndices[i]] as Img).Name);
                       func.DeleteFileFromServer("", ftpuri);
                       dbvals.photolist.RemoveAt(listBox2.SelectedIndices[i]);                      
                       listBox2.Items.RemoveAt(listBox2.SelectedIndices[i]);
                   }
               if (listBox2.Items.Count!=0)
               { listBox2.SelectedIndex = 0; }
           }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            
                System.Uri ftpuri;
                record.Rphoto = "";

                if (MessageBox.Show("Сохранить изменения?", "Внимание", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    func = new ClFunctions();
                    for (int i = 0; i < listBox2.Items.Count; i++)
                    {
                        if ((listBox2.Items[i] as Img).IsNew)
                        {
                            //MessageBox.Show(listBox2.Items[listBox2.SelectedIndices[i]] as Img).Name);
                            ftpuri = new Uri("ftp://turangaleela:Ardonskaya209@tamarrra.net:21//www/img1/" + (listBox2.Items[i] as Img).Url);
                            func.UploadFileToServer((listBox2.Items[i] as Img).OriginalPath, ftpuri);
                            (listBox2.Items[i] as Img).IsNew = false;
                            (listBox2.Items[i] as Img).Url = "http://tamarrra.net/img1/" + (listBox2.Items[i] as Img).Url;

                        }
                        record.Rphoto = String.Concat(record.Rphoto, (listBox2.Items[i] as Img).Url, ";");
                    }
                    record.Rphoto = record.Rphoto.Substring(0, record.Rphoto.Length - 1);
                    if (listBox2.Items.Count != 0)
                    { listBox2.SelectedIndex = 0; }
                }
                //if (CheckEstateDataConsistensy())
                {
                    SaveFormMoodToTheObject();
                    if (Program.objectedit.newrecord)
                    {
                        dbw.InsertNewEstate(record);
                        Form1.
                    }
                    else
                    {
                        dbw.UpdateEstate(record);
                    }
                    this.Close();
                }
            
            Program.objectedit.Close();
        }

        private bool CheckEstateDataConsistensy()
        {
            if (dateTimePicker1.Value>DateTime.Now)
            {
                MessageBox.Show("Дата регистрации указана неверно.", "Внимание", MessageBoxButtons.OK);
                return false;
            }
            if (dateTimePicker2.Value > DateTime.Now)
            {
                MessageBox.Show("Дата осмотра указана неверно.", "Внимание", MessageBoxButtons.OK);
                return false;
            }
            if (dateTimePicker1.Value > DateTime.Now)
            {
                MessageBox.Show("Дата снятия указана неверно.", "Внимание", MessageBoxButtons.OK);
                return false;
            }
            if ((comboBox19.SelectedIndex == 0) & (textBox4.Text==""))
            {
                MessageBox.Show("Тип недвижимости - квартира. Для данного типа адрес указан неполно.", "Внимание", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private void SaveFormMoodToTheObject()
        {
             
             //record.Ragent_code = Program.user.Id;
             record.Ragent_code = "";
             record.Rcity = dbw.town.Rows[comboBox3.SelectedIndex][0].ToString();
             record.Rdistrict_region = dbw.district.Rows[comboBox2.SelectedIndex][0].ToString();
             record.Rstreet = dbw.street.Rows[comboBox4.SelectedIndex][1].ToString();
             record.Restate_type = comboBox19.Text;
             try { record.Rfloor = Convert.ToInt32(textBox9.Text); }
             catch { record.Rfloor = 1; }
             try { record.Rnumber_of_storeys = Convert.ToInt32(textBox10.Text); }
             catch { record.Rnumber_of_storeys = 1; }
             try { record.Rroom_quantity = Convert.ToInt32(textBox8.Text); }
             catch { record.Rroom_quantity = 1; }
             try { record.Rhaggle = Convert.ToInt32(checkBox2.Checked); }
             catch { record.Rhaggle = 0; }
             record.Rproperty_type = comboBox13.Text;
             record.Rtitle = "";
             try { record.Routhouse_legality = Convert.ToInt32(checkBox3.Checked); }
             catch { record.Routhouse_legality = 0; }
             try { record.Ruse_for_office = Convert.ToInt32(checkBox1.Checked); }
             catch { record.Ruse_for_office = 0; }
             record.Rdescription_short="";
             record.Rbathroom_note = textBox20.Text;
             
             record.Rbathroom = "";

             if (checkBox15.Checked) { record.Rbathroom = checkBox15.Text; }
             if (checkBox16.Checked) 
                {
                    if (record.Rbathroom!="") { record.Rbathroom = String.Concat(record.Rbathroom, ",", checkBox16.Text); }
                    else { record.Rbathroom = checkBox16.Text; }
                }
             if (checkBox21.Checked) 
                {
                    if (record.Rbathroom!="") { record.Rbathroom = String.Concat(record.Rbathroom, ",", checkBox21.Text); }
                    else { record.Rbathroom = checkBox21.Text; }
                }
             if (checkBox22.Checked) 
                {
                    if (record.Rbathroom!="") {record.Rbathroom = String.Concat(record.Rbathroom, ",", checkBox22.Text); }
                    else {record.Rbathroom = checkBox22.Text;}
                }
             if (checkBox23.Checked)
                {
                 if (record.Rbathroom != "") { record.Rbathroom = String.Concat(record.Rbathroom, ",", checkBox23.Text); }
                 else { record.Rbathroom = checkBox23.Text; }
                }
             if (checkBox24.Checked)
             {
                 if (record.Rbathroom != "") { record.Rbathroom = String.Concat(record.Rbathroom, ",", checkBox24.Text); }
                 else { record.Rbathroom = checkBox24.Text; }
             }

             record.Rwalling_type = comboBox6.Text;
             record.Rkitchen_note = textBox19.Text;

             try { record.Rlevel_quantity = Convert.ToInt32(textBox11.Text); }
             catch { record.Rlevel_quantity = 0; }
             try { record.Rparking = Convert.ToInt32(checkBox4.Checked); }
             catch { record.Rparking = 0; }
             record.Rdescription_detail = textBox17.Text;
             record.Rroom_layout = comboBox7.Text;
             record.Rhouse_type = comboBox5.Text;             
             record.Rbalcony = balcony.Text;
             record.Rloggia = loggia.Text;
             
             record.Rgas_supply = "";

             if (checkBox5.Checked) { record.Rgas_supply = checkBox5.Text; }
             if (checkBox6.Checked) 
             { 
                 if (record.Rgas_supply!="") {record.Rgas_supply = String.Concat(record.Rgas_supply, ",", checkBox6.Text); }
                 else {record.Rgas_supply = checkBox6.Text;}
             }
             if (checkBox7.Checked) 
             {
                 if (record.Rgas_supply!="") { record.Rgas_supply = String.Concat(record.Rgas_supply, ",", checkBox7.Text); }
                 else {record.Rgas_supply = checkBox7.Text;}
             }
             if (checkBox8.Checked) 
             {
                 if (record.Rgas_supply!="") { record.Rgas_supply = String.Concat(record.Rgas_supply, ",", checkBox8.Text); }
                 else {record.Rgas_supply = checkBox8.Text;}
             }

             record.Rwater_supply = "";

             if (checkBox9.Checked) { record.Rwater_supply = checkBox9.Text; }
             if (checkBox10.Checked) 
             {
                 if (record.Rwater_supply!="") { record.Rwater_supply = String.Concat(record.Rwater_supply, ",", checkBox10.Text); }
                 else {record.Rwater_supply = checkBox10.Text;}
             }
             if (checkBox11.Checked) 
             {
                 if (record.Rwater_supply!="") { record.Rwater_supply = String.Concat(record.Rwater_supply, ",", checkBox11.Text); }
                 else {record.Rwater_supply = checkBox11.Text;}
             }
             if (checkBox12.Checked) 
             {
                 if (record.Rwater_supply!="") { record.Rwater_supply = String.Concat(record.Rwater_supply, ",", checkBox12.Text); }
                 else {record.Rwater_supply = checkBox12.Text;}
             }
             if (checkBox13.Checked) 
             {
                 if (record.Rwater_supply!="") { record.Rwater_supply = String.Concat(record.Rwater_supply, ",", checkBox13.Text); }
                 else {record.Rwater_supply = checkBox13.Text;}
             }
             if (checkBox14.Checked) 
             {
                 if (record.Rwater_supply!="") { record.Rwater_supply = String.Concat(record.Rwater_supply, ",", checkBox14.Text); }
                 else {record.Rwater_supply = checkBox14.Text;}
             }
             
             record.Rcondition2 = comboBox12.Text;
             
             record.Rheat_supply = "";

             if (checkBox17.Checked) { record.Rheat_supply = checkBox17.Text; }
             if (checkBox18.Checked) 
             {
                 if (record.Rheat_supply!="") { record.Rheat_supply = String.Concat(record.Rheat_supply, ",", checkBox18.Text); }
                 else {record.Rheat_supply = checkBox18.Text;}
             }
             if (checkBox19.Checked) 
             {
                 if (record.Rheat_supply!="") { record.Rheat_supply = String.Concat(record.Rheat_supply, ",", checkBox19.Text); }
                 else {record.Rheat_supply = checkBox19.Text;}
             }
             if (checkBox20.Checked) 
             {
                 if (record.Rheat_supply!="") { record.Rheat_supply = String.Concat(record.Rheat_supply, ",", checkBox20.Text); }
                 else {record.Rheat_supply = checkBox20.Text;}
             }

             record.Rinterior_door_type = comboBox10.Text;
             record.Rfront_door_type = comboBox8.Text;
             record.Rwindow_material = comboBox9.Text;
             record.Rtelephone = comboBox14.Text;
             record.Rtubing_material = comboBox16.Text;

             record.Rkitchen_floor_space = textBox14.Text;
             record.Rhall_floor_space = textBox15.Text;
             try { record.Rtotal_floor_space = Convert.ToDouble(textBox12.Text); }
             catch { record.Rtotal_floor_space = 0; }
             record.Rceiling_height = textBox16.Text;
             
             record.Rreference_point = ""; //?

             try { record.Rfloor_space = Convert.ToDouble(textBox13.Text); }
             catch { record.Rfloor_space = 0; }
             record.Rnote = textBox22.Text; //?
             record.Rbackrooms = textBox18.Text; //?
         
             try { record.Rprice = Convert.ToDouble(textBox5.Text); }
             catch { record.Rprice = 0.0; }
             record.Rprice_square = -1;
             record.Ris_special_proposal = 0;
             record.Rchange_only = 0;
             record.Rnewbuilding = 0;
             record.Rhypothec = 0;
            
             record.Ragent_code = comboBox1.Text;
             
             record.Rdate_add = dateTimePicker2.Value;
             record.Rdate_change = dateTimePicker1.Value;
             record.Rstatus = comboBox18.Text; //?
             record.Rtrade_date = dateTimePicker5.Value;

             try { record.Rtrade_price = Convert.ToDouble(textBox23.Text); }
             catch { record.Rtrade_price = 0.0; }

             try { record.Rrent_price_day = Convert.ToDouble(textBox7.Text); }
             catch { record.Rrent_price_day = 0.0; }

             try { record.Rrent_price_month = Convert.ToDouble(textBox6.Text); }
             catch { record.Rrent_price_month = 0.0; }
            
             if (comboBox17.SelectedIndex == 1)
             {
                 record.Raccount_in_rent = 1;
             }
             else
             {
                 record.Raccount_in_rent = 0;
             }
             record.Rbuilding_year = "";
             record.Rroof = "";
             record.Rterms_exchange = "";
             record.Rdistance_to_city = 0.0; 
             record.Rground_area = 0.0;
             record.Rpurpose_use = "";
             record.Rbasement = "";
             record.Rwindow_front_quantity = 0;
             record.Rmain_entrance_location = "";
             
             record.Rcommercial_outhouse = "";
             
             record.Rsite_space = "";
             record.Rsewerage = comboBox11.Text;
             //record.Rphoto = "";
             record.Roperation = comboBox17.Text;
             
             record.Rlavatory_basin = "";
             record.Rhouse_no = func.Encrypt(textBox20.Text, func.fkey);
             //record.Rblock_no = func.Encrypt(textBox20.Text;
             record.Rflat_no = func.Encrypt(textBox4.Text, func.fkey);
             try { record.RFridge = Convert.ToInt32(freezer.Checked); }
             catch { record.RFridge = 0; }

             record.Roperation = comboBox17.Text;
             record.Restate_type = comboBox19.Text;
             record.Rstatus = comboBox18.Text;
             
        }

        private void checkBox25_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            dbw.LoadTownForDistrict(dbw.district.Rows[comboBox2.SelectedIndex][0].ToString());
            comboBox3.DataSource = dbw.town;
            comboBox3.DisplayMember = dbw.town.Columns[1].ColumnName;
            comboBox3.BindingContext = this.BindingContext;
            comboBox3.SelectedIndex = 0;            
        }

        private void LoadDBComments()
        {
            listView1.Items.Clear();
            foreach (Comment cmt in dbcomm.CommentsList)
            {
                ListViewItem item = new ListViewItem();
                item.SubItems.Add(cmt.Author);
                item.SubItems.Add(cmt.Email);
                item.SubItems.Add(cmt.Date.ToShortDateString());
                item.SubItems.Add(cmt.Text);
                listView1.Items.Add(item);
            }
            if (listView1.Items.Count > 0) { listView1.Items[0].Selected = true; }
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.Enabled = true;
            dbw.LoadStreetsForTown(dbw.town.Rows[comboBox3.SelectedIndex][0].ToString());
            comboBox4.DataSource = dbw.street;
            comboBox4.DisplayMember = dbw.street.Columns[0].ColumnName;
            if (dbw.street.Rows.Count>0)
            { comboBox4.SelectedIndex = 0; }
            else
            {
              comboBox4.Items.Clear();
              comboBox4.Enabled = false;
            }
            comboBox4.BindingContext = this.BindingContext;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (textBox21.Text=="")
            {
                MessageBox.Show("Текст комментария пуст.", "Внимание", MessageBoxButtons.OK);
            }
            else
            {
                Comment cmt = new Comment();
                cmt.Author = Program.user.FIO;
                cmt.Date = DateTime.Now;
                cmt.Email = Program.user.email;
                cmt.Text = textBox21.Text;
                dbcomm.CommentsList.Add(cmt);
                dbcomm.InsertCommentInDb(dbcomm.CommentsList.Count - 1, Program.objectedit.record.Restate_type, Program.objectedit.record.Rid);
                ListViewItem item = new ListViewItem();
                item.SubItems.Add(cmt.Author);
                item.SubItems.Add(cmt.Email);
                item.SubItems.Add(cmt.Date.ToShortDateString());                
                item.SubItems.Add(cmt.Text);
                listView1.Items.Add(item);
                textBox21.Text = "";
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            func = new ClFunctions();
            for (int i = listView1.CheckedIndices.Count - 1; i >= 0; i--)
            {
                //MessageBox.Show(listBox2.Items[listBox2.SelectedIndices[i]] as Img).Name);
                if ((Program.user.IsAdmin) || (listView1.Items[listView1.CheckedIndices[i]].SubItems[1].Text == Program.user.email))
                {
                    dbcomm.DeleteCommentFromDB(listView1.CheckedIndices[i]);
                    dbcomm.CommentsList.RemoveAt(listView1.CheckedIndices[i]);
                    listView1.Items.RemoveAt(listView1.CheckedIndices[i]);                   
                }
                else
                { MessageBox.Show("Комментарий пользователя "+ listView1.Items[listView1.CheckedIndices[i]].SubItems[0].Text + " не может быть удален", "Внимание", MessageBoxButtons.OK); }
            }            
        }


        private void Notify(bool allin, int index)
        {
            string authormail = "";
            string emails = Program.user.email;
            if (Program.user.Id != record.Rid.ToString())
            {
                MySqlConnection con = new MySqlConnection(dbcomm.Connect);
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT Ag_mail from agents WHERE (Ag_id = @Val1)", con);
                cmd.Parameters.AddWithValue("@Val1", record.Rid);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    authormail = reader.GetString(0);
                }
                con.Close();
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.To.Add(authormail);
                message.Subject = String.Concat("Комментарий к объекту ", record.Rid.ToString(), (dbcomm.CommentsList[index] as Comment).Date);
                message.From = new System.Net.Mail.MailAddress(Program.user.email);
                message.Body = (dbcomm.CommentsList[index] as Comment).Text;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("yoursmtphost");
                smtp.Send(message);
            }
            if (allin)
            {
                foreach (Comment cmt in dbcomm.CommentsList)
                {
                    
                }
            }
            
        }


        private void simpleButton8_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems[0].SubItems[1].Text == Program.user.email)
            {
                DateTime save = DateTime.Now;
                (dbcomm.CommentsList[listView1.SelectedIndices[0]] as Comment).Text = textBox21.Text;
                (dbcomm.CommentsList[listView1.SelectedIndices[0]] as Comment).Date = save;
                listView1.Items[listView1.SelectedIndices[0]].SubItems[2].Text = save.ToShortTimeString();
                dbcomm.UpdateCommentInDb(listView1.SelectedIndices[0], save);

                 
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox21.Text = (dbcomm.CommentsList[listView1.SelectedIndices[0]] as Comment).Text;
            textBox21.Enabled = false;
            if ((dbcomm.CommentsList[listView1.SelectedIndices[0]] as Comment).Email == Program.user.email)
            {
                textBox21.Enabled = true;
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            Program.objectedit.Close();
        }
    }    

}
