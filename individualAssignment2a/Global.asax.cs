using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using individualAssignment2a.Repository;
using individualAssignment2a.BusinussLogic;

namespace individualAssignment2a
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
           

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Session_Start(object sender, EventArgs e)
        {
            SessionHelper sessionHelper = new SessionHelper();

           sessionHelper.Initialize();

        }
        protected void Session_End(object sender, EventArgs e)
        {
            SessionHelper sessionHelper = new SessionHelper();

            sessionHelper.End();
        }

       
    }
}
