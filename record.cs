using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace realty
{
    public class estaterecord
    {
        public long Rid;
        public string Rcity;
        public string Rdistrict_region;
        public string Rstreet;
        public string Restate_type;
        public int Rfloor = 1;
        public int Rnumber_of_storeys = 0;
        public int Rroom_quantity = 1;
        public int Rhaggle = 1;
        public string Rproperty_type;
        public string Rtitle;
        public int Routhouse_legality = 0;
        public int Ruse_for_office = 0;
        public string Rdescription_short;
        public string Rbathroom_note;
        public string Rbathroom;
        public string Rwalling_type;
        public string Rkitchen_note;
        public int Rlevel_quantity = 1;
        public int Rparking = 0;
        public string Rdescription_detail;
        public string Rroom_layout;
        public string Rhouse_type;
        public string Rbalcony;
        public string Rloggia;
        public string Rwater_supply;
        public string Rgas_supply;
        public string Rcondition2;
        public string Rheat_supply;
        public string Rinterior_door_type;
        public string Rfront_door_type;
        public string Rwindow_material;
        public string Rtelephone;
        public string Rtubing_material;
        public string Rkitchen_floor_space = "";
        public string Rhall_floor_space = "";
        public double Rtotal_floor_space = 0;
        public string Rceiling_height = "";
        public string Rreference_point; //?
        public double Rfloor_space = 0.0;
        public string Rnote; //?
        public string Rbackrooms; //?
        public double Rprice = 0;
        public double Rprice_square = 0;
        public int Ris_special_proposal = 0;
        public int Rchange_only = 0;
        public int Rnewbuilding = 0;
        public int Rhypothec = 0;
        public string Ragent_code;
        public DateTime Rdate_add;
        public DateTime Rdate_change;
        public string Rstatus; //?
        public DateTime Rtrade_date;
        public double Rrent_price_day = 0.0;
        public double Rrent_price_month = 0.0;
        public int Raccount_in_rent = 0;
        public string Rbuilding_year;
        public string Rroof;
        public string Rterms_exchange;
        public double Rdistance_to_city = 0.0; 
        public double Rground_area = 0.0;
        public string Rpurpose_use;
        public string Rbasement;
        public int Rwindow_front_quantity = 0;
        public string Rmain_entrance_location;
        public string Rcommercial_outhouse;
        public string Rsite_space;
        public string Rsewerage;
        public string Rphoto;
        public string Roperation;
        public string Rlavatory_basin;
        public string Rhouse_no = "";
        public string Rblock_no = "";
        public string Rflat_no = "";
        public int RFridge = 1;
        public double Rtrade_price = 0;
        public estaterecord()
        {
            Rdate_add = DateTime.Now;
            Rdate_change = DateTime.Now;
        }
    }

    public class terrarecord
    {
        public long Tid;
        public string Tcity;
        public string Tdistrict_region;
        public string Tstreet;
        public int Thaggle = 1;
        public string Tproperty_type;
        public string Ttitle;       
        public string Tdescription_detail;
        public string Tdescription_short;
        public string Twater_supply;
        public string Tgas_supply;
        public string Tcondition2;
        public string Tsewerage;
        public double Tdistance_to_city = 0.0;
        public int Tfence = 0;
        public double Tprice = 0;
        public double Tground_area = 0.0;
        public string Tnote;
        public string Tsite_space;
        public string Touthhouse;
        public int Telectricity = 1;
        public string Tpurpose_land;
        public int Tis_special_proposal = 0;
        public string Tagent_code;
        public DateTime Tdate_add;
        public DateTime Tdate_change;
        public string Tstatus; //?
        public string Toperation;
        public string Tphoto;
        public string Twidth;
        public string Theight;
        public DateTime Ttrade_date;
        public double Ttrade_price;


        public terrarecord()
        {
            Tdate_add = DateTime.Now;
            Tdate_change = DateTime.Now;
        }
    }
    
}
