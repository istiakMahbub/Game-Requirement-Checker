using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoProjectApplication2
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            textBox2.PasswordChar = '\u25CF';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataClasses1DataContext dbcon = new DataClasses1DataContext(@"Data Source=(LocalDB)\v11.0;AttachDbFilename="+@"c:\users\user\documents\visual studio 2013\Projects\DemoProjectApplication2\DemoProjectApplication2\Database1.mdf"+@";Integrated Security=True"); 

            string username = textBox1.Text;
            string password = textBox2.Text;
            

            var un_check = dbcon.Admins.FirstOrDefault(x=>x.username==username);
            var pass_check = dbcon.Admins.FirstOrDefault(x=>x.password==password);

            if (un_check != null && pass_check != null)
            {
                Form4 f4 = new Form4();
                f4.Show();
                this.Close();
            }
            else 
                MessageBox.Show("Please check your username/password");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
      }
  }

