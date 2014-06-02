using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Collections;

namespace realty
{
    public class DBWrap
    {
        public string ConnectionString;
        string delimiter;
        public DataTable district;
        public DataTable town;
        public DataTable street;
        public DataTable comments;
        public string[] balconyValues = new string[] { "Застеклен", "Не застеклен", "Зарешечен" };
        public string[] doorValues = new string[] { "Типовая", "Деревянная", "Металлопластик", "Пластик", "Новая", "Нормальная", "Требуется ремонт" };
        public string[] windowValues = new string[] { "Деревянные", "Евробрус", "Металлопластиковые", "Новые", "Пластиковые" };
        public string[] loggiaValues = new string[] { "Застеклена", "Не застеклена", "Зарешечена" };
        public string[] phoneValues = new string[] { "Отдельный", "Блокиратор", "Коммерческий", "Коммунальный", "Отсутствует" };
        public string[] tubeValues = new string[] { "Черные", "Металлопластиковые", "Новые", "Оцинкованные" };
        public string[] wallValues = new string[] { "Блочный", "Кирпичный", "Монолитный", "Панельный", "Камень-известняк", "Железобетон", "Каркасно-монолитный", "Кирпично-монолитный", "Монолитно-бетонный", "Блочно-кирпичный" };
        public string[] conditionValues = new string[] { "Хорошее", "Евроремонт", "Косметический ремонт", "Капитальный ремонт", "Нужен ремонт" };
        public string[] purpose_landValues = new string[] { "ИЖС", "Садоводчество", "Сельскохозяйственное", "Коммерческое" };
        public string[] roomlayoutValues = new string[] { "1 к-та проходная", "Готовый офис", "Комната с альковом", "Не правильная", "По проекту", "Полураспашонка", "Правильная", "Раздельное", "Распашонка", "Свободная", "Смежное", "Смежно-параллельное", "Смежно-раздельное", "Типовая", "Трамвай", "Улучшенная" };
        public string[] propertyValues = new string[] { "Госакт", "Государственная", "Дачный кооператив", "Долевое участие", "Муниципальная", "Неприватиз", "Общественная", "Частная (приватизированная)" };
        public string[] sewerageValues = new string[] { "Горканализация", "Своя канализация", "Удобаство во дворе" };
        public string[] housetypeValues = new string[] { "Бельгийка", "Болгарка", "Бывшее общежитие", "Высотка", "Гостинка", "Индивидуальный проект", "Коммуналка", "Малосемейка", "Общежитие", "Новый дом", "Особняк", "Сталинка", "Хрущевка", "Брежневка", "Новостройка", "Старый фонд", "Улучшенной планировки" };
        public string[] maindoorValues = new string[] { "Простая", "Железная", "Двойная", "Деревянная", "Бронированная", "Нет" };
        public string[] operationValues = new string[] { "Продажа", "Аренда" };
        public string[] estateValues = new string[] { "Квартира", "Комната", "Частный дом" };
        public string[] stateValues = new string[] { "Актуально", "Завершен" };
        public DBWrap(string connection = "server=tamarrra.net;user=user;database=RealtMultiBase;password=Ardonskaya209;charset=utf8")
        {
            ConnectionString = connection;
            delimiter = ",";
        }
        public DataTable SQLquery(string sql)
        {
            MySqlConnection con = new MySqlConnection(ConnectionString);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(sql, con);            
            MySqlDataReader reader = cmd.ExecuteReader();
            DataTable result = new DataTable();
            result.Load(reader);
            con.Close();
            return result;   
        }

