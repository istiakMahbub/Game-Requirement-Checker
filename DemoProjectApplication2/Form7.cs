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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            DataClasses1DataContext dbcon = new DataClasses1DataContext(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=" + @"C:\Users\user\documents\visual studio 2013\Projects\DemoProjectApplication2\DemoProjectApplication2\Database1.mdf" + @";Integrated Security=True");
            comboBox1.DataSource = dbcon.Category1s;
            comboBox1.DisplayMember = "genre";
            comboBox1.ValueMember = "Id";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataClasses1DataContext dbcon = new DataClasses1DataContext(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=" + @"C:\Users\user\documents\visual studio 2013\Projects\DemoProjectApplication2\DemoProjectApplication2\Database1.mdf" + @";Integrated Security=True");
            int genre_id = Int32.Parse(comboBox1.SelectedValue.ToString());
            Category1 ct = dbcon.Category1s.SingleOrDefault(x => x.Id == genre_id);

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
               
                dataGridView1.Rows.Add(ap.Id, ap.name, ap.os, ap.processor, ap.ram, ap.gpu, ap.directx, ap.hdd, ap.release, ap.genre_type, ap.genre_id, ap.image);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != null)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
               
                textBox1.Text = row.Cells["Id"].Value.ToString();
                textBox2.Text = row.Cells["Name"].Value.ToString();
               
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(textBox1.Text);
            DataClasses1DataContext dbcon = new DataClasses1DataContext(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=" + @"C:\Users\user\documents\visual studio 2013\Projects\DemoProjectApplication2\DemoProjectApplication2\Database1.mdf" + @";Integrated Security=True");
            Games_table gt = dbcon.Games_tables.SingleOrDefault(x=>x.Id==id);

            if (gt != null)
            {
                dbcon.Games_tables.DeleteOnSubmit(gt);
                dbcon.SubmitChanges();
                MessageBox.Show("Data Deleted");

              
            }
            else
                MessageBox.Show("Data Not Found");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
