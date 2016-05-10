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

        private LunchSwitchController controller = new LunchSwitchController();


        public LunchSwitchProgram(string memberId)
        {
            this.member = controller.FindMember(memberId);
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            //Fills the two pages with the default values and the details of the current (loged in) member
            FillFindPage();
            FillMyAccountPage();
        }

        //**FILL PAGE FUNCTIONS**//

        //Fills the default values of the find page
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

            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleExceptions(ex);
            }
        }

        //Fills the account wiht the logged in Member's details
        private void FillMyAccountPage()
        {
            try
            {
                textBoxUsernameMyAccount.Text = member.MemberId;
                textBoxNameMyAccount.Text = member.FullName;
                textBoxMobileMyAccount.Text = member.MobileNr;
                textBoxEmailMyAccount.Text = member.Email;
                textBoxCityMyAccount.Text = member.City;
                textBoxDescriptionMyAccount.Text = member.Description;
                comboBoxFoodCategoryMyAccount.Text = "Vegetarian";
                labelRatingMyAccount.Text = controller.FindMemberRating(member).ToString("##.0", System.Globalization.CultureInfo.InvariantCulture);

                dataGridViewLunchBoxesMyAccount.DataSource = controller.FindMembersLunchBoxes(member);
                dataGridViewMyMeetUpsMyAccount.DataSource = controller.FindMembersMeetUps(member);

                FormatLunchBoxesMyAccount();
                FormatMeetUpsMyAccount();

                DeselectRowsMyAccountPage();

            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleExceptions(ex);
            }
        }

        //**FIND PAGE**//

        //Finds the lunchboxes that correspond to the criteria and informs how many lunchboxes were found
        private void buttonSearchForLunchFindpage_Click(object sender, EventArgs e)
        {
            try
            {
                SearchLunchBoxFindPage();

                if (dataGridViewSearchLunchBoxesFindPage.RowCount > 1)
                {
                    toolStripStatusLabelLunchSwitch.Text = ("Found " + dataGridViewSearchLunchBoxesFindPage.RowCount.ToString() + " LunchBoxes");

                }
                else if (dataGridViewSearchLunchBoxesFindPage.RowCount == 1)
                {
                    toolStripStatusLabelLunchSwitch.Text = ("Found " + dataGridViewSearchLunchBoxesFindPage.RowCount.ToString() + " LunchBox");

                }
                else if (dataGridViewSearchLunchBoxesFindPage.RowCount < 1)
                {
                    toolStripStatusLabelLunchSwitch.Text = ("Didn't find any lunchboxes");
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabelLunchSwitch.Text = ExceptionHandler.HandleExceptions(ex);
            }

            DeselectRowsFindPage();
            FormatMyLunchBoxesFindPage();
            FormatSearchLunchBoxesFindPage();
        }

        //Finds the friend the current member has searched for and shows his/her details on the profile
        //Informs the current member of the number of lunhcboxes the friend has
        private void buttonSearchForAFriendFindpage_Click(object sender, EventArgs e)
        {
            try
            {
                //Finds friend and adds his/her details to the profile
                string friendId = textBoxSearchFriendFindPage.Text.ToString();
                if (friendId.ToLower().Equals(member.MemberId.ToLower()))
                {
                    toolStripStatusLabelLunchSwitch.Text = "You can not find yourself";
                }
                else
                {
                    this.SearchForAFriendFindPage(friendId);

                    try
                    {
                        if (dataGridViewFriendLunchBoxesFindPage.RowCount > 1)
                        {
                            toolStripStatusLabelLunchSwitch.Text = ("Found " + dataGridViewFriendLunchBoxesFindPage.RowCount.ToString() + " LunchBoxes");

                            dataGridViewFriendLunchBoxesFindPage.Rows[0].Selected = false;
                        }
                        else if (dataGridViewFriendLunchBoxesFindPage.RowCount == 1)
                        {
                            toolStripStatusLabelLunchSwitch.Text = ("Found " + dataGridViewFriendLunchBoxesFindPage.RowCount.ToString() + " LunchBox");

                            dataGridViewFriendLunchBoxesFindPage.Rows[0].Selected = false;
                        }
                        else if (dataGridViewFriendLunchBoxesFindPage.RowCount < 1)
                        {
                            toolStripStatusLabelLunchSwitch.Text = ("The friend does not have any lunchboxes");

                        }

                        //Formats the friendslunchboxesdatagridviews and mylunchboxesdatagridview with font, size and general design
                        FormatFriendLunchBoxesFindPage();
                        FormatMyLunchBoxesFindPage();
                        DeselectRowsFindPage();
                    }
                    catch (Exception ex)
                    {
                        toolStripStatusLabelLunchSwitch.Text = ExceptionHandler.HandleExceptions(ex);
                    }

                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabelLunchSwitch.Text = ExceptionHandler.HandleExceptions(ex);
            }
        }

        //Checks the categories chosen in the search comboboxes and shows the corresponding result
        private void SearchLunchBoxFindPage()
        {
            string foodCategory = comboBoxFoodPreferencesFindPage.SelectedItem.ToString();
            string city = comboBoxCityFindPage.SelectedItem.ToString();

            this.dataGridViewMyLunchBoxesFindPage.DataSource = controller.FindMembersLunchBoxes(member);
            this.dataGridViewFriendLunchBoxesFindPage.DataSource = null;


            //Performs the correct search and sets a message
            if (foodCategory == "ALL" && city == "ALL")
            {

                this.dataGridViewSearchLunchBoxesFindPage.DataSource = controller.FindAllLunchBoxes(member);

            }
            else if (foodCategory == "ALL" && city != null)
            {
                this.dataGridViewSearchLunchBoxesFindPage.DataSource = controller.FindLunchBoxByCity(city, member);

            }
            else if (city == "ALL" && foodCategory != null)
            {
                this.dataGridViewSearchLunchBoxesFindPage.DataSource = controller.FindLunchBoxByFoodCategory(foodCategory, member);
            }
            else
            {
                this.dataGridViewSearchLunchBoxesFindPage.DataSource = controller.FindLunchBoxByCityAndCategory(city, foodCategory, member);
            }

            try
            {
                DeselectRowsFindPage();
            }
            catch (Exception ex)
            {
                //do nothing
            }

        }

        //Finds the friend that is searched for and shows its details in the profile
        private void SearchForAFriendFindPage(string friendId)
        {

            Member friend = controller.FindMember(friendId);
            textBoxUsernameFindPage.Text = friend.MemberId;
            textBoxFullNameFindPage.Text = friend.FullName;
            textBoxMobileFindPage.Text = friend.MobileNr;
            textBoxEmailFindPage.Text = friend.Email;
            textBoxDescriptionFindPage.Text = friend.Description;
            textBoxRatingFindPage.Text = controller.FindMemberRating(friend).ToString("##.0", System.Globalization.CultureInfo.InvariantCulture);
            toolStripStatusLabelLunchSwitch.Text = "";

            //Adds the friend's lunchboxes to the lunchboxgridview and removes them from the searchgridview
            this.dataGridViewFriendLunchBoxesFindPage.DataSource = controller.FindMembersLunchBoxes(friend);
            this.dataGridViewMyLunchBoxesFindPage.DataSource = controller.FindMembersLunchBoxes(member);
            this.dataGridViewSearchLunchBoxesFindPage.DataSource = null;

            //Sets the first row as the selcted row in the friendlunchboxesdatagridview
            if (dataGridViewFriendLunchBoxesFindPage.Rows.Count != 0)
            {
                DataGridViewRow selectedRow = dataGridViewFriendLunchBoxesFindPage.Rows[0];
                lunchBoxIdSearch = selectedRow.Cells[0].Value.ToString();
            }

        }

        //Rates the found friend with the grade that the current member has chosen from a combobox
        private void buttonRateFindpage_Click(object sender, EventArgs e)
        {
            if (textBoxUsernameFindPage.Text.Equals(""))
            {
                toolStripStatusLabelLunchSwitch.Text = "Please search for a friend to rate first";
            }
            else
            {
                try
                {
                    decimal friendGrade = Convert.ToDecimal(comboBoxRateFriendFindpage.SelectedItem.ToString());
                    Member friend = controller.FindMember(textBoxUsernameFindPage.Text);
                    Rating rating = new Rating(friendGrade, friend);
                    controller.AddRating(rating);
                    textBoxRatingFindPage.Text = controller.FindMemberRating(friend).ToString("##.0", System.Globalization.CultureInfo.InvariantCulture);
                    toolStripStatusLabelLunchSwitch.Text = "Your rating was added";

                }
                catch (Exception ex)
                {
                    toolStripStatusLabelLunchSwitch.Text = ExceptionHandler.HandleExceptions(ex);
                }
            }

        }

        //Makes a switch 
        private void buttonMakeaswitchFindpage_Click(object sender, EventArgs e)
        {
            try
            {
                //Finds the selected lunchbox of member and removes one from the quantity
                int myLunchboxId = Int32.Parse(lunchBoxIdMy);
                LunchBox myLb = controller.FindLunchBox(myLunchboxId);


                //if selected from searchdatagridview, finds the searched lunchbox, removes one from
                //quantity and creates a new meetup
                if (dataGridViewSearchLunchBoxesFindPage.SelectedRows.Count != 0 && myLb != null && dataGridViewFriendLunchBoxesFindPage.DataSource == null)
                {
                    int lunchBoxId = Int32.Parse(lunchBoxIdSearch);
                    LunchBox lb = controller.FindLunchBox(lunchBoxId);

                    makeASwitch(lb, myLb);
                    SearchLunchBoxFindPage();
                    FormatSearchLunchBoxesFindPage();
                    toolStripStatusLabelLunchSwitch.Text = "The switch is now made";
                }

                //if selected from frienddatagridview, finds the friend's lunchbox, removes one from
                //quantity and creates a new meetup
                else if (dataGridViewFriendLunchBoxesFindPage.SelectedRows.Count != 0 && myLb != null && dataGridViewSearchLunchBoxesFindPage.DataSource == null)
                {
                    int friendLunchBoxId = Int32.Parse(lunchBoxIdFriend);
                    LunchBox friendLb = controller.FindLunchBox(friendLunchBoxId);

                    makeASwitch(friendLb, myLb);
                    FormatFriendLunchBoxesFindPage();
                    buttonSearchForAFriendFindpage_Click(null, null);
                    toolStripStatusLabelLunchSwitch.Text = "The switch is now made";
                }

                else
                {
                    toolStripStatusLabelLunchSwitch.Text = "Please make a selection first";
                }

                //Refresh the lists of lunchboxes and meet-ups on the current member's profile
                dataGridViewLunchBoxesMyAccount.DataSource = controller.FindMembersLunchBoxes(member);
                dataGridViewMyMeetUpsMyAccount.DataSource = controller.FindMembersMeetUps(member);
            }

            catch (Exception ex)
            {
                toolStripStatusLabelLunchSwitch.Text = ExceptionHandler.HandleExceptions(ex);
            }
            DeselectRowsFindPage();
        }


        //Makes a switch once the "make a switch" button is pressed.
        //Decreases the quantity of both selected lunchboxes, and removes if there are none left
        //Creates a meet-up, reformats the datagridviews and informs the member if the successful switch. 
        public void makeASwitch(LunchBox lb1, LunchBox lb2)
        {
            int quantity = lb1.Quantity - 1;

            int myQuantity = lb2.Quantity - 1;

            string information = (lb1.Member.MemberId + "'s " + lb1.Name + " for " + member.MemberId + "'s " + lb2.Name);
            CreateMeetUp(lb1, information);

            //Removes the other member's lunchbox if it was the last one, otherwise updates by decreasing in quantity
            if (quantity == 0)
            {
                controller.DeleteLunchBox(lb1.LunchBoxId);
            }
            else
            {
                controller.UpdateLunchBox(lb1.LunchBoxId, quantity);
            }

            //Removes the current member's lunchbox if it was the last one, otherwise updates by decreasing in quantity
            if (myQuantity == 0)
            {
                controller.DeleteLunchBox(lb2.LunchBoxId);
            }
            else
            {
                controller.UpdateLunchBox(lb2.LunchBoxId, myQuantity);
            }

            FillMyAccountPage();
            FormatMyLunchBoxesFindPage();
        }

        //Creates a new meetup once the "make a switch" button is pressed
        private void CreateMeetUp(LunchBox friendLb, string information)
        {
            MeetUp mU = new MeetUp(information);
            controller.AddMeetup(mU);
            int meetUpId = mU.MeetUpId;

            MeetUp_Member mm = new MeetUp_Member(member, mU);
            MeetUp_Member mm1 = new MeetUp_Member(friendLb.Member, mU);

            controller.AddMeetUp_Member(mm, meetUpId);
            controller.AddMeetUp_Member(mm1, meetUpId);
        }

        //**LISTENERS ON FIND PAGE**//

        //Listener to selection in the datagridview for searched lunchboxes
        private void dataGridViewSearchLunchboxesFindPage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Sets the current searched lunchbodId to the one selected
            try
            {
                int index = e.RowIndex;
                DataGridViewRow selectedRow = dataGridViewSearchLunchBoxesFindPage.Rows[index];
                lunchBoxIdSearch = selectedRow.Cells[0].Value.ToString();

            }
            catch (Exception ex)
            {
                toolStripStatusLabelLunchSwitch.Text = ExceptionHandler.HandleExceptions(ex);
            }
        }

        //Listener to selection in the datagridview for the current member's lunchboxes
        private void dataGridViewMyLunchboxesFindPage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Sets the current member's lunchbodId to the one selected
            try
            {
                int index = e.RowIndex;
                DataGridViewRow selectedRow = dataGridViewMyLunchBoxesFindPage.Rows[index];
                lunchBoxIdMy = selectedRow.Cells[0].Value.ToString();

            }
            catch (Exception ex)
            {
                toolStripStatusLabelLunchSwitch.Text = ExceptionHandler.HandleExceptions(ex);
            }

        }

        //Listener to selection in the datagridview for the foudn friend's lunchboxes
        private void dataGridViewFriendLunchboxesFindPage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Sets the current friend's lunchbodId to the one selcted
            try
            {
                int index = e.RowIndex;
                DataGridViewRow selectedRow = dataGridViewFriendLunchBoxesFindPage.Rows[index];
                lunchBoxIdFriend = selectedRow.Cells[0].Value.ToString();
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleExceptions(ex);
            }
        }

        //Checks if a friend is searched for and suggests members in the system
        //Informs the user if the memberId does not exist or it s/he is trying to search for her/himself
        private void textBoxSearchFriendFindPage_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //Fill friends searchbox
                List<string> friends = new List<string>();
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
                toolStripStatusLabelLunchSwitch.Text = ExceptionHandler.HandleExceptions(ex);
            }
        }

        //**FORMAT DATAGRIDVIEWS ON FIND PAGE **//

        //Formats the design of the datagridview of the searched lunchboxes on find page with headers, size and font
        private void FormatSearchLunchBoxesFindPage()
        {
            for (int i = 0; i < dataGridViewSearchLunchBoxesFindPage.Columns.Count; i++)
            {

                string str = dataGridViewSearchLunchBoxesFindPage.Columns[i].HeaderText;
                if (str == "LunchBoxId")
                {
                    dataGridViewSearchLunchBoxesFindPage.Columns[i].HeaderText = "Lunchbox Id";
                }
                else if (str == "FoodCategory")
                {
                    dataGridViewSearchLunchBoxesFindPage.Columns[i].HeaderText = "Food Category";
                }

            }
            dataGridViewSearchLunchBoxesFindPage.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewSearchLunchBoxesFindPage.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewSearchLunchBoxesFindPage.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewSearchLunchBoxesFindPage.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewSearchLunchBoxesFindPage.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //Change font of datagridviews
            this.dataGridViewSearchLunchBoxesFindPage.DefaultCellStyle.Font = new Font("Arial", 9);
            this.dataGridViewSearchLunchBoxesFindPage.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9);
        }

        //Formats the design of the datagridview of the found friend's lunchboxes with size and font
        private void FormatFriendLunchBoxesFindPage()
        {
            for (int i = 0; i < dataGridViewFriendLunchBoxesFindPage.Columns.Count; i++)
            {

                string str = dataGridViewFriendLunchBoxesFindPage.Columns[i].HeaderText;
                if (str == "LunchBoxId")
                {
                    dataGridViewFriendLunchBoxesFindPage.Columns[i].HeaderText = "Lunchbox Id";
                }
                else if (str == "FoodCategory")
                {
                    dataGridViewFriendLunchBoxesFindPage.Columns[i].HeaderText = "Food Category";
                }

            }
            dataGridViewFriendLunchBoxesFindPage.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewFriendLunchBoxesFindPage.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewFriendLunchBoxesFindPage.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewFriendLunchBoxesFindPage.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewFriendLunchBoxesFindPage.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //Change font of datagridviews
            this.dataGridViewFriendLunchBoxesFindPage.DefaultCellStyle.Font = new Font("Arial", 9);
            this.dataGridViewFriendLunchBoxesFindPage.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9);
        }

        //Formats the design of the datagridview of the lunchboxes of the current member with size and font 
        private void FormatMyLunchBoxesFindPage()
        {
            for (int i = 0; i < dataGridViewMyLunchBoxesFindPage.Columns.Count; i++)
            {

                string str = dataGridViewMyLunchBoxesFindPage.Columns[i].HeaderText;
                if (str == "LunchBoxId")
                {
                    dataGridViewMyLunchBoxesFindPage.Columns[i].HeaderText = "Lunchbox Id";
                }
                else if (str == "FoodCategory")
                {
                    dataGridViewMyLunchBoxesFindPage.Columns[i].HeaderText = "Food Category";
                }

            }
            dataGridViewMyLunchBoxesFindPage.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewMyLunchBoxesFindPage.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewMyLunchBoxesFindPage.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewMyLunchBoxesFindPage.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewMyLunchBoxesFindPage.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;


            //Change font of datagridviews
            this.dataGridViewMyLunchBoxesFindPage.DefaultCellStyle.Font = new Font("Arial", 9);
            this.dataGridViewMyLunchBoxesFindPage.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9);
        }

        //Deselects the selected options from the datagridviews of find page
        private void DeselectRowsFindPage()
        {
            try
            {
                //dataGridViewMyLunchBoxesFindPage.Rows[0].Selected = false;
                //dataGridViewSearchLunchBoxesFindPage.Rows[0].Selected = false;
                //dataGridViewFriendLunchBoxesFindPage.Rows[0].Selected = false;
                dataGridViewMyLunchBoxesFindPage.ClearSelection();
                dataGridViewSearchLunchBoxesFindPage.ClearSelection();
                dataGridViewFriendLunchBoxesFindPage.ClearSelection();
            }
            catch (Exception ex)
            {
                //If datagridview is empty, do nothing
            }

        }

        //**MY ACCOUNT**//

        //Saves the updated details that the current member has filled in
        //if all fields are filled
        private void buttonSaveAndUpdateMyaccount_Click(object sender, EventArgs e)
        {

            string fullName = textBoxNameMyAccount.Text;
            string city = textBoxCityMyAccount.Text;
            string email = textBoxEmailMyAccount.Text;
            string mobileNbr = textBoxMobileMyAccount.Text;
            string description = textBoxDescriptionMyAccount.Text;

            if (CheckAllUpdateAccountTextBoxes())
            {
                try
                {
                    controller.UpdateMember(member.MemberId, fullName, city, email, mobileNbr, description);
                    toolStripStatusLabelLunchSwitch.Text = "Account updated!";


                }
                catch (Exception ex)
                {
                    toolStripStatusLabelLunchSwitch.Text = ExceptionHandler.HandleExceptions(ex);
                }
            }
        }

        //Removes the selected lunchbox 
        private void buttonRemoveLunchboxMyAccont_Click(object sender, EventArgs e)
        {
            if (dataGridViewLunchBoxesMyAccount.SelectedRows.Count == 0)
            {
                toolStripStatusLabelLunchSwitch.Text = "Please select a lunchbox to delete first";
            }
            else
            {
                try
                {
                    controller.DeleteLunchBox(Convert.ToInt64(lunchBoxIdMyAccount));

                    //Resets the source of the datagridviews to update their content
                    dataGridViewLunchBoxesMyAccount.DataSource = controller.FindMembersLunchBoxes(member);
                    dataGridViewMyLunchBoxesFindPage.DataSource = controller.FindMembersLunchBoxes(member);

                    //Formats the datagridviews
                    FormatLunchBoxesMyAccount();
                    FormatMyLunchBoxesFindPage();

                    toolStripStatusLabelLunchSwitch.Text = "Lunchbox removed!";
                }
                catch (Exception ex)
                {
                    toolStripStatusLabelLunchSwitch.Text = ExceptionHandler.HandleExceptions(ex);
                }
            }
        }

        //Increases the quantity of the selected lunchbox with 1
        private void buttonPlusQuanitMyAccount_Click(object sender, EventArgs e)
        {
            if (dataGridViewLunchBoxesMyAccount.SelectedRows.Count == 0)
            {
                toolStripStatusLabelLunchSwitch.Text = "Please select which lunchbox you want to edit first";
            }
            else
            {
                try
                {
                    LunchBox friendLb = controller.FindLunchBox(Convert.ToInt64(lunchBoxIdMyAccount));
                    int quantity = friendLb.Quantity + 1;
                    controller.UpdateLunchBox(friendLb.LunchBoxId, quantity);
                    dataGridViewLunchBoxesMyAccount.DataSource = controller.FindMembersLunchBoxes(member);
                    DeselectRowsMyAccountPage();
                    toolStripStatusLabelLunchSwitch.Text = "Quantity changed";
                }
                catch (Exception ex)
                {
                    toolStripStatusLabelLunchSwitch.Text = ExceptionHandler.HandleExceptions(ex);
                }
            }

        }

        //Decreases the quantity of the selected lunchbox with 1
        private void buttonMinusQuanityMyAccount_Click(object sender, EventArgs e)
        {
            if (dataGridViewLunchBoxesMyAccount.SelectedRows.Count == 0)
            {
                toolStripStatusLabelLunchSwitch.Text = "Please select which lunchbox you want to edit first";
            }
            else
            {
                try
                {
                    LunchBox friendLb = controller.FindLunchBox(Convert.ToInt64(lunchBoxIdMyAccount));
                    int quantity = friendLb.Quantity - 1;

                    if (quantity == 0)
                    {
                        controller.DeleteLunchBox(friendLb.LunchBoxId);
                    }
                    else
                    {
                        controller.UpdateLunchBox(friendLb.LunchBoxId, quantity);
                    }

                    dataGridViewLunchBoxesMyAccount.DataSource = controller.FindMembersLunchBoxes(member);
                    DeselectRowsMyAccountPage();
                    toolStripStatusLabelLunchSwitch.Text = "Quantity changed";
                }
                catch (Exception ex)
                {
                    toolStripStatusLabelLunchSwitch.Text = ExceptionHandler.HandleExceptions(ex);
                }
            }
        }

        //Checks that all member fields are filled in
        private bool CheckAllUpdateAccountTextBoxes()
        {
            if (string.IsNullOrWhiteSpace(textBoxUsernameMyAccount.Text) ||
                string.IsNullOrWhiteSpace(textBoxNameMyAccount.Text) || string.IsNullOrWhiteSpace(textBoxMobileMyAccount.Text) ||
                string.IsNullOrWhiteSpace(textBoxEmailMyAccount.Text) || string.IsNullOrWhiteSpace(textBoxDescriptionMyAccount.Text) ||
                string.IsNullOrWhiteSpace(textBoxCityMyAccount.Text))
            {
                toolStripStatusLabelLunchSwitch.Text = "Please fill all fields";
                return false;
            }
            else
            {
                return true;
            }
        }

        //Checks that all lunchbox fields are filled in
        private bool CheckAllAddALunchTextBoxes()
        {
            if (string.IsNullOrWhiteSpace(textBoxNameAddALunchBoxMyAccount.Text) ||
                string.IsNullOrWhiteSpace(textBoxQuantityAddALunchBoxMyAccount.Text) ||
                string.IsNullOrWhiteSpace(textBoxContentAddALunchBox.Text) || string.IsNullOrWhiteSpace(textBoxQuantityAddALunchBoxMyAccount.Text))
            {
                toolStripStatusLabelLunchSwitch.Text = "Please fill all fields";
                return false;
            }
            else
            {
                return true;
            }
        }

        //Adds the new lunchbox with all its attributes if the fields are filled in correctly,
        //clears the fields again and update all lunchbox datagrid views
        private void buttonAddLunchboxMyAccount_Click(object sender, EventArgs e)
        {

            string name = textBoxNameAddALunchBoxMyAccount.Text;
            string quantityString = textBoxQuantityAddALunchBoxMyAccount.Text;
            string content = textBoxContentAddALunchBox.Text;
            string foodCategory = comboBoxFoodCategoryMyAccount.Text;

            if (CheckAllAddALunchTextBoxes() && CheckLunchBoxQuantity(quantityString))
            {
                try
                {
                    int quantity = int.Parse(quantityString);
                    LunchBox l = new LunchBox(name, content, foodCategory, quantity, member);
                    controller.AddLunchBox(l);
                    ClearAllMyAccountLunchBox();
                    dataGridViewLunchBoxesMyAccount.DataSource = controller.FindMembersLunchBoxes(member);
                    dataGridViewMyLunchBoxesFindPage.DataSource = controller.FindMembersLunchBoxes(member);
                    FormatLunchBoxesMyAccount();
                    FormatMyLunchBoxesFindPage();
                    dataGridViewLunchBoxesMyAccount.Rows[0].Selected = false;

                    toolStripStatusLabelLunchSwitch.Text = "The lunchbox was added";

                }
                catch (Exception ex)
                {
                    toolStripStatusLabelLunchSwitch.Text = ExceptionHandler.HandleExceptions(ex);
                }
            }
        }

        //Checks that the input value is an int
        private bool CheckLunchBoxQuantity(string quantityString)
        {
            int quantity;
            bool quantityOK;
            bool isNum = int.TryParse(quantityString, out quantity);
            if (isNum && quantity > 0)
            {
                quantityOK = true;
            }
            else
            {
                toolStripStatusLabelLunchSwitch.Text = "The quantity input is invalid, add a number";
                quantityOK = false;
            }

            return quantityOK;
        }

        //Removes a meet-up if selected and the button "remove" is pressed
        private void buttonRemoveMeetupMyAccount_Click(object sender, EventArgs e)
        {
            try
            {
                controller.DeleteMeetup(Convert.ToInt64(meetUpIdMyAccount));
                dataGridViewMyMeetUpsMyAccount.DataSource = controller.FindMembersMeetUps(member);
                DeselectRowsMyAccountPage();
                toolStripStatusLabelLunchSwitch.Text = "Meet up removed!";
            }
            catch (Exception ex)
            {
                toolStripStatusLabelLunchSwitch.Text = ExceptionHandler.HandleExceptions(ex);
            }

        }

        //Logs the member out after a confirmation and reopens the start page
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

        //Clear all input fields for adding a lunchbox on my account
        private void ClearAllMyAccountLunchBox()
        {
            textBoxNameAddALunchBoxMyAccount.Text = "";
            textBoxQuantityAddALunchBoxMyAccount.Text = "";
            textBoxContentAddALunchBox.Text = "";
            comboBoxFoodCategoryMyAccount.Text = "";
        }

        //Deselects the selected options from the datagridviews of my account
        private void DeselectRowsMyAccountPage()
        {
            try
            {
                //dataGridViewLunchBoxesMyAccount.Rows[0].Selected = false;
                //dataGridViewMyMeetUpsMyAccount.Rows[0].Selected = false;
                dataGridViewLunchBoxesMyAccount.ClearSelection();
                dataGridViewMyMeetUpsMyAccount.ClearSelection();
            }
            catch (Exception ex)
            {
                //If datagridview is empty, do nothing
            }
        }

        //**LISTENERS ON MY ACCOUNT**//

        //Listener to a selection in the list of lunchboxes on my account
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
                ExceptionHandler.HandleExceptions(ex);
            }

        }

        //Listener to a selection in the list of meet-ups on my account
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
                ExceptionHandler.HandleExceptions(ex);
            }

        }

        //If a meet-up is doubled cliked on on the current member's profile, the member will be
        //redirected to the rating of the friend that the switch was made with
        private void dataGridViewMyMeetUpsMyAccount_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                MeetUp mU = controller.FindMeetup(Convert.ToInt64(meetUpIdMyAccount));
                Member friend = controller.FindFriendInMeetUp(controller.FindMeetup(Convert.ToInt64(meetUpIdMyAccount)), member);
                textBoxSearchFriendFindPage.Text = friend.MemberId;
                buttonSearchForAFriendFindpage_Click(null, null);
                tabProgram.SelectedIndex = 0;
                comboBoxRateFriendFindpage.DroppedDown = true;
            }
            catch (Exception ex)
            {
                toolStripStatusLabelLunchSwitch.Text = ExceptionHandler.HandleExceptions(ex);
            }
        }

        //**FORMATS DATAGRIDVIEWS ON MY ACCOUNT**//
        //Formats the design of the lunchbox datagridviews on my account, with headers, size and font
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
            dataGridViewLunchBoxesMyAccount.Columns[1].Width = 110;
            dataGridViewLunchBoxesMyAccount.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewLunchBoxesMyAccount.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewLunchBoxesMyAccount.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //Formats the lunchboxdatagridview on my account
            dataGridViewLunchBoxesMyAccount.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridViewLunchBoxesMyAccount.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 11);
            dataGridViewMyMeetUpsMyAccount.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridViewMyMeetUpsMyAccount.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 11);
        }

        //Formats the design of the datagridview of meet-ups on my account with headers, size and font
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

        //**GENERAL**//

        //Listener of the change of tab
        private void tabProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeselectRowsMyAccountPage();

        }

    }
}