        public void InsertNewEstate(estaterecord item)
        {

            string insertsql = String.Concat( "INSERT INTO realestate(Rcity, Rdistrict_region, Rstreet, Restate_type, Rfloor, Rnumber_of_storeys,", 
                                  "Rroom_quantity, Rhaggle, Rproperty_type, Rtitle, Routhouse_legality, Ruse_for_office, Rdescription_short,",
                                  "Rbathroom_note, Rbathroom, Rwalling_type, Rkitchen_note, Rlevel_quantity, Rparking, Rdescription_detail,");
            insertsql = String.Concat(insertsql, "Rroom_layout, Rhouse_type, Rbalcony, Rloggia, Rwater_supply, Rgas_supply, Rcondition2, Rheat_supply,",
                                  "Rinterior_door_type, Rfront_door_type, Rwindow_material, Rtelephone, Rtubing_material, Rkitchen_floor_space,");
            insertsql = String.Concat(insertsql, "Rhall_floor_space, Rtotal_floor_space, Rceiling_height, Rreference_point, Rfloor_space, Rnote, Rbackrooms,",
                                  "Rprice, Rprice_square, Ris_special_proposal, Rchange_only, Rnewbuilding, Rhypothec, Ragent_code,");
            insertsql = String.Concat(insertsql, "Rdate_add, Rdate_change, Rstatus, Rtrade_date, Rrent_price_day, Rrent_price_month, Raccount_in_rent,",
                                  "Rbuilding_year, Rroof, Rterms_exchange, Rdistance_to_city, Rground_area, Rpurpose_use, Rbasement,");
            insertsql = String.Concat(insertsql, "Rwindow_front_quantity, Rmain_entrance_location, Rcommercial_outhouse, Rsite_space, Rsewerage, Rphoto,",
                                  "Roperation, Rlavatory_basin, Rhouse_no, Rblock_no, Rflat_no, RFridge, Rtrade_price) VALUES (@Rcity, @Rdistrict_region, @Rstreet,");
            insertsql = String.Concat(insertsql, "@Restate_type, @Rfloor, @Rnumber_of_storeys, @Rroom_quantity, @Rhaggle, @Rproperty_type, @Rtitle,",
                                  "@Routhouse_legality, @Ruse_for_office, @Rdescription_short, @Rbathroom_note, @Rbathroom, @Rwalling_type, @Rkitchen_note,");
            insertsql = String.Concat(insertsql, "@Rlevel_quantity, @Rparking, @Rdescription_detail, @Rroom_layout, @Rhouse_type, @Rbalcony, @Rloggia,",
                                  "@Rwater_supply, @Rgas_supply, @Rcondition2, @Rheat_supply, @Rinterior_door_type, @Rfront_door_type, @Rwindow_material, ");
          
            insertsql = String.Concat(insertsql, "@Rtelephone, @Rtubing_material, @Rkitchen_floor_space, @Rhall_floor_space, @Rtotal_floor_space,",
                                  "@Rceiling_height, @Rreference_point, @Rfloor_space, @Rnote, @Rbackrooms, @Rprice, @Rprice_square, @Ris_special_proposal,");
            insertsql = String.Concat(insertsql, "@Rchange_only, @Rnewbuilding, @Rhypothec, @Ragent_code, @Rdate_add, @Rdate_change, @Rstatus, @Rtrade_date,",
                                  "@Rrent_price_day, @Rrent_price_month, @Raccount_in_rent, @Rbuilding_year, @Rroof, @Rterms_exchange, @Rdistance_to_city,");
            insertsql = String.Concat(insertsql, "@Rground_area, @Rpurpose_use, @Rbasement, @Rwindow_front_quantity, @Rmain_entrance_location, @Rcommercial_outhouse,",
                                  "@Rsite_space, @Rsewerage, @Rphoto, @Roperation, @Rlavatory_basin, @Rhouse_no, @Rblock_no, @Rflat_no, @RFridge, @Rtrade_price)");
            
            MySqlConnection con = new MySqlConnection(ConnectionString);
            con.Open();
            MySqlCommand command = new MySqlCommand(insertsql, con);

            command.Parameters.AddWithValue("@Rcity", item.Rcity);
            command.Parameters.AddWithValue("@Rdistrict_region", item.Rdistrict_region);
            command.Parameters.AddWithValue("@Rstreet", item.Rstreet);
            command.Parameters.AddWithValue("@Restate_type", item.Restate_type);
            command.Parameters.AddWithValue("@Rfloor", item.Rfloor);
            command.Parameters.AddWithValue("@Rnumber_of_storeys", item.Rnumber_of_storeys);
            command.Parameters.AddWithValue("@Rroom_quantity", item.Rroom_quantity);
            command.Parameters.AddWithValue("@Rhaggle", item.Rhaggle);
            command.Parameters.AddWithValue("@Rproperty_type", item.Rproperty_type);
            command.Parameters.AddWithValue("@Rtitle", item.Rtitle);
            command.Parameters.AddWithValue("@Routhouse_legality", item.Routhouse_legality);
            command.Parameters.AddWithValue("@Ruse_for_office", item.Ruse_for_office);
            command.Parameters.AddWithValue("@Rdescription_short", item.Rdescription_short);
            command.Parameters.AddWithValue("@Rbathroom_note", item.Rbathroom_note);        
            command.Parameters.AddWithValue("@Rbathroom", item.Rbathroom);
            command.Parameters.AddWithValue("@Rwalling_type", item.Rwalling_type);
            command.Parameters.AddWithValue("@Rkitchen_note", item.Rkitchen_note);
            command.Parameters.AddWithValue("@Rlevel_quantity", item.Rlevel_quantity);
            command.Parameters.AddWithValue("@Rparking", item.Rparking);
            command.Parameters.AddWithValue("@Rdescription_detail", item.Rdescription_detail);
            command.Parameters.AddWithValue("@Rroom_layout", item.Rroom_layout);
            command.Parameters.AddWithValue("@Rhouse_type", item.Rhouse_type);
            command.Parameters.AddWithValue("@Rbalcony", item.Rbalcony);
            command.Parameters.AddWithValue("@Rloggia", item.Rloggia);
            command.Parameters.AddWithValue("@Rwater_supply", item.Rwater_supply);
            command.Parameters.AddWithValue("@Rgas_supply", item.Rgas_supply);
            command.Parameters.AddWithValue("@Rcondition2", item.Rcondition2);
            command.Parameters.AddWithValue("@Rheat_supply", item.Rheat_supply);
            command.Parameters.AddWithValue("@Rinterior_door_type", item.Rinterior_door_type);
            command.Parameters.AddWithValue("@Rfront_door_type", item.Rfront_door_type);
            command.Parameters.AddWithValue("@Rwindow_material", item.Rwindow_material);
            command.Parameters.AddWithValue("@Rtelephone", item.Rtelephone);
            command.Parameters.AddWithValue("@Rtubing_material", item.Rtubing_material);
            command.Parameters.AddWithValue("@Rkitchen_floor_space", item.Rkitchen_floor_space);
            command.Parameters.AddWithValue("@Rhall_floor_space", item.Rhall_floor_space);
            command.Parameters.AddWithValue("@Rtotal_floor_space", item.Rtotal_floor_space);
            command.Parameters.AddWithValue("@Rceiling_height", item.Rceiling_height);
            command.Parameters.AddWithValue("@Rreference_point", item.Rreference_point);
            command.Parameters.AddWithValue("@Rfloor_space", item.Rfloor_space);
            command.Parameters.AddWithValue("@Rnote", item.Rnote);
            command.Parameters.AddWithValue("@Rbackrooms", item.Rbackrooms);
            command.Parameters.AddWithValue("@Rprice", item.Rprice);        
            command.Parameters.AddWithValue("@Rprice_square", item.Rprice_square);
            command.Parameters.AddWithValue("@Ris_special_proposal", item.Ris_special_proposal);
            command.Parameters.AddWithValue("@Rchange_only", item.Rchange_only);
            command.Parameters.AddWithValue("@Rnewbuilding", item.Rnewbuilding);
            command.Parameters.AddWithValue("@Rhypothec", item.Rhypothec);
            command.Parameters.AddWithValue("@Ragent_code", item.Ragent_code);
            command.Parameters.AddWithValue("@Rdate_add", item.Rdate_add);
            command.Parameters.AddWithValue("@Rdate_change", item.Rdate_change);
            command.Parameters.AddWithValue("@Rstatus", item.Rstatus);
            command.Parameters.AddWithValue("@Rtrade_date", item.Rtrade_date);
            command.Parameters.AddWithValue("@Rrent_price_day", item.Rrent_price_day);
            command.Parameters.AddWithValue("@Rrent_price_month", item.Rrent_price_month);
            command.Parameters.AddWithValue("@Raccount_in_rent", item.Raccount_in_rent);
            command.Parameters.AddWithValue("@Rbuilding_year", item.Rbuilding_year);
            command.Parameters.AddWithValue("@Rroof", item.Rroof);
            command.Parameters.AddWithValue("@Rterms_exchange", item.Rterms_exchange);
            command.Parameters.AddWithValue("@Rdistance_to_city", item.Rdistance_to_city);
            command.Parameters.AddWithValue("@Rground_area", item.Rground_area);
            command.Parameters.AddWithValue("@Rpurpose_use", item.Rpurpose_use);        
            command.Parameters.AddWithValue("@Rbasement", item.Rbasement);
            command.Parameters.AddWithValue("@Rwindow_front_quantity", item.Rwindow_front_quantity);
            command.Parameters.AddWithValue("@Rmain_entrance_location", item.Rmain_entrance_location);
            command.Parameters.AddWithValue("@Rcommercial_outhouse", item.Rcommercial_outhouse);
            command.Parameters.AddWithValue("@Rsite_space", item.Rsite_space);
            command.Parameters.AddWithValue("@Rsewerage", item.Rsewerage);
            command.Parameters.AddWithValue("@Rphoto", item.Rphoto);
            command.Parameters.AddWithValue("@Roperation", item.Roperation);
            command.Parameters.AddWithValue("@Rlavatory_basin", item.Rlavatory_basin);
            command.Parameters.AddWithValue("@Rhouse_no", item.Rhouse_no);
            command.Parameters.AddWithValue("@Rblock_no", item.Rblock_no);
            command.Parameters.AddWithValue("@Rflat_no", item.Rflat_no);
            command.Parameters.AddWithValue("@RFridge", item.RFridge);
            command.Parameters.AddWithValue("@Rtrade_price", item.Rtrade_price); 
        
            command.ExecuteNonQuery();
            int id = Convert.ToInt32(command.LastInsertedId);

            //add photo upload and comments insertion

            con.Close();
        }


