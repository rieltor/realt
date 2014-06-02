using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace realty
{
    public class Agent
    {
       private string _id;
       public string  FIO;
       private bool _isadmin;
       public string agency;
       public string phone;
       public string email;
       public Agent()
       {
           _isadmin = false;
       }
       public bool IsAdmin
       {
           get { return _isadmin; }          
       }
       public string Id
       {
           get { return _id; }
       }
       public bool Login(string login, string pass)
       {
           MySqlConnection con = new MySqlConnection("server=tamarrra.net;user=user;database=RealtMultiBase;password=Ardonskaya209;charset=utf8");
           con.Open();
           MySqlCommand cmd = new MySqlCommand("SELECT Ag_id, Ag_FIO, Ag_agency, Ag_isadmin, Ag_phone, Ag_mail from agents WHERE ((Ag_login='"+ login+ "') AND (Ag_password='"+pass+"'))", con);
           MySqlDataReader reader = cmd.ExecuteReader();
           DataTable result = new DataTable();
           result.Load(reader);
           con.Close();
           if (result.Rows.Count==1)
           {
               _id = result.Rows[0][0].ToString();
               FIO = result.Rows[0][1].ToString();
               agency = result.Rows[0][2].ToString();
               phone = result.Rows[0][4].ToString();
               email = result.Rows[0][5].ToString();
               if (Convert.ToInt32(result.Rows[0][3]) == 1) { _isadmin = true; }
               return true;
           } 
           else
           {
               return false;
           }
       }

    }
    public class Img
    {
        private string _name;
        private string _url;
        private string _description;
        private bool _isnew;
        private Image _picture;
        private string _originalPath;

        public Img()
        {
            _name = "";
            _url = "";
            _description = "";
            _isnew = false;
            _originalPath = "";
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string OriginalPath
        {
            get { return _originalPath; }
            set { _originalPath = value; }
        }

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        public bool IsNew
        {
            get { return _isnew; }
            set { _isnew = value; }
        }

        public Image Picture
        {
            get { return _picture; }
            set { _picture = value; }
        }
    }
    public partial class Photos
    {
        //public Img[] photolist;
        public ArrayList photolist; 
        public Photos(string src)
        {
            Img photo;
            photolist = new ArrayList();
            if (src != "")
            {
                string[] urls = src.Split(';');
                //photolist = new Img[urls.Length];
               

                for (int i = 0; i < urls.Length; i++)
                {
                    //photolist[i] = new Img();
                    //photolist[i].Url = urls[i];
                    //photolist[i].Name = urls[i].Substring(urls[i].LastIndexOf('/') + 1);
                    // photolist[i].Description = "Полный размер на сайте";
                    photo = new Img();
                    photo.Url = urls[i];
                    photo.OriginalPath = urls[i];
                    photo.Name = urls[i].Substring(urls[i].LastIndexOf('/') + 1);
                    photo.Description = "Полный размер на сайте";
                    photo.IsNew = false;
                    photolist.Add(photo);
                }
            }
        }

    
        public void LoadImagesFromURLs()
        {

            for (int i = 0; i < photolist.Count; i++)
            {
                try
                {
                    WebRequest req = WebRequest.Create((photolist[i] as Img).Url);
                    WebResponse response = req.GetResponse();
                    Stream stream = response.GetResponseStream();
                    (photolist[i] as Img).Picture = Image.FromStream(stream);
                    stream.Close();
                    (photolist[i] as Img).Description = "Полный размер";
                }
                catch (Exception)
                {
                    (photolist[i] as Img).Description = "Ошибка во время загрузки";
                } 
            }
        }

        public void LoadImageFromURL(int index)
        {
                try
                {
                    WebRequest req = WebRequest.Create((photolist[index] as Img).Url);
                    WebResponse response = req.GetResponse();
                    Stream stream = response.GetResponseStream();
                    (photolist[index] as Img).Picture = Image.FromStream(stream);
                    stream.Close();
                    (photolist[index] as Img).Description = "Полный размер";
                }
                catch (Exception)
                {
                    (photolist[index] as Img).Description = "Ошибка во время загрузки";
                }           
        }
    }
}