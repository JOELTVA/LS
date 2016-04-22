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

        private Member member = new Member();
        private string lunchBoxIdMyAccount;
        private string meetUpIdMyAccount;
        private string lunchBoxIdSearch;
        private string lunchBoxIdFriend;
        private string lunchBoxIdMy;

        LunchSwitchController controller = new LunchSwitchController();
        HandleException handleException = new HandleException();


        public LunchSwitchProgram(string memberId)
        {
            this.member = controller.FindMember(memberId);
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            FillMyAccountPage();
            FillFindPage();
        }

        //REMOVE??



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
                controller.DeleteLunchBox(Convert.ToInt64(lunchBoxIdMyAccount));
                dataGridViewLunchBoxesMyAccount.DataSource = controller.FindMembersLunchBoxes(member);

            }
            catch (Exception ex)
            {
                handleException.HandleExceptions(ex);
            }

        }

        //Fills the account iwht the logged in Member's details
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

                dataGridViewLunchBoxesMyAccount.DataSource = controller.FindMembersLunchBoxes(member);
                FormatLunchBoxesMyAccount();
                dataGridViewMyMeetUpsMyAccount.DataSource = controller.FindMembersMeetUps(member);
                FormatMeetUpsMyAccount();
            }
            catch (Exception ex)
            {
                handleException.HandleExceptions(ex);
            }
        }

        private void FillFindPage()
        {

            try
            {
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
                foreach (var a in controller.FindAllLunchBoxesCitys())
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

                //Fill friends searchbox
                List<String> friends = new List<String>();
                foreach (var f in controller.FindAllMembersExceptUser(member))
                {
                    friends.Add(f.MemberId);
                }
                var friendsCollection = new AutoCompleteStringCollection();
                friendsCollection.AddRange(friends.ToArray());

                this.textBoxSearchFriendFindPage.AutoCompleteCustomSource = friendsCollection;

            }
            catch (Exception ex)
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

            }
            catch (Exception ex)
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

        private void FormatLunchBoxesMyAccount()
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

            //Formats the lunchboxdatagridview on my account
            dataGridViewLunchBoxesMyAccount.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridViewLunchBoxesMyAccount.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 11);
            dataGridViewMyMeetUpsMyAccount.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridViewMyMeetUpsMyAccount.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 11);
        }

        private void FormatMeetUpsMyAccount()
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

            //Formats the meetupsdatagridview on my account
            dataGridViewMyMeetUpsMyAccount.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridViewMyMeetUpsMyAccount.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 11);
        }

        private void FormatSearchLunchboxesFindPage()
        {
            for (int i = 0; i < dataGridViewSearchLunchboxesFindPage.Columns.Count; i++)
            {

                string str = dataGridViewSearchLunchboxesFindPage.Columns[i].HeaderText;
                if (str == "LunchBoxId")
                {
                    dataGridViewSearchLunchboxesFindPage.Columns[i].HeaderText = "Lunchbox Id";
                }
                else if (str == "FoodCategory")
                {
                    dataGridViewSearchLunchboxesFindPage.Columns[i].HeaderText = "Food Category";
                }

            }
            dataGridViewSearchLunchboxesFindPage.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewSearchLunchboxesFindPage.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewSearchLunchboxesFindPage.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewSearchLunchboxesFindPage.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewSearchLunchboxesFindPage.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //Change font of datagridviews
            this.dataGridViewSearchLunchboxesFindPage.DefaultCellStyle.Font = new Font("Arial", 9);
            this.dataGridViewSearchLunchboxesFindPage.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9);
        }

        private void FormatFriendLunchboxesFindPage()
        {
            for (int i = 0; i < dataGridViewFriendLunchboxesFindPage.Columns.Count; i++)
            {

                string str = dataGridViewFriendLunchboxesFindPage.Columns[i].HeaderText;
                if (str == "LunchBoxId")
                {
                    dataGridViewFriendLunchboxesFindPage.Columns[i].HeaderText = "Lunchbox Id";
                }
                else if (str == "FoodCategory")
                {
                    dataGridViewFriendLunchboxesFindPage.Columns[i].HeaderText = "Food Category";
                }

            }
            dataGridViewFriendLunchboxesFindPage.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewFriendLunchboxesFindPage.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewFriendLunchboxesFindPage.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewFriendLunchboxesFindPage.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewFriendLunchboxesFindPage.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //Change font of datagridviews
            this.dataGridViewFriendLunchboxesFindPage.DefaultCellStyle.Font = new Font("Arial", 9);
            this.dataGridViewFriendLunchboxesFindPage.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9);
        }

        private void FormatMyLunchboxesFindPage()
        {
            for (int i = 0; i < dataGridViewMyLunchboxesFindPage.Columns.Count; i++)
            {

                string str = dataGridViewMyLunchboxesFindPage.Columns[i].HeaderText;
                if (str == "LunchBoxId")
                {
                    dataGridViewMyLunchboxesFindPage.Columns[i].HeaderText = "Lunchbox Id";
                }
                else if (str == "FoodCategory")
                {
                    dataGridViewMyLunchboxesFindPage.Columns[i].HeaderText = "Food Category";
                }

            }
            dataGridViewMyLunchboxesFindPage.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewMyLunchboxesFindPage.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewMyLunchboxesFindPage.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewMyLunchboxesFindPage.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewMyLunchboxesFindPage.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;


            //Change font of datagridviews
            this.dataGridViewMyLunchboxesFindPage.DefaultCellStyle.Font = new Font("Arial", 9);
            this.dataGridViewMyLunchboxesFindPage.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9);
        }


        private void buttonAddLunchboxMyaccount_Click(object sender, EventArgs e)
        {

            string name = textBoxNameAddALunchBoxMyAccount.Text;
            string quantityString = textBoxQuantityAddALunchBoxMyAccount.Text;
            string content = textBoxContentAddALunchBox.Text;
            string foodCategory = comboBoxFoodCategoryMyAccount.Text;

            //FIXA QUANTITY


            if (CheckAllAddALunchTextBoxes())
            {
                try
                {
                    LunchBox l = new LunchBox(name, content, foodCategory, 2, member);
                    controller.AddLunchBox(l);
                    dataGridViewLunchBoxesMyAccount.DataSource = controller.FindMembersLunchBoxes(member);
                    FormatLunchBoxesMyAccount();
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

            }
            catch (Exception ex)
            {
                handleException.HandleExceptions(ex);
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
                handleException.HandleExceptions(ex);
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

            this.dataGridViewMyLunchboxesFindPage.DataSource = controller.FindMembersLunchBoxes(member);
            this.dataGridViewFriendLunchboxesFindPage.DataSource = null;

            if (foodCategory == "ALL" && city == "ALL")
            {
                this.dataGridViewSearchLunchboxesFindPage.DataSource = controller.FindAllLunchBoxes(member);

            }
            else if (foodCategory == "ALL" && city != null)
            {
                this.dataGridViewSearchLunchboxesFindPage.DataSource = controller.FindLunchBoxByCity(city, member);
            }
            else if (city == "ALL" && foodCategory != null)
            {
                this.dataGridViewSearchLunchboxesFindPage.DataSource = controller.FindLunchBoxByFoodCategory(foodCategory, member);
            }
            else
            {
                this.dataGridViewSearchLunchboxesFindPage.DataSource = controller.FindLunchBoxByCityAndCategory(city, foodCategory, member);
            }

            //Formats the search and members datagridviews with font, size and general design
            FormatSearchLunchboxesFindPage();
            FormatMyLunchboxesFindPage();

        }

        private void buttonSearchForAFriendFindpage_Click(object sender, EventArgs e)
        {
            //Finds friend and adds his/her details to the profile
            string friendId = comboBoxUsernameFindPage.SelectedItem.ToString();
            Member friend = controller.FindMember(friendId);
            textBoxUsernameFindPage.Text = friend.MemberId;
            textBoxFullNameFindPage.Text = friend.FullName;
            textBoxMobileFindPage.Text = friend.MobileNr;
            textBoxEmailFindPage.Text = friend.Email;
            textBoxDescriptionFindPage.Text = friend.Description;

            //Adds the friends lunchboxes to the lunchboxgridview and removes them from the searchgridview
            this.dataGridViewFriendLunchboxesFindPage.DataSource = controller.FindMembersLunchBoxes(friend);
            this.dataGridViewMyLunchboxesFindPage.DataSource = controller.FindMembersLunchBoxes(member);
            this.dataGridViewSearchLunchboxesFindPage.DataSource = null;

            //Formats the friendsdatagridviews with font, size and general design
            FormatFriendLunchboxesFindPage();
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
            //Finds the selected lunchbox of member and removes one from the quantity
            int myLunchboxId = Int32.Parse(lunchBoxIdMy);
            LunchBox myLb = controller.FindLunchBox(myLunchboxId);
            int myQuantity = myLb.Quantity - 1;
            controller.UpdateLunchBox(myLb.LunchBoxId, myQuantity);

            //if selected from searchdatagridview, finds the searched lunchbox, removes one from
            //quantity and creates a new meetup
            if (myLb != null && dataGridViewFriendLunchboxesFindPage.DataSource == null)
            {

                int lunchboxId = Int32.Parse(lunchBoxIdSearch);
                LunchBox lb = controller.FindLunchBox(lunchboxId);
                int quantity = lb.Quantity - 1;
                controller.UpdateLunchBox(lb.LunchBoxId, quantity);
                string information = (member.MemberId + "'s " + myLb.Name + " for " + lb.Member.MemberId + "'s " + lb.Name);

                CreateMeetUp(lb, information);

                labelMakeASwitchMessageFindPage.Text = "The switch is now made";
            }

            //if selected from frienddatagridview, finds the friend's lunchbox, removes one from
            //quantity and creates a new meetup
            else if (dataGridViewFriendLunchboxesFindPage.SelectedCells != null && dataGridViewSearchLunchboxesFindPage.DataSource == null)
            {

                int friendLunchboxId = Int32.Parse(lunchBoxIdFriend);
                LunchBox friendLb = controller.FindLunchBox(friendLunchboxId);
                int friendQuantity = friendLb.Quantity - 1;
                controller.UpdateLunchBox(friendLb.LunchBoxId, friendQuantity);
                string information = (member.MemberId + "'s " + myLb.Name + " for " + friendLb.Member.MemberId + "'s " + friendLb.Name);

                CreateMeetUp(friendLb, information);

                labelMakeASwitchMessageFindPage.Text = "The switch is now made";

            }
            else
            {
                labelMakeASwitchMessageFindPage.Text = "Please make a selection first";
            }
        }

        private void dataGridViewSearchLunchboxesFindPage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                DataGridViewRow selectedRow = dataGridViewSearchLunchboxesFindPage.Rows[index];
                lunchBoxIdSearch = selectedRow.Cells[0].Value.ToString();

            }
            catch (Exception ex)
            {
                handleException.HandleExceptions(ex);
            }
        }

        private void dataGridViewMyLunchboxesFindPage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                DataGridViewRow selectedRow = dataGridViewMyLunchboxesFindPage.Rows[index];
                lunchBoxIdMy = selectedRow.Cells[0].Value.ToString();

            }
            catch (Exception ex)
            {
                handleException.HandleExceptions(ex);
            }

        }

        private void dataGridViewFriendLunchboxesFindPage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                DataGridViewRow selectedRow = dataGridViewFriendLunchboxesFindPage.Rows[index];
                lunchBoxIdFriend = selectedRow.Cells[0].Value.ToString();

            }
            catch (Exception ex)
            {
                handleException.HandleExceptions(ex);
            }
        }

        //Creates a new meetup once the "make a switch" button is pressed
        private void CreateMeetUp(LunchBox friendLb, string information)
        {
            MeetUp mU = new MeetUp(information);
            controller.AddMeetup(mU);

            MeetUp_Member mm = new MeetUp_Member(member, mU);
            MeetUp_Member mm1 = new MeetUp_Member(friendLb.Member, mU);
            controller.AddMeetUp_Member(mm);
            controller.AddMeetUp_Member(mm1);
        }

        private void buttonPlusQuanitMyAccount_Click(object sender, EventArgs e)
        {
            LunchBox friendLb = controller.FindLunchBox(Convert.ToInt64(lunchBoxIdMyAccount));
            int quantity = friendLb.Quantity + 1;
            controller.UpdateLunchBox(friendLb.LunchBoxId, quantity);
            dataGridViewLunchBoxesMyAccount.DataSource = controller.FindMembersLunchBoxes(member);
            //dataGridViewLunchBoxesMyAccount.Update();
            //dataGridViewLunchBoxesMyAccount.Refresh();
        }

        private void buttonMinusQuanityMyAccount_Click(object sender, EventArgs e)
        {
            LunchBox friendLb = controller.FindLunchBox(Convert.ToInt64(lunchBoxIdMyAccount));
            int quantity = friendLb.Quantity - 1;
            controller.UpdateLunchBox(friendLb.LunchBoxId, quantity);
            dataGridViewLunchBoxesMyAccount.DataSource = controller.FindMembersLunchBoxes(member);
            //dataGridViewLunchBoxesMyAccount.Update();
            //dataGridViewLunchBoxesMyAccount.Refresh();
        }
    }
}
