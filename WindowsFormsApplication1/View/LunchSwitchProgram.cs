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
                textBoxDescriptionMyAccount.Text = member.Description;
                comboBoxFoodCategoryMyAccount.Text = "Vegetarian";
                textBoxAverageRatingMyAccount.Text = controller.FindMemberRating(member).ToString();


                dataGridViewLunchBoxesMyAccount.DataSource = controller.FindMembersLunchboxes(member);
                formatLunchBoxesMyAccount();
                dataGridViewMyMeetUpsMyAccount.DataSource = controller.FindMembersMeetUps(member);
                formatMeetUpsMyAccount();
            }
            catch (Exception ex)
            {
                handleException.HandleExceptions(ex);
            }
        }

       private void FillFindPage() {

            try { 
                //Fill foodCategories
                this.comboBoxFoodPreferencesFindPage.Items.Clear();
                string[] foodCategories = new string[6] { "ALL", "Vegetarian", "Chicken", "Beef", "Pork", "Fish" };
                foreach (var a in foodCategories)
                {
                    this.comboBoxFoodPreferencesFindPage.Items.Add(a);
                }
                this.comboBoxFoodPreferencesFindPage.SelectedIndex = 0;

                //Fill Cities
                this.comboBoxCityFindPage.Items.Clear();
                this.comboBoxCityFindPage.Items.Add("ALL");
                foreach (var a in controller.FindAllLunchboxesCitys())
                {
                    this.comboBoxCityFindPage.Items.Add(a);
                }
                this.comboBoxCityFindPage.SelectedIndex = 0;

                //Fill ratings
                this.comboBoxRateFriendFindpage.Items.Clear();
                int[] ratings = new int[5] { 1, 2, 3, 4, 5 };
                foreach (var a in ratings)
                {
                    this.comboBoxRateFriendFindpage.Items.Add(a);
                }

                //Fill friends
                this.comboBoxUsernameFindPage.Items.Clear();
                foreach (var a in controller.FindAllMembers())
                {
                    if (!a.MemberId.Equals(member.MemberId))
                    {
                        this.comboBoxUsernameFindPage.Items.Add(a.MemberId);
                    }
                }

                //Change font of datagridviews
                this.dataGridViewSearchLunchboxesFindPage.DefaultCellStyle.Font = new Font("Arial", 10);
                this.dataGridViewFriendLunchboxesFindPage.DefaultCellStyle.Font = new Font("Arial", 10);
                this.dataGridViewMyLunchboxesFindPage.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10);

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
            string description = textBoxDescriptionMyAccount.Text;

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
            if (string.IsNullOrWhiteSpace(textBoxNameAddALunchBoxMyAccount.Text) || string.IsNullOrWhiteSpace(textBoxQuantityAddALunchBoxMyAccount.Text) || string.IsNullOrWhiteSpace(textBoxContentAddALunchBox.Text) || string.IsNullOrWhiteSpace(textBoxQuantityAddALunchBoxMyAccount.Text))
            {
                labelAddALunchboxMessage.Text = "Please fill in all fields";
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

            string name =  textBoxNameAddALunchBoxMyAccount.Text;
            string quantityString = textBoxQuantityAddALunchBoxMyAccount.Text;
            string content = textBoxContentAddALunchBox.Text;
            string foodCategory = comboBoxFoodCategoryMyAccount.Text;
             
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
            textBoxNameAddALunchBoxMyAccount.Text = "";
            textBoxQuantityAddALunchBoxMyAccount.Text = "";
            textBoxContentAddALunchBox.Text = "";
            comboBoxFoodCategoryMyAccount.Text = "";
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
            string foodCategory = comboBoxFoodPreferencesFindPage.SelectedItem.ToString();
            string city = comboBoxCityFindPage.SelectedItem.ToString();

            this.dataGridViewMyLunchboxesFindPage.DataSource = controller.FindMembersLunchboxes(member);
            this.dataGridViewFriendLunchboxesFindPage.DataSource = null;

            if (foodCategory == "ALL" && city == "ALL")
            {
                this.dataGridViewSearchLunchboxesFindPage.DataSource = controller.FindAllLunchboxes(member);

            }
            else if (foodCategory == "ALL" && city != null)
            {
                this.dataGridViewSearchLunchboxesFindPage.DataSource = controller.FindLunchboxByCity(city, member);
            }
            else if (city == "ALL" && foodCategory != null)
            {
                this.dataGridViewSearchLunchboxesFindPage.DataSource = controller.FindLunchboxByFoodCategory(foodCategory, member);
            }
            else
            {
                this.dataGridViewSearchLunchboxesFindPage.DataSource = controller.FindLunchboxByCityAndCategory(city, foodCategory, member);
            }
        }

        private void buttonSearchForAFriendFindpage_Click(object sender, EventArgs e)
        {
            string friendId = comboBoxUsernameFindPage.SelectedItem.ToString();
            Member friend = controller.FindMember(friendId);
            textBoxUsernameFindPage.Text = friend.MemberId;
            textBoxFullNameFindPage.Text = friend.FullName;
            textBoxMobileFindPage.Text = friend.MobileNr;
            textBoxEmailFindPage.Text = friend.Email;
            textBoxDescriptionFindPage.Text = friend.Description;

            this.dataGridViewFriendLunchboxesFindPage.DataSource = controller.FindMembersLunchboxes(friend);
            this.dataGridViewMyLunchboxesFindPage.DataSource = controller.FindMembersLunchboxes(member);
            this.dataGridViewSearchLunchboxesFindPage.DataSource = null;
        }

        private void buttonRateFindpage_Click(object sender, EventArgs e)
        {         
            decimal friendGrade = Convert.ToDecimal(comboBoxRateFriendFindpage.SelectedItem.ToString());
            Member friend = controller.FindMember(textBoxUsernameFindPage.Text);
            Rating rating = new Rating(friendGrade, friend);
            controller.AddRating(rating);
        }

        private void buttonMakeaswitchFindpage_Click(object sender, EventArgs e)
        {

            //int index = dataGridViewSearchLunchboxesFindPage.SelectedRows[0].Index;// get the Row Index


            DataGridViewRow mySelectedRow = dataGridViewMyLunchboxesFindPage.SelectedRows[0];
            int myLunchboxId = Int32.Parse(mySelectedRow.Cells[0].Value.ToString());
            LunchBox myLb = controller.FindLunchbox(myLunchboxId);
            int myQuantity = myLb.Quantity - 1;
            controller.UpdateLunchbox(myLb.LunchBoxId, myQuantity);

            if (dataGridViewSearchLunchboxesFindPage.SelectedCells != null && dataGridViewFriendLunchboxesFindPage.DataSource == null)
            {
                DataGridViewRow selectedRow = dataGridViewSearchLunchboxesFindPage.SelectedRows[0];
                int lunchboxId = Int32.Parse(selectedRow.Cells[0].Value.ToString());
                LunchBox lb = controller.FindLunchbox(lunchboxId);
                int quantity = lb.Quantity - 1;
                controller.UpdateLunchbox(lb.LunchBoxId, quantity);
                string information = (member.MemberId + "'s " + myLb.Name + " for " + lb.Member.MemberId + "'s " + lb.Name);
                //Create a new meetup
                labelMakeASwitchMessageFindPage.Text = "The switch is now made";
            }
            else
            {
                labelMakeASwitchMessageFindPage.Text = "Please make a selection first";
            }

            if (dataGridViewFriendLunchboxesFindPage.SelectedCells != null && dataGridViewSearchLunchboxesFindPage.DataSource == null)
            {
                DataGridViewRow friendSelectedRow = dataGridViewSearchLunchboxesFindPage.SelectedRows[0];
                int friendLunchboxId = Int32.Parse(friendSelectedRow.Cells[0].Value.ToString());
                LunchBox friendLb = controller.FindLunchbox(friendLunchboxId);
                int friendQuantity = friendLb.Quantity - 1;
                controller.UpdateLunchbox(friendLb.LunchBoxId, friendQuantity);
                string information = (member.MemberId + "'s " + myLb.Name + " for " + friendLb.Member.MemberId + "'s " + friendLb.Name);
                //Create a new meetup
                labelMakeASwitchMessageFindPage.Text = "The switch is now made";
            }
            else
            {
                labelMakeASwitchMessageFindPage.Text = "Please make a selection first";
            }

            //if(listBoxLunchboxesFindPage.SelectedItem != null && listBoxFriendsLunchboxesFindPage.Items.Count == 0)
            //{
            //    string extractedFriendLB = new string(listBoxMyLunchboxesFindPage.SelectedItem.ToString().Trim().TakeWhile(char.IsDigit).ToArray());
            //    Lunchbox friendLunchbox = controller.FindLunchbox(Convert.ToInt64(extractedFriendLB));
            //    int quantityFriendLB = friendLunchbox.Quantity - 1;
            //    controller.UpdateLunchbox(friendLunchbox.LunchBoxId, quantityFriendLB);
            //    string information = (u.UserId + "'s " + myLB.Name + " for " + friendLunchbox.User.UserId + "'s " + friendLunchbox.Name);
            //    CreateNewMeetUp(u.UserId, friendLunchbox.User.UserId, information);
            //    labelMakeASwitchMessageFindPage.Text = "The switch is now made";
            //}

            //if (listBoxFriendsLunchboxesFindPage.SelectedItem != null && listBoxLunchboxesFindPage.Items.Count == 0)
            //{
            //    string extractedFriendLB = new string(listBoxFriendsLunchboxesFindPage.SelectedItem.ToString().Trim().TakeWhile(char.IsDigit).ToArray());
            //    Lunchbox friendLunchbox = controller.FindLunchbox(Convert.ToInt64(extractedFriendLB));
            //    int quantityFriendLB = friendLunchbox.Quantity - 1;
            //    controller.UpdateLunchbox(friendLunchbox.LunchBoxId, quantityFriendLB);
            //    string information = (u.UserId + "'s " + myLB.Name + " for " + friendLunchbox.User.UserId + "'s " + friendLunchbox.Name);
            //    CreateNewMeetUp(u.UserId, friendLunchbox.User.UserId, information);
            //    labelMakeASwitchMessageFindPage.Text = "The switch is now made";
            //}
        }

        //    CreateNewMeetUp(member, friendLunchbox.Member.memberId, information){
        //      MeetUp meetUp = new MeetUp(member, friend, information);
        //      }

    }
}
