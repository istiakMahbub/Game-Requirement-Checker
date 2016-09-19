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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
            button1.BackColor = Color.Navy;
            button2.BackColor = Color.Navy;            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selectName = comboBox1.Text.ToString();
            DataClasses1DataContext dbcon = new DataClasses1DataContext(@"Data Source=(LocalDB)\v11.0;AttachDbFilename="+@"C:\Users\user\documents\visual studio 2013\Projects\DemoProjectApplication2\DemoProjectApplication2\Database1.mdf"+@";Integrated Security=True");
            Games_table found = dbcon.Games_tables.SingleOrDefault(x => x.name == selectName);

            if (found != null)
            {
                textBox1.Text = found.os;
                textBox2.Text = found.processor;
                textBox3.Text = found.ram;
                textBox4.Text = found.gpu;
                textBox5.Text = found.directx;
                textBox6.Text = found.hdd;
                textBox10.Text = found.release;
                textBox8.Text = found.genre_type;
                textBox9.Text = found.genre_id.ToString();
                pictureBox1.Image = Image.FromFile(found.image);
            }
            else
                MessageBox.Show("Not Found");
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.Games_table' table. You can move, or remove it, as needed.
            this.games_tableTableAdapter.Fill(this.database1DataSet.Games_table);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