        public void UpdateEstate(estaterecord item)
        {
            string updatesql = @"UPDATE realestate SET Rcity = @Rcity, Rdistrict_region = @Rdistrict_region, Rstreet = @Rstreet, Restate_type = @Restate_type, Rfloor = @Rfloor,
                                 Rnumber_of_storeys = @Rnumber_of_storeys, Rroom_quantity = @Rroom_quantity, Rhaggle = @Rhaggle, Rproperty_type = @Rproperty_type, Rtitle = @Rtitle, 
                                 Routhouse_legality = @Routhouse_legality, Ruse_for_office = @Ruse_for_office, Rdescription_short = @Rdescription_short, Rbathroom_note = @Rbathroom_note, 
                                 Rbathroom = @Rbathroom, Rbathroom_note = @Rbathroom_note, Rwalling_type = @Rwalling_type, Rkitchen_note = @Rkitchen_note, Rlevel_quantity = @Rlevel_quantity, 
                                 Rparking = @Rparking, Rdescription_detail = @Rdescription_detail, Rroom_layout = @Rroom_layout, Rhouse_type = @Rhouse_type, Rbalcony = @Rbalcony, Rloggia = @Rloggia, 
                                 Rwater_supply = @Rwater_supply, Rgas_supply = @Rgas_supply, Rcondition2 = @Rcondition2, Rheat_supply = @Rheat_supply, Rinterior_door_type = @Rinterion_door_type, 
                                 Rfront_door_type = @Rfront_door_type, Rwindow_material = @Rwindow_material, Rtelephone = @Rtelephone, Rtubing_material = @Rtubing_material, 
                                 Rkitchen_floor_space = @Rkitchen_floor_space, Rhall_floor_space = @Rhall_floor_space, Rtotal_floor_space = @Rtotal_floor_space, Rceiling_height = @Rceiling_height,
                                 Rreference_point = @Rreference_point, Rfloor_space = Rfloor_space, Rnote = @Rnote, Rbackrooms = @Rbackrooms, Rprice = @Rprice, Rprice_square = @Rprice_square, 
                                 Ris_special_proposal = @Ris_special_proposal, Rchange_only = @Rchange_only, Rnewbuilding = @Rnewbuilding, Rhypothec = @Rhypothec, Ragent_code = @Ragent_code,
                                 Rdate_add = @Rdate_add, Rdate_change = @Rdate_change, Rstatus = @Rstatus, Rtrade_date = @Rtrade_date, Rrent_price_day = @Rrent_price_day, 
                                 Rrent_price_month = @Rrent_price_month, Raccount_in_rent = @Raccount_in_rent, Rbuilding_year = @Rbuilding_year, Rroof = @Rroof, Rterms_exchange = @Rterms_exchange,
                                 Rdistance_to_city = @Rdistance_to_city, Rground_area = @Rground_area, Rpurpose_use = @Rpurpose_use, Rbasement = @Rbasement, Rwindow_front_quantity = @Rwindow_front_quantity, 
                                 Rmain_entrance_location = @Rmain_entrance_location, Rcommercial_outhouse = @Rcommercial_outhouse, Rsite_space = @Rsite_space, Rsewerage = @Rsewerage, Rphoto = @Rphoto,
                                 Roperation = @Roperation, Rlavatory_basin = @Rlavatory_basin, Rhouse_no= @Rhouse_no, Rblock_no = @Rblock_no, Rflat_no = @Rflat_no, RFridge = @Rfridge, Rtrade_price = @Rtrade_price WHERE Rid = @Rid ";
                                         
            MySqlConnection con = new MySqlConnection(ConnectionString);
            con.Open();
            MySqlCommand command = new MySqlCommand(updatesql, con);
            
            command.Parameters.AddWithValue("@Rcity", item.Rcity);
            command.Parameters.AddWithValue("@Rdistrict_region", item.Rdistrict_region);
            command.Parameters.AddWithValue("@Rstreet", item.Rstreet);
            command.Parameters.AddWithValue("@Restate_type", item.Restate_type);
            command.Parameters.AddWithValue("@Rfloor", item.Rfloor);
            command.Parameters.AddWithValue("@Rnumber_of_storeys", item.Rnumber_of_storeys);
            command.Parameters.AddWithValue("@Rroom_quantity", item.Rroom_quantity);
            command.Parameters.AddWithValue("@Rhaggle", item.Rhaggle);
            command.Parameters.AddWithValue("@Rproperty_type", item.Rproperty_type);
            command.Parameters.AddWithValue("@Rtitle", item.Rtitle);
            command.Parameters.AddWithValue("@Routhouse_legality", item.Routhouse_legality);
            command.Parameters.AddWithValue("@Ruse_for_office", item.Ruse_for_office);
            command.Parameters.AddWithValue("@Rdescription_short", item.Rdescription_short);
            command.Parameters.AddWithValue("@Rbathroom_note", item.Rbathroom_note);
            command.Parameters.AddWithValue("@Rbathroom", item.Rbathroom);
            command.Parameters.AddWithValue("@Rwalling_type", item.Rwalling_type);
            command.Parameters.AddWithValue("@Rkitchen_note", item.Rkitchen_note);
            command.Parameters.AddWithValue("@Rlevel_quantity", item.Rlevel_quantity);
            command.Parameters.AddWithValue("@Rparking", item.Rparking);
            command.Parameters.AddWithValue("@Rdescription_detail", item.Rdescription_detail);
            command.Parameters.AddWithValue("@Rroom_layout", item.Rroom_layout);
            command.Parameters.AddWithValue("@Rhouse_type", item.Rhouse_type);
            command.Parameters.AddWithValue("@Rbalcony", item.Rbalcony);
            command.Parameters.AddWithValue("@Rloggia", item.Rloggia);
            command.Parameters.AddWithValue("@Rwater_supply", item.Rwater_supply);
            command.Parameters.AddWithValue("@Rgas_supply", item.Rgas_supply);
            command.Parameters.AddWithValue("@Rcondition2", item.Rcondition2);
            command.Parameters.AddWithValue("@Rheat_supply", item.Rheat_supply);
            command.Parameters.AddWithValue("@Rinterior_door_type", item.Rinterior_door_type);
            command.Parameters.AddWithValue("@Rfront_door_type", item.Rfront_door_type);
            command.Parameters.AddWithValue("@Rwindow_material", item.Rwindow_material);
            command.Parameters.AddWithValue("@Rtelephone", item.Rtelephone);
            command.Parameters.AddWithValue("@Rtubing_material", item.Rtubing_material);
            command.Parameters.AddWithValue("@Rkitchen_floor_space", item.Rkitchen_floor_space);
            command.Parameters.AddWithValue("@Rhall_floor_space", item.Rhall_floor_space);
            command.Parameters.AddWithValue("@Rtotal_floor_space", item.Rtotal_floor_space);
            command.Parameters.AddWithValue("@Rceiling_height", item.Rceiling_height);
            command.Parameters.AddWithValue("@Rreference_point", item.Rreference_point);
            command.Parameters.AddWithValue("@Rfloor_space", item.Rfloor_space);
            command.Parameters.AddWithValue("@Rnote", item.Rnote);
            command.Parameters.AddWithValue("@Rbackrooms", item.Rbackrooms);
            command.Parameters.AddWithValue("@Rprice", item.Rprice);
            command.Parameters.AddWithValue("@Rprice_square", item.Rprice_square);
            command.Parameters.AddWithValue("@Ris_special_proposal", item.Ris_special_proposal);
            command.Parameters.AddWithValue("@Rchange_only", item.Rchange_only);
            command.Parameters.AddWithValue("@Rnewbuilding", item.Rnewbuilding);
            command.Parameters.AddWithValue("@Rhypothec", item.Rhypothec);
            command.Parameters.AddWithValue("@Ragent_code", item.Ragent_code);
            command.Parameters.AddWithValue("@Rdate_add", item.Rdate_add);
            command.Parameters.AddWithValue("@Rdate_change", item.Rdate_change);
            command.Parameters.AddWithValue("@Rstatus", item.Rstatus);
            command.Parameters.AddWithValue("@Rtrade_date", item.Rtrade_date);
            command.Parameters.AddWithValue("@Rrent_price_day", item.Rrent_price_day);
            command.Parameters.AddWithValue("@Rrent_price_month", item.Rrent_price_month);
            command.Parameters.AddWithValue("@Raccount_in_rent", item.Raccount_in_rent);
            command.Parameters.AddWithValue("@Rbuilding_year", item.Rbuilding_year);
            command.Parameters.AddWithValue("@Rroof", item.Rroof);
            command.Parameters.AddWithValue("@Rterms_exchange", item.Rterms_exchange);
            command.Parameters.AddWithValue("@Rdistance_to_city", item.Rdistance_to_city);
            command.Parameters.AddWithValue("@Rground_area", item.Rground_area);
            command.Parameters.AddWithValue("@Rpurpose_use", item.Rpurpose_use);
            command.Parameters.AddWithValue("@Rbasement", item.Rbasement);
            command.Parameters.AddWithValue("@Rwindow_front_quantity", item.Rwindow_front_quantity);
            command.Parameters.AddWithValue("@Rmain_entrance_location", item.Rmain_entrance_location);
            command.Parameters.AddWithValue("@Rcommercial_outhouse", item.Rcommercial_outhouse);
            command.Parameters.AddWithValue("@Rsite_space", item.Rsite_space);
            command.Parameters.AddWithValue("@Rsewerage", item.Rsewerage);
            command.Parameters.AddWithValue("@Rphoto", item.Rphoto);
            command.Parameters.AddWithValue("@Roperation", item.Roperation);
            command.Parameters.AddWithValue("@Rlavatory_basin", item.Rlavatory_basin);
            command.Parameters.AddWithValue("@Rhouse_no", item.Rhouse_no);
            command.Parameters.AddWithValue("@Rblock_no", item.Rblock_no);
            command.Parameters.AddWithValue("@Rflat_no", item.Rflat_no);
            command.Parameters.AddWithValue("@RFridge", item.RFridge);
            command.Parameters.AddWithValue("@Rid", item.Rid);
            command.Parameters.AddWithValue("@Rtrade_price", item.Rid);

            command.ExecuteNonQuery();
            //add photo upload and comments insertion
            con.Close();
        }

