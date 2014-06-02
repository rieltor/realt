using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using SharpCompress;
using SharpCompress.Archive;
using SharpCompress.Common;

using System.Xml;
using System.Net;
using System.Xml.Linq;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace realty
{
    public partial class LoadXML : Form
    {
        string flname;
        ClFunctions func;
        string unzipDir;
        List<ImageStruct> Images;
        public LoadXML()
        {
            InitializeComponent();
            
        }




        private string unzipFile( string  flname1)
        {
                     
                DirectoryInfo dirInfo;
                FileInfo[] tempFiles;
                if (!Directory.Exists(unzipDir)) dirInfo = Directory.CreateDirectory(unzipDir);
                else dirInfo = new DirectoryInfo(unzipDir);

                string result = "";

                var archive = ArchiveFactory.Open(flname1);
                foreach (var entry in archive.Entries)
                {
                    if (!entry.IsDirectory)
                    {
                        entry.WriteToDirectory(unzipDir, ExtractOptions.None | ExtractOptions.Overwrite);
                    }
                }
                    
                tempFiles = dirInfo.GetFiles();
                if (tempFiles.Length==0) {
                    MessageBox.Show("Возникла ошибка, возможно файл загрузки пуст или имеет неверный формат");
                    return "";
                }
                string tempstr;
                ImageStruct img;
                for (int i=0; i<tempFiles.Length; i++)
                {
                    tempstr = tempFiles[i].FullName;
                    if (tempstr.IndexOf(".xml") == tempstr.Length - 4)
                    {
                        flname1 = tempstr;
                        result = flname1;
                    }
                    else
                    {
                        tempstr = tempstr.ToLower();
                        img.FullPath = tempstr;
                        img.name = tempFiles[i].Name;
                        Images.Add(img);
                       
                    }
                }
           // dirInfo.Delete(true);    
            return result;
        }
        private void btSelectXML_Click(object sender, EventArgs e)
        {
            
            if (openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                if (openFileDialog1.CheckFileExists == false)
                {
                    MessageBox.Show("Файл не выбран");
                    return;
                }
                
                flname = openFileDialog1.FileName;
               

                

                if (flname != "")
                {
                    btLaunch.Enabled = true;
                    txbFileName.Text = flname;
                }

               
               
           }
        }

     

        private void LoadXML_Load(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Zip files (*.zip)|*.zip|XML files (*.xml)|*.xml";
            openFileDialog1.FileName = "";
            openFileDialog1.InitialDirectory = Application.StartupPath;
            btLaunch.Enabled = false;
            func = new ClFunctions();
            Images = new List<ImageStruct>();
            unzipDir = Application.StartupPath + "\\tempFolder";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        //сдеаль: проверить загрузку.  progressbar
        private void btLaunch_Click(object sender, EventArgs e)
        {
              
                //string connStr =func.connStr;
                //MySqlConnection conn = new MySqlConnection(connStr);
               DirectoryInfo dirInfo;
             
               btLaunch.Enabled = false;
               btSelectXML.Enabled = false;
               txbMessage.Text = "";
               if (Directory.Exists(unzipDir))
               {
                   dirInfo = new DirectoryInfo(unzipDir);
                   dirInfo.Delete(true);//удаление директории, в которую распакован архив
               }

               if (flname.IndexOf(".zip")==flname.Length-4)
               {
                   
                   flname=unzipFile(flname);
                   if (flname=="")   
                   {
                       txbMessage.Text += "Неверный формат файла " + Environment.NewLine;
                        return;
                   }
                     if (Directory.Exists(unzipDir)) dirInfo = new DirectoryInfo(unzipDir);
                }

                XmlDocument doc = new XmlDocument();
                doc.Load(flname);             
                try
                {
                    XmlNodeList nodelist;                   
                    XmlNodeList IDList;
                    XmlNodeList DescriptionList;

                    string estate_type="";
                    string street = "";
                    string city = "";
                    string house_no = "";
                    string block_no = "";
                    string flat_no = "";
                    string operation = "";
                    string rec_decsep = "", cur_decsep="";
                    string agent_code = "1";

                    string key = func.fkey;
                   
                 
                    //string addressSql=""; 
                    string allphotos="";

                    //StatusList=doc.GetElementsByTagName("STATUS");
                    //запомнить идентиф для вывода сообщений об ошибках
                    IDList = doc.GetElementsByTagName("SUBJECT_ID");
                    
                    //название операции по объекту
                    nodelist=doc.GetElementsByTagName("SALE");
                    if (nodelist.Count > 0) operation = "Продажа";
                    nodelist = doc.GetElementsByTagName("LEASE"); //съем
                    if (nodelist.Count > 0)
                    {
                        txbMessage.Text += "Операция \"съем\" не поддерживается "+ Environment.NewLine;
                        return;
                        
                    }
                   
                    nodelist = doc.GetElementsByTagName("RENT");
                    if (nodelist.Count > 0) operation = "Аренда";
                    nodelist = doc.GetElementsByTagName("EXCHANGE");
                    if (nodelist.Count > 0) operation = "Обмен";

               
                    //десятичный разделитель
                    nodelist = doc.GetElementsByTagName("DECIMAL_SEPARATOR");
                    if (nodelist.Count > 0) rec_decsep = nodelist.Item(0).InnerText.Trim();
                    cur_decsep = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

                    DescriptionList = doc.GetElementsByTagName("DESCRIPTION");
                    XmlNode node;
                  
                    string sql = "",sql1="";
                    List<TownStruct> TownList=new List<TownStruct>();
                    
                    TownStruct sometown;
                   
                    string errormessage="";
                   // DataTable sqlDataTable;
                    string tmp="";
                    string cur_item = "";
                    string remark= "", remarks="", orientir="";
                    double dblnum;
                    List<DistrictsStruct> districts = func.getDistricts(out errormessage);
                    DistrictsStruct cur_district;
                    if (errormessage != "") { txbMessage.Text += "Ошибка доступа к базе "; return; }
                    errormessage = "";
                    List<AddressStruct> Addresses = func.getAllAddresses(out errormessage);
                    AddressStruct someaddress;
                    if (errormessage != "") { txbMessage.Text += "Ошибка доступа к базе "; return; }

                   
                    PhotoStruct cur_photo;
                    List<PhotoStruct> photos = new List<PhotoStruct>();
                    string[] tmstr_array;

                    progressBar1.Maximum = DescriptionList.Count;

                    for (int i = 0; i < DescriptionList.Count; i++)
                    {
                        Application.DoEvents();
                                         
                        node = DescriptionList.Item(i); // описание каждого объекта
                        
                        sql = "";
                        estate_type = "";
                        street = "";
                        city = "";
                        house_no = "";
                        block_no = "";
                        flat_no = "";
                        errormessage="";
                       

                        // проверить адрес,  если адрес неполный, то выдать сообщение ,  в противном случае проверить есть ли данный объект в базе
                        nodelist = ((XmlElement)node).GetElementsByTagName("ESTATE_TYPE");
                        if (nodelist.Count > 0)
                        {
                            estate_type = nodelist.Item(0).InnerText.Trim().ToUpper();
                            bool iscorrect=false;
                            for (int k=0; k< func.estate_typeValues.Length; k++)
                            {
                                if (func.estate_typeValues[k].ToUpper()== estate_type) 
                                {
                                    estate_type=func.estate_typeValues[k];
                                    iscorrect=true;
                                    break;
                                }
                            }
                            if (iscorrect==false) 
                            {
                                 txbMessage.Text += "Для объекта с идентификатором № " + IDList.Item(i).InnerText + " неподдерживаемый тип недвижимости:"+estate_type+", объект не будет записан в базу " + Environment.NewLine;
                                 progressBar1.PerformStep();
                                 continue;
                            }
                            //sql += " Aestate_type='" + estate_type+"'";
                           // addresitem_count++;
                        }

                        nodelist = ((XmlElement)node).GetElementsByTagName("CITY");
                        if (nodelist.Count > 0){city = nodelist.Item(0).InnerText.Trim(); }

                        nodelist = ((XmlElement)node).GetElementsByTagName("STREET");
                        if (nodelist.Count > 0){street = nodelist.Item(0).InnerText.Trim().ToLower(); }

                        nodelist = ((XmlElement)node).GetElementsByTagName("HOUSE_NO");
                        if (nodelist.Count > 0)                        {
                            house_no = nodelist.Item(0).InnerText.Trim ();
                            house_no = func.Encrypt(house_no, key);   
                            if (house_no.IndexOf("/")>0)
                            {
                                tmstr_array = house_no.Split('/');
                                block_no = tmstr_array[tmstr_array.Length - 1];
                                house_no= tmstr_array[0];
                            }
                            else
                            {
                                tmp=house_no[house_no.Length -1].ToString();
                                if (Double.TryParse(tmp, out dblnum) == false) block_no = tmp;
                            }
                            tmp = "";
                        }

                        nodelist = ((XmlElement)node).GetElementsByTagName("BLOCK_NO");
                        if (nodelist.Count > 0)
                        {
                            block_no = nodelist.Item(0).InnerText.Trim();
                          
                        }
                        nodelist = ((XmlElement)node).GetElementsByTagName("FLAT_NO");
                        if (nodelist.Count > 0)
                        {
                            flat_no = nodelist.Item(0).InnerText.Trim();
                            flat_no = func.Encrypt(flat_no, key);
                        }

                        if (city == "")
                        {
                            txbMessage.Text += "Для объекта с идентификатором № " + IDList.Item(i).InnerText + " не задан город (нас. пункт), объект не будет записан в базу " + Environment.NewLine;
                            progressBar1.PerformStep();
                            continue;
                        }
                        else
                        {
                            city=func.preparecity(city);
                            //запомнить улицы для загружаемых городов
                            sometown=TownList.Find(n=>n.name==city);
                            if (sometown.rr_code == null && sometown.rr_code !="")
                            {
                                sometown.streets=func.getStreets(city,  out sometown.rr_code, out errormessage);
                                if (errormessage != "") { txbMessage.Text += "Ошибка доступа к базе "; return; }                               
                                if (sometown.rr_code != null && sometown.rr_code!="") {  TownList.Add(sometown);sometown.name = city; city = sometown.rr_code; }
                                else
                                {
                                    txbMessage.Text += "Для объекта с идентификатором № " + IDList.Item(i).InnerText + " неверно не задан город (нас. пункт), объект не будет записан в базу " + Environment.NewLine;
                                    progressBar1.PerformStep();
                                    continue;
                                }
                            }
                        }

                        if (street == "" &&  sometown.streets!=null && sometown.streets.Count > 0)
                        {
                            txbMessage.Text += "Для объекта с идентификатором № " + IDList.Item(i).InnerText + " не задана улица, объект не будет записан в базу " + Environment.NewLine;
                            progressBar1.PerformStep();
                            continue;
                        }
                        else
                        {
                            if (street != "" && sometown.streets != null && sometown.streets.Count > 0)
                            {
                                
                                tmp = func.findStreet(sometown.streets,street);
                                if (tmp != null && tmp != "") street = tmp;//если улица не найдена в списке улиц
                                else
                                {
                                    txbMessage.Text += "Для объекта с идентификатором № " + IDList.Item(i).InnerText + " неверно  введено название улицы (такой нет в базе), объект не будет записан в базу " + Environment.NewLine;
                                    progressBar1.PerformStep();
                                    continue;
                                }
                            }
                        
                              
                        }
                        if (estate_type!= "Земельный участок")
                        {
                                //house_no
                                if (house_no == "")
                                {
                                    txbMessage.Text += "Для объекта с идентификатором № " + IDList.Item(i).InnerText + " не задан номер дома, объект не будет записан в базу " + Environment.NewLine;
                                    progressBar1.PerformStep();
                                    continue;
                                }
                               //flat_no
                                if (estate_type=="Квартира" &&  flat_no=="")
                                {
                                    txbMessage.Text += "Для объекта с идентификатором № " + IDList.Item(i).InnerText + " не задан номер квартиры, объект не будет записан в базу " + Environment.NewLine;
                                    progressBar1.PerformStep();
                                    continue;
                                }
                                //if (sql!="") {
                                //    addressSql="INSERT INTO raddress  SET "+sql;
                                //}
                                //sql = "SELECT Rid FROM raddress WHERE " + sql;
                                //sqlDataTable = func.ReadFromTable(sql, out errormessage);

                                if (city != "" || house_no != "" || block_no != "" || flat_no != "" || street != "")
                                {
                                    if (city != "" && house_no != "" && block_no != "" && flat_no != "" && street != "")
                                    {
                                        if (Addresses.Find(n => n.block_no == block_no && n.flat_no == flat_no && n.city == city && n.street == street && n.house_no == house_no).city!= null)
                                        {
                                            txbMessage.Text += "Объект, обозначенный в файле загрузки идентификатором № " + IDList.Item(i).InnerText + "уже существует в базе" + Environment.NewLine;
                                            progressBar1.PerformStep();
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        if (city != "" && house_no != "" && flat_no != "" && street != "")
                                        {
                                            if (Addresses.Find(n => n.flat_no == flat_no && n.city == city && n.street == street && n.house_no == house_no).city != null)
                                            {
                                                txbMessage.Text += "Объект, обозначенный в файле загрузки идентификатором № " + IDList.Item(i).InnerText + "уже существует в базе" +  Environment.NewLine;
                                                progressBar1.PerformStep();
                                                continue;
                                            }
                                        }

                                        else
                                        {

                                            if (city != "" && house_no != "" && street != "")
                                            {
                                                if (Addresses.Find(n => n.city == city && n.street == street && n.house_no == house_no).city != null)
                                                {
                                                    txbMessage.Text += "Объект, обозначенный в файле загрузки идентификатором № " + IDList.Item(i).InnerText + "уже существует в базе" + Environment.NewLine;
                                                    progressBar1.PerformStep();
                                                    continue;
                                                }
                                            }

                                            else
                                            {
                                                if (city != "" && house_no != "")
                                                {
                                                    if (Addresses.Find(n => n.city == city && n.house_no == house_no).city != null)
                                                    {
                                                        txbMessage.Text += "Объект, обозначенный в файле загрузки идентификатором № " + IDList.Item(i).InnerText + "уже существует в базе" + Environment.NewLine;
                                                        progressBar1.PerformStep();
                                                        continue;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                   
                                }
                                someaddress.city = city;
                                someaddress.street = street;
                                someaddress.house_no = house_no;
                                someaddress.block_no = block_no;
                                someaddress.flat_no = flat_no;
                                Addresses.Add(someaddress);  

                        }
                       /// else sql = "SELECT Rid FROM rterra WHERE " + sql; для земель пока убрать проверку на уникальность

                       
                        
                        sql = "";
                        sql1 = "";
                        remarks = "";
                        orientir = "";
                        if (estate_type!= "Земельный участок")
                        {
                            sql = "INSERT INTO realestate  SET Rcity='"+sometown.rr_code+"'";
                            if (estate_type!="")sql += ", Restate_type ='" + estate_type + "' ";
                           
                            if (operation != "") sql += ", Roperation='" + operation + "'";
                            //else sql = "INSERT INTO terra  SET Restate_type ='" + estate_type + "' ";
                            foreach (XmlNode descript in node.ChildNodes)
                            {
                                errormessage = "";
                                if (descript.Name == "REFERENCE_POINT") orientir += descript.InnerText.Trim() + " ";
                             

                                if (descript.Name == "STREET")
                                {
                                    sql += ", Rstreet='" + street + "'";
                                }

                                if (descript.Name == "HOUSE_NO")
                                {
                                    sql += ", Rhouse_no='" + house_no + "'";
                                }

                                if (descript.Name == "BLOCK_NO")
                                {
                                    sql += ", Rblock_no='" + block_no + "'";
                                }

                                if (descript.Name == "FLAT_NO")
                                {
                                    sql += ", Rflat_no='" + flat_no + "'";
                                }


                                if (descript.Name == "PRICE")
                                {
                                    tmp = descript.InnerText.Trim();
                                    if (rec_decsep != "") tmp = tmp.Replace(rec_decsep, cur_decsep);
                                    if (Double.TryParse(tmp, out dblnum)) sql += ", Rprice=" + dblnum.ToString();

                                }
                                if (descript.Name == "USE_FOR_OFFICE")
                                {
                                    sql += ", Ruse_for_office=1";

                                }

                                if (descript.Name == "HOUSE_TYPE")
                                {
                                    cur_item = func.toUpperFirst(descript.InnerText);
                                    if (!func.housetypeValues.Contains(cur_item)) remarks += "Тип дома:" + cur_item + Environment.NewLine;
                                    else sql += ", Rhouse_type='" + cur_item + "'";

                                }

                                if (descript.Name == "WALLING_TYPE")
                                {

                                    sql1 = ", Rwalling_type='";
                                    int counter = 0;
                                    remark = "";
                                    foreach (XmlNode someItem in descript.ChildNodes)
                                    {

                                        cur_item = "";
                                        if (someItem.Name == "ITEM")
                                        {
                                           
                                            cur_item =func.toUpperFirst(someItem.InnerText);
                                            if (!func.wallValues.Contains(cur_item)) remark += cur_item + ",";
                                            else
                                            {
                                                if (counter > 0) sql1 += ", ";
                                                sql1 += cur_item;
                                                counter++;
                                            }
                                           

                                        }
                                    }

                                    if (counter > 0) sql += sql1 + "'";
                                    else if (remark == "") { remark = descript.InnerText.Trim(); }   //записать описание в примечание                               
                                    if (remark != "") 
                                    {
                                        if (remark[remark.Length - 1]==',') remark=remark.Remove(remark.Length - 1);
                                        remarks += " Материал постройки:" + remark+Environment.NewLine; 
                                    }

                                }

                                if (descript.Name == "ROOM_QUANTITY")
                                {
                                    tmp = descript.InnerText.Trim();
                                    if (rec_decsep != "") tmp = tmp.Replace(rec_decsep, cur_decsep);
                                    if (Double.TryParse(tmp, out dblnum)) sql += ", Rroom_quantity=" + dblnum.ToString();


                                }

                                if (descript.Name == "FLOOR")
                                {

                                    tmp = descript.InnerText.Trim();
                                    if (rec_decsep != "") tmp = tmp.Replace(rec_decsep, cur_decsep);
                                    if (Double.TryParse(tmp, out dblnum)) sql += ", Rfloor=" + dblnum.ToString();
                                }

                                if (descript.Name == "NUMBER_OF_STOREYS") //этажность здания
                                {
                                    tmp = descript.InnerText.Trim();
                                    if (rec_decsep != "") tmp = tmp.Replace(rec_decsep, cur_decsep);
                                    if (Double.TryParse(tmp, out dblnum)) sql += ", Rnumber_of_storeys=" + dblnum.ToString();


                                }
                                if (descript.Name == "TOTAL_FLOOR_AREA" || descript.Name == "TOTAL_FLOOR_SPACE")
                                {
                                    tmp = descript.InnerText.Trim();
                                    if (rec_decsep != "") tmp = tmp.Replace(rec_decsep, cur_decsep);
                                    if (Double.TryParse(tmp, out dblnum)) sql += ", Rtotal_floor_space=" + dblnum.ToString();
                                }

                                if (descript.Name == "FLOOR_AREA" || descript.Name == "FLOOR_SPACE")
                                {
                                    tmp = descript.InnerText.Trim();
                                    if (rec_decsep != "") tmp = tmp.Replace(rec_decsep, cur_decsep);
                                    if (Double.TryParse(tmp, out dblnum)) sql += ", Rfloor_space=" + dblnum.ToString();
                                }

                                if (descript.Name == "KITCHEN_FLOOR_AREA" || descript.Name == "KITCHEN_FLOOR_SPACE")
                                {
                                    sql += ", Rkitchen_floor_space='" + descript.InnerText.Trim() + "'";

                                }
                                if (descript.Name == "HALL_FLOOR_AREA" || descript.Name == "HALL_FLOOR_SPACE")
                                {
                                    sql += ", Rhall_floor_space='" + descript.InnerText.Trim() + "'";

                                }

                                if (descript.Name == "ROOM_LAYOUT")
                                {
                                    cur_item = func.toUpperFirst(descript.InnerText);
                                    if (!func.roomlayotValues.Contains(cur_item)) remarks += " Планировка комнат:" + cur_item + Environment.NewLine;
                                    else sql += ", Rroom_layout='" + cur_item + "'";

                                }

                                if (descript.Name == "TELEPHONE")
                                {

                                    sql1 = ", Rtelephone='";
                                    int counter = 0;
                                    remark = "";
                                    foreach (XmlNode someItem in descript.ChildNodes)
                                    {

                                        cur_item = "";
                                        if (someItem.Name == "ITEM")
                                        {
                                            cur_item = func.toUpperFirst(someItem.InnerText);
                                            if (!func.phoneValues.Contains(cur_item)) remark += cur_item + ",";
                                            else
                                            {
                                                if (counter > 0) sql1 += ", ";
                                                sql1 += cur_item;
                                                counter++;
                                            }

                                        }
                                    }
                                    if (counter > 0) sql += sql1 + "'";
                                    else if (remark == "") { remark = descript.InnerText.Trim(); }
                                    if (remark != "") 
                                    {
                                        if (remark[remark.Length - 1]==',') remark=remark.Remove(remark.Length - 1);
                                        remarks += " Телефон:" + remark+Environment.NewLine;    
                                    }
                                }


                                if (descript.Name == "GAS_SUPPLY")
                                {

                                    sql1 = ", Rgas_supply='";                                   
                                    int counter = 0;
                                    remark = "";
                                    foreach (XmlNode someItem in descript.ChildNodes)
                                    {

                                        cur_item = "";
                                        if (someItem.Name == "ITEM")
                                        {

                                            cur_item = func.toUpperFirst(someItem.InnerText) ;
                                            if (!func.gasValues.Contains(cur_item)) remark += cur_item + ",";
                                            else
                                            {
                                                if (counter > 0) sql1 += ", ";
                                                sql1 += cur_item;
                                                counter++;
                                            }

                                        }
                                    }
                                    if (counter > 0) sql += sql1 + "'";
                                    else if (remark == "") { remark = descript.InnerText.Trim(); }
                                    if (remark != "") 
                                    {
                                        if (remark[remark.Length - 1]==',') remark=remark.Remove(remark.Length - 1);
                                        remarks += " Газ:" + remark + Environment.NewLine;  
                                    }

                                }

                                if (descript.Name == "WATER_SUPPLY")
                                {

                                    sql1 = ", Rwater_supply='";
                                    int counter = 0;
                                    remark = "";
                                    foreach (XmlNode someItem in descript.ChildNodes)
                                    {

                                        cur_item = "";
                                        if (someItem.Name == "ITEM")
                                        {
                                            cur_item = func.toUpperFirst(someItem.InnerText);
                                            if (!func.waterValues.Contains(cur_item)) remark += cur_item + ",";
                                            else
                                            {
                                                if (counter > 0) sql1 += ", ";
                                                sql1 += cur_item;
                                                counter++;
                                            }

                                        }
                                    }
                                    if (counter > 0) sql += sql1 + "'";
                                    else if (remark == "") { remark = descript.InnerText.Trim(); }
                                    if (remark != "") 
                                    {
                                        if (remark[remark.Length - 1]==',') remark=remark.Remove(remark.Length - 1);
                                        remarks += " Вода:" + remark+ Environment.NewLine; 
                                    }

                                }


                                if (descript.Name == "HEAT_SUPPLY")
                                {

                                    sql1 = ", Rheat_supply='";
                                    int counter = 0;
                                    remark = "";
                                    foreach (XmlNode someItem in descript.ChildNodes)
                                    {

                                        cur_item = "";
                                        if (someItem.Name == "ITEM")
                                        {
                                            cur_item = func.toUpperFirst(someItem.InnerText);
                                            if (!func.heatValues.Contains(cur_item)) remark += cur_item + ",";
                                            else
                                            {
                                                if (counter > 0) sql1 += ", ";
                                                sql1 += cur_item;
                                                counter++;
                                            }

                                        }
                                    }
                                    if (counter > 0) sql += sql1 + "'";
                                    else if (remark == "") { remark = descript.InnerText.Trim(); }
                                    if (remark != "") 
                                    {
                                        if (remark[remark.Length - 1]==',') remark=remark.Remove(remark.Length - 1);
                                        remarks += " Отопление:" + remark + Environment.NewLine; 
                                    }

                                }


                                if (descript.Name == "BATHROOM")
                                {

                                    sql1 = ", Rbathroom='";
                                    int counter = 0;
                                    remark = "";
                                    foreach (XmlNode someItem in descript.ChildNodes)
                                    {

                                        cur_item = "";
                                        if (someItem.Name == "ITEM")
                                        {
                                            cur_item = func.toUpperFirst(someItem.InnerText);
                                            if (!func.bathValues.Contains(cur_item)) remark += cur_item + ",";
                                            else
                                            {
                                                if (counter > 0) sql1 += ", ";
                                                sql1 += cur_item;
                                                counter++;
                                            }

                                        }
                                    }
                                    if (counter > 0) sql += sql1 + "'";
                                    else if (remark == "") { remark = descript.InnerText.Trim(); }
                                    if (remark != "") 
                                    {
                                        if (remark[remark.Length - 1]==',') remark=remark.Remove(remark.Length - 1);
                                        remarks += " Санузел:" + remark + Environment.NewLine; 
                                    }
                                }

                                if (descript.Name == "BATHROOM_NOTE")
                                {
                                    sql += ", Rbathroom_note='" + descript.InnerText.Trim() + "'";

                                }

                                if (descript.Name == "BALCONY")
                                {

                                    sql1 = ", Rbalcony='";
                                    int counter = 0;
                                    remark = "";
                                    foreach (XmlNode someItem in descript.ChildNodes)
                                    {

                                        cur_item = "";
                                        if (someItem.Name == "ITEM")
                                        {
                                            cur_item = func.toUpperFirst(someItem.InnerText);
                                            if (!func.balconyValues.Contains(cur_item)) remark += cur_item + ",";
                                            else
                                            {
                                                if (counter > 0) sql1 += ", ";
                                                sql1 += cur_item;
                                                counter++;
                                            }

                                        }
                                    }
                                    if (counter > 0) sql += sql1 + "'";
                                    else if (remark == "") { remark = descript.InnerText.Trim(); }
                                    if (remark != "")
                                    {
                                        if (remark[remark.Length - 1]==',') remark=remark.Remove(remark.Length - 1);
                                        remarks += " Балкон:" + remark + Environment.NewLine; 
                                    }
                                }

                                if (descript.Name == "LOGGIA")
                                {

                                    sql1 = ", Rloggia='";
                                    int counter = 0;
                                    remark = "";
                                    foreach (XmlNode someItem in descript.ChildNodes)
                                    {

                                        cur_item = "";
                                        if (someItem.Name == "ITEM")
                                        {
                                            cur_item = func.toUpperFirst(someItem.InnerText);
                                            if (!func.loggiaValues.Contains(cur_item)) remark += cur_item + ",";
                                            else
                                            {
                                                if (counter > 0) sql1 += ", ";
                                                sql1 += cur_item;
                                                counter++;
                                            }

                                        }
                                    }
                                    if (counter > 0) sql += sql1 + "'";
                                    else if (remark == "") { remark = descript.InnerText.Trim(); }
                                    if (remark != "")
                                    {
                                        if (remark[remark.Length - 1] == ',') remark = remark.Remove(remark.Length - 1);
                                        remarks += " Лоджия:" + remark + Environment.NewLine; 
                                    }
                                       
                                }


                                if (descript.Name == "KITCHEN_NOTE")
                                {
                                    sql += ", Rkitchen_note='" + descript.InnerText.Trim() + "'";

                                }

                                if (descript.Name == "INTERIOR_DOOR_TYPE")
                                {

                                    sql1 = ", Rinterior_door_type='";
                                    int counter = 0;
                                    remark = "";
                                    foreach (XmlNode someItem in descript.ChildNodes)
                                    {

                                        cur_item = "";
                                        if (someItem.Name == "ITEM")
                                        {
                                            cur_item = func.toUpperFirst(someItem.InnerText);
                                            if (!func.intdoorValues.Contains(cur_item)) remark += cur_item + ",";
                                            else
                                            {
                                                if (counter > 0) sql1 += ", ";
                                                sql1 += cur_item;
                                                counter++;
                                            }

                                        }
                                    }
                                    if (counter > 0) sql += sql1 + "'";
                                    else if (remark == "") { remark = descript.InnerText.Trim(); }
                                    if (remark != "")
                                    {
                                        if (remark[remark.Length - 1] == ',') remark = remark.Remove(remark.Length - 1);
                                        remarks += " Столярка/Двери:" + remark + Environment.NewLine;
                                    }
                                }

                                if (descript.Name == "PROPERTY_TYPE")
                                {
                                    cur_item = func.toUpperFirst(descript.InnerText);
                                    if (!func.propertyValues.Contains(cur_item)) remarks = "Вид собственности:" + cur_item + Environment.NewLine;
                                    else   sql += ", Rproperty_type='" + cur_item + "'";

                                }

                                if (descript.Name == "NOTE")remarks += descript.InnerText.Trim()+ Environment.NewLine;
                              

                                if (descript.Name == "CEILING_HEIGHT")
                                {
                                    sql += ", Rceiling_height='" + descript.InnerText.Trim() + "'";

                                }

                                if (descript.Name == "WINDOW_MATERIAL")
                                {

                                    sql1 = ", Rwindow_material='";
                                    int counter = 0;
                                    remark = "";
                                    foreach (XmlNode someItem in descript.ChildNodes)
                                    {

                                        cur_item = "";
                                        if (someItem.Name == "ITEM")
                                        {
                                            cur_item = func.toUpperFirst(someItem.InnerText);
                                            if (!func.windowValues.Contains(cur_item)) remark += cur_item + ",";
                                            else
                                            {
                                                if (counter > 0) sql1 += ", ";
                                                sql1 += cur_item;
                                                counter++;
                                            }

                                        }
                                    }
                                    if (counter > 0) sql += sql1 + "'";
                                    else if (remark == "") { remark = descript.InnerText.Trim(); }
                                    if (remark != "")
                                    {
                                        if (remark[remark.Length - 1] == ',') remark = remark.Remove(remark.Length - 1);
                                        remarks += " Столярка/Окна:" + remark + Environment.NewLine;
                                    }
                                }

                                if (descript.Name == "DISTANCE_TO_CITY")
                                {
                                    sql += ", Rdistance_to_city='" + descript.InnerText.Trim() + "'";

                                }

                                if (descript.Name == "PRICE_SQUARE")
                                {

                                    tmp = descript.InnerText.Trim();
                                    if (rec_decsep != "") tmp = tmp.Replace(rec_decsep, cur_decsep);
                                    if (Double.TryParse(tmp, out dblnum)) sql += ", Rprice_square=" + dblnum.ToString();

                                }


                                if (descript.Name == "DISTRICT_REGION")
                                {
                                    tmp = "";
                                    if (city == "1500000100000") tmp = "1500000000000";
                                    else
                                    {
                                        tmp = descript.InnerText.Trim().ToLower();
                                        cur_district = districts.Find(n => n.rr_town.ToLower().IndexOf(tmp) >= 0);
                                        if (cur_district.rr_code != null && cur_district.rr_code!="") tmp = cur_district.rr_code;
                                        else { orientir += tmp + " "; tmp = ""; }
                                    }
                                                                      
                                   if(tmp!="") sql += ", Rdistrict_region='" + tmp + "'";

                                }

                                if (descript.Name == "TITLE")
                                {
                                    sql += ", Rtitle='" + descript.InnerText.Trim() + "'";

                                }
                                if (descript.Name == "DESCRIPTION_SHORT")
                                {
                                    sql += ", Rdescription_short='" + descript.InnerText.Trim() + "'";

                                }

                                if (descript.Name == "DESCRIPTION_DETAIL")
                                {
                                    sql += ", Rdescription_detail='" + descript.InnerText.Trim() + "'";

                                }

                              

                                if (descript.Name == "HAGGLE")//возможен торрг
                                {
                                    sql += ", Rhaggle=1";

                                }

                                if (descript.Name == "OUTHOUSE_LEGALITY")//законность пристоек
                                {
                                    sql += ", Routhouse_legality='" + descript.InnerText.Trim() + "'";

                                }

                                if (descript.Name == "LEVEL_QUANTITY")
                                {
                                    tmp = descript.InnerText.Trim();
                                    if (rec_decsep != "") tmp = tmp.Replace(rec_decsep, cur_decsep);
                                    if (Double.TryParse(tmp, out dblnum)) sql += ", Rlevel_quantity=" + dblnum.ToString();
                                }

                                if (descript.Name == "PARKING")
                                {
                                    sql += ", Rparking=1";

                                }
                                if (descript.Name == "CONDITION2") //общее состояние
                                {
                                    cur_item = func.toUpperFirst(descript.InnerText);
                                    if (!func.conditionValues.Contains(cur_item)) remarks += " Общее состояние:" + cur_item + Environment.NewLine;
                                    else sql += ", Rcondition2='" +  cur_item + "'";

                                }


                                if (descript.Name == "TUBING_MATERIAL")
                                {

                                    sql1 = ", Rtubing_material='";
                                    int counter = 0;
                                    remark = "";
                                    foreach (XmlNode someItem in descript.ChildNodes)
                                    {

                                        cur_item = "";
                                        if (someItem.Name == "ITEM")
                                        {
                                            cur_item = func.toUpperFirst(someItem.InnerText);
                                            if (!func.tubeValues.Contains(cur_item)) remark += cur_item + ",";
                                            else
                                            {
                                                if (counter > 0) sql1 += ", ";
                                                sql1 += cur_item;
                                                counter++;
                                            }

                                        }
                                    }
                                    if (counter > 0) sql += sql1 + "'";
                                    else if (remark == "") { remark = descript.InnerText.Trim(); }
                                    if (remark != "")
                                    {
                                        if (remark[remark.Length - 1] == ',') remark = remark.Remove(remark.Length - 1);
                                        remarks += " Трубы:" + remark + Environment.NewLine;
                                    }
                                }

                                if (descript.Name == "BACKROOMS")
                                {
                                    sql += ", Rbackrooms='" + descript.InnerText.Trim() + "'";

                                }

                                if (descript.Name == "IS_SPECIAL_PROPORSAL")
                                {
                                    sql += ", Ris_special_proposal=1";

                                }

                                if (descript.Name == "CHANGE_ONLY")
                                {
                                    sql += ", Rchange_only='" + descript.InnerText.Trim() + "'";

                                }
                                if (descript.Name == "NEWBUILDING")
                                {
                                    sql += ", Rnewbuilding=1";

                                }
                                if (descript.Name == "HYPOTHEC")
                                {
                                    sql += ", Rhypothec=1";

                                }

                                if (descript.Name == "RENT_PRICE_DAY")
                                {
                                    
                                        sql += ", Rrent_price_day='" + descript.InnerText.Trim() + "'";

                                }
                                if (descript.Name == "RENT_PRICE_MONTH")
                                {
                                    sql += ", Rrent_price_month='" + descript.InnerText.Trim() + "'";

                                }

                                if (descript.Name == "ACCOUNT_IN_RENT")
                                {
                                    sql += ", Raccount_in_rent=1";

                                }

                                if (descript.Name == "BUILDING_YEAR")
                                {
                                    sql += ", Rbuilding_year='" + descript.InnerText.Trim() + "'";

                                }

                                if (descript.Name == "FRONT_DOOR_TYPE")
                                {

                                    sql1 = ", Rfront_door_type='";
                                    int counter = 0;
                                    remark = "";
                                    foreach (XmlNode someItem in descript.ChildNodes)
                                    {

                                        cur_item = "";
                                        if (someItem.Name == "ITEM")
                                        {
                                            cur_item = func.toUpperFirst(someItem.InnerText);
                                            if (!func.front_door_typeValues.Contains(cur_item)) remark += cur_item + ",";
                                            else
                                            {
                                                if (counter > 0) sql1 += ", ";
                                                sql1 += cur_item;
                                                counter++;
                                            }

                                        }
                                    }
                                    if (counter > 0) sql += sql1 + "'";
                                    else if (remark == "") { remark = descript.InnerText.Trim(); }
                                    if (remark != "")
                                    {
                                        if (remark[remark.Length - 1] == ',') remark = remark.Remove(remark.Length - 1);                                        
                                        remarks += " Входная Дверь:" + remark + Environment.NewLine;
                                    }
                                }

                                //if (descript.Name == "ROOF")
                                //{

                                //    sql1 = ", Rroof='";
                                //    int counter = 0;
                                //    remark = "";
                                //    foreach (XmlNode someItem in descript.ChildNodes)
                                //    {
                                //        if (someItem.Name == "ITEM")
                                //        {
                                //            if (counter > 0) sql1 += ", ";
                                //            sql1 += someItem.InnerText.Trim();
                                //            counter++;

                                //        }
                                //    }
                                //    if (counter > 0) sql += sql1 + "'";
                                //    else { remark = descript.InnerText.Trim(); }
                                //    if (remark != "") sql += sql1+remark + "'";///remarks = " Кровля:" + remark.Remove(remark.Length - 1) + Environment.NewLine; 
                                //}


                                //if (descript.Name == "FENCE")//ограда
                                //{
                                //    sql += ", Rfence='" + descript.InnerText.Trim() + "'";

                                //}

                                if (descript.Name == "TERMS_EXCHANGE")
                                {
                                    sql += ", Rterms_exchange='" + descript.InnerText.Trim() + "'";

                                }

                                if (descript.Name == "PERSONAL_ESTATE")
                                {

                                    sql1 = ", Rpersonal_estate='";
                                    int counter = 0;
                                    remark = "";
                                    foreach (XmlNode someItem in descript.ChildNodes)
                                    {


                                        if (someItem.Name == "ITEM")
                                        {
                                            if (counter > 0) sql1 += ", ";
                                            sql1 += someItem.InnerText.Trim();
                                            counter++;

                                        }
                                    }
                                    if (counter > 0) sql += sql1 + "'";
                                    else { remark = descript.InnerText.Trim(); }
                                    if (remark != "") sql += sql1+remark + "'";
                                }

                                if (descript.Name == "GROUND_AREA")
                                {
                                    sql += ", Rground_area='" + descript.InnerText.Trim() + "'";

                                }

                                if (descript.Name == "PURPOSE_USE")
                                {
                                    sql += ", Rpurpose_use='" + descript.InnerText.Trim() + "'";

                                }

                                if (descript.Name == "BASEMENT")
                                {
                                    sql += ", Rbasement=1";

                                }

                                if (descript.Name == "WINDOW_FRONT_QUANTITY")
                                {
                                    sql += ", Rwindow_front_quantity='" + descript.InnerText.Trim() + "'";

                                }

                                //if (descript.Name == "COMMERCIAL_OUTHOUSE")
                                //{

                                //    sql1 = ", Rcommercial_outhouse='";
                                //    int counter = 0;
                                //    remark = "";
                                //    foreach (XmlNode someItem in descript.ChildNodes)
                                //    {


                                //        if (someItem.Name == "ITEM")
                                //        {
                                //            if (counter > 0) sql1 += ", ";
                                //            sql1 += someItem.InnerText.Trim();
                                //            counter++;

                                //        }
                                //    }
                                //    if (counter > 0) sql += sql1 + "'";
                                //    else { remark = descript.InnerText; }
                                //    if (remark != "") sql += remark + "'";
                                //}

                                if (descript.Name == "SITE_SPACE" || descript.Name == "SITE_AREA")
                                {
                                    sql += ", Rsite_space='" + descript.InnerText.Trim() + "'";

                                }

                                if (descript.Name == "SEWERAGE") //канализация
                                {
                                    cur_item = func.toUpperFirst(descript.InnerText);
                                    if (!func.sewerageValues.Contains(cur_item)) remarks += "Горканализация:" + cur_item + Environment.NewLine;
                                    else sql += ", Rsewerage='" + cur_item + "'";

                                }
                                //if (descript.Name == "MAIN_ENTRANCE_LOCATION")
                                //{

                                //    sql1 = ", Rmain_entrance_location='";
                                //    int counter = 0;
                                //    remark = "";
                                //    foreach (XmlNode someItem in descript.ChildNodes)
                                //    {


                                //        if (someItem.Name == "ITEM")
                                //        {
                                //            if (counter > 0) sql1 += ", ";
                                //            sql1 += someItem.InnerText.Trim();
                                //            counter++;

                                //        }
                                //    }
                                //    if (counter > 0) sql += sql1 + "'";
                                //    else { remark = descript.InnerText; }
                                //    if (remark != "") sql += remark + "'";
                                //}

                             
                                if (descript.Name == "PHOTO")
                                {
                                    photos.Clear();
                                    allphotos = "";
                                    string pathstr;
                                    foreach (XmlNode someItem in descript.ChildNodes)
                                    {
                                        if (someItem.Name == "ITEM")
                                        {
                                            cur_photo.name=someItem.InnerText.Trim();
                                            cur_photo.uri=DateTime.UtcNow.GetHashCode().ToString();
                                            cur_photo.uri= "image_"+ cur_photo.uri;
                                            pathstr = Images.Find(n => n.name == cur_photo.name).FullPath;
                                            if ((pathstr.IndexOf(".png") == pathstr.Length - 4)) 
                                            {
                                                cur_photo.uri += ".png";
                                            }
                                            if ((pathstr.IndexOf(".jpg") == pathstr.Length - 4))
                                            {
                                                cur_photo.uri += ".jpg";
                                            }
                                            if ((pathstr.IndexOf(".gif") == pathstr.Length - 4))
                                            {
                                                cur_photo.uri += ".gif";
                                            }
                                            photos.Add(cur_photo);
                                            allphotos += cur_photo.uri + ";";
                                        }

                                    }
                                    if (allphotos.Length > 1) allphotos = allphotos.Remove(allphotos.Length - 1);

                                }
                            }
                                if (remarks != "") sql += ", Rnote='" + remarks + "'";
                                if (orientir!="") sql += ", Rreference_point='" + orientir + "'";
                                sql += ", Rdate_add='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "'";
                                sql += ", Rstatus='Актуально'";
                                sql += ", Ragent_code=" + agent_code;
                                if (allphotos != "") sql += ", Rphoto ='" + allphotos + "'";

                                errormessage = func.InsertToTable(sql);

                               
                                if (allphotos != "" && errormessage != "")
                                {
                                    string pathstr1;
                                    foreach (PhotoStruct st in photos)
                                    {

                                        pathstr1 = Images.Find(n => n.name == st.name).FullPath;
                                        func.UploadFileToServer(pathstr1, new Uri("ftp://turangaleela:Ardonskaya209@tamarrra.net:21/www/img1/" + st.uri));

                                    }
                               
                                }
                                
                                
                                if (errormessage != "") txbMessage.Text += " Ошибка записи в базу, объект " + IDList.Item(i).InnerText + ": " + errormessage + Environment.NewLine;
                            
                        } // ----------------------end if

                        else //-------------------------------------------------Земельный участок
                        {
                            sql1="";
                            remarks="";
                            orientir = "";
                           
                            sql = "INSERT INTO terra SET Tcity='" + sometown.rr_code+"'";
                            if (operation != "") sql += ", Toperation='" + operation + "'";
                            foreach (XmlNode descript in node.ChildNodes)
                            {
                                errormessage = "";
                                tmp ="";
                                if (descript.Name == "PRICE")
                                {
                                    tmp = descript.InnerText.Trim();
                                    if (rec_decsep != "") tmp = tmp.Replace(rec_decsep, cur_decsep);
                                    if (Double.TryParse(tmp, out dblnum)) sql += ", Tprice=" + dblnum.ToString(); 

                                }

                                if (descript.Name == "STREET")
                                {
                                    sql += ", Tstreet='" + street + "'";
                                }

                                if (descript.Name == "GAS_SUPPLY")
                                {

                                    sql1 = ", Tgas_supply='";
                                    int counter = 0;
                                    remark = "";
                                    foreach (XmlNode someItem in descript.ChildNodes)
                                    {

                                        cur_item = "";
                                        if (someItem.Name == "ITEM")
                                        {

                                            cur_item = func.toUpperFirst(someItem.InnerText);
                                            if (!func.gasValues.Contains(cur_item)) remark += cur_item + ",";
                                            else
                                            {
                                                if (counter > 0) sql1 += ", ";
                                                sql1 += cur_item;
                                                counter++;
                                            }

                                        }
                                    }
                                    if (counter > 0) sql += sql1 + "'";
                                    else if (remark == "") { remark = descript.InnerText; }
                                    if (remark != "")
                                    {
                                        if (remark[remark.Length - 1] == ',') remark = remark.Remove(remark.Length - 1);
                                        remarks += " Газ:" + remark + Environment.NewLine;
                                    }

                                }

                                if (descript.Name == "WATER_SUPPLY")
                                {

                                    sql1 = ", Twater_supply='";
                                    int counter = 0;
                                    remark = "";
                                    foreach (XmlNode someItem in descript.ChildNodes)
                                    {

                                        cur_item = "";
                                        if (someItem.Name == "ITEM")
                                        {
                                            cur_item = func.toUpperFirst(someItem.InnerText);
                                            if (!func.waterValues.Contains(cur_item)) remark += cur_item + ",";
                                            else
                                            {
                                                if (counter > 0) sql1 += ", ";
                                                sql1 += cur_item;
                                                counter++;
                                            }

                                        }
                                    }
                                    if (counter > 0) sql += sql1 + "'";
                                    else if (remark == "") { remark = descript.InnerText; }
                                    if (remark != "")
                                    {
                                        if (remark[remark.Length - 1] == ',') remark = remark.Remove(remark.Length - 1);
                                        remarks += " Вода:" + remark + Environment.NewLine;
                                    }

                                }

                                if (descript.Name == "SEWERAGE")
                                {
                                    cur_item = func.toUpperFirst(descript.InnerText);
                                    if (!func.sewerageValues.Contains(cur_item)) remarks += "Горканализация:" + cur_item + Environment.NewLine;
                                    else sql += ", Tsewerage='" + cur_item + "'";

                                }


                                if (descript.Name == "PROPERTY_TYPE")//вид собственности
                                {
                                    cur_item = func.toUpperFirst(descript.InnerText);
                                    if (!func.propertyValues.Contains(cur_item)) remarks += "Вид собственности:" + cur_item + Environment.NewLine;
                                    else sql += ", Tproperty_type='" + descript.InnerText.Trim() + "'";

                                }

                                if (descript.Name == "NOTE") remarks += descript.InnerText.Trim() + Environment.NewLine;

                                if (descript.Name == "DISTANCE_TO_CITY")
                                {
                                    sql += ", Tdistance_to_city='" + descript.InnerText.Trim() + "'";

                                }

                                if (descript.Name == "DISTRICT_REGION")
                                {
                                    tmp = "";
                                    if (city == "1500000100000") tmp = "1500000000000";
                                    else
                                    {
                                        tmp = descript.InnerText.Trim().ToLower();
                                        cur_district = districts.Find(n => n.rr_town.ToLower().IndexOf(tmp) >= 0);
                                        if (cur_district.rr_code != null && cur_district.rr_code!="") tmp = cur_district.rr_code;
                                        else tmp = "";
                                    }
                                                                      
                                    if(tmp!="") sql += ", Tdistrict_region='" + tmp + "'";
                                

                                }

                                if (descript.Name == "TITLE")
                                {
                                    sql += ", Ttitle='" + descript.InnerText.Trim() + "'";

                                }
                                if (descript.Name == "DESCRIPTION_SHORT")
                                {
                                    sql += ", Tdescription_short='" + descript.InnerText.Trim() + "'";

                                }

                                if (descript.Name == "DESCRIPTION_DETAIL")
                                {
                                    sql += ", Tdescription_detail='" + descript.InnerText.Trim() + "'";

                                }

                                if (descript.Name == "ELECTICITY")
                                {

                                    tmp = descript.InnerText.Trim().ToLower();
                                    if (tmp != "" && tmp != "нет" && tmp != "отсутствует")
                                    {
                                        sql += ", Telectricity=1";

                                    }

                                }

                                if (descript.Name == "HAGGLE")//возможен торрг
                                {
                                    sql += ", Thaggle=1";

                                }

                                if (descript.Name == "OUTHOUSE")
                                {

                                    sql1 = ", Touthouse='";
                                    int counter = 0;
                                    remark = "";
                                    foreach (XmlNode someItem in descript.ChildNodes)
                                    {


                                        if (someItem.Name == "ITEM")
                                        {
                                            if (counter > 0) sql1 += ", ";
                                            sql1 += someItem.InnerText.Trim();
                                            counter++;

                                        }
                                    }
                                    if (counter > 0) sql += sql1 + "'";
                                    else remark = descript.InnerText.Trim();
                                    if (remark != "") sql += sql1 + remark + "'";

                                }

                               
                                if (descript.Name == "CONDITION2") //общее состояние
                                {
                                    cur_item = func.toUpperFirst(descript.InnerText);
                                    if (!func.conditionValues.Contains(cur_item)) remarks += " Общее состояние:" + cur_item + Environment.NewLine;
                                    else sql += ", Tcondition2='" + cur_item + "'";

                                }


                                if (descript.Name == "IS_SPECIAL_PROPORSAL")
                                {
                                    sql += ", Tis_special_proposal=1" ;

                                }


                                if (descript.Name == "GROUND_AREA")
                                {
                                    tmp = descript.InnerText.Trim();
                                    if (rec_decsep != "") tmp = tmp.Replace(rec_decsep, cur_decsep);
                                    if (Double.TryParse(tmp, out dblnum)) sql += ", Tground_area=" + dblnum.ToString(); 
                                }
                               
                               



                                if (descript.Name == "SITE_SPACE" || descript.Name == "SITE_AREA")
                                {
                                    sql += ", Tsite_space='" + descript.InnerText.Trim() + "'";

                                }

                                if (descript.Name == "PURPOSE_LAND")
                                {

                                    sql1 = ", Tpurpose_land='";
                                    int counter = 0;
                                    remark = "";
                                    foreach (XmlNode someItem in descript.ChildNodes)
                                    {

                                        cur_item = "";
                                        if (someItem.Name == "ITEM")
                                        {
                                            cur_item = func.toUpperFirst(someItem.InnerText);
                                            if (!func.purpose_landValues.Contains(cur_item)) remark += cur_item + ",";
                                            else
                                            {
                                                if (counter > 0) sql1 += ", ";
                                                sql1 += cur_item;
                                                counter++;
                                            }

                                        }
                                    }
                                    if (counter > 0) sql += sql1 + "'";
                                    else if (remark == "") { remark = descript.InnerText.Trim(); }
                                    if (remark != "")
                                    {
                                        if (remark[remark.Length - 1] == ',') remark = remark.Remove(remark.Length - 1);
                                        remarks += " Назначение земли:" + remark + Environment.NewLine;
                                    }

                                }

                               
                                if (descript.Name == "PHOTO")
                                {
                                    photos.Clear();
                                    allphotos = "";
                                    string pathstr;
                                    foreach (XmlNode someItem in descript.ChildNodes)
                                    {
                                        if (someItem.Name == "ITEM")
                                        {
                                            cur_photo.name = someItem.InnerText.Trim();
                                            cur_photo.uri = DateTime.UtcNow.GetHashCode().ToString();
                                            cur_photo.uri = "image_" + cur_photo.uri;
                                            pathstr = Images.Find(n => n.name == cur_photo.name).FullPath;
                                            if ((pathstr.IndexOf(".png") == pathstr.Length - 4))
                                            {
                                                cur_photo.uri += ".png";
                                            }
                                            if ((pathstr.IndexOf(".jpg") == pathstr.Length - 4))
                                            {
                                                cur_photo.uri += ".jpg";
                                            }
                                            if ((pathstr.IndexOf(".gif") == pathstr.Length - 4))
                                            {
                                                cur_photo.uri += ".gif";
                                            }
                                            photos.Add(cur_photo);
                                            allphotos += cur_photo.uri + ";";
                                        }

                                    }
                                    if (allphotos.Length>1) allphotos=allphotos.Remove(allphotos.Length-1);
                                  
                                }

                                if (descript.Name == "FENCE")//ограда
                                {
                                    sql += ", Tfence='" + descript.InnerText.Trim() + "'";

                                }
                                if (descript.Name == "TWIDTH")//ограда
                                {
                                    sql += ", Twidth='" + descript.InnerText.Trim() + "'";

                                }

                                if (descript.Name == "THEIGHT")//ограда
                                {
                                    sql += ", Theight='" + descript.InnerText.Trim() + "'";

                                }
                            }
                                if (remarks != "") sql +=", Tnote='"+ remarks + "'";
                                sql += ", Tdate_add='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") +"'";
                                sql += ", Tstatus='Актуально'";
                                sql += ", Tagent_code=" + agent_code;

                                if (allphotos != "") sql += ", Tphoto ='" + allphotos + "'";

                                errormessage = func.InsertToTable(sql);


                                if (allphotos != "" && errormessage != "")
                                {
                                    string pathstr1;
                                    foreach (PhotoStruct st in photos)
                                    {

                                        pathstr1 = Images.Find(n => n.name == st.name).FullPath;
                                        func.UploadFileToServer(pathstr1, new Uri("ftp://w_sdelka15-ru_f5558dfb:56de21560jkl@ftp.sdelka15-ru.1gb.ru/http//AvitoFiles//test//" + st.uri));

                                    }

                                }


                                if (errormessage != "") txbMessage.Text += " Ошибка записи в базу, объект " + IDList.Item(i).InnerText + ": " + errormessage + Environment.NewLine;
                             
                        }

                        progressBar1.Step = 1;
                        progressBar1.PerformStep();

                    }

                    if (Directory.Exists(unzipDir))
                    {
                        dirInfo = new DirectoryInfo(unzipDir);
                        dirInfo.Delete(true);//удаление директории, в которую распакован архив
                    }
                    txbMessage.Text += "Загрузка данных завершена.";
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show(ex.ToString());
                }

                btSelectXML.Enabled = true;
              
               // this.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }
    }
}
