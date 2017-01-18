using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media.Imaging;
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

        public ArrayList databaseSelectImage(String tableName)
        {
            return databaseSelectImage(tableName, "");
        }

        public ArrayList databaseSelectImage(String tableName, String whereClause)
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
                    String ID = reader.GetString(0);
                    Stream StreamObj = new MemoryStream((byte[])reader.GetValue(2));
                    BitmapImage BitObj = new BitmapImage();
                    BitObj.BeginInit();
                    BitObj.StreamSource = StreamObj;
                    BitObj.EndInit();

                    DatabaseImage i = new DatabaseImage(ID, BitObj);
                    output.Add(i);
                }

                return output;
            }
            catch (Exception ex)
            {
                ErrorLogger el = new ErrorLogger();
                el.logError("DatabaseHelper : databaseSelect", ex.Message);
                return new ArrayList();
            }
        }


        public String[] databaseSelectFirst(String tableName)
        {
            return databaseSelectFirst(tableName, "");
        }

        public String[] databaseSelectFirst(String tableName, String whereClause)
        {
            ArrayList arr = databaseSelect(tableName, whereClause);
            foreach(String[] row in arr){
                return row;
            }
            return new String[0];
        }

        public String[] databaseSelectLast(String tableName)
        {
            return databaseSelectLast(tableName, "");
        }

        public String[] databaseSelectLast(String tableName, String whereClause)
        {
            ArrayList arr = databaseSelect(tableName, "");
            String[] last = new String[0];
            foreach (String[] row in arr)
            {
                last = row;
            }
            return last;
        }

        public ArrayList databaseSelect(String tableName)
        {
            return databaseSelect(tableName, "");
        }

        //Sample Call: ArrayList arr = dbHelper.DatabaseSelect("User", "`email` = '" + EmailTextbox.Text + "' AND `password` = '" + pw + "'");
        public ArrayList databaseSelect(String tableName, String whereClause)
        {
            return databaseSelect(tableName, whereClause, "");
        }

        public ArrayList databaseSelect(String tableName, String whereClause, String orderCol)
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

                if (!orderCol.Equals(""))
                {
                    sql += " ORDER BY " + "`" + orderCol + "`";
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
                el.logError("DatabaseHelper : databaseSelect", ex.Message);
                return new ArrayList();
            }
        }

        public bool databaseInsertImageFromFile(String fileName, int propID)
        {
            byte[] image = File.ReadAllBytes(fileName);
            return databaseInsertImage("Image", propID.ToString(), image);
        }

        public bool databaseInsertImage(String tableName, String PropertyID, byte[] arr)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                string CmdString = "INSERT INTO `Image` (`PropID`, `Image`) VALUES(@PropID, @Image)";
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
                el.logError("DatabaseHelper : databaseInsertImage", ex.Message);
                return false;
            }
        }

        public bool databaseInsert(String tableName, String Columns, String Values)
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
                ErrorLogger el = new ErrorLogger();
                el.logError("DatabaseHelper : databaseInsert", ex.Message);
                return false;
            }
        }

        public bool databaseUpdate(String tableName, String updateColumns, String whereClause)
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
                ErrorLogger el = new ErrorLogger();
                el.logError("DatabaseHelper : databaseUpdate", ex.Message);
                return false;
            }
        }

        public bool databaseDelete(String tableName, String whereClause)
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
                ErrorLogger el = new ErrorLogger();
                el.logError("DatabaseHelper : databaseDelete", ex.Message);
                return false;
            }
        }
    }
}