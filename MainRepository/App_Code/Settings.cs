using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainRepository.App_Code
{
    public static class Settings
    {
        private static dbShoppingEntities connection;

        public static dbShoppingEntities Connection
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Session != null)
                {
                    if (HttpContext.Current.Session[SessionKeys.OTO_CONNECTION] != null)
                    {
                        connection = (dbShoppingEntities)HttpContext.Current.Session[SessionKeys.OTO_CONNECTION];
                    }
                    else
                    {
                        if (connection != null)
                        {
                            //_conn.Dispose();
                        }
                        HttpContext.Current.Session[SessionKeys.OTO_CONNECTION] = connection = makeNewConnection();

                    }
                }
                else if (connection == null)
                {
                    connection = makeNewConnection();
                }


                return connection;
            }
            set
            {
                connection = value;
                if (HttpContext.Current != null && HttpContext.Current.Session != null)
                {
                    HttpContext.Current.Session[SessionKeys.OTO_CONNECTION] = connection;
                }
            }
        }

        public static dbShoppingEntities makeNewConnection()
        {
            return new dbShoppingEntities();
        }

        private static User activeUser;

        public static User ActiveUser
        {
            get
            {

                if (HttpContext.Current.Session[SessionKeys.UT_CURRENT_USER] != null)
                {
                    activeUser = (User)HttpContext.Current.Session[SessionKeys.UT_CURRENT_USER];
                }
                else
                {
                    activeUser = null;
                }

                return activeUser;
            }
            set
            {
                HttpContext.Current.Session[SessionKeys.UT_CURRENT_USER] = activeUser = value;
            }
        }
    }
}