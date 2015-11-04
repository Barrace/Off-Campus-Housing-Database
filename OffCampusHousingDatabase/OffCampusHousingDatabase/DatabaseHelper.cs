using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace OffCampusHousingDatabase
{
    class DatabaseHelper
    {
        String connectionString;

        public DatabaseHelper(String connectionString)
        {
            this.connectionString = connectionString;
        }

        public ArrayList DatabaseSelectImage(String tableName)
        {
            return DatabaseSelectImage(tableName, "");
        }

        public ArrayList DatabaseSelectImage(String tableName, String whereClause)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                String sql = "SELECT * FROM `" + tableName + "` ";

                if (!whereClause.Equals(""))
                {
                    sql += " WHERE " + whereClause;
                }

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataReader reader = cmd.ExecuteReader();

                //number of columns
                int numCols = reader.FieldCount;

                ArrayList output = new ArrayList();

                while (reader.Read())
                {
                    Object[] row = new Object[numCols];
                    for (int i = 0; i < numCols; i++)
                    {
                        row[i] = (reader.GetValue(i));
                    }
                    output.Add(row);
                }

                conn.Close();

                return output;
            }
            catch (Exception ex)
            {
                ErrorLogger el = new ErrorLogger();
                el.logError("DatabaseHelper : DatabaseSelect", ex.Message);
                return new ArrayList();
            }
        }


        public String[] DatabaseSelectFirst(String tableName)
        {
            return DatabaseSelectFirst(tableName, "");
        }

        public String[] DatabaseSelectFirst(String tableName, String whereClause)
        {
            ArrayList arr = DatabaseSelect(tableName, whereClause);
            foreach(String[] row in arr){
                return row;
            }
            return null;
        }

        public ArrayList DatabaseSelect(String tableName)
        {
            return DatabaseSelect(tableName, "");
        }

        //Sample Call: ArrayList arr = dbHelper.DatabaseSelect("User", "`email` = '" + EmailTextbox.Text + "' AND `password` = '" + pw + "'");
        public ArrayList DatabaseSelect(String tableName, String whereClause)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                String sql = "SELECT * FROM `" + tableName + "` ";

                if (!whereClause.Equals(""))
                {
                    sql += " WHERE " + whereClause;
                }

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataReader reader = cmd.ExecuteReader();

                //number of columns
                int numCols = reader.FieldCount;

                ArrayList output = new ArrayList();

                while (reader.Read())
                {
                    String[] row = new String[numCols];
                    for (int i = 0; i < numCols; i++)
                    {
                        row[i] = (reader.GetString(i));
                    }
                    output.Add(row);
                }

                conn.Close();

                return output;
            }
            catch (Exception ex)
            {
                ErrorLogger el = new ErrorLogger();
                el.logError("DatabaseHelper : DatabaseSelect", ex.Message);
                return new ArrayList();
            }
        }


        public bool DatabaseInsertImage(String tableName, String PropertyID, byte[] arr)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                string CmdString = "INSERT INTO `Image` (`PropID`, `Image`) VALUES(@Property, @Image)";
                MySqlCommand cmd = new MySqlCommand(CmdString, conn);

                cmd.Parameters.Add("@PropID", MySqlDbType.VarChar, 45);
                cmd.Parameters.Add("@Image", MySqlDbType.Blob);

                cmd.Parameters["@PropID"].Value = PropertyID;
                cmd.Parameters["@Image"].Value = arr;

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                ErrorLogger el = new ErrorLogger();
                el.logError("DatabaseHelper : DatabaseInsertImage", ex.Message);
                return false;
            }
        }

        public bool DatabaseInsert(String tableName, String Columns, String Values)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                String sql = "INSERT INTO `" + tableName + "`(" + Columns + ") VALUES (" + Values + ")";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.ExecuteReader();
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DatabaseUpdate(String tableName, String updateColumns, String whereClause)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                String sql = "UPDATE `" + tableName + "` SET " + updateColumns + " WHERE " + whereClause;

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.ExecuteReader();
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool DatabaseDelete(String tableName, String whereClause)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                String sql = "DELETE FROM `" + tableName + "` WHERE " + whereClause;

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.ExecuteReader();
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}