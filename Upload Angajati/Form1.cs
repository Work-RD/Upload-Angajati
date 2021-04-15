using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace Upload_Angajati
{
    public partial class Form1 : Form
    {
        Timer timer1 = new Timer
        {
            Interval = 900000  
        };

        /*Timer timer2 = new Timer
        {
            Interval = 300000
        };*/

        public bool conexiune = false;
        public string ip = "";
        public string pc = "";
        public string error1 = "";
        public string error2= "";
        public string error3 = "";
        public string error4 = "";
        public string error5 = "";
        public string error6 = "";
        public string autobuz = "";

        //private SqlConnection myConnection = new SqlConnection(Form1.myConnectionString);
        private MySqlConnection myConn = new MySqlConnection(Form1.myConnString);
        public Form1()
        {
            InitializeComponent();
            timer1.Tick += new System.EventHandler(OnTimerEventUpdateSQL);
            timer1.Enabled = true;
            //timer2.Tick += new System.EventHandler(OnTimerEventConnectVPN);
            //timer2.Enabled = true;
        }

        public static string myConnString = string.Concat(new string[]
        {
            "server=",
            "serverName",
            ";uid=",
            "user",
            ";password=",
            "password",
            ";database=",
            "databaseName",
            ";port=",
            "portNumber",
            ";"
        });

        private void Form1_Load(object sender, EventArgs e)
        {
            /* try
             {
                 this.myConnection.Open();
                 this.myConnection.Close();
                 timer1.Tick += new System.EventHandler(OnTimerEventUpdateSQL);
                 timer1.Enabled = true;
             }
             catch (Exception ex)
             {
                 int num = (int)MessageBox.Show("Setari baza de date incorecte!\r\n");
                 int num2 = (int)new Setari().ShowDialog();
             }*/
        }

        /*private void OnTimerEventConnectVPN(object source, EventArgs e)
        {
            try
            {
                this.myConnection.Open();
                this.myConnection.Close();
                timer2.Enabled = false;
                conexiune = true;
                getIPv4();
            }
            catch (Exception ex)
            {
                conexiune = false;
            }
            if (conexiune == false)
            {
                Process scriptProc = new Process();
                scriptProc.StartInfo.FileName = @"C:\Transport\transport.vbs";
                scriptProc.Start();
                scriptProc.WaitForExit();
                scriptProc.Close();
            }
        }*/

        /*private void getIPv4()
        {
            try
            {
                var vpn = NetworkInterface.GetAllNetworkInterfaces().First(x => x.Description == "Cisco AnyConnect Secure Mobility Client Virtual Miniport Adapter for Windows x64");
                ip = vpn.GetIPProperties().UnicastAddresses.First(x => x.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Address.ToString();
                pc = Environment.MachineName;
                this.myConnection.Close();
                this.myConnection.Open();
                string query = "UPDATE autobuz SET ip = '" + ip + "', data = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE pc = '" + pc + "'";
                SqlCommand myCommand = new SqlCommand(query, this.myConnection);
                myCommand.ExecuteNonQuery();
                this.myConnection.Close();
            }
            catch (Exception)
            {
            }
        }*/

        private void OnTimerEventUpdateSQL(object source, EventArgs e)
        {
            error1 = "NU";
            error2 = "NU";
            error3 = "NU";
            error4 = "NU";
            error5 = "NU";
            error6 = "NU";
            /// primul import
            try
            {
                string path = @"C:\Transport\Transport Angajati RESPINS.txt";
                StreamReader streamReader = new StreamReader(path);
                string line = "";
                string filedelimiter = ",";

                this.myConn.Close();
                this.myConn.Open();
                while ((line = streamReader.ReadLine()) != null)
                {
                    string query = "INSERT INTO transport (numar_angajat, numar_cartela, data_transport, ruta, schimb, accept, transport) VALUES ('" + line.Replace(filedelimiter, "','") + "')";
                    MySqlCommand myCommand = new MySqlCommand(query, this.myConn);
                    myCommand.ExecuteNonQuery();
                }
                streamReader.Close();
                File.Delete(path);
                this.myConn.Close();
            }
            catch (Exception)
            {
                error1 = "DA";
            }
            /// al doilea import
            try
            {
                string path = @"C:\Transport\Transport Angajati NEGASIT.txt";
                StreamReader streamReader = new StreamReader(path);
                string line = "";
                string filedelimiter = ",";

                this.myConn.Close();
                this.myConn.Open();
                while ((line = streamReader.ReadLine()) != null)
                {
                    string query = "INSERT INTO transport (numar_angajat, numar_cartela, data_transport, ruta, schimb, loc,accept, transport) VALUES ('" + line.Replace(filedelimiter, "','") + "')";
                    MySqlCommand myCommand = new MySqlCommand(query, this.myConn);
                    myCommand.ExecuteNonQuery();
                }
                streamReader.Close();
                File.Delete(path);
                this.myConn.Close();
            }
            catch (Exception)
            {
                error2 = "DA";
            }
            /// al treilea import
            try
            {
                string path = @"C:\Transport\Transport Angajati ACCEPTAT.txt";
                StreamReader streamReader = new StreamReader(path);
                string line = "";
                string filedelimiter = ",";

                this.myConn.Close();
                this.myConn.Open();
                while ((line = streamReader.ReadLine()) != null)
                {
                    string query = "INSERT INTO transport (numar_angajat, numar_cartela, data_transport, ruta, schimb, loc,accept, transport) VALUES ('" + line.Replace(filedelimiter, "','") + "')";
                    MySqlCommand myCommand = new MySqlCommand(query, myConn);
                    myCommand.ExecuteNonQuery();
                }
                streamReader.Close();
                File.Delete(path);
                this.myConn.Close();
            }
            catch (Exception)
            {
                error3 = "DA";
            }
            /// al patrulea import
            try
            {
                string path = @"C:\Transport\Transport Angajati GASIT.txt";
                StreamReader streamReader = new StreamReader(path);
                string line = "";
                string filedelimiter = ",";

                this.myConn.Close();
                this.myConn.Open();
                while ((line = streamReader.ReadLine()) != null)
                {
                    string query = "INSERT INTO transport (numar_angajat, numar_cartela, data_transport, ruta, schimb, loc, transport) VALUES ('" + line.Replace(filedelimiter, "','") + "')";
                    MySqlCommand myCommand = new MySqlCommand(query, this.myConn);
                    myCommand.ExecuteNonQuery();
                }
                streamReader.Close();
                File.Delete(path);
                this.myConn.Close();
            }
            catch (Exception)
            {
                error4 = "DA";
            }
            /// al cincilea import
            try
            {
                string path = @"C:\Transport\Transport Angajati LIPSA NEGASIT.txt";
                StreamReader streamReader = new StreamReader(path);
                string line = "";
                string filedelimiter = ",";

                this.myConn.Close();
                this.myConn.Open();
                while ((line = streamReader.ReadLine()) != null)
                {
                    string query = "INSERT INTO transport (numar_angajat, numar_cartela, data_transport, ruta, schimb, accept, transport) VALUES ('" + line.Replace(filedelimiter, "','") + "')";
                    MySqlCommand myCommand = new MySqlCommand(query, this.myConn);
                    myCommand.ExecuteNonQuery();
                }
                streamReader.Close();
                File.Delete(path);
                this.myConn.Close();
            }
            catch (Exception)
            {
                error5 = "DA";
            }
            /// al saselea import
            try
            {
                string path = @"C:\Transport\Transport Angajati LIPSA GASIT.txt";
                StreamReader streamReader = new StreamReader(path);
                string line = "";
                string filedelimiter = ",";

                this.myConn.Close();
                this.myConn.Open();
                while ((line = streamReader.ReadLine()) != null)
                {
                    string query = "INSERT INTO transport (numar_angajat, numar_cartela, data_transport, ruta, schimb, accept, transport) VALUES ('" + line.Replace(filedelimiter, "','") + "')";
                    MySqlCommand myCommand = new MySqlCommand(query, this.myConn);
                    myCommand.ExecuteNonQuery();
                }
                streamReader.Close();
                File.Delete(path);
                this.myConn.Close();
            }
            catch (Exception)
            {
                error6 = "DA";
            }

            if (error1 != "DA" || error2 != "DA" || error3 != "DA" || error4 != "DA" || error5 != "DA" || error6 != "DA")
            {
                try
                {
                    pc = Environment.MachineName;

                    this.myConn.Close();
                    this.myConn.Open();
                    MySqlDataReader sqlDataReader = new MySqlCommand("SELECT autobuz FROM autobuz WHERE pc='" + pc + "'", this.myConn).ExecuteReader();
                    bool flag = sqlDataReader.Read();
                    if (flag)
                    {
                        autobuz = sqlDataReader.GetValue(0).ToString();
                    }
                    this.myConn.Close();

                    /*try
                    {
                        var vpn = NetworkInterface.GetAllNetworkInterfaces().First(x => x.Description == "Cisco AnyConnect Secure Mobility Client Virtual Miniport Adapter for Windows x64");
                        ip = vpn.GetIPProperties().UnicastAddresses.First(x => x.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Address.ToString();
                    }
                    catch (Exception)
                    {

                    }*/

                    this.myConn.Close();
                    this.myConn.Open();
                    new MySqlCommand(string.Concat(new string[]
                                    {
                               "INSERT INTO evidenta (autobuz, pc, data) VALUES ('",
                                autobuz,
                                "', '",
                                pc,
                                "', '",
                                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                "')"
                                    }), this.myConn).ExecuteNonQuery();
                    this.myConn.Close();
                }
                catch (Exception)
                {

                }

            }

            ExportDataFromSQLServerToSQLLite();
        }

        public void ExportDataFromSQLServerToSQLLite()
        {
            try
            {
                if (!Directory.Exists("Upload"))
                {
                    Directory.CreateDirectory("Upload");
                }
                SQLiteConnection.CreateFile("Upload/Rute.sqlite");
                SQLiteConnection sqlLiteCon = new SQLiteConnection("Data Source=Upload/Rute.sqlite;Version=3;");

                sqlLiteCon.Open();
                new SQLiteCommand("CREATE TABLE IF NOT EXISTS autobuz (autobuz varchar(50), locuri varchar(50), locuri_rezerva varchar(50), pc varchar(50), ruta varchar(50))", sqlLiteCon).ExecuteNonQuery();
                new SQLiteCommand("CREATE TABLE IF NOT EXISTS personal (numar varchar(128), numar_cartela varchar(10))", sqlLiteCon).ExecuteNonQuery();
                new SQLiteCommand("CREATE TABLE IF NOT EXISTS rute (numar_angajat varchar(128), nevoie_transport varchar(10), ruta varchar(128), schimb varchar(8), data datetime, data_final datetime, loc varchar(50))", sqlLiteCon).ExecuteNonQuery();
                new SQLiteCommand("CREATE TABLE IF NOT EXISTS transport (numar_angajat varchar(128), numar_cartela varchar(128), data_transport datetime, ruta varchar(50), schimb varchar(50), loc varchar(50), transport varchar(50))", sqlLiteCon).ExecuteNonQuery();
                sqlLiteCon.Close();


                DataTable dataTable = new DataTable();

                string query = "SELECT autobuz, locuri, locuri_rezerva, pc, ruta FROM autobuz WHERE retired != 'da' OR retired IS NULL";
                MySqlCommand cmd = new MySqlCommand(query, this.myConn);
                this.myConn.Close();
                this.myConn.Open();
                MySqlDataAdapter dadapter = new MySqlDataAdapter(cmd);
                dadapter.Fill(dataTable);
                this.myConn.Close();
                dadapter.Dispose();

                foreach (DataRow row in dataTable.Rows)
                {
                    sqlLiteCon.Open();
                    new SQLiteCommand(string.Concat(new string[]
                                    {
                                "INSERT INTO autobuz (autobuz, locuri, locuri_rezerva, pc, ruta) VALUES ('",
                                row["autobuz"].ToString(),
                                "', '",
                                row["locuri"].ToString(),
                                "', '",
                                row["locuri_rezerva"].ToString(),
                                "', '",
                                row["pc"].ToString(),
                                "', '",
                                row["ruta"].ToString(),
                                "')"
                                }), sqlLiteCon).ExecuteNonQuery();
                    sqlLiteCon.Close();
                }

                DataTable dataTable1 = new DataTable();

                string query1 = "SELECT numar, numar_cartela FROM personal WHERE numar_cartela != '' AND plecat = 'nu'";
                MySqlCommand cmd1 = new MySqlCommand(query1, this.myConn);
                this.myConn.Close();
                this.myConn.Open();
                MySqlDataAdapter dadapter1 = new MySqlDataAdapter(cmd1);
                dadapter1.Fill(dataTable1);
                this.myConn.Close();
                dadapter1.Dispose();

                foreach (DataRow row1 in dataTable1.Rows)
                {
                    sqlLiteCon.Open();
                    new SQLiteCommand(string.Concat(new string[]
                                   {
                                "INSERT INTO personal (numar, numar_cartela) VALUES ('",
                                row1["numar"].ToString(),
                                "', '",
                                row1["numar_cartela"].ToString(),
                                "')"
                                }), sqlLiteCon).ExecuteNonQuery();
                    sqlLiteCon.Close();
                }

                DateTime lastSaturday = DateTime.Now.AddDays(-1);
                while (lastSaturday.DayOfWeek != DayOfWeek.Saturday)
                {
                    lastSaturday = lastSaturday.AddDays(-1);
                }
                string lastSaturday2 = lastSaturday.ToString("yyyy-MM-dd HH:mm:ss");

                DataTable dataTable2 = new DataTable();

                string query2 = "SELECT numar_angajat, nevoie_transport, ruta, schimb, DATE_FORMAT(data, '%Y-%m-%d %H:%i:%s') as data, DATE_FORMAT(data_final, '%Y-%m-%d %H:%i:%s') as data_final, loc FROM rute WHERE data >='" + lastSaturday2 + "'";
                MySqlCommand cmd2 = new MySqlCommand(query2, this.myConn);
                this.myConn.Close();
                this.myConn.Open();
                MySqlDataAdapter dadapter2 = new MySqlDataAdapter(cmd2);
                dadapter2.Fill(dataTable2);
                this.myConn.Close();
                dadapter2.Dispose();

                foreach (DataRow row2 in dataTable2.Rows)
                {
                    sqlLiteCon.Open();
                    new SQLiteCommand(string.Concat(new string[]
                                  {
                                "INSERT INTO rute (numar_angajat, nevoie_transport, ruta, schimb, data, data_final, loc) VALUES ('",
                                row2["numar_angajat"].ToString(),
                                "', '",
                                row2["nevoie_transport"].ToString(),
                                "', '",
                                row2["ruta"].ToString(),
                                "', '",
                                row2["schimb"].ToString(),
                                "', '",
                                row2["data"].ToString(),
                                "', '",
                                row2["data_final"].ToString(),
                                 "', '",
                                row2["loc"].ToString(),
                                "')"
                                }), sqlLiteCon).ExecuteNonQuery();
                    sqlLiteCon.Close();
                }

                DataTable dataTable3 = new DataTable();

                string query3 = "SELECT numar_angajat, numar_cartela, DATE_FORMAT(data_transport, '%Y-%m-%d') as data_transport, ruta, schimb, loc, transport FROM transport WHERE DATE_FORMAT(data_transport, '%Y-%m-%d') = CURDATE()";
                MySqlCommand cmd3 = new MySqlCommand(query3, this.myConn);
                this.myConn.Close();
                this.myConn.Open();
                MySqlDataAdapter dadapter3 = new MySqlDataAdapter(cmd3);
                dadapter3.Fill(dataTable3);
                this.myConn.Close();
                dadapter3.Dispose();

                foreach (DataRow row3 in dataTable3.Rows)
                {
                    sqlLiteCon.Open();
                    new SQLiteCommand(string.Concat(new string[]
                                    {
                                "INSERT INTO transport (numar_angajat, numar_cartela, data_transport, ruta, schimb, loc, transport) VALUES ('",
                                row3["numar_angajat"].ToString(),
                                "', '",
                                row3["numar_cartela"].ToString(),
                                "', '",
                                row3["data_transport"].ToString(),
                                "', '",
                                row3["ruta"].ToString(),
                                "', '",
                                row3["schimb"].ToString(),
                                "', '",
                                row3["loc"].ToString(),
                                "', '",
                                row3["transport"].ToString(),
                                "')"
                                    }), sqlLiteCon).ExecuteNonQuery();
                    sqlLiteCon.Close();
                }
                FileInfo file = new FileInfo("Rute.sqlite");
                if (File.Exists("Rute.sqlite") && IsFileLocked(file) == false)
                {
                    ReplaceFile("Upload/Rute.sqlite", "Rute.sqlite", "Upload/Rute.bac");
                }
            }
            catch (Exception)
            {

            }
        }
        public static void ReplaceFile(string FileToMoveAndDelete, string FileToReplace, string BackupOfFileToReplace)
        {
            File.Replace(FileToMoveAndDelete, FileToReplace, BackupOfFileToReplace, false);
        }

        protected virtual bool IsFileLocked(FileInfo file)
        {
            try
            {
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                return true;
            }

            return false;
        }

        private void label1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            error1 = "NU";
            error2 = "NU";
            error3 = "NU";
            error4 = "NU";
            error5 = "NU";
            error6 = "NU";
            try
            {
                /// primul import
                string path = @"C:\Transport\Transport Angajati RESPINS.txt";
                StreamReader streamReader = new StreamReader(path);
                string line = "";
                string filedelimiter = ",";
                this.myConn.Close();
                this.myConn.Open();
                while ((line = streamReader.ReadLine()) != null)
                {
                    string query = "INSERT INTO transport (numar_angajat, numar_cartela, data_transport, ruta, schimb, accept, transport) VALUES ('" + line.Replace(filedelimiter, "','") + "')";
                    MySqlCommand myCommand = new MySqlCommand(query, this.myConn);
                    myCommand.ExecuteNonQuery();
                }
                streamReader.Close();
                File.Delete(path);
                this.myConn.Close();
            }
            catch (Exception)
            {
                error1 = "DA";
            }
            try
            {
                /// al doilea import
                string path1 = @"C:\Transport\Transport Angajati NEGASIT.txt";
                StreamReader streamReader1 = new StreamReader(path1);
                string line1 = "";
                string filedelimiter1 = ",";
                this.myConn.Close();
                this.myConn.Open();
                while ((line1 = streamReader1.ReadLine()) != null)
                {
                    string query1 = "INSERT INTO transport (numar_angajat, numar_cartela, data_transport, ruta, schimb, loc,accept, transport) VALUES ('" + line1.Replace(filedelimiter1, "','") + "')";
                    MySqlCommand myCommand1 = new MySqlCommand(query1, this.myConn);
                    myCommand1.ExecuteNonQuery();
                }
                streamReader1.Close();
                File.Delete(path1);
                this.myConn.Close();
            }
            catch (Exception)
            {
                error2 = "DA";
            }
            try
            {
                /// al treilea import
                string path2 = @"C:\Transport\Transport Angajati ACCEPTAT.txt";
                StreamReader streamReader2 = new StreamReader(path2);
                string line2 = "";
                string filedelimiter2 = ",";
                this.myConn.Close();
                this.myConn.Open();
                while ((line2 = streamReader2.ReadLine()) != null)
                {
                    string query2 = "INSERT INTO transport (numar_angajat, numar_cartela, data_transport, ruta, schimb, loc,accept, transport) VALUES ('" + line2.Replace(filedelimiter2, "','") + "')";
                    MySqlCommand myCommand2 = new MySqlCommand(query2, this.myConn);
                    myCommand2.ExecuteNonQuery();
                }
                streamReader2.Close();
                File.Delete(path2);
                this.myConn.Close();
            }
            catch (Exception)
            {
                error3 = "DA";
            }
            try
            {
                /// al patrulea import
                string path3 = @"C:\Transport\Transport Angajati GASIT.txt";
                StreamReader streamReader3 = new StreamReader(path3);
                string line3 = "";
                string filedelimiter3 = ",";
                this.myConn.Close();
                this.myConn.Open();
                while ((line3 = streamReader3.ReadLine()) != null)
                {
                    string query3 = "INSERT INTO transport (numar_angajat, numar_cartela, data_transport, ruta, schimb, loc, transport) VALUES ('" + line3.Replace(filedelimiter3, "','") + "')";
                    MySqlCommand myCommand3 = new MySqlCommand(query3, this.myConn);
                    myCommand3.ExecuteNonQuery();
                }
                streamReader3.Close();
                File.Delete(path3);
                this.myConn.Close();
            }
            catch (Exception)
            {
                error4 = "DA";
            }
            try
            {
                /// al cincilea import
                string path4 = @"C:\Transport\Transport Angajati LIPSA NEGASIT.txt";
                StreamReader streamReader4 = new StreamReader(path4);
                string line4 = "";
                string filedelimiter4 = ",";
                this.myConn.Close();
                this.myConn.Open();
                while ((line4 = streamReader4.ReadLine()) != null)
                {
                    string query4 = "INSERT INTO transport (numar_angajat, numar_cartela, data_transport, ruta, schimb, accept, transport) VALUES ('" + line4.Replace(filedelimiter4, "','") + "')";
                    MySqlCommand myCommand4 = new MySqlCommand(query4, this.myConn);
                    myCommand4.ExecuteNonQuery();
                }
                streamReader4.Close();
                File.Delete(path4);
                this.myConn.Close();
            }
            catch (Exception)
            {
                error5 = "DA";
            }
            try
            {
                /// al saselea import
                string path5 = @"C:\Transport\Transport Angajati LIPSA GASIT.txt";
                StreamReader streamReader5 = new StreamReader(path5);
                string line5 = "";
                string filedelimiter5 = ",";
                this.myConn.Close();
                this.myConn.Open();
                while ((line5 = streamReader5.ReadLine()) != null)
                {
                    string query5 = "INSERT INTO transport (numar_angajat, numar_cartela, data_transport, ruta, schimb, accept, transport) VALUES ('" + line5.Replace(filedelimiter5, "','") + "')";
                    MySqlCommand myCommand5 = new MySqlCommand(query5, this.myConn);
                    myCommand5.ExecuteNonQuery();
                }
                streamReader5.Close();
                File.Delete(path5);
                this.myConn.Close();
            }
            catch (Exception)
            {
                error6 = "DA";
            }

            if (error1 != "DA" || error2 != "DA" || error3 != "DA" || error4 != "DA" || error5 != "DA" || error6 != "DA")
            {
                MessageBox.Show("Date incarcate cu succes.");
            }
            else
            {
                MessageBox.Show("Eroare incarcare date.");
            }
        }

        /*private void label1_DoubleClick(object sender, EventArgs e)
        {
            int num = (int)new Setari().ShowDialog();
        }*/
    }
}