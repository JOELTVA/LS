using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using System.Text;
using LS.Model;
using LS.Controller;
using LS.DAL;
using LS.View;


namespace LS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            LunchSwitchController controller = new LunchSwitchController(); 
            
            //Hittar användare
            Member m = controller.FindMember("Joel");
            Member m1 = controller.FindMember("Amanda");
            
            //Hitta lådor
            LunchBox l = controller.FindLunchbox(72);
            LunchBox l1 = controller.FindLunchbox(67);

            //Gör meetUp
            string s = ("Joel traded " + l.Name + " for Amandas " + l1.Name);
            MeetUp mU = new MeetUp(s);
            controller.AddMeetup(mU);

            //Lägger in MeetUp_Members
            MeetUp_Member mm = new MeetUp_Member(m, mU);
            MeetUp_Member mm1 = new MeetUp_Member(m1, mU);
            controller.AddMeetUp_Member(mm);
            controller.AddMeetUp_Member(mm1);

            //Lägger till rating
            Rating r = new Rating(1, m);
            Rating r1 = new Rating(1, m);
            controller.AddRating(r);
            



            ////Tar bort deras lunchlådor. Här ska din minusquantitysak in.
            //controller.DeleteLunchbox(61);
            //controller.DeleteLunchbox(62);
           


           
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LunchSwitchStartpage());



        }
    }
}
