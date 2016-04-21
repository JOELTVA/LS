using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LS.Controller;
using LS.Exceptions;
using LS.Model;

namespace LS.View
{
    public partial class LunchSwitchStartpage : Form

    {
        LunchSwitchController controller = new LunchSwitchController();
        HandleException handleException = new HandleException();

        public LunchSwitchStartpage()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void labelPassword_Click(object sender, EventArgs e)
        {

        }

        private void labelAlreadyAMemeber_Click(object sender, EventArgs e)
        {

        }

        private void textBoxUsernameStartpage_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonRegisterStartpage_Click(object sender, EventArgs e)
        {
            labelRegisterMessage.Text = "";

            string registerUserid = textBoxUsernameStartpage.Text;
            string registerPassword = textBoxPasswordStartpage.Text;
            string registerFullName = textBoxNameStartpage.Text;
            string registerMobile = textBoxPhoneStartpage.Text;
            string registerEmail = textBoxEmailStartpage.Text;
            string registerCity = textBoxCityStartpage.Text;
            string registerDescription = textBoxDescripstionStartpage.Text;


            string registerUserId = textBoxUsernameStartpageAlreadyAMember.Text;


            if (CheckAllRegisterTextBoxes())
            {
                try
                {
                    Member m = new Member(registerUserid, registerPassword, registerFullName, registerMobile,
                        registerEmail, registerCity, registerDescription);
                    controller.AddMember(m);
                    LunchSwitchProgram lunchSwitchProgram = new LunchSwitchProgram(m.MemberId);
                    this.Visible = false;
                    lunchSwitchProgram.Visible = true;

                }
                catch (Exception ex)
                {
                    labelRegisterMessage.Text = handleException.HandleExceptions(ex);
                }


            }

        }

        private void groupBoxRegisterStartpage_Enter(object sender, EventArgs e)
        {

        }

        private void buttonLoginStartpage_Click(object sender, EventArgs e)
        {
            labelLogInMessage.Text = "";
            string logInMemberId = textBoxUsernameStartpageAlreadyAMember.Text;
            string logInPassword = textBoxPasswordStartpageAlreadyAMember.Text;

            if (CheckAllLogInTextBoxes())
            {
                try
                {
                    Member m = controller.FindMember(logInMemberId);
                    if (m.Password.Equals(logInPassword))
                    {
                        LunchSwitchProgram lunchSwitchProgram = new LunchSwitchProgram(m.MemberId);
                        this.Visible = false;
                        lunchSwitchProgram.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    labelLogInMessage.Text = handleException.HandleExceptions(ex);

                }
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxUsernameStartpageAlreadyAMember_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxPasswordStartpageAldreadyAMember_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxNameStartpage_TextChanged(object sender, EventArgs e)
        {

        }
        private void ClearAllTextBoxesStartPage()
        {

        }
        private bool CheckAllLogInTextBoxes()
        {
            if (string.IsNullOrWhiteSpace(textBoxUsernameStartpageAlreadyAMember.Text) || string.IsNullOrWhiteSpace(textBoxPasswordStartpageAlreadyAMember.Text))
            {
                labelLogInMessage.Text = "Please fill in all fields";
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool CheckAllRegisterTextBoxes()
        {


            if (string.IsNullOrWhiteSpace(textBoxUsernameStartpage.Text) || string.IsNullOrWhiteSpace(textBoxPasswordStartpage.Text)
                || string.IsNullOrWhiteSpace(textBoxEmailStartpage.Text) || string.IsNullOrWhiteSpace(textBoxCityStartpage.Text)
                || string.IsNullOrWhiteSpace(textBoxDescripstionStartpage.Text) || string.IsNullOrWhiteSpace(textBoxPhoneStartpage.Text)
                || string.IsNullOrWhiteSpace(textBoxNameStartpage.Text))
            {

                labelRegisterMessage.Text = "Please fill in all fields";
                return false;
            }
            else
            {
                return true;
            }


        }


    }
}
