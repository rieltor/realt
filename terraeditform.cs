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
    public partial class terraeditform : Form
    {
        static public Photos dbvals;
        static public Comments dbcomm;
        DBWrap dbw;
        ClFunctions func;
        terrarecord record;
        public bool newrecord = false;
        
        public terraeditform()
        {
            InitializeComponent();
            dbvals = new Photos("");
            //dbvals.LoadImageFromURL(0);
            // Add check items to the control's dropdown.
            dbvals.LoadImagesFromURLs();
            listBox2.Items.Clear();
            listView1.Items.Clear();
            for (int i = 0; i < dbvals.photolist.Count; i++)
            { listBox2.Items.Add(dbvals.photolist[i]); }
            //listBox2.Items.AddRange(dbvals.photolist);
            listBox2.DisplayMember = "Name";
            listBox2.SelectedIndex = 1;
            pictureEdit1.Image = (dbvals.photolist[1] as Img).Picture;
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

        private void label43_Click(object sender, EventArgs e)
        {

        }
    }
}
