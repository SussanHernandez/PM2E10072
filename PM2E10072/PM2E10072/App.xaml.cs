using PM2E10072.Controllers;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E10072
{
    public partial class App : Application
    {
        static DBProc dbProc;

        public static DBProc Instancia
        {
            get
            {
                if (dbProc == null)
                {
                    String dbname = "E2.db3";
                    String dbpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    String dbfulp = Path.Combine(dbpath, dbname);
                    dbProc = new DBProc(dbfulp);
                }

                return dbProc;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
