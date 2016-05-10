using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LS.Model;
using LS.Exceptions;

namespace LS.DAL
{
    public class LunchSwitchAccess
    {


        private LunchSwitchEDM db = new LunchSwitchEDM();

        //Member
        public Member FindMember(string memberId)
        {
            try
            {
                return db.Members.Find(memberId);
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
                db.Members.Add(m);
                db.SaveChanges();
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
                Member m = FindMember(memberId);
                db.Members.Remove(m);
                db.SaveChanges();
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
                Member u = db.Members.Find(memberId);
                u.FullName = fullName;
                u.City = city;
                u.Email = email;
                u.MobileNr = mobileNr;
                u.Description = description;
                db.Entry(u).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
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
                List<Member> listMembers = (from u in db.Members select u).ToList();
                return listMembers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Member> FindAllMembersExpectUser(Member m)
        {
            try
            {
                List<Member> listMembers = (from u in db.Members where u.MemberId != m.MemberId select u).ToList();
                return listMembers;
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
                List<Member> listMember = (from u in db.Members where u.City == city && u.MemberId != m.MemberId select u).ToList();
                return listMember;
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
                return db.LunchBoxes.Find(lunchBoxId);
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
                db.LunchBoxes.Add(l);
                db.SaveChanges();
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
                LunchBox l = FindLunchBox(lunchBoxId);
                db.LunchBoxes.Remove(l);
                db.SaveChanges();
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
                LunchBox l = db.LunchBoxes.Find(lunchBoxId);
                l.Quantity = quantity;
                db.Entry(l).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
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
                List<LunchBox> listLunchBoxes = (from l in db.LunchBoxes where l.MemberId == m.MemberId select l).ToList();
                return listLunchBoxes;
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
                List<LunchBox> listLunchBoxes = (from l in db.LunchBoxes where l.MemberId != m.MemberId select l).ToList();
                return listLunchBoxes;
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
                List<LunchBox> listLunchBox = (from l in db.LunchBoxes where l.FoodCategory == foodCategory && l.MemberId != m.MemberId select l).ToList();
                return listLunchBox;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> FindAllLunchBoxesCitys()
        {
            List<string> cities = new List<string>();
            List<Member> Members = this.FindAllMembers();
            HashSet<string> citiesHash = new HashSet<string>();

            foreach (Member m in Members)
            {
                citiesHash.Add(m.City);

            }

            cities = citiesHash.ToList();
            return cities;

        }

        public List<LunchBox> FindLunchBoxByCity(string city, Member m)
        {
            List<LunchBox> lunchBoxes = new List<LunchBox>();
            List<Member> Members = this.FindMemberByCity(city, m);
            foreach (Member member in Members)
            {
                foreach (LunchBox l in FindMembersLunchBoxes(member))
                {

                    lunchBoxes.Add(l);

                }
            }
            return lunchBoxes;
        }

        public List<LunchBox> FindLunchBoxByCityAndCategory(string city, string foodCategory, Member m)
        {
            List<LunchBox> lunchBoxes = new List<LunchBox>();
            foreach (LunchBox l in FindLunchBoxByCity(city, m))
            {
                if (l.FoodCategory.Equals(foodCategory))
                {
                    lunchBoxes.Add(l);
                }
            }
            return lunchBoxes;
        }


        //Meetup
        public MeetUp FindMeetup(long meetupId)
        {
            try
            {
                return db.MeetUps.Find(meetupId);
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
                db.MeetUps.Add(m);
                db.SaveChanges();
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
                MeetUp m = FindMeetup(meetupId);
                db.MeetUps.Remove(m);
                db.SaveChanges();
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
                List<MeetUp> listMeetups = (from m in db.MeetUps select m).ToList();
                return listMeetups;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Member_MeetUp
        public List<MeetUp> FindMembersMeetUps(Member m)
        {
            List<MeetUp_Member> listMeetUpMembers = this.FindAllMeetUp_Members();
            List<MeetUp> listMeetUps = this.FindAllMeetups();
            List<MeetUp> membersMeetUps = new List<MeetUp>();
            foreach (MeetUp_Member meetUpMember in listMeetUpMembers)
            {
                foreach (MeetUp meetUp in listMeetUps)
                {
                    if (meetUp.MeetUpId == meetUpMember.MeetUpId && meetUpMember.MemberId == m.MemberId)
                    {
                        membersMeetUps.Add(meetUp);
                    }
                }
            }
            return membersMeetUps;
        }

        public void AddMeetUp_Member(MeetUp_Member mm, int meetUpId)
        {
            try
            {

                int count = 0;
                List<MeetUp_Member> mmList = this.FindAllMeetUp_Members();
                foreach (MeetUp_Member mmTemp in mmList)
                {

                    if (mmTemp.MeetUpId.Equals(meetUpId))
                    {

                        count++;

                    }
                }
                if (count < 2)
                {
                    db.MeetUp_Members.Add(mm);
                    db.SaveChanges();
                }

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
                List<MeetUp_Member> listMeetUpMembers = (from mm in db.MeetUp_Members select mm).ToList();
                return listMeetUpMembers;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //Rating
        public void AddRating(Rating r)
        {
            try
            {
                db.Ratings.Add(r);
                db.SaveChanges();
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
                List<Rating> listRatings = (from r in db.Ratings select r).ToList();
                return listRatings;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Decimal FindMemberRating(Member m)
        {
            List<Rating> memberRatings = new List<Rating>();
            int count = 0;
            Decimal totalGrade = 0;
            Decimal grade = 0;
            try
            {

                List<Rating> allRatings = this.FindAllRatings();
                foreach (Rating r in allRatings)
                {
                    if (m.MemberId == r.MemberId)
                    {
                        memberRatings.Add(r);
                        count++;
                        totalGrade += r.Grade;
                    }
                }
                grade = totalGrade / count;
            }
            catch (Exception ex)
            {
                return grade;
            }

            return grade;
        }

        public Member FindFriendInMeetUp(MeetUp mU, Member m)
        {
            List<MeetUp_Member> meetUpMembers = this.FindAllMeetUp_Members();


            foreach (MeetUp_Member mUM in meetUpMembers)
            {
                if (mUM.MeetUpId.Equals(mU.MeetUpId) && !mUM.MemberId.Equals(m.MemberId))
                {
                    try
                    {
                        Member friend = this.FindMember(mUM.MemberId);
                        return friend;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }
            return null;


        }
    }
}