        public estaterecord ReadEstate(string itemid)
        {
            estaterecord item = new estaterecord();
            string selectsql = String.Concat("SELECT * FROM realestate WHERE Rid ='", itemid, "'");
            MySqlConnection con = new MySqlConnection(ConnectionString);
            con.Open();
            MySqlCommand command = new MySqlCommand(selectsql, con);
            MySqlDataReader reader = command.ExecuteReader();
            DataTable result = new DataTable();
            result.Load(reader);
            item.Rid = Convert.ToInt32(itemid);
            item.Rcity = result.Rows[0][1].ToString();
            item.Rdistrict_region = result.Rows[0][2].ToString();
            item.Rstreet= result.Rows[0][3].ToString();
            item.Restate_type = result.Rows[0][4].ToString();
            try { item.Rfloor = Convert.ToInt32(result.Rows[0][5]);}
            catch { item.Rfloor = 0; }
            try { item.Rnumber_of_storeys = Convert.ToInt32(result.Rows[0][6]); }
            catch { item.Rnumber_of_storeys = 0; }
            try { item.Rroom_quantity = Convert.ToInt32(result.Rows[0][7]); }
            catch { item.Rroom_quantity = 1; }
            try { item.Rhaggle = Convert.ToInt32(result.Rows[0][8]); }
            catch { item.Rhaggle = 0; }
            item.Rproperty_type = result.Rows[0][9].ToString();
            item.Rtitle = result.Rows[0][10].ToString();
            try { item.Routhouse_legality = Convert.ToInt32(result.Rows[0][11]); }
            catch { item.Routhouse_legality = 0; }
            try { item.Ruse_for_office = Convert.ToInt32(result.Rows[0][12]); }
            catch { item.Ruse_for_office = 0; }
            item.Rdescription_short = result.Rows[0][13].ToString();
            item.Rbathroom_note = result.Rows[0][14].ToString();
            item.Rbathroom = result.Rows[0][15].ToString();
            item.Rwalling_type = result.Rows[0][16].ToString();
            item.Rkitchen_note = result.Rows[0][17].ToString();
            try { item.Rlevel_quantity = Convert.ToInt32(result.Rows[0][18]); }
            catch { item.Rlevel_quantity = 0; }
            try { item.Rparking = Convert.ToInt32(result.Rows[0][19]); }
            catch { item.Rparking = 0; }
            item.Rdescription_detail = result.Rows[0][20].ToString();
            item.Rroom_layout = result.Rows[0][21].ToString();
            item.Rhouse_type = result.Rows[0][22].ToString();
            item.Rbalcony = result.Rows[0][23].ToString();
            item.Rloggia = result.Rows[0][24].ToString();
            item.Rwater_supply = result.Rows[0][25].ToString();
            item.Rgas_supply = result.Rows[0][26].ToString();
            item.Rcondition2 = result.Rows[0][27].ToString();
            item.Rheat_supply = result.Rows[0][28].ToString();
            item.Rinterior_door_type = result.Rows[0][29].ToString();
            item.Rfront_door_type = result.Rows[0][30].ToString();
            item.Rwindow_material = result.Rows[0][31].ToString();
            item.Rtelephone = result.Rows[0][32].ToString();
            item.Rtubing_material = result.Rows[0][33].ToString();
            item.Rkitchen_floor_space = result.Rows[0][34].ToString();
            item.Rhall_floor_space = result.Rows[0][35].ToString();
            try { item.Rtotal_floor_space = Convert.ToDouble(result.Rows[0][36]); }
            catch { item.Rtotal_floor_space = 0; }
            item.Rceiling_height = result.Rows[0][37].ToString();
            item.Rreference_point = result.Rows[0][38].ToString();
            try { item.Rfloor_space = Convert.ToDouble(result.Rows[0][39]); }
            catch { item.Rfloor_space = 0; }
            item.Rnote = result.Rows[0][40].ToString();
            item.Rbackrooms = result.Rows[0][41].ToString();
            try { item.Rprice = Convert.ToDouble(result.Rows[0][42]); }
            catch { item.Rprice = 0; }
            try { item.Rprice_square = Convert.ToDouble(result.Rows[0][43]); }
            catch { item.Rprice_square = 0; }
            try { item.Ris_special_proposal = Convert.ToInt32(result.Rows[0][44]); }
            catch { item.Ris_special_proposal = 0; }
            try { item.Rchange_only = Convert.ToInt32(result.Rows[0][45]); }
            catch { item.Rchange_only = 0; }
            try { item.Rnewbuilding = Convert.ToInt32(result.Rows[0][46]); }
            catch { item.Rnewbuilding = 0; }
            try { item.Rhypothec = Convert.ToInt32(result.Rows[0][47]); }
            catch { item.Rhypothec = 0; }
            item.Ragent_code = result.Rows[0][48].ToString();
            try { item.Rdate_add = Convert.ToDateTime(result.Rows[0][49]); }
            catch { item.Rdate_add = DateTime.Now; }
            try { item.Rdate_change = Convert.ToDateTime(result.Rows[0][50]); }
            catch { item.Rdate_change = DateTime.Now; }
            item.Rstatus = result.Rows[0][51].ToString();
            try { item.Rtrade_date = Convert.ToDateTime(result.Rows[0][52]); }
            catch { item.Rtrade_date = DateTime.Now; }
            try { item.Rrent_price_day = Convert.ToDouble(result.Rows[0][53]); }
            catch { item.Rrent_price_day = 0; }
            try { item.Rrent_price_month = Convert.ToDouble(result.Rows[0][54]); }
            catch { item.Rrent_price_month = 0; }
            try { item.Raccount_in_rent = Convert.ToInt32(result.Rows[0][55]); }
            catch { item.Raccount_in_rent = 0; }
            item.Rbuilding_year = result.Rows[0][56].ToString();
            item.Rroof = result.Rows[0][57].ToString();
            item.Rterms_exchange = result.Rows[0][58].ToString();
            try { item.Rdistance_to_city = Convert.ToDouble(result.Rows[0][59]); }
            catch { item.Rdistance_to_city = 0; }
            try { item.Rground_area = Convert.ToDouble(result.Rows[0][60]); }
            catch { item.Rground_area = 0; }
            item.Rpurpose_use = result.Rows[0][61].ToString();
            item.Rbasement = result.Rows[0][62].ToString();
            try { item.Rwindow_front_quantity = Convert.ToInt32(result.Rows[0][63]); }
            catch { item.Rwindow_front_quantity = 0; }
            item.Rmain_entrance_location = result.Rows[0][64].ToString();
            item.Rcommercial_outhouse = result.Rows[0][65].ToString();
            item.Rsite_space = result.Rows[0][66].ToString();
            item.Rsewerage = result.Rows[0][67].ToString();
            item.Rphoto = result.Rows[0][68].ToString();
            item.Roperation = result.Rows[0][69].ToString();
            item.Rlavatory_basin = result.Rows[0][70].ToString();
            item.Rhouse_no = result.Rows[0][71].ToString();
            item.Rblock_no = result.Rows[0][72].ToString();
            item.Rflat_no = result.Rows[0][73].ToString();
            try { item.RFridge = Convert.ToInt32(result.Rows[0][74]); }
            catch { item.RFridge = 0; }
            try { item.Rtrade_price = Convert.ToDouble(result.Rows[0][75]); }
            catch { item.Rtrade_price = 0; }
            con.Close();
            return item;
        }

