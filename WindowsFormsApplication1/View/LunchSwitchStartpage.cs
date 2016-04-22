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

        private void buttonRegisterStartpage_Click(object sender, EventArgs e)
        {
            labelRegisterMessageStartpage.Text = "";

            string registerUserid = textBoxUsernameStartpage.Text;
            string registerPassword = textBoxPasswordStartpage.Text;
            string registerFullName = textBoxNameStartpage.Text;
            string registerMobile = textBoxPhoneStartpage.Text;
            string registerEmail = textBoxEmailStartpage.Text;
            string registerCity = textBoxCityStartpage.Text;
            string registerDescription = textBoxDescripstionStartpage.Text;
            string registerUserId = textBoxUsernameAlreadyAMemberStartpage.Text;


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
                    labelRegisterMessageStartpage.Text = handleException.HandleExceptions(ex);
                }
            }

        }

        private void buttonLoginStartpage_Click(object sender, EventArgs e)
        {
            labelLogInMessageStartpage.Text = "";
            string logInMemberId = textBoxUsernameAlreadyAMemberStartpage.Text;
            string logInPassword = textBoxPasswordAlreadyAMemberStartpage.Text;

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
                    labelLogInMessageStartpage.Text = handleException.HandleExceptions(ex);

                }
            }

        }

        private bool CheckAllLogInTextBoxes()
        {
            if (string.IsNullOrWhiteSpace(textBoxUsernameAlreadyAMemberStartpage.Text) || string.IsNullOrWhiteSpace(textBoxPasswordAlreadyAMemberStartpage.Text))
            {
                labelLogInMessageStartpage.Text = "Please fill in all fields";
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

                labelRegisterMessageStartpage.Text = "Please fill in all fields";
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
