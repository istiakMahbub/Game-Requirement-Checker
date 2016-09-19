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
    public partial class Form6 : Form
    {
        string image = "";
        public Form6()
        {
            InitializeComponent();
            button1.BackColor = Color.Navy;
            button2.BackColor = Color.Navy;
            button3.BackColor = Color.Navy;
            button4.BackColor = Color.Navy;
            DataClasses1DataContext dbcon = new DataClasses1DataContext(@"Data Source=(LocalDB)\v11.0;AttachDbFilename="+@"C:\Users\user\documents\visual studio 2013\Projects\DemoProjectApplication2\DemoProjectApplication2\Database1.mdf"+@";Integrated Security=True");
            comboBox1.DataSource = dbcon.Category1s;
            comboBox1.ValueMember = "Id";
            comboBox1.DisplayMember = "genre";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != null)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox11.Text = row.Cells["Id"].Value.ToString();
                textBox1.Text = row.Cells["Name"].Value.ToString();
                textBox2.Text = row.Cells["Os"].Value.ToString();
                textBox3.Text = row.Cells["Processor"].Value.ToString();
                textBox4.Text = row.Cells["Ram"].Value.ToString();
                textBox5.Text = row.Cells["Gpu"].Value.ToString();
                textBox6.Text = row.Cells["DirectX"].Value.ToString();
                textBox7.Text = row.Cells["Hdd"].Value.ToString();

                textBox8.Text = row.Cells["Release"].Value.ToString();
                textBox9.Text = row.Cells["Genre_type"].Value.ToString();
                textBox10.Text = row.Cells["Genre_id"].Value.ToString();
                pictureBox1.Image = Image.FromFile(row.Cells["Image"].Value.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataClasses1DataContext dbcon = new DataClasses1DataContext(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=" + @"C:\Users\user\documents\visual studio 2013\Projects\DemoProjectApplication2\DemoProjectApplication2\Database1.mdf" + @";Integrated Security=True");
            int genre_id = Int32.Parse(comboBox1.SelectedValue.ToString());
            Category1 ct = dbcon.Category1s.SingleOrDefault(x=>x.Id==genre_id);

            dataGridView1.Rows.Clear();
            foreach (Games_table ap in ct.Games_tables)
            {
                dataGridView1.ColumnCount = 12;
                dataGridView1.Columns[0].Name = "Id";
                dataGridView1.Columns[1].Name = "name";
                dataGridView1.Columns[2].Name = "os";
                dataGridView1.Columns[3].Name = "processor";
                dataGridView1.Columns[4].Name = "ram";
                dataGridView1.Columns[5].Name = "gpu";
                dataGridView1.Columns[6].Name = "directx";
                dataGridView1.Columns[7].Name = "hdd";
                dataGridView1.Columns[8].Name = "release";
                dataGridView1.Columns[9].Name = "genre_type";
                dataGridView1.Columns[10].Name = "genre_id";
                dataGridView1.Columns[11].Name = "image";
                dataGridView1.Rows.Add(ap.Id, ap.name,ap.os,ap.processor,ap.ram,ap.gpu,ap.directx,ap.hdd,ap.release,ap.genre_type, ap.genre_id,ap.image);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Choose Image(*.jpg; *.png; *.gif)|*.jpg; *.png; *.gif;";
            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(ofd.FileName);
                image = ofd.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(textBox11.Text);
            DataClasses1DataContext dbcon = new DataClasses1DataContext(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=" + @"C:\Users\user\documents\visual studio 2013\Projects\DemoProjectApplication2\DemoProjectApplication2\Database1.mdf" + @";Integrated Security=True");
            Games_table gt = dbcon.Games_tables.SingleOrDefault(x=>x.Id==id);

            if (gt != null)
            {
                gt.name = textBox1.Text;
                gt.os = textBox2.Text;
                gt.processor = textBox3.Text;
                gt.ram = textBox4.Text;
                gt.gpu = textBox5.Text;
                gt.directx = textBox6.Text;
                gt.hdd = textBox7.Text;
                gt.release = textBox8.Text;
                gt.genre_type = textBox9.Text;
                gt.genre_id =Int32.Parse(textBox10.Text);
                gt.image = image;
                dbcon.SubmitChanges();
                MessageBox.Show("Data Updated");

                textBox1.Text ="";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
                pictureBox1.Image = null;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