        public void InsertNewTerra(terrarecord item)
        {

            string insertsql = String.Concat("INSERT INTO realestate(Tcity, Tdistrict_region, Tstreet, ",
                                  "Thaggle, Tproperty_type, Ttitle, Tdescription_detail,",
                                  "Twater_supply, Tgas_supply, Tcondition2, Tsewerage, Tdistance_to_city, Tdescription_short, Tfence, Tprice, ");
            insertsql = String.Concat(insertsql, "Tground_area ,Tnote, Tsite_space, Touthhouse, Telectricity, Tpurpose_land, Tis_special_proposal, ",
                                  "Tagent_code, Tdate_add, Tdate_change, Tstatus, Toperation, Tphoto, Twidth, Theight, Ttrade_date, Ttrade_price ) VALUES (");

            insertsql = String.Concat(insertsql, "@Tcity, @Tdistrict_region, @Tstreet, @Thaggle, @Tproperty_type, @Ttitle, @Tdescription_detail, ",
                                   "@Twater_supply, @Tgas_supply, @Tcondition2, @Tsewerage, @Tdistance_to_city, @Tdescription_short, @Tfence, @Tprice");
            insertsql = String.Concat(insertsql, "@Tground_area, @Tnote, @Tsite_space, @Touthhouse, @Telectricity, @Tpurpose_land, @Tis_special_proposal, ",
                                  "@Tagent_code, @Tdate_add, @Tdate_change, @Tstatus, @Toperation, @Tphoto, @Twidth, @Theight, @Ttrade_date, @Ttrade_price)");
            
            
            MySqlConnection con = new MySqlConnection(ConnectionString);
            con.Open();
            MySqlCommand command = new MySqlCommand(insertsql, con);

            command.Parameters.AddWithValue("@Tcity", item.Tcity);
            command.Parameters.AddWithValue("@Tdistrict_region", item.Tdistrict_region);
            command.Parameters.AddWithValue("@Tstreet", item.Tstreet);
                       
            command.Parameters.AddWithValue("@Thaggle", item.Thaggle);
            command.Parameters.AddWithValue("@Tproperty_type", item.Tproperty_type);
            command.Parameters.AddWithValue("@Ttitle", item.Ttitle);
            
            command.Parameters.AddWithValue("@Tdescription_short", item.Tdescription_short);           
            command.Parameters.AddWithValue("@Tdescription_detail", item.Tdescription_detail);
        
            command.Parameters.AddWithValue("@Twater_supply", item.Twater_supply);
            command.Parameters.AddWithValue("@Tgas_supply", item.Tgas_supply);
            command.Parameters.AddWithValue("@Tcondition2", item.Tcondition2);

            command.Parameters.AddWithValue("@Tsewerage", item.Tsewerage);
            command.Parameters.AddWithValue("@Tdistance_to_city", item.Tdistance_to_city);
            command.Parameters.AddWithValue("@Tfence", item.Tfence);
            command.Parameters.AddWithValue("@Tprice", item.Tprice);

            command.Parameters.AddWithValue("@Tground_area", item.Tground_area);
            command.Parameters.AddWithValue("@Tnote", item.Tnote);
            command.Parameters.AddWithValue("@Tsite_space", item.Tsite_space);
            command.Parameters.AddWithValue("@Touthhouse", item.Touthhouse);
            command.Parameters.AddWithValue("@Telectricity", item.Telectricity);
            command.Parameters.AddWithValue("@Tpurpose_land", item.Tpurpose_land);            
            command.Parameters.AddWithValue("@Tis_special_proposal", item.Tis_special_proposal);
           
            command.Parameters.AddWithValue("@Tagent_code", item.Tagent_code);
            command.Parameters.AddWithValue("@Tdate_add", item.Tdate_add);
            command.Parameters.AddWithValue("@Tdate_change", item.Tdate_change);
            command.Parameters.AddWithValue("@Tstatus", item.Tstatus);
            command.Parameters.AddWithValue("@Toperation", item.Toperation);
            command.Parameters.AddWithValue("@Tphoto", item.Tphoto);
            command.Parameters.AddWithValue("@Twidth", item.Twidth);
            command.Parameters.AddWithValue("@Theight", item.Theight);
            command.Parameters.AddWithValue("@Ttrade_date", item.Ttrade_date);
            command.Parameters.AddWithValue("@Theight", item.Ttrade_price);
             
            command.ExecuteNonQuery();
            int id = Convert.ToInt32(command.LastInsertedId);

            //add photo upload and comments insertion

            con.Close();
        }


