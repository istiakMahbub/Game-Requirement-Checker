using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Net;
using System.Net.NetworkInformation;

namespace DemoProjectApplication2
{
    public partial class Form5 : Form
    {
        string image = "";
        public Form5()
        {
            InitializeComponent();
            button1.BackColor = Color.Navy;
            button2.BackColor = Color.Navy;
            button3.BackColor = Color.Navy;
            button4.BackColor = Color.Navy;
        }

        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool connection = NetworkInterface.GetIsNetworkAvailable();
            if (connection == true)
            {
                List<GameClass> gameList = new List<GameClass>();
                int id = 8514;
                for (int i = 0; i < 50; i++, id++)
                {

                    try
                    {
                        WebClient web = new WebClient();
                        string ss = "http://www.game-debate.com/games/index.php?g_id=" + id.ToString();
                        string url = web.DownloadString(ss);
                        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                        doc.LoadHtml(url);



                        HtmlNode nameNode = doc.DocumentNode.SelectSingleNode("//*[@id=\"art_g_title\"]/span[1]");
                        HtmlNode osNode = doc.DocumentNode.SelectSingleNode("//*[@id=\"systemRequirementsOuterBox\"]/div[3]/div[6]/span");
                        HtmlNode processorNode = doc.DocumentNode.SelectSingleNode("//*[@id=\"systemRequirementsOuterBox\"]/div[3]/div[3]/div[1]/div[1]/a");
                        HtmlNode ramNode = doc.DocumentNode.SelectSingleNode("//*[@id=\"systemRequirementsOuterBox\"]/div[3]/div[5]/div[1]/span");
                        HtmlNode gpuNode = doc.DocumentNode.SelectSingleNode("//*[@id=\"systemRequirementsOuterBox\"]/div[3]/div[4]/div[1]/div[1]/a");
                        HtmlNode directxNode = doc.DocumentNode.SelectSingleNode("//*[@id=\"systemRequirementsOuterBox\"]/div[3]/div[7]/span");
                        HtmlNode hddNode = doc.DocumentNode.SelectSingleNode("//*[@id=\"systemRequirementsOuterBox\"]/div[3]/div[8]/span");
                        HtmlNode imageNode = doc.DocumentNode.SelectSingleNode("//img[@class='gamepicimg']/@src");



                        string name = (nameNode.InnerText);
                        string os = (osNode.InnerText);
                        string processor = (processorNode.InnerText);
                        string ram = (ramNode.InnerText);
                        string gpu = (gpuNode.InnerText);
                        string directx = (directxNode.InnerText);
                        string hdd = (hddNode.InnerText);
                        string image = imageNode.Attributes["src"].Value;
                        var imageStream = HttpWebRequest.Create(image).GetResponse().GetResponseStream();


                        Image imageSave = Image.FromStream(imageStream);
                        Random r = new Random();
                        string fname = RemoveSpecialCharacters(name) + "_" + r.Next().ToString() + ".jpg";
                        imageSave.Save(@"C:\Users\user\Documents\Visual Studio 2013\Projects\DemoProjectApplication2\DemoProjectApplication2\bin\Debug\Image\" + fname);



                        GameClass gs = new GameClass();
                        gs.Name = name;
                        gs.Os = os;
                        gs.Processor = processor;
                        gs.Ram = ram;
                        gs.Gpu = gpu;
                        gs.DirectX = directx;
                        gs.Hdd = hdd;
                        gameList.Add(new GameClass() { Name = name, Os = os, Processor = processor, Ram = ram, Gpu = gpu, DirectX = directx, Hdd = hdd });


                    }

                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.ToString());
                    }

                }

                dataGridView1.DataSource = gameList;
            }
            else
                MessageBox.Show("Check Your Internet Connection");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != null)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["Name"].Value.ToString();
                textBox2.Text = row.Cells["Os"].Value.ToString();
                textBox3.Text = row.Cells["Processor"].Value.ToString();
                textBox4.Text = row.Cells["Ram"].Value.ToString();
                textBox5.Text = row.Cells["Gpu"].Value.ToString();
                textBox6.Text = row.Cells["DirectX"].Value.ToString();
                textBox7.Text = row.Cells["Hdd"].Value.ToString();

                textBox8.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
                pictureBox1.Image = null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string os = textBox2.Text;
            string processor = textBox3.Text;
            string ram = textBox4.Text;
            string gpu = textBox5.Text;
            string directx = textBox6.Text;
            string hdd = textBox7.Text;
            string release = textBox8.Text;
            string genre = textBox9.Text;
            int genre_id = Int32.Parse(textBox10.Text);

            Games_table gt = new Games_table();
            gt.name = name;
            gt.os = os;
            gt.processor = processor;
            gt.ram = ram;
            gt.gpu = gpu;
            gt.directx = directx;
            gt.hdd = hdd;
            gt.release = release;
            gt.genre_type = genre;
            gt.genre_id = genre_id;
            gt.image = image;

            DataClasses1DataContext dbcon = new DataClasses1DataContext(@"Data Source=(LocalDB)\v11.0;AttachDbFilename="+@"C:\Users\user\documents\visual studio 2013\Projects\DemoProjectApplication2\DemoProjectApplication2\Database1.mdf"+@";Integrated Security=True");
            dbcon.Games_tables.InsertOnSubmit(gt);
            dbcon.SubmitChanges();
            MessageBox.Show("Data Inserted");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class GameClass
    {
        public string Name { get; set; }
        public string Os { get; set; }
        public string Processor { get; set; }
        public string Ram { get; set; }
        public string Gpu { get; set; }
        public string DirectX { get; set; }
        public string Hdd { get; set; }
    }
}
