using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace realty
{
    public class Comment
    {
        private string _author;
        private string _text;
        private DateTime _date;
        private string _email;
        private long _id;
        public bool IsNew;

        public string Author
        { 
            get {return _author; }
            set { _author = value; }
        }
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public Comment()
        {
            _author = "";
            _date = DateTime.Now;
            _text = "";
            _email = "";
            _id = 0;
            IsNew = true;
        }

        
    }
    public class Comments
    {
        public ArrayList CommentsList;
        public string Connect; 
        public Comments()
        {
            CommentsList = new ArrayList();
            Connect = "server=tamarrra.net;user=user;database=RealtMultiBase;password=Ardonskaya209;charset=utf8";
        }

        public bool DeleteCommentFromDB (int index)
        {
            try
            {
                MySqlConnection con = new MySqlConnection(Connect);
                con.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE from rcomments WHERE (Rc_id = @Val1)", con);
                cmd.Parameters.AddWithValue("@Val1", index.ToString());
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public bool UpdateCommentInDb(int index, DateTime save)
        {
            try
            {
                MySqlConnection con = new MySqlConnection(Connect);
                con.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE rcomments SET Rc_comment = @Val1, Rc_data = @Val3 WHERE Rc_id = @Val2", con);
                cmd.Parameters.AddWithValue("@Val1", (CommentsList[index] as Comment).Text);
                cmd.Parameters.AddWithValue("@Val2", (CommentsList[index] as Comment).Id);
                cmd.Parameters.AddWithValue("@Val3", (CommentsList[index] as Comment).Date);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool InsertCommentInDb(int index, string esttype, long obj)
        {
            try
            {
                MySqlConnection con = new MySqlConnection(Connect);
                con.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO rcomments(Rc_agent, Rc_comment, Rc_data, Rc_object, Rc_estate_type) VALUES (@Val1, @Val2, @Val3, @Val4, @Val5)", con);
                cmd.Parameters.AddWithValue("@Val1", (CommentsList[index] as Comment).Author);
                cmd.Parameters.AddWithValue("@Val2", (CommentsList[index] as Comment).Text);
                cmd.Parameters.AddWithValue("@Val3", (CommentsList[index] as Comment).Date);
                cmd.Parameters.AddWithValue("@Val4", obj);
                cmd.Parameters.AddWithValue("@Val5", esttype);                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
                (CommentsList[index] as Comment).Id = Convert.ToInt32(cmd.LastInsertedId);
                con.Close();               
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool LoadCommentsFromDb(string esttype, long obj)
        {
            try
            {
                MySqlConnection con = new MySqlConnection(Connect);
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT Rc_id, Rc_agent, Rc_mail, Rc_data, Rc_comment from rcomments WHERE ((Rc_object = @Val1) AND (Rc_estate_type = @Val2) ORDER by Rc_data)", con);
                cmd.Parameters.AddWithValue("@Val1", obj);
                cmd.Parameters.AddWithValue("@Val2", esttype);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();
                DataTable result = new DataTable();
                result.Load(reader);
                for (int i = 0; i < result.Rows.Count - 1;i++ )
                {
                    Comment cmt = new Comment();
                    cmt.Id = Convert.ToInt64(result.Rows[i][0]);
                    cmt.Author = result.Rows[i][1].ToString();
                    cmt.Email = result.Rows[i][2].ToString();
                    cmt.Date = Convert.ToDateTime(result.Rows[i][3]);
                    cmt.Text = result.Rows[i][4].ToString();
                    cmt.IsNew = false;
                    CommentsList.Add(cmt);

                }
                    con.Close();
                return true;

            }
            catch
            {
                return false;
            }
        }
    }
}