        public void UpdateTerra(terrarecord item)
        {

            string updatesql = @"UPDATE realestate SET Tcity = @Tcity, Tdistrict_region = @Tdistrict_region, Tstreet = @Tstreet, 
                               Thaggle = @Thaggle, Tproperty_type = @Tproperty_type, Ttitle = @Ttitle, Tdescription_detail = @Tdescription_detail,
                               Twater_supply = @Twater_supply, Tgas_supply = @Tgas_supply, Tcondition2 = @Tcondition2, Tsewerage = @Tsewerage,
                               Tdistance_to_city = @Tdistance_to_city, Tdescription_short = @Tdescription_short, Tfence = @Tfence, Tprice = @Tprice, 
                               Tground_area = @Tground_area ,Tnote = @Tnote, Tsite_space = @Tsite_space, Touthhouse = @Touthouse, Telectricity = @Telectricity, 
                               Tpurpose_land = @Tpurpose_land, Tis_special_proposal = @Tis_special_proposal, Tagent_code = @Tagent_add, Tdate_add = @Tdate_add, 
                               Tdate_change = @Tdate_change, Tstatus = @Tstatus, Toperation = @Toperation, Tphoto = @Tphoto, Twidth = @Twidth, Theight = @Theight, 
                               Ttrade_date = @Ttrade_date, Ttrade_price = @Ttrade_price WHERE TId = @TId";

            MySqlConnection con = new MySqlConnection(ConnectionString);
            con.Open();
            MySqlCommand command = new MySqlCommand(updatesql, con);

            command.Parameters.AddWithValue("@TId", item.Tid);
            command.Parameters.AddWithValue("@Tcity", item.Tcity);
            command.Parameters.AddWithValue("@Tdistrict_region", item.Tdistrict_region);
            command.Parameters.AddWithValue("@Tstreet", item.Tstreet);

            command.Parameters.AddWithValue("@Thaggle", item.Thaggle);
            command.Parameters.AddWithValue("@Tproperty_type", item.Tproperty_type);
            command.Parameters.AddWithValue("@Ttitle", item.Ttitle);

            command.Parameters.AddWithValue("@Tdescription_short", item.Tdescription_short);
            command.Parameters.AddWithValue("@Tdescription_detail", item.Tdescription_detail);

            command.Parameters.AddWithValue("@Twater_supply", item.Twater_supply);
            command.Parameters.AddWithValue("@Tgas_supply", item.Tgas_supply);
            command.Parameters.AddWithValue("@Tcondition2", item.Tcondition2);

            command.Parameters.AddWithValue("@Tsewerage", item.Tsewerage);
            command.Parameters.AddWithValue("@Tdistance_to_city", item.Tdistance_to_city);
            command.Parameters.AddWithValue("@Tfence", item.Tfence);
            command.Parameters.AddWithValue("@Tprice", item.Tprice);

            command.Parameters.AddWithValue("@Tground_area", item.Tground_area);
            command.Parameters.AddWithValue("@Tnote", item.Tnote);
            command.Parameters.AddWithValue("@Tsite_space", item.Tsite_space);
            command.Parameters.AddWithValue("@Touthhouse", item.Touthhouse);
            command.Parameters.AddWithValue("@Telectricity", item.Telectricity);
            command.Parameters.AddWithValue("@Tpurpose_land", item.Tpurpose_land);
            command.Parameters.AddWithValue("@Tis_special_proposal", item.Tis_special_proposal);

            command.Parameters.AddWithValue("@Tagent_code", item.Tagent_code);
            command.Parameters.AddWithValue("@Tdate_add", item.Tdate_add);
            command.Parameters.AddWithValue("@Tdate_change", item.Tdate_change);
            command.Parameters.AddWithValue("@Tstatus", item.Tstatus);
            command.Parameters.AddWithValue("@Toperation", item.Toperation);
            command.Parameters.AddWithValue("@Tphoto", item.Tphoto);
            command.Parameters.AddWithValue("@Twidth", item.Twidth);
            command.Parameters.AddWithValue("@Theight", item.Theight);
            command.Parameters.AddWithValue("@Ttrade_date", item.Ttrade_date);
            command.Parameters.AddWithValue("@Ttrade_price", item.Ttrade_price);

            command.ExecuteNonQuery();
            int id = Convert.ToInt32(command.LastInsertedId);

            //add photo upload and comments insertion

            con.Close();
        }

