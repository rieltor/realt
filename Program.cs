﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace realty
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        static public Agent user;
        static public editform objectedit;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}