using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Net;
using System.IO;
using System.Security.Cryptography;

namespace realty
{
    public struct StreetsStruct
    {
        public string rs_code;
        public string name;
        public string string_type;

    }

    public struct ImageStruct
    {
        public string FullPath;
        public string name;
    }

    public struct PhotoStruct
    {
        public string uri;
        public string name;
    }

    public struct ChListStruct
    {
        public string FieldName;
        public string caption;
    }

    public struct IdNameStruct
    {
        public string id;
        public string name;
    }
    public struct DistrictsStruct
    {
        public string rr_code;
        public string rr_town;


    }

    public struct TownStruct
    {
        public string rr_code;
        public string name;
        public List<StreetsStruct> streets;


    }



    public struct AddressStruct
    {
        public string city;
        public string street;
        public string house_no;
        public string block_no;
        public string flat_no;
        //public string estate_type;

    }
    class ClFunctions
    {
        
        public  string connStr ;
        public string[] roomlayotValues;
        public string[] balconyValues;
        public string[] loggiaValues;
        public string[] phoneValues;
        public string[] tubeValues;
        public string[] wallValues;
        public string[] waterValues;
        public string[] gasValues;
        public string[] heatValues;
        public string[] bathValues;
        public string[] intdoorValues;
        public string[] windowValues;
        public string[] conditionValues;
        public string[] propertyValues;
        public string[] sewerageValues;
        public string[] housetypeValues;
        public string[] purpose_landValues;
        public string[] front_door_typeValues;
        public string[] operationValues;
        public string[] estate_typeValues;
        public string[] floors;
        public string[] floorsNumber;
        public string[] satusValue;
        public string[] terraMinAreaValue;
        public string[] terraMaxAreaValue;
        public string fkey;
        public ClFunctions()
        {
            connStr = "server=tamarrra.net;user=user;database=RealtMultiBase;password=Ardonskaya209;charset=utf8";
            //"server=tamarrra.net;user=user;database=RealtMultiBase;password=Ardonskaya209;charset=utf8";
            //"server=localhost;user=root;database=realtmultibase;password=robingood;CharSet=utf8";

 

            roomlayotValues = new string[] { "1 к-та проходная", "Готовый офис", "Комната с альковом", "Не правильная","По проекту", "Полураспашонка", "Правильная", "Раздельное","Распашонка", "Свободная", "Смежное", "Смежно-параллельное","Смежно-раздельное", "Типовая", "Трамвай", "Улучшенная"};
            propertyValues = new string[] { "Госакт", "Государственная", "Дачный кооператив", "Долевое участие", "Муниципальная", "Неприватиз", "Общественная", "Частная (приватизированная)"};
            sewerageValues = new string[] { "Горканализация", "Своя канализация", "Удобаство во дворе" };
            housetypeValues = new string[] { "Бельгийка", "Болгарка", "Бывшее общежитие", "Высотка", "Гостинка", "Индивидуальный проект", "Коммуналка", "Малосемейка", "Общежитие", "Новый дом", "Особняк", "Сталинка", "Хрущевка", "Брежневка", "Новостройка", "Старый фонд", "Улучшенной планировки" };
            
            balconyValues = new string[] { "Застеклен", "Не застеклен", "Зарешечен" };
            loggiaValues = new string[] { "Застеклена", "Не застеклена", "Зарешечена" };
            phoneValues = new string[] { "Отдельный", "Блокиратор", "Коммерческий", "Коммунальный", "Отсутствует" };
            tubeValues = new string[] { "Черные", "Металлопластиковые", "Новые", "Оцинкованные" };
            intdoorValues = new string[] { "Деревянная", "Металлопластик","Пластик" };
            windowValues = new string[] { "Деревянные", "Евробрус", "Металлопластиковые", "Новые", "Пластиковые", };
            gasValues = new string[] { "Природный", "Электроплита", "Балонный", "Нет" };
            heatValues = new string[] { "ТЭЦ", "Электрическое", "Автономное", "Нет" };
            bathValues = new string[] { "Туалет/ванна", "Туалет/душ", "Только туалет", "Совмещенный", "Отсутствует", "Раздельный" };
            waterValues = new string[] { "Холодная (водопровод)", "Горячая (ТЭЦ)", "Горячая (бойлер)", "Холодная (колодец)", "Горячая (АГВ)", "Водопровода нет" };
            wallValues = new string[] { "Блочный", "Кирпичный", "Монолитный", "Панельный" ,"Камень-известняк", "Железобетон", "Каркасно-монолитный", "Кирпично-монолитный","Монолитно-бетонный", "Блочно-кирпичный"};
            conditionValues = new string[] { "Хорошее", "Евроремонт", "Косметический ремонт", "Капитальный ремонт", "Нужен ремонт" };
            purpose_landValues = new string[] { "ИЖС", "Садоводчество", "Сельскохозяйственное", "Коммерческое"};
            front_door_typeValues = new string[] { " Простая", "Железная", "Двойная", "Деревянная", "Бронированная" };

            operationValues = new string[] { "Продажа", "Аренда", "Обмен" };
            estate_typeValues = new string[] { "Земельный участок", "Квартира", "Коммерческая недвижимость" , "Дом"};
            floors = new string[] { "Не первый", "Не последний", "Не первый и не последний" };
            floorsNumber = new string[] { "< 5", "5-8", "9-12", "> 12" };
            satusValue = new string[] { "Актуально", "Продано", "Сдано в аренду", "Совершен обмен" };
            terraMinAreaValue = new string[] { "0", "2", "4", "6","8","10","15","20","30","50", "100","250","500","1000"};
            terraMaxAreaValue = new string[] { "1", "3", "5", "7", "9", "14", "19", "29", "49", "99", "249", "499", "999", "> 1000" };
            fkey="realty14";
        }
        
