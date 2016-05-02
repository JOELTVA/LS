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
                dataGridViewMyLunchBoxesFindPage.DataSource = controller.FindMembersLunchBoxes(member);
                toolStripStatusLabelLunchSwitch.Text = "Lunchbox removed!";


            }
            catch (Exception ex)
            {
                toolStripStatusLabelLunchSwitch.Text = handleException.HandleExceptions(ex);
            }

        }

        //Fills the account iwht the logged in Member's details
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
                if (dataGridViewLunchBoxesMyAccount.Rows.Count != 0)
                {
                    dataGridViewLunchBoxesMyAccount.Rows[0].Selected = true;
                }
                        
                dataGridViewMyMeetUpsMyAccount.DataSource = controller.FindMembersMeetUps(member);
                if (dataGridViewMyMeetUpsMyAccount.Rows.Count != 0)
                {
                    dataGridViewMyMeetUpsMyAccount.Rows[0].Selected = true;
                }

                FormatLunchBoxesMyAccount();
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
                    toolStripStatusLabelLunchSwitch.Text = handleException.HandleExceptions(ex);
                }
            }

        }

        private bool CheckAllUpdateAccountTextBoxes()
        {
            if (string.IsNullOrWhiteSpace(textBoxUsernameMyAccount.Text) ||
                string.IsNullOrWhiteSpace(textBoxNameMyAccount.Text) || string.IsNullOrWhiteSpace(textBoxMobileMyAccount.Text) ||
                string.IsNullOrWhiteSpace(textBoxEmailMyAccount.Text) || string.IsNullOrWhiteSpace(textBoxDescriptionMyAccount.Text) || 
                string.IsNullOrWhiteSpace(textBoxCityMyAccount.Text))
            {
                labelMessageUpdateAccountMyAccount.Text = "Please fill all fields";
                return false;
            }
            else
            {
                return true;
            }
        }

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


        private void buttonAddLunchboxMyaccount_Click(object sender, EventArgs e)
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
                    dataGridViewLunchBoxesMyAccount.DataSource = controller.FindMembersLunchBoxes(member);
                    FormatLunchBoxesMyAccount();
                    ClearAllMyAccountLunchBox();
                    labelAddALunchBoxMessage.Text = "The lunchbox was added";
                }
                catch (Exception ex)
                {
                    labelAddALunchBoxMessage.Text = handleException.HandleExceptions(ex);
                }
            }
        }

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
                labelAddALunchBoxMessage.Text = "The quantity input is invalid, add a number";
                quantityOK = false;
            }

            return quantityOK;
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
                toolStripStatusLabelLunchSwitch.Text = "Meet up removed!";

            }
            catch (Exception ex)
            {
                toolStripStatusLabelLunchSwitch.Text = handleException.HandleExceptions(ex);
            }

        }

        private void buttonSearchForLunchFindpage_Click(object sender, EventArgs e)
        {
     
            try
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

            } catch (Exception ex)
            {
                toolStripStatusLabelLunchSwitch.Text = handleException.HandleExceptions(ex); 
            }

            //Sets the first row as the selcted row in the searchlunchboxesdatagridview
            if (dataGridViewSearchLunchBoxesFindPage.Rows.Count != 0)
            {
                DataGridViewRow selectedRow = dataGridViewSearchLunchBoxesFindPage.Rows[0];
                lunchBoxIdSearch = selectedRow.Cells[0].Value.ToString();
                FormatSearchLunchBoxesFindPage();
            }

            //Sets the first row as the selcted row in the mylunchboxesdatagridview
            if (dataGridViewMyLunchBoxesFindPage.Rows.Count != 0)
            {
                DataGridViewRow selectedRow = dataGridViewMyLunchBoxesFindPage.Rows[0];
                lunchBoxIdSearch = selectedRow.Cells[0].Value.ToString();
                FormatMyLunchBoxesFindPage();
            }

            //Formats the search and members datagridviews with font, size and general design
            
            

        }

        private void buttonSearchForAFriendFindpage_Click(object sender, EventArgs e)
        {
           
 
            try
            {

                //Finds friend and adds his/her details to the profile
                string friendId = comboBoxUsernameFindPage.SelectedItem.ToString();
                //string friendId = textBoxSearchFriendFindPage.Text; ANVÄNDS NÄR AUTOSEARCHBOX FUNKAR
                Member friend = controller.FindMember(friendId);
                textBoxUsernameFindPage.Text = friend.MemberId;
                textBoxFullNameFindPage.Text = friend.FullName;
                textBoxMobileFindPage.Text = friend.MobileNr;
                textBoxEmailFindPage.Text = friend.Email;
                textBoxDescriptionFindPage.Text = friend.Description;

                labelMessageRatingFindPage.Text = "";

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

                //Sets the first row as the selcted row in the mylunchboxesdatagridview
                if (dataGridViewMyLunchBoxesFindPage.Rows.Count != 0)
                {
                    DataGridViewRow selectedRow = dataGridViewMyLunchBoxesFindPage.Rows[0];
                    lunchBoxIdSearch = selectedRow.Cells[0].Value.ToString();
                }

                if (dataGridViewFriendLunchBoxesFindPage.RowCount > 1)
                {
                    toolStripStatusLabelLunchSwitch.Text = ("Found " + dataGridViewFriendLunchBoxesFindPage.RowCount.ToString() + " LunchBoxes");

                }
                else if (dataGridViewFriendLunchBoxesFindPage.RowCount == 1)
                {
                    toolStripStatusLabelLunchSwitch.Text = ("Found " + dataGridViewFriendLunchBoxesFindPage.RowCount.ToString() + " LunchBox");

                }
                else if (dataGridViewFriendLunchBoxesFindPage.RowCount < 1)
                {
                    toolStripStatusLabelLunchSwitch.Text = ("Didn't find any lunchboxes");

                }


                //Formats the friendsdatagridviews with font, size and general design
                FormatFriendLunchBoxesFindPage();


            }
            catch (Exception ex)
            {


                toolStripStatusLabelLunchSwitch.Text = handleException.HandleExceptions(ex);


            }
            
        }

        private void buttonRateFindpage_Click(object sender, EventArgs e)
        {
            try
            {
                decimal friendGrade = Convert.ToDecimal(comboBoxRateFriendFindpage.SelectedItem.ToString());
                Member friend = controller.FindMember(textBoxUsernameFindPage.Text);
                Rating rating = new Rating(friendGrade, friend);
                controller.AddRating(rating);
                toolStripStatusLabelLunchSwitch.Text = "Your rating was added";
            }
            catch (Exception ex)
            {
                toolStripStatusLabelLunchSwitch.Text = handleException.HandleExceptions(ex);
            }
            
        }

        private void buttonMakeaswitchFindpage_Click(object sender, EventArgs e)
        {

            try
            {
   
            //Finds the selected lunchbox of member and removes one from the quantity
            int myLunchboxId = Int32.Parse(lunchBoxIdMy);
            //Kan bli null fortfarande.
            LunchBox myLb = controller.FindLunchBox(myLunchboxId);
            int myQuantity = myLb.Quantity - 1;

            //Removes lunchbox if it was the last one, otherwise updates by decreasing in quantity
            if (myQuantity == 0){
                controller.DeleteLunchBox(myLunchboxId);
            }
            else { 
                controller.UpdateLunchBox(myLb.LunchBoxId, myQuantity);
            }

            //if selected from searchdatagridview, finds the searched lunchbox, removes one from
            //quantity and creates a new meetup
            if (dataGridViewFriendLunchBoxesFindPage.SelectedCells != null && myLb != null && dataGridViewFriendLunchBoxesFindPage.DataSource == null)
            {

                int lunchBoxId = Int32.Parse(lunchBoxIdSearch);
                LunchBox lb = controller.FindLunchBox(lunchBoxId);
                int quantity = lb.Quantity - 1;

                //Removes lunchbox if it was the last one, otherwise updates by decreasing in quantity
                if (quantity == 0)
                {
                    controller.DeleteLunchBox(lunchBoxId);
                }
                else
                {
                    controller.UpdateLunchBox(lunchBoxId, quantity);
                }

                string information = (member.MemberId + "'s " + myLb.Name + " for " + lb.Member.MemberId + "'s " + lb.Name);

                CreateMeetUp(lb, information);

                labelMakeASwitchMessageFindPage.Text = "The switch is now made";
            }

            //if selected from frienddatagridview, finds the friend's lunchbox, removes one from
            //quantity and creates a new meetup
            else if (dataGridViewFriendLunchBoxesFindPage.SelectedCells != null && myLb != null && dataGridViewSearchLunchBoxesFindPage.DataSource == null)
            {

                int friendLunchBoxId = Int32.Parse(lunchBoxIdFriend);
                LunchBox friendLb = controller.FindLunchBox(friendLunchBoxId);
                int friendQuantity = friendLb.Quantity - 1;

                //Removes lunchbox if it was the last one, otherwise updates by decreasing in quantity
                if (friendQuantity == 0){
                    controller.DeleteLunchBox(friendLunchBoxId);
                }
                else
                {
                    controller.UpdateLunchBox(friendLunchBoxId, friendQuantity);
                }

                controller.UpdateLunchBox(friendLb.LunchBoxId, friendQuantity);
                string information = (member.MemberId + "'s " + myLb.Name + " for " + friendLb.Member.MemberId + "'s " + friendLb.Name);

                CreateMeetUp(friendLb, information);

                toolStripStatusLabelLunchSwitch.Text = "The switch is now made";

            }

            else
            {
                labelMakeASwitchMessageFindPage.Text = "Please make a selection first";
            }

            }

            catch (Exception ex)
            {
                toolStripStatusLabelLunchSwitch.Text = handleException.HandleExceptions(ex);
            }
        }

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
                toolStripStatusLabelLunchSwitch.Text = handleException.HandleExceptions(ex);
            }
        }

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
                toolStripStatusLabelLunchSwitch.Text = handleException.HandleExceptions(ex);
            }

        }

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
            try
            {
                LunchBox friendLb = controller.FindLunchBox(Convert.ToInt64(lunchBoxIdMyAccount));
                int quantity = friendLb.Quantity + 1;
                controller.UpdateLunchBox(friendLb.LunchBoxId, quantity);
                dataGridViewLunchBoxesMyAccount.DataSource = controller.FindMembersLunchBoxes(member);
            }
            catch (Exception ex)
            {
                toolStripStatusLabelLunchSwitch.Text = handleException.HandleExceptions(ex);
            }
           
        }

        private void buttonMinusQuanityMyAccount_Click(object sender, EventArgs e)
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
            }
            catch (Exception ex)
            {

                  toolStripStatusLabelLunchSwitch.Text = handleException.HandleExceptions(ex);
            }
        }

        private void textBoxSearchFriendFindPage_TextChanged(object sender, EventArgs e)
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
    }
}