        public terrarecord ReadTerra(string itemid)
        {
            terrarecord item = new terrarecord();
            string selectsql = String.Concat("SELECT * FROM terra WHERE TId ='", itemid, "'");
            MySqlConnection con = new MySqlConnection(ConnectionString);
            con.Open();
            MySqlCommand command = new MySqlCommand(selectsql, con);
            MySqlDataReader reader = command.ExecuteReader();
            DataTable result = new DataTable();
            result.Load(reader);
            item.Tid = Convert.ToInt32(itemid);
            item.Tcity = result.Rows[0][1].ToString();
            item.Tdistrict_region = result.Rows[0][2].ToString();
            item.Tstreet = result.Rows[0][3].ToString();
            
            try { item.Thaggle = Convert.ToInt32(result.Rows[0][4]); }
            catch { item.Thaggle = 0; }
            item.Tproperty_type = result.Rows[0][5].ToString();
            item.Ttitle = result.Rows[0][6].ToString();
            item.Tdescription_detail = result.Rows[0][7].ToString();
            item.Twater_supply = result.Rows[0][8].ToString();
            item.Tgas_supply = result.Rows[0][9].ToString();
            item.Tcondition2 = result.Rows[0][10].ToString();
            item.Tsewerage = result.Rows[0][11].ToString();
            try { item.Tdistance_to_city = Convert.ToDouble(result.Rows[0][12]); }
            catch { item.Tdistance_to_city = 0; }
            item.Tdescription_short = result.Rows[0][13].ToString();
            try { item.Tfence = Convert.ToInt32(result.Rows[0][14]); }
            catch { item.Tfence = 0; }
            try { item.Tprice = Convert.ToDouble(result.Rows[0][15]); }
            catch { item.Tprice = 0; }
            try { item.Tground_area = Convert.ToDouble(result.Rows[0][16]); }
            catch { item.Tground_area = 0; }
            item.Tnote = result.Rows[0][17].ToString();
            item.Tsite_space = result.Rows[0][18].ToString();
            item.Touthhouse = result.Rows[0][19].ToString();
            try { item.Telectricity = Convert.ToInt32(result.Rows[0][20]); }
            catch { item.Telectricity = 0; }
            item.Tpurpose_land = result.Rows[0][21].ToString();
            try { item.Tis_special_proposal = Convert.ToInt32(result.Rows[0][22]); }
            catch { item.Tis_special_proposal = 0; }
            item.Tagent_code = result.Rows[0][23].ToString();
            try { item.Tdate_add = Convert.ToDateTime(result.Rows[0][24]); }
            catch { item.Tdate_add = DateTime.Now; }
            try { item.Tdate_change = Convert.ToDateTime(result.Rows[0][25]); }
            catch { item.Tdate_change = DateTime.Now; }
            item.Tstatus = result.Rows[0][26].ToString();
            item.Toperation = result.Rows[0][27].ToString();
            item.Tphoto = result.Rows[0][28].ToString();
            item.Twidth = result.Rows[0][29].ToString();
            item.Theight = result.Rows[0][30].ToString();
            try { item.Ttrade_date = Convert.ToDateTime(result.Rows[0][31]); }
            catch { item.Ttrade_date = DateTime.Now; }
            try { item.Ttrade_price = Convert.ToDouble(result.Rows[0][32]); }
            catch { item.Ttrade_price = 0; }  
            
            con.Close();
            return item;
        }