        public string preparecity(string city)
        {
            if (city != "")
            {
                city = city.Replace("г.", "").Trim();
                city = city.Replace("c.", "").Trim();
                city = city.Replace("пгт.", "").Trim();
                city = city.Replace("р-н.", "").Trim();
                city = city.Replace("п.", "").Trim();
                string tt = city[0].ToString().ToUpper();
                city = city.ToLower();
                city = tt + city.Remove(0,1);
            }
            return city;
        }

        public string toUpperFirst(string somestr)
        {
            somestr = somestr.Trim();
            if (somestr != "")
            {

                string tt = somestr[0].ToString().ToUpper();
                somestr = somestr.ToLower();
                somestr = tt + somestr.Remove(0, 1);
            }
            return somestr;
        }
        public  DataTable ReadFromTable(string sql,out string message)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
             message = "";
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                 
                    
                    DataTable dt = new DataTable();
                    dt.Load(rdr);
                    conn.Close();
                    return dt;                  
                 
                  
                }
                catch (Exception ex)
                {  
                    conn.Close();
                   message=ex.ToString();
                }
                 return null;              
            
        }

        public string InsertToTable(string sql, out long id)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            string message = "";
            id=0;
            try
            {
                conn.Open();             
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
               if(cmd.LastInsertedId>0) id = cmd.LastInsertedId;
            }
            catch (Exception ex)
            {
                message = ex.ToString();
            } 
            conn.Close();
            return message;

        }

        public string InsertToTable(string sql)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            string message = "";
            
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
              
            }
            catch (Exception ex)
            {
                message = ex.ToString();
            }
            conn.Close();
            return message;

        }

        public string findStreet(List<StreetsStruct> streets, string streetN)
        {
            int count=streets.Count;
            
            streetN=streetN.ToLower().Trim();
            int indx;
            string[] strparts;
            string[] SlashParts;
            string[] cur_strparts;
            string tmp, streetName;
            int eq=0;
            int slash = 0;
            slash = streetN.IndexOf("/");
            if (streetN.IndexOf("ул.") >= 0)
            {
                streetN = streetN.Replace("ул.", "").Trim();
            }

            if (slash > 0) SlashParts = streetN.Split('/');
            else SlashParts = new string[] { streetN };
            for (int t = 0; t < SlashParts.Length; t++)
            {
                streetName = SlashParts[t];
                indx = streetName.IndexOf(".");
                for (int i = 0; i < count; i++)
                {
                    if (streets[i].name == "генерала дзусова" && streetName == "дзусова") return streets[i].rs_code;
                    if (streetName == streets[i].name) return streets[i].rs_code;
                    cur_strparts = streetName.Split(' ');
                    tmp = streets[i].name;
                    strparts = tmp.Split(' ');
                    if (cur_strparts.Length == strparts.Length)
                    {
                        eq = 0;
                        for (int j = 0; j < cur_strparts.Length; j++) if (cur_strparts[j] == strparts[j]) eq++;
                        if (eq == cur_strparts.Length) return streets[i].rs_code;
                    }

                    if (indx > 0)
                    {
                        cur_strparts = streetName.Split('.');
                        if (strparts.Length > 1 && cur_strparts.Length > 1 && (cur_strparts[0].Length == 1||cur_strparts[0]=="бр" ))
                        {
                            if (strparts[0][0].ToString() == cur_strparts[0] || (cur_strparts[0] == "бр" &&  strparts[0].IndexOf("бр") >=0))
                                for (int j = 0; j < cur_strparts.Length; j++)
                                    if (cur_strparts[j] != "" && cur_strparts[j].IndexOf(strparts[1]) >= 0) 
                                    { return streets[i].rs_code; }

                        }
                        else if (cur_strparts[0].Length != 1 && streetName.IndexOf(tmp) >= 0) return streets[i].rs_code;
                    }

                }
            }
               

            return "";
        }
        public List<StreetsStruct> getStreets(string town, out string rr_code, out string message )
        {
            town=town.Trim();
            message = "";
            rr_code = "";
            if (town!="")
            {
                town=preparecity(town);
                
                string sql = "SELECT region15.RR_code, rstreets.RS_street, rstreets.RS_type, rstreets.RS_code  from rstreets, region15 WHERE ( (RR_town Like '%" + town + "%' ) AND (CAST( RS_code AS CHAR )  Like CONCAT(LEFT(RR_code,11),'%'))) ORDER by RS_street";
                MySqlConnection conn = new MySqlConnection(connStr);

                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    List<StreetsStruct> streets = new List<StreetsStruct>();
                    StreetsStruct somestreet;
                    while (rdr.Read())
                    {
                        rr_code = rdr[0].ToString();
                        somestreet.name = rdr[1].ToString().ToLower();
                        somestreet.string_type = rdr[2].ToString();
                        somestreet.rs_code = rdr[3].ToString();
                        if (somestreet.rs_code != null && somestreet.rs_code!="") streets.Add(somestreet);
                     
                    }
                    rdr.Close();
                    conn.Close();
                    return streets; 
                }
                catch (Exception ex)
                {
                   message=ex.ToString();
                }
                conn.Close();
            }
           
            return null;
        }


        public List<AddressStruct> getAllAddresses( out string message)
        {
          

                string sql = "SELECT Rstreet, Rcity, Rhouse_no, Rblock_no, Rflat_no FROM realestate WHERE ( Rstatus <> 'продано' AND  Rstatus <> 'сдано' )   ORDER by Rstreet";
                MySqlConnection conn = new MySqlConnection(connStr);

                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    List<AddressStruct> Alladdr = new List<AddressStruct>();
                    AddressStruct someaddress;
                    while (rdr.Read())
                    {
                       
                       someaddress.street = rdr[0].ToString();
                       someaddress.city = rdr[1].ToString();
                       someaddress.house_no = rdr[2].ToString();
                       someaddress.block_no = rdr[3].ToString();
                       someaddress.flat_no = rdr[4].ToString();
                       if(someaddress.city!=null) Alladdr.Add(someaddress);
                    }
                    rdr.Close();
                    conn.Close();
                    message = "";
                    return Alladdr;
                }
                catch (Exception ex)
                {
                    message = ex.ToString();
                }
                conn.Close();
            

            return null;
        }
        public List<DistrictsStruct> getDistricts(out string message)
        {
          
            message = "";


                string sql = "SELECT RR_code, RR_town  FROM  region15 WHERE  RR_type='р-н' ORDER by RR_town";
                MySqlConnection conn = new MySqlConnection(connStr);

                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    List<DistrictsStruct> districts = new List<DistrictsStruct>();
                    DistrictsStruct somedist;
                    while (rdr.Read())
                    {
                        somedist.rr_code = rdr[0].ToString();
                        somedist.rr_town = rdr[1].ToString();
                        districts.Add(somedist);

                    }
                    rdr.Close();
                    conn.Close();
                    return districts;
                }
                catch (Exception ex)
                {
                    message = ex.ToString();
                }
                conn.Close();
                return null;
         }

        public List<DistrictsStruct> getTowns(out string message)
        {

            message = "";


            string sql = "SELECT RR_type, RR_code, RR_town   from region15 WHERE NOT RR_type='Респ' AND NOT RR_type='р-н' ORDER by RR_type, RR_town";
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                List<DistrictsStruct> towns = new List<DistrictsStruct>();
                DistrictsStruct sometown;
                while (rdr.Read())
                {
                    sometown.rr_code = rdr[1].ToString();
                    sometown.rr_town = rdr[0].ToString() +"." + rdr[2].ToString(); 
                    towns.Add(sometown);

                }
                rdr.Close();
                conn.Close();
                return towns;
            }
            catch (Exception ex)
            {
                message = ex.ToString();
            }
            conn.Close();
            return null;
        }


        public List<IdNameStruct> getAgency(out string message)
        {

            message = "";


            string sql = "SELECT Aid, Aname  FROM  agency  ORDER BY Aname";
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                List<IdNameStruct> agencies = new List<IdNameStruct>();
                IdNameStruct someagency;
                while (rdr.Read())
                {
                    someagency.id = rdr[0].ToString();
                    someagency.name= rdr[1].ToString();
                    agencies.Add(someagency);

                }
                rdr.Close();
                conn.Close();
                return agencies;
            }
            catch (Exception ex)
            {
                message = ex.ToString();
            }
            conn.Close();
            return null;
        }


        public List<IdNameStruct> getAgents(string agency,out string message)
        {

            message = "";


            string sql = "SELECT Ag_id, Ag_FIO  FROM  agents WHERE Ag_Agency="+agency+" ORDER Ag_FIO ";
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                List<IdNameStruct> agents = new List<IdNameStruct>();
                IdNameStruct someagent;
                while (rdr.Read())
                {
                    someagent.id = rdr[0].ToString();
                    someagent.name = rdr[1].ToString();
                    agents.Add(someagent);

                }
                rdr.Close();
                conn.Close();
                return agents;
            }
            catch (Exception ex)
            {
                message = ex.ToString();
            }
            conn.Close();
            return null;
        }


        public  bool UploadFileToServer(string fileName, Uri serverUri)
        {

            /// ftp://turangaleela:Ardonskaya209@tamarrra.net:21//www/img1/
            // The URI described by serverUri should use the ftp:// scheme. 
            // It contains the name of the file on the server. 
            // Example: ftp://contoso.com/someFile.txt. 
            //  
            // The fileName parameter identifies the file containing the data to be uploaded. 

            if (serverUri.Scheme != Uri.UriSchemeFtp)
            {
                return false;
            }
            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverUri);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            // Don't set a time limit for the operation to complete.
            request.Timeout = System.Threading.Timeout.Infinite;

            // Copy the file contents to the request stream. 
            const int bufferLength = 2048;
            byte[] buffer = new byte[bufferLength];

            int count = 0;
            int readBytes = 0;
            FileStream stream = File.OpenRead(fileName);
            Stream requestStream = request.GetRequestStream();
            do
            {
                readBytes = stream.Read(buffer, 0, bufferLength);
                requestStream.Write(buffer, 0, bufferLength);
                count += readBytes;
            }
            while (readBytes != 0);

            Console.WriteLine("Writing {0} bytes to the stream.", count);
            // IMPORTANT: Close the request stream before sending the request.
            requestStream.Close();

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Console.WriteLine("Upload status: {0}, {1}", response.StatusCode, response.StatusDescription);

            response.Close();
            stream.Close();
            return true;
        }

        private bool UploadFiletoFtp(string sourceFile)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.sdelka15-ru.1gb.ru/http//AvitoFiles//test");
            request.Method = WebRequestMethods.Ftp.UploadFile;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential("user", "pass");

            // Copy the contents of the file to the request stream.
            StreamReader sourceStream = new StreamReader(sourceFile);
            byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            sourceStream.Close();
            request.ContentLength = fileContents.Length;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);
            requestStream.Close();

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            bool res;
            if (response.StatusCode == FtpStatusCode.CommandOK) res = true;
            else res = false;

            response.Close();
            return res;
        }


        // Encrypt the string.
        public string  Encrypt(string PlainText, string sKey)
        {



            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            //A 64 bit key and IV is required for this provider.
            //Set secret key For DES algorithm.
            byte [] bt=ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            //Set initialization vector.
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);


            // Create a memory stream.
            MemoryStream ms = new MemoryStream();

            // Create a CryptoStream using the memory stream and the 
            // CSP DES key.  
            CryptoStream encStream = new CryptoStream(ms, DES.CreateEncryptor(), CryptoStreamMode.Write);

            // Create a StreamWriter to write a string
            // to the stream.
            StreamWriter sw = new StreamWriter(encStream);

            // Write the plaintext to the stream.
            sw.WriteLine(PlainText);

            // Close the StreamWriter and CryptoStream.
            sw.Close();
            encStream.Close();

            // Get an array of bytes that represents
            // the memory stream.
            byte[] buffer = ms.ToArray();

            // Close the memory stream.
            ms.Close();

            // Return the encrypted byte array.
            string res="";
            foreach (byte b in buffer)
                res += string.Format("{0:x2}", b);  

            return res;
        }

        public string Decrypt(string ss, string sKey)
        {
            // Create a memory stream to the passed buffer.          
            byte[] CypherText = Encoding.Unicode.GetBytes(ss);  
            MemoryStream ms = new MemoryStream(CypherText);

            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            //A 64 bit key and IV is required for this provider.
            //Set secret key For DES algorithm.
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            //Set initialization vector.
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
          
            // Create a CryptoStream using the memory stream and the 
            // CSP DES key. 
            CryptoStream encStream = new CryptoStream(ms, DES.CreateDecryptor(), CryptoStreamMode.Read);

            // Create a StreamReader for reading the stream.
            StreamReader sr = new StreamReader(encStream);

            // Read the stream as a string.
            string val = sr.ReadLine();

            // Close the streams.
            sr.Close();
            encStream.Close();
            ms.Close();

            return val;
        }


        public bool DeleteFileFromServer(string fileName, Uri serverUri)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverUri);
                //If you need to use network credentials
                //request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                //additionally, if you want to use the current user's network credentials, just use:
                //System.Net.CredentialCache.DefaultNetworkCredentials

                request.Method = WebRequestMethods.Ftp.DeleteFile;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                // MessageBox.Show(response.StatusDescription);

                bool res;
                if (response.StatusCode == FtpStatusCode.CommandOK) res = true;
                else res = false;

                response.Close();
                return res;
            }
            catch { return true; }
        }
  public string GetMD5String(string s)  
    {  
      //переводим строку в байт-массим  
      byte[] bytes = Encoding.Unicode.GetBytes(s);  
  
      //создаем объект для получения средст шифрования  
      MD5CryptoServiceProvider CSP =  
          new MD5CryptoServiceProvider();  
          
      //вычисляем хеш-представление в байтах  
      byte[] byteHash = CSP.ComputeHash(bytes);  
  
      string hash = string.Empty;  
  
      //формируем одну цельную строку из массива  
      foreach (byte b in byteHash)  
          hash += string.Format("{0:x2}", b);  
  
      return hash;  
    }  

    }
}
