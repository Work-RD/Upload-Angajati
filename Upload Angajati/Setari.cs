using System;
using System.Windows.Forms;

namespace Upload_Angajati
{
    public partial class Setari : Form
    {
        public Setari()
        {
            InitializeComponent();
        }

		private void button1_Click(object sender, EventArgs e)
		{
			SetariInitiale.Default.serverIP = this.textBox1.Text;
			SetariInitiale.Default.databaseName = this.textBox2.Text;
			SetariInitiale.Default.portNo = this.textBox3.Text;
			SetariInitiale.Default.dUsername = this.textBox4.Text;
			SetariInitiale.Default.dPassword = this.textBox5.Text;
			SetariInitiale.Default.Save();
			Application.Restart();
		}
		/*private void Settings_Load(object sender, EventArgs e)
		{
			this.TopMost = true;
			this.textBox1.Text = SetariInitiale.Default.serverIP;
			this.textBox2.Text = SetariInitiale.Default.databaseName;
			this.textBox3.Text = SetariInitiale.Default.portNo;
			this.textBox4.Text = SetariInitiale.Default.dUsername;
			this.textBox5.Text = SetariInitiale.Default.dPassword;
			Form1.myConnectionString = string.Concat(new string[]
				{
					"Data Source=",
					this.textBox1.Text,
					",",
					this.textBox3.Text,
					";Network Library=DBMSSOCN;Initial Catalog=",
					this.textBox2.Text,
					";User ID=",
					this.textBox4.Text,
					";Password=",
					this.textBox5.Text,
					";"
				});
		}*/
	}
}
