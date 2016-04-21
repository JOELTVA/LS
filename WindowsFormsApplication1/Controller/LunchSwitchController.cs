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

        DAL.LunchSwitchAccess lsa = new DAL.LunchSwitchAccess(); //ska gå via LunchSwitchAccess i DAL istället och LunchSwitchAccess kallar på LunchSwitchEDM

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
            } catch (Exception ex)
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

        //Lunchbox
        public LunchBox FindLunchbox(long lunchBoxId)
        {
            try
            {
                return lsa.FindLunchbox(lunchBoxId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddLunchbox(LunchBox l)
        {
            try
            {
                lsa.AddLunchbox(l);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteLunchbox(long lunchBoxId)
        {
            try
            {
                lsa.DeleteLunchbox(lunchBoxId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LunchBox> FindAllLunchboxes(Member m)
        {
            try
            {
                return lsa.FindAllLunchboxes(m);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<LunchBox> FindMembersLunchboxes(Member m)
        {
            try
            {
                return lsa.FindMembersLunchboxes(m);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LunchBox> FindLunchboxByFoodCategory(string foodCategory, Member m)
        {
            try
            {
                return lsa.FindLunchboxByFoodCategory(foodCategory, m);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> FindAllLunchboxesCitys()
        {
            try
            {
                return lsa.FindAllLunchboxesCitys();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LunchBox> FindLunchboxByCity(string city, Member m)
        {
            try
            {
                return lsa.FindLunchboxByCity(city, m);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LunchBox> FindLunchboxByCityAndCategory(String city, String foodCategory, Member m)
        {
            try
            {
                return lsa.FindLunchboxByCityAndCategory(city, foodCategory, m);
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
        public void AddMeetUp_Member(MeetUp_Member mm)
        {
            try
            {
                lsa.AddMeetUp_Member(mm);
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


    }
}

