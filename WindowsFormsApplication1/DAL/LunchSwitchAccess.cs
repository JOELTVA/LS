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


        LunchSwitchEDM db = new LunchSwitchEDM();

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

        public List<Member> FindMemberByCity(string city, Member m)
        {
            try
            {
                List<Member> listMember = (from u in db.Members where u.City == city && u != m select u).ToList();
                return listMember;
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
                return db.LunchBoxes.Find(lunchBoxId);
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
                db.LunchBoxes.Add(l);
                db.SaveChanges();
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
                LunchBox l = FindLunchbox(lunchBoxId);
                db.LunchBoxes.Remove(l);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateLunchbox(long lunchBoxId, int quantity)
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

        public List<LunchBox> FindMembersLunchboxes(Member m)
        {
            try
            {
                List<LunchBox> listLunchboxes = (from l in db.LunchBoxes where l.MemberId == m.MemberId select l).ToList();
                return listLunchboxes;
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
                List<LunchBox> listLunchboxes = (from l in db.LunchBoxes where l.MemberId == m.MemberId select l).ToList();
                return listLunchboxes;
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
                List<LunchBox> listLunchbox = (from l in db.LunchBoxes where l.FoodCategory == foodCategory && l.Member != m select l).ToList();
                return listLunchbox;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Add methods: findAllLunchboxCities, findLunchboxByCity, findLunchboxByCityAndCategory
        //Don't need own queries only busniess logic that depends on already made queries! 
        public List<String> FindAllLunchboxesCitys()
        {
            List<String> cities = new List<String>();
            List<Member> Members = this.FindAllMembers();
            foreach(Member m in Members)
            {
                cities.Add(m.City);
            }
            return cities;
        }

        public List<LunchBox> FindLunchboxByCity(String city, Member m)
        {
            List<Member> Members = this.FindMemberByCity(city, m);
            List<LunchBox> lunchboxes = new List<LunchBox>();
            foreach(Member member in Members)
            {
                foreach(LunchBox l in this.FindAllLunchboxes(member))
                {
                    if(member.Equals(l.Member))
                    {
                        lunchboxes.Add(l);
                    }
                }
            }
            return lunchboxes;
        }

        public List<LunchBox> FindLunchboxByCityAndCategory(String city, String foodCategory, Member m)
        {
           List<LunchBox> lunchboxes = new List<LunchBox>();
            foreach(LunchBox l in FindLunchboxByCity(city, m))
            {
                if (l.FoodCategory.Equals(foodCategory))
                {
                    lunchboxes.Add(l);
                }
            }
            return lunchboxes;
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
   
        public void AddMeetUp_Member(MeetUp_Member mm)
        {
            try
            {
                db.MeetUp_Members.Add(mm);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<MeetUp_Member> FindAllMeetUp_Members()
        {   try
            {
                List<MeetUp_Member> listMeetUpMembers = (from mm in db.MeetUp_Members select mm).ToList();
                return listMeetUpMembers;     
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
                throw ex;
            }

            return grade;
        }




    }
}
