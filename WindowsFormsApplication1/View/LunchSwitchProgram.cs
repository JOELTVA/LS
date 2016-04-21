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
    public partial class LunchSwitchProgram : Form
    {

        public Member member = new Member();
        public String lunchBoxIdMyAccount;
        public String meetUpIdMyAccount;
        LunchSwitchController controller = new LunchSwitchController();
        HandleException handleException = new HandleException();


        public LunchSwitchProgram(string memberId)
        {
            this.member = controller.FindMember(memberId);
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            FillMyAccountPage();

            dataGridViewLunchBoxesMyAccount.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridViewLunchBoxesMyAccount.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 11);
            dataGridViewMyMeetUpsMyAccount.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridViewMyMeetUpsMyAccount.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 11);
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }


        private void buttonLogOutMyaccount_Click(object sender, EventArgs e)
        {

            DialogResult answer = MessageBox.Show("Are you sure that you want to log out and exit Lunch-switch?",
            "Important Question", MessageBoxButtons.YesNo);
            if (answer == DialogResult.Yes)
            {
                this.Visible = false;
                LunchSwitchStartpage startPage = new LunchSwitchStartpage();
                startPage.WindowState = FormWindowState.Maximized;
                startPage.Visible = true;
            }
            else
            {
                //do nothing, close the message box
            }       
        }

        private void buttonRemoveLunchboxMyaccont_Click(object sender, EventArgs e)
        {

           try
            {
                controller.DeleteLunchbox(Convert.ToInt64(lunchBoxIdMyAccount));
                dataGridViewLunchBoxesMyAccount.DataSource = controller.FindAllLunchboxes(member);

            }
            catch (Exception ex)
            {
                handleException.HandleExceptions(ex);
            }

        }

        private void FillMyAccountPage()
        {
            try
            {
               
                textBoxUsernameMyAccount.Text = member.MemberId;
                textBoxNameMyAccount.Text = member.FullName;
                textBoxPhoneMyAccount.Text = member.MobileNr;
                textBoxEmailMyAccount.Text = member.Email;
                textBoxCityMyAccount.Text = member.City;
                textBoxDescriptionMyaccount.Text = member.Description;
                comboBoxFoodCategoryMyaccount.Text = "Vegetarian";
                textBoxAverageRatingMyAccount.Text = controller.FindMemberRating(member).ToString();


                dataGridViewLunchBoxesMyAccount.DataSource = controller.FindMembersLunchboxes(member);
                formatLunchBoxesMyAccount();
                dataGridViewMyMeetUpsMyAccount.DataSource = controller.FindMembersMeetUps(member);
                formatMeetUpsMyAccount();





            } catch (Exception ex)
            {
               handleException.HandleExceptions(ex);
            }
            
            
        }



        private void buttonSaveAndUpdateMyaccount_Click(object sender, EventArgs e)
        {


            string fullName = textBoxNameMyAccount.Text;
            string city = textBoxCityMyAccount.Text;
            string email = textBoxEmailMyAccount.Text;
            string mobileNbr = textBoxPhoneMyAccount.Text;
            string description = textBoxDescriptionMyaccount.Text;

            try
            {
                controller.UpdateMember(member.MemberId, fullName, city, email, mobileNbr, description);
                labelUpdateAccountMyAccount.Text = "Account updated!";

            } catch (Exception ex)
            {
                labelUpdateAccountMyAccount.Text = handleException.HandleExceptions(ex);
            }

        }

        private bool CheckAllAddALunchTextBoxes()
        {
            if (string.IsNullOrWhiteSpace(textBoxNameAddALunchBox.Text) || string.IsNullOrWhiteSpace(textBoxQuantityAddALunchBox.Text) || string.IsNullOrWhiteSpace(textBoxContentAddALunchBox.Text) || string.IsNullOrWhiteSpace(textBoxQuantityAddALunchBox.Text))
            {
                labelAddALunchBoxMessage.Text = "Please fill in all fields";
                return false;
            }
            else
            {
                return true;
            }
        }

        private void formatLunchBoxesMyAccount()
        {

            for (int i = 0; i < dataGridViewLunchBoxesMyAccount.Columns.Count; i++)
            {
                
                string str = dataGridViewLunchBoxesMyAccount.Columns[i].HeaderText;
                if (str == "FoodCategory")
                {
                    dataGridViewLunchBoxesMyAccount.Columns[i].HeaderText = "Food Category";
                }
                else if (str == "LunchBoxId")
                {
                    dataGridViewLunchBoxesMyAccount.Columns[i].HeaderText = "Id";
                }
            }

            dataGridViewLunchBoxesMyAccount.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewLunchBoxesMyAccount.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewLunchBoxesMyAccount.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewLunchBoxesMyAccount.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewLunchBoxesMyAccount.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        
        }

        private void formatMeetUpsMyAccount()
        {

            for (int i = 0; i < dataGridViewMyMeetUpsMyAccount.Columns.Count; i++)
            {

                string str = dataGridViewMyMeetUpsMyAccount.Columns[i].HeaderText;
                if (str == "MeetUpId")
                {
                    dataGridViewMyMeetUpsMyAccount.Columns[i].HeaderText = "Id";
                }
               
            }

            dataGridViewMyMeetUpsMyAccount.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewMyMeetUpsMyAccount.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
           
        }


        private void buttonAddLunchboxMyaccount_Click(object sender, EventArgs e)
        {

            string name =  textBoxNameAddALunchBox.Text;
            string quantityString = textBoxQuantityAddALunchBox.Text;
            string content = textBoxContentAddALunchBox.Text;
            string foodCategory = comboBoxFoodCategoryMyaccount.Text;
             
            //FIXA QUANTITY

     
            if (CheckAllAddALunchTextBoxes())
            {
               try
                {
                    LunchBox l = new LunchBox(name, content, foodCategory, 2, member);
                    controller.AddLunchbox(l);
                    dataGridViewLunchBoxesMyAccount.DataSource = controller.FindAllLunchboxes(member);
                    formatLunchBoxesMyAccount();
                    ClearAllMyAccountLunchBox();

                }
                catch (Exception ex)
                {
                    labelUpdateAccountMyAccount.Text = handleException.HandleExceptions(ex);
                }
               
            }
        }

        private void ClearAllMyAccountLunchBox()
        {
            textBoxNameAddALunchBox.Text = "";
            textBoxQuantityAddALunchBox.Text = "";
            textBoxContentAddALunchBox.Text = "";
            comboBoxFoodCategoryMyaccount.Text = "";
        }

 
        private void dataGridLunchBoxesMyAccount_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                int index = e.RowIndex;
                DataGridViewRow selectedRow = dataGridViewLunchBoxesMyAccount.Rows[index];
                lunchBoxIdMyAccount = selectedRow.Cells[0].Value.ToString();

            } catch (Exception ex)
            {
                //ska inte throwas
            }

        }

        private void dataGridViewMyMeetUpsMyAccount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                DataGridViewRow selectedRow = dataGridViewMyMeetUpsMyAccount.Rows[index];
                meetUpIdMyAccount = selectedRow.Cells[0].Value.ToString();

            }
            catch (Exception ex)
            {
               //ska inte slängas
            }

        }

        private void buttonRemoveMeetupMyaccount_Click(object sender, EventArgs e)
        {
            try
            {
                controller.DeleteMeetup(Convert.ToInt64(meetUpIdMyAccount));
                dataGridViewMyMeetUpsMyAccount.DataSource = controller.FindMembersMeetUps(member);

            }
            catch (Exception ex)
            {
                handleException.HandleExceptions(ex);
            }

        }

        private void buttonSearchForLunchFindpage_Click(object sender, EventArgs e)
        {

        }
    }
}
