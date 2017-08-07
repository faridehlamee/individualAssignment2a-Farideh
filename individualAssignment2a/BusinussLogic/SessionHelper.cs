using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace individualAssignment2a.BusinussLogic
{
    public class SessionHelper
    {
        public const string SESSION_START = "Session_Start";
       

        // Get data stored under the current session.
        // This data is stored on the server in a collection.
        public DateTime Start
        {
            get
            {
                try
                {
                    return (DateTime)HttpContext.Current.Session[SESSION_START];
                }
                catch
                {
                    Initialize();
                }
                return (DateTime)HttpContext.Current.Session[SESSION_START];
            }
        }
       

        // Return value from session cookie manually if the session does not exist.
        public string SessionID
        {
            get
            {
                try
                {
                    if (HttpContext.Current.Session.SessionID != null)
                        return HttpContext.Current.Session.SessionID;
                }
                catch (Exception e)
                {
                    
                }
                return null;
            }
        }
        public void Initialize()
        {
            shoppingcartfaridehEntities db = new shoppingcartfaridehEntities();
         //   HttpContext.Current.Session[SESSION_START] = DateTime.Now;

            try
            {
                Visit newVisit = new Visit();
                newVisit.sessionID = SessionID;
                newVisit.started = DateTime.Now;
                db.Visits.Add(newVisit);
                db.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }

        public void End()
        {
            shoppingcartfaridehEntities db = new shoppingcartfaridehEntities();
            var maxDateTimeThreshhold = DateTime.Now.AddMinutes(-1 * 2);

            var expiredProductVisits = from ProductVisit in db.ProductVisits
                                       where ProductVisit.updated <= maxDateTimeThreshhold
                                       select ProductVisit;
            if(expiredProductVisits != null)
            {
                foreach (ProductVisit productVisit in expiredProductVisits)
                {
                    db.ProductVisits.Remove(productVisit);
                }

                db.SaveChanges();   
            }

            var abandoneVisits = from v in db.Visits
                                 where !db.ProductVisits.Any(pv => pv.sessionID == v.sessionID)
                                 select v;
            if(abandoneVisits != null)
            {
                foreach(Visit visit in abandoneVisits)
                {
                    db.Visits.Remove(visit);
                }
                db.SaveChanges();
            }
        }
        public void Clear()
        {
            if (SessionID != null)
            {
                HttpContext.Current.Session.Clear(); // remove stored items
                HttpContext.Current.Session.Abandon();
            }
        }
    }
}
