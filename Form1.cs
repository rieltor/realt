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

using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace realty
{
    public partial class Form1 : Form
    {
        private ClFunctions func;
        private ChListStruct[] allFields;
        private ChListStruct[] terraFields;

        private List<ChListStruct> RsaleCheckedFields;
        private List<ChListStruct> RexchCheckedFields;
        private List<ChListStruct> RrentCheckedFields;
        private List<ChListStruct> TerraCheckedFields;
        private List<DistrictsStruct> TownList;
        private List<DistrictsStruct> districts;
        private string terraId, realtId, realtEstate_type;
        private bool SaleSearch;
        private bool ExchSearch;
        private bool RentSearch;
        private bool TerraSearch;
        private AdminStruct Current_Agent;
        
        public Form1()
        {
            InitializeComponent();
            func = new ClFunctions();
        
            allFields = new ChListStruct[18];
            //allFields[0].FieldName = "Rcity";
            //allFields[0].caption = "Населенный пункт";
            //allFields[1].FieldName = "Rstreet";
            //allFields[1].caption = "Улица";
            allFields[0].FieldName = "Rtotal_floor_space";
            allFields[0].caption = "Площадь (кв.м)";           
            allFields[1].FieldName = "Rroom_quantity";        
            allFields[1].caption = "Кол-во комнат";
            allFields[2].FieldName = "Rprice";
            allFields[2].caption = "Стоимость (руб)";
            allFields[3].FieldName = "Rfloor";
            allFields[3].caption  = "Этаж"; 
            allFields[4].FieldName = "Rnumber_of_storeys";
            allFields[4].caption = "Этажность"; 
             
           
            allFields[5].FieldName = "Restate_type";
            allFields[5].caption = "Тип недвидимости"; 
            allFields[6].FieldName = "Rwalling_type";
            allFields[6].caption = "Материал стен"; 

            allFields[7].FieldName = "Rroom_layout";
            allFields[7].caption = "Планировка комнат"; 
            allFields[8].FieldName = "Rhouse_type";
            allFields[8].caption = "Тип дома";
            allFields[9].FieldName = "Rcondition2";
            allFields[9].caption = "Состояние";

            allFields[10].FieldName = "Rdate_add";
            allFields[10].caption = "Дата добавления";

            allFields[11].FieldName = "Rdate_change";
            allFields[11].caption = "Дата последнего изменения";

            allFields[12].FieldName = "Rdistrict_region";
            allFields[12].caption = "Район";

            allFields[13].FieldName = "Rtrade_price";
            allFields[13].caption = "Стоимость сделки";

            allFields[14].FieldName = "Rtrade_date";
            allFields[14].caption = "Дата сделки";

            allFields[15].FieldName = "Rrent_price_day";
            allFields[15].caption = "Аренда в день (руб)"; 
            allFields[16].FieldName = "Rrent_price_month";
            allFields[16].caption = "Аренда в месяц (руб)"; 
            allFields[17].FieldName = "Rpurpose_use";
            allFields[17].caption = "Назначение недвижимости";

            RsaleCheckedFields = new List<ChListStruct>();
            RexchCheckedFields = new List<ChListStruct>();
            RrentCheckedFields = new List<ChListStruct>();
            TerraCheckedFields= new List<ChListStruct>();

           terraFields = new ChListStruct[12];
           terraFields[0].FieldName = "Tground_area";
           terraFields[0].caption = "Площадь (сотки)";

           terraFields[1].FieldName = "Tprice";
           terraFields[1].caption = "Стоимость";   
           terraFields[2].FieldName = "Twidth";
           terraFields[2].caption = "Ширина";
           terraFields[3].FieldName = "Theight";
           terraFields[3].caption = "Глубина";

           terraFields[4].FieldName = "Tpurpose_land";
           terraFields[4].caption = "Назначение земли";

           terraFields[5].FieldName = "Tdistrict_region";
           terraFields[5].caption = "Район";

           terraFields[6].FieldName = "Tdate_add";
           terraFields[6].caption = "Дата добавления";

           terraFields[7].FieldName = "Tdate_change";
           terraFields[7].caption = "Дата последнего изменения";

           terraFields[8].FieldName = "Tdistance_to_city";
           terraFields[8].caption = "Расстояние до города";
           
           terraFields[9].FieldName = "Toperation";
           terraFields[9].caption = "Операция";

           terraFields[10].FieldName = "Ttrade_date";
           terraFields[10].caption = "Дата сделки";

           terraFields[11].FieldName = "Ttrade_price";
           terraFields[11].caption = "Стоимость сделки";
           terraId = "";
           realtId = "";

            realtEstate_type="";
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            editform objectedit = new editform();
            objectedit.Show();
        }
        private void clearFields()
        {
            //очистка полей поиска
            cbxRoom.EditValue = "";
            cbxFloor.EditValue = "";
            


            cbxFloorsNumber.EditValue = "";

            cbxMinPrice.EditValue = "";
            cbxMaxPrice.EditValue = "";
           
            cbxTerraMinPrice.EditValue = "";
            cbxTerraMaxPrice.EditValue = "";

            cbxMinArea.EditValue = "";
            cbxMaxArea.EditValue = "";
            cbxTerraMinArea.EditValue = "";
            cbxTerraMaxArea.EditValue = "";
            
            cbxWalls.EditValue = "";
            cbxDistrict.EditValue = "";
            cbxTerraDistrict.EditValue = "";
            
            cbxCity.EditValue = "";
            cbxTerraCity.EditValue = "";

            terraId = "";
            realtId = "";
            
            beTerraId.EditValue = "";
            beRealtId.EditValue = "";
           
            SaleSearch = false;
            RentSearch = false;
            ExchSearch = false;
        }

        private void BtLoadXML_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadXML frldxml = new LoadXML();
            frldxml.Show();
        }


        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            
            //string sql = "SELECT realestate.Restate_type, realestate.Rdistrict_region, realestate.Rstreet, realestate.Rfloor, "
            //+ "realestate.Rnumber_of_storeys, realestate.Rroom_quantity,  realestate.Rlevel_quantity, realestate.Rtotal_floor_space, "
            //+ "  realestate.Rprice, CONCAT (agents.Ag_FIO, '  ',agency.Aname, ': \n т.',  agency.APhone) as Ragency FROM "
            //+ " realestate, agency, agents WHERE (realestate.Ragent_code=agents.Ag_id AND agency.Aid= agents.Ag_agency)";


            clearFields();
            if (xtraTabControl1.SelectedTabPageIndex < 3)
            {
                ribbonPageGroup3.Visible = true;
                ribbonPageGroup4.Visible = false;
            }
            else
            {
                ribbonPageGroup4.Visible = true;
                ribbonPageGroup3.Visible = false;
                
            }

            if (xtraTabControl1.SelectedTabPageIndex==0)//sale
            {
                RefreshSale(SaleSearch);
            }

            if (xtraTabControl1.SelectedTabPageIndex==1)//rent
            {
               RefreshRent(RentSearch);
           
            }
            if (xtraTabControl1.SelectedTabPageIndex == 2)//exchange
            {
               RefreshExch(ExchSearch);
            }

            if (xtraTabControl1.SelectedTabPageIndex == 3)//terra
            {
                RefreshTerra(TerraSearch);
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.Width = (int)(Screen.PrimaryScreen.WorkingArea.Width*0.95);
            this.Height = (int)(Screen.PrimaryScreen.WorkingArea.Width * 0.5);

            FrEnter enter = new FrEnter();
           Current_Agent = enter.showFrom();
           if (Current_Agent.id == null || Current_Agent.id == "") { this.Close(); return; }


            RrentCheckedFields.Clear();
            RsaleCheckedFields.Clear();  
            RexchCheckedFields.Clear();
            TerraCheckedFields.Clear();
            
            for (int i=0 ; i<2; i++)
            {
                RrentCheckedFields.Add(allFields[i]);//поля выбранные пользователем
                RsaleCheckedFields.Add(allFields[i]);
                RexchCheckedFields.Add(allFields[i]);
                TerraCheckedFields.Add(terraFields[i]);
               
            }
            RsaleCheckedFields.Add(allFields[2]);
            RexchCheckedFields.Add(allFields[2]);
            TerraCheckedFields.Add(terraFields[2]);
            TerraCheckedFields.Add(terraFields[3]);
          
            for (int i = 3; i < 15; i++) //поля  для CheckedCombpBox
            {

                RentFieldsChList.Properties.Items.Add(allFields[i].caption, CheckState.Unchecked, true);
                SaleFieldsChList.Properties.Items.Add(allFields[i].caption, CheckState.Unchecked, true);
                ExchFieldsChList.Properties.Items.Add(allFields[i].caption, CheckState.Unchecked, true);
            }

            for (int i = 15; i < 18; i++)
            {

                RentFieldsChList.Properties.Items.Add(allFields[i].caption, CheckState.Unchecked, true);
            
            }

            //terra
            for (int i = 4; i < 11; i++) //поля  для CheckedCombpBox
            {
                TerraFieldsChList.Properties.Items.Add(terraFields[i].caption, CheckState.Unchecked, true);
            }

         

            SaleSearch = false;
            RentSearch = false;
            ExchSearch = false;
            TerraSearch = false;
        
            //заполнение полей поиска недвижимости
            repositoryItemCbxRooms.Items.Add("");
            for (int i = 1; i <15; i++)
            {
                repositoryItemCbxRooms.Items.Add(i);
            }

            repositoryItemCbxFloor.Items.Add("");
            for (int i=0; i<func.floors.Length; i++)
            {
                repositoryItemCbxFloor.Items.Add(func.floors[i]);
            }

            repositoryItemCbxFloorsNumber.Items.Add("");
            for (int i = 0; i < func.floorsNumber.Length; i++)
            {
                repositoryItemCbxFloorsNumber.Items.Add(func.floorsNumber[i]);
            }


            repositoryItemCbxMinPrice.Items.Add("");
            repositoryItemCbxMinPrice.Items.Add(0);
            repositoryItemCbxMinPrice.Items.Add(500000);
            for (int i = 1000000; i < 15000000; i+=250000)
            {
                repositoryItemCbxMinPrice.Items.Add(i);
            }

            repositoryItemCbxMaxPrice.Items.Add("");  
            repositoryItemCbxMaxPrice.Items.Add(500000);  
            for (int i = 1000000; i < 20000000; i += 250000)
            {
                repositoryItemCbxMaxPrice.Items.Add(i);
            }
            repositoryItemCbxMaxPrice.Items.Add(" > 20000000");
           
           
            repositoryItemCbxMinArea.Items.Add("");
            repositoryItemCbxMinArea.Items.Add(0);   
            for (int i = 20; i < 200; i +=5)
            {
                repositoryItemCbxMinArea.Items.Add(i);
            }

            repositoryItemCbxMaxArea.Items.Add("");
            for (int i = 19; i < 199; i += 5)
            {
                repositoryItemCbxMaxArea.Items.Add(i);
            }
            repositoryItemCbxMaxArea.Items.Add(" > 200");

           
            repositoryItemCbxWalls.Items.Add("");
            for (int i = 0; i < func.wallValues.Length; i++)
            {
                repositoryItemCbxWalls.Items.Add(func.wallValues[i]);
            }
         

            string errormessage="";
            districts = func.getDistricts(out errormessage); //районы
            TownList = func.getTowns(out errormessage);//города
            
            repositoryItemCbxDistrict.Items.Add("");
            for (int i = 0; i < districts.Count; i++)
            {
                repositoryItemCbxDistrict.Items.Add(districts[i].rr_town);
            }

            repositoryItemCbxCity.Items.Add("");
            for (int i = 0; i < TownList.Count; i++)
            {
                repositoryItemCbxCity.Items.Add(TownList[i].rr_town);
            }

            //house_type
            for (int i = 0; i < func.estate_typeValues.Length - 1; i++)
            {
                repositoryItemRdbEstate_type.Items[i].Value = func.estate_typeValues[i + 1];
            }
            rdbEstate_type.EditValue = repositoryItemRdbEstate_type.Items[0].Value;
            
            
            //статус объекта продажи  
            cbxSaleStatus.Items.Clear();
            for (int i = 0; i < 2; i++ )
            {
                cbxSaleStatus.Items.Add(func.satusValue[i]);
            }
           
            cbxSaleStatus.DropDownStyle = ComboBoxStyle.DropDownList; 
            cbxSaleStatus.SelectedIndex = 0;

            //статус объекта аренды

            cbxRentStatus.Items.Add(func.satusValue[0]);
            cbxRentStatus.Items.Add(func.satusValue[2]);
            cbxRentStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxRentStatus.SelectedIndex = 0;

            //статус объекта обмена
            cbxExchStatus.Items.Add(func.satusValue[0]);
            cbxExchStatus.Items.Add(func.satusValue[3]);
            cbxExchStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxExchStatus.SelectedIndex = 0;

            //статус земли
           
            cbxTerraStatus.Items.Add(func.satusValue[0]);
            cbxTerraStatus.Items.Add(func.satusValue[2]);
            cbxTerraStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxTerraStatus.SelectedIndex = 0;



            //заполнение полей поиска земельных участков

           

            repositoryItemCbxTerraMinPrice.Items.Add("");
            repositoryItemCbxTerraMinPrice.Items.Add(0);
            repositoryItemCbxTerraMinPrice.Items.Add(500000);
            for (int i = 1000000; i < 15000000; i += 250000)
            {
                repositoryItemCbxTerraMinPrice.Items.Add(i);
            }

            repositoryItemCbxTerraMaxPrice.Items.Add("");
            repositoryItemCbxTerraMaxPrice.Items.Add(500000);
            for (int i = 1000000; i < 20000000; i += 250000)
            {
                repositoryItemCbxTerraMaxPrice.Items.Add(i);
            }
            repositoryItemCbxMaxPrice.Items.Add(" > 20000000");


            repositoryItemCbxTerraMinArea.Items.Add("");
            for (int i = 0; i < func.terraMinAreaValue.Length; i ++)
            {
                repositoryItemCbxTerraMinArea.Items.Add(func.terraMinAreaValue[i]);
            }

            repositoryItemCbxTerraMaxArea.Items.Add("");
            for (int i = 0; i < func.terraMaxAreaValue.Length; i++)
            {
                repositoryItemCbxTerraMaxArea.Items.Add(func.terraMaxAreaValue[i]);
            }

            repositoryItemCbxTerraDistrict.Items.Add("");
            for (int i = 0; i < districts.Count; i++)
            {
                repositoryItemCbxTerraDistrict.Items.Add(districts[i].rr_town);
            }

            repositoryItemCbxCity.Items.Add("");
            for (int i = 0; i < TownList.Count; i++)
            {
                repositoryItemCbxTerraCity.Items.Add(TownList[i].rr_town);
            }


            xtraTabControl1.SelectedTabPageIndex = 0;

            
         
            //RefreshSale(SaleSearch);

           
        }

        private void RefreshSale(bool search)
        {

            MySqlDataAdapter daRealty;
            string[] tempArray;
            string someval;
            string sql = "SELECT realestate.Rid, agents.Ag_id, agency.Aid, realestate.Rphoto,  region15.RR_town, rstreets.RS_street, ";
            for (int i=0; i< RsaleCheckedFields.Count; i++)
            {
               
                sql += RsaleCheckedFields[i].FieldName + ", "; 
            }
            sql += " CONCAT_WS(' ',agents.Ag_FIO, ' т.', agents.Ag_phone) " +
                " FROM   realestate,  agents, agency, region15, rstreets WHERE (realestate.Ragent_code=agents.Ag_id " +
                " AND  region15.RR_code= realestate.Rcity AND agency.Aid= agents.Ag_agency "+
                " AND rstreets.RS_code= realestate.Rstreet) AND "+
                "  (realestate.Restate_type <> '" + func.estate_typeValues[0] +
                "' AND  realestate.Roperation ='" + func.operationValues[0] + "')";

            if (realtEstate_type != "") sql += " AND realestate.Restate_type='" + realtEstate_type+ "'";
            //новостройка  //вторичка
            if (ChbNewHouse.Checked == false || ChbOldHouse.Checked == false)
            {    //новостройка 
                if (ChbNewHouse.Checked == true) sql += " AND realestate.Rnewbuilding=1";
                //вторичка
                if (ChbOldHouse.Checked == true) sql += " AND realestate.Rnewbuilding=0";
            }
          
            if (cbxSaleStatus.SelectedIndex>=0) sql += " AND realestate.Rstatus='" + cbxSaleStatus.Text + "'";
            if (search == true)
            {

                if (realtId == "")
                {
                    ///для вывода результатов поиска
                    if (cbxRoom.EditValue != null && cbxRoom.EditValue.ToString() != "") sql += " AND realestate.Rroom_quantity=" + cbxRoom.EditValue; //количество комнат
                    if (cbxFloor.EditValue != null && cbxFloor.EditValue.ToString() != "") // этаж
                    {
                        someval = cbxFloor.EditValue.ToString();
                        if (someval == func.floors[0]) sql += " AND realestate.Rfloor > 1";
                        if (someval == func.floors[1]) sql += " AND realestate.Rfloor <> realestate.Rnumber_of_storeys";
                        if (someval == func.floors[2]) sql += " AND (realestate.Rfloor <> realestate.Rnumber_of_storeys AND realestate.Rfloor > 1)";
                    }

                    if (cbxFloorsNumber.EditValue != null && cbxFloorsNumber.EditValue.ToString() != "") //этажность
                    {
                        someval = cbxFloorsNumber.EditValue.ToString();
                        if (someval.IndexOf('-') > 0)
                        {
                            tempArray = someval.Split('-');
                            sql += " AND (realestate.Rnumber_of_storeys >= " + tempArray[0] + " AND realestate.Rnumber_of_storeys <=" + tempArray[1] + ")";
                        }
                        else sql += " AND realestate.Rnumber_of_storeys " + someval;
                    }
                    //цена
                    if (cbxMinPrice.EditValue != null && cbxMinPrice.EditValue.ToString() != "") sql += " AND (realestate.Rprice >= " + cbxMinPrice.EditValue.ToString();

                    if (cbxMaxPrice.EditValue != null && cbxMaxPrice.EditValue.ToString() != "")
                    {
                        someval = cbxMaxPrice.EditValue.ToString();
                        if (someval != repositoryItemCbxMaxPrice.Items[repositoryItemCbxMaxPrice.Items.Count - 1].ToString())
                            sql += " AND realestate.Rprice <= " + someval;
                        else sql += " AND realestate.Rprice  " + someval;
                    }
                    if (cbxMinPrice.EditValue != null && cbxMinPrice.EditValue.ToString() != "") sql += ") ";

                    //площадь
                    if (cbxMinArea.EditValue != null && cbxMinArea.EditValue.ToString() != "") sql += " AND (realestate.Rtotal_floor_space >= " + cbxMinArea.EditValue.ToString();

                    if (cbxMaxArea.EditValue != null && cbxMaxArea.EditValue.ToString() != "")
                    {
                        someval = cbxMaxArea.EditValue.ToString();
                        if (someval != repositoryItemCbxMaxArea.Items[repositoryItemCbxMaxArea.Items.Count - 1].ToString())
                            sql += " AND realestate.Rtotal_floor_space <= " + someval;
                        else sql += " AND realestate.Rtotal_floor_space  " + someval;
                    }
                    if (cbxMinArea.EditValue != null && cbxMinArea.EditValue.ToString() != "") sql += ") ";

                    //материал стен
                    if (cbxWalls.EditValue != null && cbxWalls.EditValue.ToString() != "") sql += " AND realestate.Rwalling_type Like '%" + cbxWalls.EditValue.ToString() + "%'";
                    //город
                    if (cbxCity.EditValue != null && cbxCity.EditValue.ToString() != "")
                    {
                        someval = TownList.Find(n => n.rr_town == cbxCity.EditValue.ToString()).rr_code;
                        if (someval != null) sql += " AND realestate.Rcity ='" + someval + "'";
                    }
                    else
                    {
                        //район
                        if (cbxDistrict.EditValue != null && cbxDistrict.EditValue.ToString() != "")
                        {
                            someval = districts.Find(n => n.rr_town == cbxDistrict.EditValue.ToString()).rr_code;
                            if (someval != null) sql += " AND realestate.Rdistrict_region ='" + someval + "'";
                        }
                    }
                }
                else
                {
                    sql += " AND realestate.Rid=" + realtId.Trim();
                }
            }

            sql += " ORDER BY realestate.Rdate_add DESC";

            try
            {
                daRealty = new MySqlDataAdapter(sql, func.connStr);
                MySqlCommandBuilder cb = new MySqlCommandBuilder(daRealty);

                DataSet dsRealty = new DataSet();
                daRealty.Fill(dsRealty);
             
                dsRealty.Tables[0].Columns[4].Caption = "Населенный пункт";
                dsRealty.Tables[0].Columns[4].ReadOnly = true;
                dsRealty.Tables[0].Columns[5].Caption = "Улица";
                dsRealty.Tables[0].Columns[5].ReadOnly = true;

                for (int i = 0; i < RsaleCheckedFields.Count; i++)
                {

                    dsRealty.Tables[0].Columns[i + 6].ReadOnly = true;
                    dsRealty.Tables[0].Columns[i+6].Caption = RsaleCheckedFields[i].caption;
                }
                int count = dsRealty.Tables[0].Columns.Count;
                dsRealty.Tables[0].Columns[count-1].Caption = "Агент ,тел";
                dsRealty.Tables[0].Columns[count-1].ReadOnly = true;
                dsRealty.Tables[0].Columns.Add("Выбрать", typeof(bool));
                
                
              

                //MySqlConnection conn = new MySqlConnection(func.connStr);
                //if (conn.State == ConnectionState.Open) conn.Close();

                try
                {

                    gridViewSale.Columns.Clear();                  
                    RealtySaleGridC.DataSource = null;
                    RealtySaleGridC.DataSource = dsRealty.Tables[0];
                    gridViewSale.Columns[0].Visible = false;
                    gridViewSale.Columns[1].Visible = false;

                    gridViewSale.Columns[2].Visible = false; //агент
                    gridViewSale.Columns[3].Visible = false;//агентство

                    DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();

                    gridViewSale.Columns[count].UnboundType= DevExpress.Data.UnboundColumnType.Boolean;
                    gridViewSale.Columns[count].ColumnEdit = repositoryItemCheckEdit1;
                    gridViewSale.Columns[count].OptionsColumn.AllowEdit = true;
                    
                    //repositoryItemCheckEdit1.NullText = "";
                    //repositoryItemCheckEdit1.ValueChecked = 0;
                    //repositoryItemCheckEdit1.ValueUnchecked = 1;
                    //repositoryItemCheckEdit1.ValueGrayed = 0;
                    repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;                    
                    gridViewSale.Columns[count].VisibleIndex = 0;
                  

                }

                finally
                {

                    RealtySaleGridC.EndUpdate();

                }
               
               

            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }


        private void RefreshExch(bool search)
        {

            MySqlDataAdapter daRealty;
            string[] tempArray;
            string someval;
            string sql = "SELECT realestate.Rid, agents.Ag_id, agency.Aid, realestate.Rphoto, region15.RR_town, rstreets.RS_street, ";
            for (int i = 0; i < RexchCheckedFields.Count; i++)
            {

                sql += RexchCheckedFields[i].FieldName + ", ";
            }
            sql += " CONCAT_WS(' ',agents.Ag_FIO, ' т.', agents.Ag_phone) " +
                " FROM   realestate LEFT JOIN  agents  ON  realestate.Ragent_code=agents.Ag_id " +
                " LEFT JOIN agency ON   agency.Aid= agents.Ag_agency" +
                " LEFT JOIN region15 ON  region15.RR_code= realestate.Rcity" +
                " LEFT JOIN rstreets ON  rstreets.RS_code= realestate.Rstreet" +
                " WHERE (realestate.Restate_type <> '" + func.estate_typeValues[0] +
                "' AND  realestate.Roperation ='" + func.operationValues[2] + "')";

            if (realtEstate_type != "") sql += " AND realestate.Restate_type='" + realtEstate_type + "'";
            //новостройка  //вторичка
            if (ChbNewHouse.Checked == false || ChbOldHouse.Checked == false)
            {    //новостройка 
                if (ChbNewHouse.Checked == true) sql += " AND realestate.Rnewbuilding=1";
                //вторичка
                if (ChbOldHouse.Checked == true) sql += " AND realestate.Rnewbuilding=0";
            }
            if (cbxExchStatus.SelectedIndex >= 0) sql += " AND realestate.Rstatus='" + cbxExchStatus.Text + "'";
            if (search == true)
            {
                if (realtId == "")
                {
                    ///для вывода результатов поиска
                    if (cbxRoom.EditValue != null && cbxRoom.EditValue.ToString() != "") sql += " AND realestate.Rroom_quantity=" + cbxRoom.EditValue; //количество комнат
                    if (cbxFloor.EditValue != null && cbxFloor.EditValue.ToString() != "") // этаж
                    {
                        someval = cbxFloor.EditValue.ToString();
                        if (someval == func.floors[0]) sql += " AND realestate.Rfloor > 1";
                        if (someval == func.floors[1]) sql += " AND realestate.Rfloor <> realestate.Rnumber_of_storeys";
                        if (someval == func.floors[2]) sql += " AND (realestate.Rfloor <> realestate.Rnumber_of_storeys AND realestate.Rfloor > 1)";
                    }

                    if (cbxFloorsNumber.EditValue != null && cbxFloorsNumber.EditValue.ToString() != "") //этажность
                    {
                        someval = cbxFloorsNumber.EditValue.ToString();
                        if (someval.IndexOf('-') > 0)
                        {
                            tempArray = someval.Split('-');
                            sql += " AND (realestate.Rnumber_of_storeys >= " + tempArray[0] + " AND realestate.Rnumber_of_storeys <=" + tempArray[1] + ")";
                        }
                        else sql += " AND realestate.Rnumber_of_storeys " + someval;
                    }
                    //цена
                    if (cbxMinPrice.EditValue != null && cbxMinPrice.EditValue.ToString() != "") sql += " AND (realestate.Rprice >= " + cbxMinPrice.EditValue.ToString();

                    if (cbxMaxPrice.EditValue != null && cbxMaxPrice.EditValue.ToString() != "")
                    {
                        someval = cbxMaxPrice.EditValue.ToString();
                        if (someval != repositoryItemCbxMaxPrice.Items[repositoryItemCbxMaxPrice.Items.Count - 1].ToString())
                            sql += " AND realestate.Rprice <= " + someval;
                        else sql += " AND realestate.Rprice  " + someval;
                    }
                    if (cbxMinPrice.EditValue != null && cbxMinPrice.EditValue.ToString() != "") sql += ") ";

                    //площадь
                    if (cbxMinArea.EditValue != null && cbxMinArea.EditValue.ToString() != "") sql += " AND (realestate.Rtotal_floor_space >= " + cbxMinArea.EditValue.ToString();

                    if (cbxMaxArea.EditValue != null && cbxMaxArea.EditValue.ToString() != "")
                    {
                        someval = cbxMaxArea.EditValue.ToString();
                        if (someval != repositoryItemCbxMaxArea.Items[repositoryItemCbxMaxArea.Items.Count - 1].ToString())
                            sql += " AND realestate.Rtotal_floor_space <= " + someval;
                        else sql += " AND realestate.Rtotal_floor_space  " + someval;
                    }
                    if (cbxMinArea.EditValue != null && cbxMinArea.EditValue.ToString() != "") sql += ") ";

                    //материал стен
                    if (cbxWalls.EditValue != null && cbxWalls.EditValue.ToString() != "") sql += " AND realestate.Rwalling_type Like '%" + cbxWalls.EditValue.ToString() + "%'";
                    //город
                    if (cbxCity.EditValue != null && cbxCity.EditValue.ToString() != "")
                    {
                        someval = TownList.Find(n => n.rr_town == cbxCity.EditValue.ToString()).rr_code;
                        if (someval != null) sql += " AND realestate.Rcity ='" + someval + "'";
                    }
                    else
                    {
                        //район
                        if (cbxDistrict.EditValue != null && cbxDistrict.EditValue.ToString() != "")
                        {
                            someval = districts.Find(n => n.rr_town == cbxDistrict.EditValue.ToString()).rr_code;
                            if (someval != null) sql += " AND realestate.Rdistrict_region ='" + someval + "'";
                        }
                    }
                }
                else
                {
                    sql += " AND realestate.Rid=" + realtId.Trim();
                }
            }

            sql += " ORDER BY realestate.Rdate_add DESC";

            try
            {
                daRealty = new MySqlDataAdapter(sql, func.connStr);
                MySqlCommandBuilder cb = new MySqlCommandBuilder(daRealty);

                DataSet dsRealty = new DataSet();
                daRealty.Fill(dsRealty);

                dsRealty.Tables[0].Columns[4].Caption = "Населенный пункт";
                dsRealty.Tables[0].Columns[4].ReadOnly = true;
                dsRealty.Tables[0].Columns[5].Caption = "Улица";
                dsRealty.Tables[0].Columns[5].ReadOnly = true;

                for (int i = 0; i < RexchCheckedFields.Count; i++)
                {

                    dsRealty.Tables[0].Columns[i + 6].ReadOnly = true;
                    dsRealty.Tables[0].Columns[i + 6].Caption = RexchCheckedFields[i].caption;
                }
                int count = dsRealty.Tables[0].Columns.Count;
                dsRealty.Tables[0].Columns[count - 1].Caption = "Агент ,тел";
                dsRealty.Tables[0].Columns[count - 1].ReadOnly = true;

                dsRealty.Tables[0].Columns.Add("Выбрать", typeof(bool));



                //MySqlConnection conn = new MySqlConnection(func.connStr);
                //if (conn.State == ConnectionState.Open) conn.Close();

                try
                {

                    gridViewExch.Columns.Clear();
                    RealtyExchGridC.DataSource = null;
                    RealtyExchGridC.DataSource = dsRealty.Tables[0];
                    gridViewExch.Columns[0].Visible = false;
                    gridViewExch.Columns[1].Visible = false;
                    gridViewExch.Columns[2].Visible = false;
                    gridViewExch.Columns[3].Visible = false;
                    DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
                    gridViewExch.Columns[count].ColumnEdit = repositoryItemCheckEdit1;
                    gridViewExch.Columns[count].UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
                    gridViewExch.Columns[count].OptionsColumn.AllowEdit = true;
                    repositoryItemCheckEdit1.NullText = "";
                    repositoryItemCheckEdit1.ValueChecked = 0;
                    repositoryItemCheckEdit1.ValueUnchecked = 1;
                    repositoryItemCheckEdit1.ValueGrayed = "";
                    repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;                    
                    gridViewExch.Columns[count].VisibleIndex = 0;


                }

                finally
                {

                    RealtyExchGridC.EndUpdate();

                }



            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void RefreshRent(bool search)
        {

            MySqlDataAdapter daRealty;
            string sql = "SELECT realestate.Rid, agents.Ag_id, agency.Aid, realestate.Rphoto, region15.RR_town, rstreets.RS_street,";
            string[] tempArray;
            string someval;

            for (int i = 0; i < RrentCheckedFields.Count; i++)
            {
               
                sql += RrentCheckedFields[i].FieldName + ", "; 
            }
            sql += " CONCAT_WS(' ',agents.Ag_FIO, 'т.', agents.Ag_phone) " +
                " FROM   realestate LEFT JOIN  agents  ON  realestate.Ragent_code=agents.Ag_id " +
                " LEFT JOIN agency ON   agency.Aid= agents.Ag_agency" +
                " LEFT JOIN region15 ON  region15.RR_code= realestate.Rcity" +
                " LEFT JOIN rstreets ON  rstreets.RS_code= realestate.Rstreet" +
                " WHERE (realestate.Restate_type <> '" + func.estate_typeValues[0] +
                "' AND  realestate.Roperation ='" + func.operationValues[1] + "')";

            if (realtEstate_type != "") sql += " AND realestate.Restate_type='" + realtEstate_type + "'";
            //новостройка  //вторичка
            if (ChbNewHouse.Checked == false ||  ChbOldHouse.Checked == false) 
            {    //новостройка 
                 if (ChbNewHouse.Checked == true) sql += " AND realestate.Rnewbuilding=1";
                 //вторичка
                 if (ChbOldHouse.Checked == true) sql += " AND realestate.Rnewbuilding=0";
            }

            if (cbxRentStatus.SelectedIndex >= 0) sql += " AND realestate.Rstatus='" + cbxRentStatus.Text + "'";
            if (search == true  )
            {
                if (realtId == "")
                {
                    ///для вывода результатов поиска
                    if (cbxRoom.EditValue != null && cbxRoom.EditValue.ToString() != "") sql += " AND realestate.Rroom_quantity=" + cbxRoom.EditValue; //количество комнат
                    if (cbxFloor.EditValue != null && cbxFloor.EditValue.ToString() != "") // этаж
                    {
                        someval = cbxFloor.EditValue.ToString();
                        if (someval == func.floors[0]) sql += " AND realestate.Rfloor > 1";
                        if (someval == func.floors[1]) sql += " AND realestate.Rfloor <> realestate.Rnumber_of_storeys";
                        if (someval == func.floors[2]) sql += " AND (realestate.Rfloor <> realestate.Rnumber_of_storeys AND realestate.Rfloor > 1)";
                    }

                    if (cbxFloorsNumber.EditValue != null && cbxFloorsNumber.EditValue.ToString() != "") //этажность
                    {
                        someval = cbxFloorsNumber.EditValue.ToString();
                        if (someval.IndexOf('-') > 0)
                        {
                            tempArray = someval.Split('-');
                            sql += " AND (realestate.Rnumber_of_storeys >= " + tempArray[0] + " AND realestate.Rnumber_of_storeys <=" + tempArray[1] + ")";
                        }
                        else sql += " AND realestate.Rnumber_of_storeys " + someval;
                    }
                    //цена
                    if (cbxMinPrice.EditValue != null && cbxMinPrice.EditValue.ToString() != "") sql += " AND (realestate.Rprice >= " + cbxMinPrice.EditValue.ToString();

                    if (cbxMaxPrice.EditValue != null && cbxMaxPrice.EditValue.ToString() != "")
                    {
                        someval = cbxMaxPrice.EditValue.ToString();
                        if (someval != repositoryItemCbxMaxPrice.Items[repositoryItemCbxMaxPrice.Items.Count - 1].ToString())
                            sql += " AND realestate.Rprice <= " + someval;
                        else sql += " AND realestate.Rprice  " + someval;
                    }
                    if (cbxMinPrice.EditValue != null && cbxMinPrice.EditValue.ToString() != "") sql += ") ";

                    //площадь
                    if (cbxMinArea.EditValue != null && cbxMinArea.EditValue.ToString() != "") sql += " AND (realestate.Rtotal_floor_space >= " + cbxMinArea.EditValue.ToString();

                    if (cbxMaxArea.EditValue != null && cbxMaxArea.EditValue.ToString() != "")
                    {
                        someval = cbxMaxArea.EditValue.ToString();
                        if (someval != repositoryItemCbxMaxArea.Items[repositoryItemCbxMaxArea.Items.Count - 1].ToString())
                            sql += " AND realestate.Rtotal_floor_space <= " + someval;
                        else sql += " AND realestate.Rtotal_floor_space  " + someval;
                    }
                    if (cbxMinArea.EditValue != null && cbxMinArea.EditValue.ToString() != "") sql += ") ";

                    //материал стен
                    if (cbxWalls.EditValue != null && cbxWalls.EditValue.ToString() != "") sql += " AND realestate.Rwalling_type Like '%" + cbxWalls.EditValue.ToString() + "%'";
                    //город
                    if (cbxCity.EditValue != null && cbxCity.EditValue.ToString() != "")
                    {
                        someval = TownList.Find(n => n.rr_town == cbxCity.EditValue.ToString()).rr_code;
                        if (someval != null) sql += " AND realestate.Rcity ='" + someval + "'";
                    }
                    else
                    {
                        //район
                        if (cbxDistrict.EditValue != null && cbxDistrict.EditValue.ToString() != "")
                        {
                            someval = districts.Find(n => n.rr_town == cbxDistrict.EditValue.ToString()).rr_code;
                            if (someval != null) sql += " AND realestate.Rdistrict_region ='" + someval + "'";
                        }
                    }
                }
                else
                {
                    sql += " AND realestate.Rid=" + realtId.Trim();
                }
            }
            sql += " ORDER BY realestate.Rdate_add DESC";

            try
            {
                daRealty = new MySqlDataAdapter(sql, func.connStr);
                MySqlCommandBuilder cb = new MySqlCommandBuilder(daRealty);

                DataSet dsRealty = new DataSet();
                daRealty.Fill(dsRealty);
                dsRealty.Tables[0].Columns[4].Caption = "Населенный пункт";
                dsRealty.Tables[0].Columns[4].ReadOnly = true;
                dsRealty.Tables[0].Columns[5].Caption = "Улица";
                dsRealty.Tables[0].Columns[5].ReadOnly = true;
                for (int i = 0; i < RrentCheckedFields.Count; i++)
                {
                    dsRealty.Tables[0].Columns[i+6].ReadOnly = true;
                    dsRealty.Tables[0].Columns[i+6].Caption = RrentCheckedFields[i].caption;            
                }
                int count = dsRealty.Tables[0].Columns.Count;
             
                dsRealty.Tables[0].Columns[count - 1].Caption = "Агент ,тел";
                dsRealty.Tables[0].Columns[count - 1].ReadOnly = true;
                dsRealty.Tables[0].Columns.Add("Выбрать", typeof(bool));
                //MySqlConnection conn = new MySqlConnection(func.connStr);
                //if (conn.State == ConnectionState.Open) conn.Close();

                 try
                 {

                    gridViewRent.Columns.Clear();                   
                    RealtyRentGridC.DataSource = null;
                    RealtyRentGridC.DataSource = dsRealty.Tables[0];
                    gridViewRent.Columns[0].Visible = false;
                    gridViewRent.Columns[1].Visible = false;
                    gridViewRent.Columns[2].Visible = false;
                    gridViewRent.Columns[3].Visible = false;
                    DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
                    
                     gridViewRent.Columns[count].ColumnEdit = repositoryItemCheckEdit1;
                    gridViewRent.Columns[count].UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
                    gridViewRent.Columns[count].OptionsColumn.AllowEdit = true;
                    repositoryItemCheckEdit1.NullText = "";
                    repositoryItemCheckEdit1.ValueChecked = 0;
                    repositoryItemCheckEdit1.ValueUnchecked = 1;
                    repositoryItemCheckEdit1.ValueGrayed = "";
                    repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
                    
                    gridViewRent.Columns[count].VisibleIndex = 0;
                 }

                finally

                {

                    RealtyRentGridC.EndUpdate();

                }

             

            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }



         private void RefreshTerra(bool search)
        {

            MySqlDataAdapter daRealty;
            
            string someval;
            string sql = "SELECT terra.TId,  agents.Ag_id, agency.Aid, terra.Tphoto,  region15.RR_town, rstreets.RS_street, ";
            for (int i = 0; i < TerraCheckedFields.Count; i++)
            {
               
                sql += TerraCheckedFields[i].FieldName + ", "; 
            }
            sql += " CONCAT_WS(' ',agents.Ag_FIO, 'т.', agents.Ag_phone) " +
                " FROM   terra  LEFT JOIN  agents  ON  terra.Tagent_code=agents.Ag_id " +
                " LEFT JOIN agency ON   agency.Aid= agents.Ag_agency" +
                " LEFT JOIN region15 ON  region15.RR_code= terra.Tcity" +
                " LEFT JOIN rstreets ON  rstreets.RS_code= terra.Tstreet";


            if (cbxTerraStatus.SelectedIndex >= 0) sql += " WHERE terra.Tstatus='" + cbxTerraStatus.Text + "'";
            if (search == true)
            {

                if (terraId == "")
                {
                    //цена
                    if (cbxTerraMinPrice.EditValue != null && cbxTerraMinPrice.EditValue.ToString() != "") sql += " AND (terra.Tprice >= " + cbxTerraMinPrice.EditValue.ToString();

                    if (cbxTerraMaxPrice.EditValue != null && cbxTerraMaxPrice.EditValue.ToString() != "")
                    {
                        someval = cbxTerraMaxPrice.EditValue.ToString();
                        if (someval != repositoryItemCbxTerraMaxPrice.Items[repositoryItemCbxTerraMaxPrice.Items.Count - 1].ToString())
                            sql += " AND terra.Tprice <= " + someval;
                        else sql += " AND terra.Tprice  " + someval;
                    }
                    if (cbxTerraMinPrice.EditValue != null && cbxTerraMinPrice.EditValue.ToString() != "") sql += ") ";

                    //площадь
                    if (cbxTerraMinArea.EditValue != null && cbxTerraMinArea.EditValue.ToString() != "") sql += " AND (terra.Tground_area >= " + cbxTerraMinArea.EditValue.ToString();

                    if (cbxTerraMaxArea.EditValue != null && cbxTerraMaxArea.EditValue.ToString() != "")
                    {
                        someval = cbxTerraMaxArea.EditValue.ToString();
                        if (someval != repositoryItemCbxTerraMaxArea.Items[repositoryItemCbxTerraMaxArea.Items.Count - 1].ToString())
                            sql += " AND terra.Tground_area <= " + someval;
                        else sql += " AND terra.Tground_area  " + someval;
                    }
                    if (cbxTerraMinArea.EditValue != null && cbxTerraMinArea.EditValue.ToString() != "") sql += ") ";

                    //город
                    if (cbxTerraCity.EditValue != null && cbxTerraCity.EditValue.ToString() != "")
                    {
                        someval = TownList.Find(n => n.rr_town == cbxTerraCity.EditValue.ToString()).rr_code;
                        if (someval != null) sql += " AND terra.Tcity ='" + someval + "'";
                    }
                    else
                    {
                        //район
                        if (cbxTerraDistrict.EditValue != null && cbxTerraDistrict.EditValue.ToString() != "")
                        {
                            someval = districts.Find(n => n.rr_town == cbxTerraDistrict.EditValue.ToString()).rr_code;
                            if (someval != null) sql += " AND terra.Tdistrict_region ='" + someval + "'";
                        }
                    }
                }
                else
                {
                    sql += " AND terra.TId=" + terraId.Trim();
                }
            }

            sql += " ORDER BY terra.Tdate_add DESC";

            try
            {
                daRealty = new MySqlDataAdapter(sql, func.connStr);
                MySqlCommandBuilder cb = new MySqlCommandBuilder(daRealty);

                DataSet dsRealty = new DataSet();
                daRealty.Fill(dsRealty);
             
                dsRealty.Tables[0].Columns[4].Caption = "Населенный пункт";
                dsRealty.Tables[0].Columns[4].ReadOnly = true;
                dsRealty.Tables[0].Columns[5].Caption = "Улица";
                dsRealty.Tables[0].Columns[5].ReadOnly = true;

                for (int i = 0; i < TerraCheckedFields.Count; i++)
                {

                    dsRealty.Tables[0].Columns[i + 6].ReadOnly = true;
                    dsRealty.Tables[0].Columns[i+6].Caption =TerraCheckedFields[i].caption;
                }
                int count = dsRealty.Tables[0].Columns.Count;
                dsRealty.Tables[0].Columns[count-1].Caption = "Агент ,тел";
                dsRealty.Tables[0].Columns[count-1].ReadOnly = true;

                dsRealty.Tables[0].Columns.Add("Выбрать", typeof(bool));
                
              

                //MySqlConnection conn = new MySqlConnection(func.connStr);
                //if (conn.State == ConnectionState.Open) conn.Close();

                try
                {

                    gridViewTerra.Columns.Clear();                  
                    terraGridC.DataSource = null;
                    terraGridC.DataSource = dsRealty.Tables[0];
                    gridViewTerra.Columns[0].Visible = false;
                    gridViewTerra.Columns[1].Visible = false;
                    gridViewTerra.Columns[2].Visible = false;
                    gridViewTerra.Columns[3].Visible = false;
                    DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();

                    gridViewTerra.Columns[count].ColumnEdit = repositoryItemCheckEdit1;
                    gridViewTerra.Columns[count].UnboundType = DevExpress.Data.UnboundColumnType.Boolean;                   
                    gridViewTerra.Columns[count].OptionsColumn.AllowEdit = true;
                    repositoryItemCheckEdit1.NullText = "";
                    repositoryItemCheckEdit1.ValueChecked = 0;
                    repositoryItemCheckEdit1.ValueUnchecked = 1;
                    repositoryItemCheckEdit1.ValueGrayed = "";
                    repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
                    
                    gridViewTerra.Columns[count].VisibleIndex = 0;
                  

                }

                finally
                {

                    terraGridC.EndUpdate();

                }
               
               

            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }


        private void SaleFieldsChList_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
           
            if (e.CloseMode ==DevExpress.XtraEditors.PopupCloseMode.Normal){
                RsaleCheckedFields.Clear();
                for (int i = 0; i < 3; i++)RsaleCheckedFields.Add(allFields[i]);
                int count=SaleFieldsChList.Properties.Items.Count; 
                for (int i=0; i<count; i++)
                {
                    if (SaleFieldsChList.Properties.Items[i].CheckState== CheckState.Checked)
                    {
                        RsaleCheckedFields.Add(allFields[i+3]);// столбцы выбранные пользователем
                    }
                }

                RefreshSale(SaleSearch);
          }
        }

        private void RentFieldsChList_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            if (e.CloseMode == DevExpress.XtraEditors.PopupCloseMode.Normal)
            {
                RrentCheckedFields.Clear();
                for (int i = 0; i < 2; i++) RrentCheckedFields.Add(allFields[i]);
                int count = RentFieldsChList.Properties.Items.Count;
                for (int i = 0; i < count; i++)
                {
                    if (RentFieldsChList.Properties.Items[i].CheckState == CheckState.Checked)
                    {
                       
                        RrentCheckedFields.Add(allFields[i + 3]);// столбцы выбранные пользователем
                    }
                }

                RefreshRent(RentSearch);
            }
        }

        private void btFind_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                SaleSearch = true;
                RefreshSale(SaleSearch);
            }

            if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                RentSearch=true;
                RefreshRent(RentSearch);
            }

            if (xtraTabControl1.SelectedTabPageIndex == 2)
            {
                ExchSearch = true;
                RefreshExch(ExchSearch);
            }
        }

        private void btCLearSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                SaleSearch = false;
                RefreshSale(SaleSearch);
            }

            if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                RentSearch = false;
                RefreshRent(RentSearch);
            }

            if (xtraTabControl1.SelectedTabPageIndex == 2)
            {
                ExchSearch = false;
                RefreshExch(ExchSearch);
            }

            clearFields();
        }

        private void rdbEstate_type_EditValueChanged(object sender, EventArgs e)
        {
          
        }

        private void ChbNewHouse_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                RefreshSale(SaleSearch);
            }

            if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                RefreshRent(RentSearch);

            }

            if (xtraTabControl1.SelectedTabPageIndex == 2)
            {
                RefreshExch(ExchSearch);
            }
        }

        private void ChbOldHouse_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                RefreshSale(SaleSearch);
            }

            if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                RefreshRent(RentSearch);

            }

            if (xtraTabControl1.SelectedTabPageIndex == 2)
            {
               
                RefreshExch(ExchSearch);
            }
        }

        private void ExchFieldsChList_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            if (e.CloseMode == DevExpress.XtraEditors.PopupCloseMode.Normal)
            {
                RexchCheckedFields.Clear();
                for (int i = 0; i < 3; i++) RexchCheckedFields.Add(allFields[i]);
                int count = ExchFieldsChList.Properties.Items.Count;
                for (int i = 0; i < count; i++)
                {
                    if (ExchFieldsChList.Properties.Items[i].CheckState == CheckState.Checked)
                    {
                        RexchCheckedFields.Add(allFields[i + 3]);// столбцы выбранные пользователем
                    }
                }

                RefreshExch(ExchSearch);
            }
        }

        private void cbxRentStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 1)
            { 
                RefreshRent(RentSearch);
            }

           
        }

        private void cbxExchStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 2)
            {
                RefreshExch(ExchSearch);
            }
            
        }

        private void cbxSaleStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            { 
                RefreshSale(SaleSearch);
            }
           
        }

        private void TerraFieldsChList_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            if (e.CloseMode == DevExpress.XtraEditors.PopupCloseMode.Normal)
            {
                TerraCheckedFields.Clear();
                for (int i = 0; i < 4; i++) TerraCheckedFields.Add(terraFields[i]);
                int count = TerraFieldsChList.Properties.Items.Count;
                for (int i = 0; i < count; i++)
                {
                    if (TerraFieldsChList.Properties.Items[i].CheckState == CheckState.Checked)
                    {
                        TerraCheckedFields.Add(terraFields[i + 4]);// столбцы выбранные пользователем
                    }
                }

                RefreshTerra(TerraSearch);
            }
        }

        private void cbxTerraStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 3)
            {
                RefreshTerra(TerraSearch);
            }
        }

        private void barBtTerraFind_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 3)
            {
                TerraSearch = true;
                RefreshTerra(TerraSearch);
            }
        }

        private void barBtTerraClear_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 3)
            {
                TerraSearch = false;
                RefreshTerra(TerraSearch);
                clearFields();
            }
        }

        private void barBtAgents_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AgentsForm Agf = new AgentsForm();
            Agf.Current_Agent = Current_Agent;
            Agf.Show();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e) //недвижимость продажа
        {
            if (gridViewSale.SelectedRowsCount == 0 ||  gridViewSale.FocusedColumn.Name =="colВыбрать") return;
            
            Point pt = gridViewSale.GridControl.PointToClient(Control.MousePosition);
            GridHitInfo info = gridViewSale.CalcHitInfo(pt);
            if (!info.InRow && !info.InRowCell) return;
            DataRowView rw=(DataRowView)gridViewSale.GetFocusedRow();   
            if (rw != null) MessageBox.Show(rw[0].ToString());
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)//недвижимость аренда
        {
            if (gridViewRent.SelectedRowsCount == 0 || gridViewRent.FocusedColumn.Name == "colВыбрать") return;
            Point pt = gridViewRent.GridControl.PointToClient(Control.MousePosition);
            GridHitInfo info = gridViewRent.CalcHitInfo(pt);
            if (!info.InRow && !info.InRowCell) return;
            DataRowView rw = (DataRowView)gridViewRent.GetFocusedRow(); 
        }

        private void gridView4_DoubleClick(object sender, EventArgs e)//недвижимость обмен
        {
            if (gridViewExch.SelectedRowsCount == 0 || gridViewExch.FocusedColumn.Name == "colВыбрать") return;
            Point pt = gridViewExch.GridControl.PointToClient(Control.MousePosition);
            GridHitInfo info = gridViewExch.CalcHitInfo(pt);
            if (!info.InRow && !info.InRowCell) return;
            DataRowView rw = (DataRowView)gridViewExch.GetFocusedRow(); 
        }

        private void gridView6_DoubleClick(object sender, EventArgs e) //земельные участки
        {
            if (gridViewTerra.SelectedRowsCount == 0 || gridViewTerra.FocusedColumn.Name == "colВыбрать") return;
            Point pt = gridViewTerra.GridControl.PointToClient(Control.MousePosition);
            GridHitInfo info = gridViewTerra.CalcHitInfo(pt);
           
            DataRowView rw = (DataRowView)gridViewTerra.GetFocusedRow(); 
        }

        private void barBtEditRecord_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            string val="";
            DataRowView rw=null;
            bool ischecked = false;
            int i=0;
            if (xtraTabControl1.SelectedTabPageIndex == 0) //sale
            {
                int count = gridViewSale.RowCount;     
                
                
                for ( i=0; i< count; i++) {
                    rw = (DataRowView)gridViewSale.GetRow(i);
                    val = rw[gridViewSale.Columns.Count - 1].ToString();
                   
                    if (val.ToLower() == "true")
                    {
                        ischecked = true;
                        break;
                    }
                       
                }
                if (ischecked == true && rw!=null) MessageBox.Show(rw[0].ToString());
            }

            if (xtraTabControl1.SelectedTabPageIndex == 1) //rent
            {
                int count = gridViewRent.RowCount;
             
                for ( i = 0; i < count; i++)
                {
                    rw = (DataRowView)gridViewRent.GetRow(i);
                    val = rw[gridViewRent.Columns.Count - 1].ToString();
                    if (val.ToLower() == "true")
                    {
                        ischecked = true;
                        break;
                    }
                }
              //use rw

            }
            if (xtraTabControl1.SelectedTabPageIndex == 2)//exchange
            {
                int count = gridViewExch.RowCount;

                for ( i = 0; i < count; i++)
                {
                    rw = (DataRowView)gridViewExch.GetRow(i);                   
                    val = rw[gridViewExch.Columns.Count - 1].ToString();
                    if (val.ToLower() == "true")
                    {
                        ischecked = true;
                        break;
                    }
                }
                //use rw
            }

            if (xtraTabControl1.SelectedTabPageIndex == 3) //terra
            {

                int count = gridViewTerra.RowCount;

                for ( i = 0; i < count; i++)
                {
                    rw = (DataRowView)gridViewTerra.GetRow(i);
                    val = rw[gridViewTerra.Columns.Count - 1].ToString();
                    if (val.ToLower() == "true")
                    {
                        ischecked = true;
                        break;
                    }
                }
            }
        }

       
        private void gridViewSale_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
         
            //if (e.Column.Name != "colВыбрать")  return;            
            //DataRowView rw = (DataRowView)gridViewSale.GetFocusedRow();           
            //rw[gridViewSale.Columns.Count - 1] = Convert.ToBoolean(e.Value);
         
        }

        private void gridViewRent_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            //if (e.Column.Name != "colВыбрать") return;
            //DataRowView rw = (DataRowView)gridViewRent.GetFocusedRow();
            //rw[gridViewRent.Columns.Count - 1] = Convert.ToBoolean(e.Value);
          
        }

        private void gridViewExch_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            //if (e.Column.Name != "colВыбрать") return;
            //DataRowView rw = (DataRowView)gridViewExch.GetFocusedRow();
            //rw[gridViewExch.Columns.Count - 1] = Convert.ToBoolean(e.Value);
           
        }

        private void gridViewTerra_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            //if (e.Column.Name != "colВыбрать") return;
            //DataRowView rw = (DataRowView)gridViewTerra.GetFocusedRow();
            //rw[gridViewTerra.Columns.Count - 1] = Convert.ToBoolean(e.Value);
        }

        private void beTerraId_EditValueChanged(object sender, EventArgs e)
        {
            terraId = ((DevExpress.XtraBars.BarEditItem)sender).EditValue.ToString();
        }

        private void beRealtId_EditValueChanged(object sender, EventArgs e)
        {
            realtId = ((DevExpress.XtraBars.BarEditItem) sender).EditValue.ToString();
        }

    

        private void repositoryItemRdbEstate_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                RefreshSale(SaleSearch);
            }

            if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                RefreshRent(RentSearch);

            }

            if (xtraTabControl1.SelectedTabPageIndex == 2)
            {
                RefreshExch(ExchSearch);
            }
            //rdbEstate_type.Refresh();
        }

        private void repositoryItemRdbEstate_type_EditValueChanged(object sender, EventArgs e)
        {
            realtEstate_type = ((DevExpress.XtraEditors.RadioGroup)sender).EditValue.ToString();
        }

        private void gridViewSale_Click(object sender, EventArgs e)
        {
            if (gridViewSale.FocusedColumn.Name != "colВыбрать") return;
            Point pt = gridViewSale.GridControl.PointToClient(Control.MousePosition);
            GridHitInfo info = gridViewSale.CalcHitInfo(pt);
            if (!info.InRow && !info.InRowCell) return;
            DataRowView rw = (DataRowView)gridViewSale.GetFocusedRow();
            int num=gridViewSale.Columns.Count - 1;           
            if (rw[num].Equals(true)) rw[num] = false;
            else  rw[num] = true;
            gridViewSale.RefreshRow(gridViewSale.FocusedRowHandle);

        }

        private void gridViewRent_Click(object sender, EventArgs e)
        {
            if (gridViewRent.FocusedColumn.Name != "colВыбрать") return;
            Point pt = gridViewRent.GridControl.PointToClient(Control.MousePosition);
            GridHitInfo info = gridViewRent.CalcHitInfo(pt);
            if (!info.InRow && !info.InRowCell) return;
            DataRowView rw = (DataRowView)gridViewRent.GetFocusedRow();
            int num = gridViewRent.Columns.Count - 1;
            if (rw[num].Equals(true)) rw[num] = false;
            else rw[num] = true;
            gridViewRent.RefreshRow(gridViewRent.FocusedRowHandle);
        }

        private void gridViewExch_Click(object sender, EventArgs e)
        {
            if (gridViewExch.FocusedColumn.Name != "colВыбрать") return;
            Point pt = gridViewExch.GridControl.PointToClient(Control.MousePosition);
            GridHitInfo info = gridViewExch.CalcHitInfo(pt);
            if (!info.InRow && !info.InRowCell) return;
            DataRowView rw = (DataRowView)gridViewExch.GetFocusedRow();
            int num = gridViewExch.Columns.Count - 1;
            if (rw[num].Equals(true)) rw[num] = false;
            else rw[num] = true;
            gridViewExch.RefreshRow(gridViewExch.FocusedRowHandle);
        }

        private void gridViewTerra_Click(object sender, EventArgs e)
        {
            if (gridViewTerra.FocusedColumn.Name != "colВыбрать") return;
            Point pt = gridViewTerra.GridControl.PointToClient(Control.MousePosition);
            GridHitInfo info = gridViewTerra.CalcHitInfo(pt);
            if (!info.InRow && !info.InRowCell) return;
            DataRowView rw = (DataRowView)gridViewTerra.GetFocusedRow();
            int num = gridViewTerra.Columns.Count - 1;
            if (rw[num].Equals(true)) rw[num] = false;
            else rw[num] = true;
            gridViewTerra.RefreshRow(gridViewTerra.FocusedRowHandle);
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранные записи?", "Предупреждение", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;
            string val = "";
            DataRowView rw = null;
            int i = 0;
            string sql = "", nodelete = "", msg="", photo="";
            if (xtraTabControl1.SelectedTabPageIndex == 0) //sale
            {
                int count = gridViewSale.RowCount;

                sql="DELETE FROM realestate WHERE ";
                nodelete = "";
                for (i = 0; i < count; i++)
                {
                    rw = (DataRowView)gridViewSale.GetRow(i);
                    val = rw[gridViewSale.Columns.Count - 1].ToString();

                    if (val.ToLower() == "true")
                    {
                        //проверка прав rw[1]-gentid, rw[2] agencyid, rw[3]-photo
                        if (rw[1].ToString() == Current_Agent.id || (Current_Agent.isadmin == true && rw[2].ToString() == Current_Agent.agency))
                        {
                            sql += "Rid=" + rw[0].ToString();
                            sql += " OR ";
                            photo = rw[3].ToString();
                            if (photo != "") func.deleteFiles(photo);
                        }
                        else nodelete += ", " + rw[0].ToString();
                    }

                }
                if (sql.IndexOf(" OR ") > 0)
                {
                    sql = sql.Remove(sql.Length - 4, 4);
                    msg = func.InsertToTable(sql);
                    if (msg != "") { MessageBox.Show("Возникла ошибка при удалении объекта"); }
                    else { MessageBox.Show("Объект удален");}
                    RefreshSale(SaleSearch); //refresh grid after deleting

                }
                if (nodelete != "")
                {
                    nodelete = nodelete.Remove(0, 1);
                    MessageBox.Show("У текущего пользователя нет прав на удаление объекта(-ов) #:" + nodelete);
                }
              
            }

            if (xtraTabControl1.SelectedTabPageIndex == 1) //rent
            {
                int count = gridViewRent.RowCount;
                sql = "DELETE FROM realestate WHERE ";
                for (i = 0; i < count; i++)
                {
                    rw = (DataRowView)gridViewRent.GetRow(i);
                    val = rw[gridViewRent.Columns.Count - 1].ToString();
                    if (val.ToLower() == "true")
                    {
                        if (rw[1].ToString() == Current_Agent.id || (Current_Agent.isadmin == true && rw[2].ToString() == Current_Agent.agency))
                        {
                            sql += "Rid=" + rw[0].ToString();
                            sql += " OR ";
                            photo = rw[3].ToString();
                            if (photo != "") func.deleteFiles(photo);
                        }
                        else nodelete += "," + rw[0].ToString();
                    }

                }
                if (sql.IndexOf(" OR ") > 0)
                {
                    sql = sql.Remove(sql.Length - 4, 4);
                    msg = func.InsertToTable(sql);
                    if (msg != "") { MessageBox.Show("Возникла ошибка при удалении объекта"); }
                    else { MessageBox.Show("Объект удален"); }
                    RefreshRent(RentSearch);
                }
                if (nodelete != "")
                {
                    nodelete = nodelete.Remove(0, 1);
                    MessageBox.Show("У текущего пользователя нет прав на удаление объекта(-ов) #:" + nodelete);
                }

            }
            if (xtraTabControl1.SelectedTabPageIndex == 2)//exchange
            {
                int count = gridViewExch.RowCount;
                sql = "DELETE FROM realestate WHERE ";
                for (i = 0; i < count; i++)
                {
                    rw = (DataRowView)gridViewExch.GetRow(i);
                    val = rw[gridViewExch.Columns.Count - 1].ToString();
                    if (val.ToLower() == "true")
                    {
                        if (rw[1].ToString() == Current_Agent.id || (Current_Agent.isadmin == true && rw[2].ToString() == Current_Agent.agency))
                        {
                            sql += "Rid=" + rw[0].ToString();
                            sql += " OR ";
                            photo = rw[3].ToString();
                            if (photo != "") func.deleteFiles(photo);
                           
                        }
                        else nodelete += "," + rw[0].ToString();
                    }

                }
                if (sql.IndexOf(" OR ") > 0)
                {
                    sql = sql.Remove(sql.Length - 4, 4);
                    msg = func.InsertToTable(sql);
                    if (msg != "") { MessageBox.Show("Возникла ошибка при удалении объекта"); }
                    else { MessageBox.Show("Объект удален"); }
                    RefreshExch(ExchSearch);
                }
                if (nodelete != "")
                {
                    nodelete = nodelete.Remove(0, 1);
                    MessageBox.Show("У текущего пользователя нет прав на удаление объекта(-ов) #:" + nodelete);
                }
            }

            if (xtraTabControl1.SelectedTabPageIndex == 3) //terra
            {

                int count = gridViewTerra.RowCount;
                sql = "DELETE FROM terra WHERE ";
                for (i = 0; i < count; i++)
                {
                    rw = (DataRowView)gridViewTerra.GetRow(i);
                    val = rw[gridViewTerra.Columns.Count - 1].ToString();
                    if (val.ToLower() == "true")
                    {
                        if (rw[1].ToString() == Current_Agent.id || (Current_Agent.isadmin == true && rw[2].ToString() == Current_Agent.agency))
                        {
                            sql += "TId=" + rw[0].ToString();
                            sql += " OR ";
                            photo = rw[3].ToString();
                            if (photo != "") func.deleteFiles(photo);
                        }
                        else nodelete += "," + rw[0].ToString();
                    }

                }
                if (sql.IndexOf(" OR ") > 0)
                {
                    sql = sql.Remove(sql.Length - 4, 4);
                    msg = func.InsertToTable(sql);
                    if (msg != "") { MessageBox.Show("Возникла ошибка при удалении объекта"); }
                    else { MessageBox.Show("Объект удален"); }
                    RefreshTerra(TerraSearch);
                }
                if (nodelete != "")
                {
                    nodelete = nodelete.Remove(0, 1);
                    MessageBox.Show("У текущего пользователя нет прав на удаление объекта(-ов) #:" + nodelete);
                }
            }


        }

        

      


       
    }
}
