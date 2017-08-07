using individualAssignment2a.BusinussLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace individualAssignment2a.Repository
{
    public class VisitRepository
    {

        public void Delete(string id)
        {
            shoppingcartfaridehEntities db = new shoppingcartfaridehEntities();
            Visit visit = db.Visits.Where(v => v.sessionID == id).FirstOrDefault();
            db.Visits.Remove(visit);
            db.SaveChanges();

        }

        public void Add()
        {
            shoppingcartfaridehEntities db = new shoppingcartfaridehEntities();
            SessionHelper sessionHelper = new SessionHelper();
            Visit visit = new Visit();
            visit.sessionID = sessionHelper.SessionID;
            Delete(visit.sessionID);
            visit.started = DateTime.Now;
            db.Visits.Add(visit);
            db.SaveChanges();
        }

       
    }
}