        public void LoadStreetsForTown(string code)
        {
            code = code.Substring(0, 11);
            street = SQLquery(String.Concat("SELECT CONCAT(RS_type, '. ', RS_street) as stname, RS_code from rstreets WHERE CAST(RS_code AS CHAR) Like CONCAT('", code, "','%') ORDER by RS_street"));            
        }
        public void LoadTownForDistrict(string code)
        {
            code = code.Substring(0, 5);
            town = SQLquery(String.Concat("SELECT RR_code, RR_town from region15 WHERE ((RR_type!='р-н') AND (CAST(RR_code AS CHAR) Like CONCAT('", code, "','%'))) ORDER by RR_town"));
        }
        public void LoadAddressBase()
        {            
            district = SQLquery(@"SELECT RR_code, RR_town from region15 WHERE RR_type='р-н' ORDER by RR_code");
            LoadTownForDistrict("1500000000000");
            LoadStreetsForTown("1500000100000");  
        }
        public void LoadCommentsForObject(string ObjectID, string ObjectEstateType)
        {
            string sql = String.Concat("SELECT Rc_data, Rc_agent, Rc_comment from rcomments WHERE ((Rc_object='", ObjectID, "') AND (Rc_estate_type='");
            sql = String.Concat(sql, ObjectEstateType, "')) ORDER by Rc_data DESC");
            town = SQLquery(sql);
        }

    }
}