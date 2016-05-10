using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LS.Model;

namespace LS.Controller
{
    public class LunchSwitchController
    {

        private DAL.LunchSwitchAccess lsa = new DAL.LunchSwitchAccess();

        //Member
        public Member FindMember(string memberId)
        {
            try
            {
                return lsa.FindMember(memberId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void AddMember(Member m)
        {
            try
            {
                lsa.AddMember(m);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteMember(string memberId)
        {
            try
            {
                lsa.DeleteMember(memberId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateMember(string memberId, string fullName, string city, string email, string mobileNr, string description)
        {
            try
            {
                lsa.UpdateMember(memberId, fullName, city, email, mobileNr, description);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Member> FindAllMembers()
        {
            try
            {
                return lsa.FindAllMembers();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Member> FindAllMembersExceptUser(Member m)
        {
            try
            {
                return lsa.FindAllMembersExpectUser(m);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Member> FindMemberByCity(string city, Member m)
        {
            try
            {
                return lsa.FindMemberByCity(city, m);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //LunchBox
        public LunchBox FindLunchBox(long lunchBoxId)
        {
            try
            {
                return lsa.FindLunchBox(lunchBoxId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddLunchBox(LunchBox l)
        {
            try
            {
                lsa.AddLunchBox(l);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteLunchBox(long lunchBoxId)
        {
            try
            {
                lsa.DeleteLunchBox(lunchBoxId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateLunchBox(long lunchBoxId, int quantity)
        {
            try
            {
                lsa.UpdateLunchBox(lunchBoxId, quantity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LunchBox> FindAllLunchBoxes(Member m)
        {
            try
            {
                return lsa.FindAllLunchBoxes(m);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LunchBox> FindMembersLunchBoxes(Member m)
        {
            try
            {
                return lsa.FindMembersLunchBoxes(m);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LunchBox> FindLunchBoxByFoodCategory(string foodCategory, Member m)
        {
            try
            {
                return lsa.FindLunchBoxByFoodCategory(foodCategory, m);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> FindAllLunchBoxesCitys()
        {
            try
            {
                return lsa.FindAllLunchBoxesCitys();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LunchBox> FindLunchBoxByCity(string city, Member m)
        {
            try
            {
                return lsa.FindLunchBoxByCity(city, m);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LunchBox> FindLunchBoxByCityAndCategory(String city, String foodCategory, Member m)
        {
            try
            {
                return lsa.FindLunchBoxByCityAndCategory(city, foodCategory, m);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Meetup
        public MeetUp FindMeetup(long meetupId)
        {
            try
            {
                return lsa.FindMeetup(meetupId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddMeetup(MeetUp m)
        {
            try
            {
                lsa.AddMeetup(m);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteMeetup(long meetupId)
        {
            try
            {
                lsa.DeleteMeetup(meetupId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MeetUp> FindAllMeetups()
        {
            try
            {
                return lsa.FindAllMeetups();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<MeetUp> FindMembersMeetUps(Member m)
        {
            try
            {
                return lsa.FindMembersMeetUps(m);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<MeetUp_Member> FindAllMeetUp_Members()
        {
            try
            {
                return lsa.FindAllMeetUp_Members();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AddMeetUp_Member(MeetUp_Member mm, int meetUpId)
        {
            try
            {
                lsa.AddMeetUp_Member(mm, meetUpId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddRating(Rating r)
        {
            try
            {
                lsa.AddRating(r);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Rating> FindAllRatings()
        {
            try
            {
                return lsa.FindAllRatings();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public Decimal FindMemberRating(Member m)
        {
            try
            {
                return lsa.FindMemberRating(m);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Member FindFriendInMeetUp(MeetUp mU, Member m)
        {
            try
            {
                return lsa.FindFriendInMeetUp(mU, m);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}

