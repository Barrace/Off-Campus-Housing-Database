using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace OffCampusHousingDatabase
{
    class ErrorLogger
    {
        public void logError(String sender, String description)
        {
            DatabaseHelper dbHelper = new DatabaseHelper(ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString);
            dbHelper.databaseInsert("ErrorLogs", "`Sender`,`Description`", "'" + sender + "','" + description.Replace("'","") + "'");
        }
    }
}
