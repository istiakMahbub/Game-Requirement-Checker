using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using Microsoft.Win32;
using System.IO;
using System.Text.RegularExpressions;

namespace DemoProjectApplication2
{
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
            button1.BackColor = Color.Navy;
            button2.BackColor = Color.Navy;
            button3.BackColor = Color.Navy;
            DataClasses1DataContext dbcon = new DataClasses1DataContext(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=" + @"C:\Users\user\documents\visual studio 2013\Projects\DemoProjectApplication2\DemoProjectApplication2\Database1.mdf" + @";Integrated Security=True");
            comboBox1.DataSource = dbcon.Games_tables;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "name";
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = (from x in new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get().OfType<ManagementObject>()
                             select x.GetPropertyValue("Caption")).FirstOrDefault().ToString();

            var cpu = new ManagementObjectSearcher("select * from Win32_Processor").Get().Cast<ManagementObject>().First();
            textBox2.Text = (string)cpu["Name"];



            ManagementObjectSearcher ram = new ManagementObjectSearcher("Select * From Win32_ComputerSystem");
            foreach (ManagementObject WniPART in ram.Get())
            {
                UInt64 r1 = Convert.ToUInt64(WniPART["TotalPhysicalMemory"]);
                UInt64 r2 = r1 / 1000 / 1000 / 1000;
                textBox3.Text = r2.ToString() + " " + "GB";
            }

            ManagementObjectSearcher gpu = new ManagementObjectSearcher("select * from Win32_VideoController");
            foreach (ManagementObject WniPART in gpu.Get())
            {
                textBox4.Text = (string)WniPART["Name"];
            }



            DriveInfo[] alldrives = DriveInfo.GetDrives();
            UInt64 total = 0;
            foreach (DriveInfo d in alldrives)
            {
                if (d.IsReady == true)
                {

                    UInt64 hdd = Convert.ToUInt64(d.TotalFreeSpace);
                    UInt64 hdd2 = hdd / 1000 / 1000 / 1000;
                    total += hdd2;

                }
                textBox6.Text = total.ToString() + " " + "GB";
            }

            textBox5.Text = GetDirectxMajorVersion().ToString();
        }

        public int GetDirectxMajorVersion()
        {
            int directxMajorVersion = 0;

            var OSVersion = Environment.OSVersion;

            // if Windows Vista or later
            if (OSVersion.Version.Major >= 6)
            {
                // if Windows 7 or later
                if (OSVersion.Version.Major > 6 || OSVersion.Version.Minor >= 1)
                {
                    directxMajorVersion = 11;
                }
                // if Windows Vista
                else
                {
                    directxMajorVersion = 10;
                }
            }
            // if Windows XP or earlier.
            else
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\DirectX"))
                {
                    string versionStr = key.GetValue("Version") as string;
                    if (!string.IsNullOrEmpty(versionStr))
                    {
                        var versionComponents = versionStr.Split('.');
                        if (versionComponents.Length > 1)
                        {
                            int directXLevel;
                            if (int.TryParse(versionComponents[1], out directXLevel))
                            {
                                directxMajorVersion = directXLevel;
                            }
                        }
                    }
                }
            }

            return directxMajorVersion;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectName = comboBox1.Text.ToString();
            DataClasses1DataContext dbcon = new DataClasses1DataContext(@"Data Source=(LocalDB)\v11.0;AttachDbFilename="+@"C:\Users\user\documents\visual studio 2013\Projects\DemoProjectApplication2\DemoProjectApplication2\Database1.mdf"+@";Integrated Security=True");
            Games_table found = dbcon.Games_tables.SingleOrDefault(x => x.name == selectName);

            if (found != null)
            {
                textBox12.Text = found.os;
                textBox11.Text = found.processor;
                textBox10.Text = found.ram;
                textBox9.Text = found.gpu;
                textBox8.Text = found.directx;
                textBox7.Text = found.hdd;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string myString = textBox6.Text;
            string newString = myString.Remove(myString.IndexOf(' '));
            int hddCompare = Int32.Parse(newString);

            string myString1 = textBox7.Text;
            string newString1 = myString1.Remove(myString1.IndexOf(' '));
            int hddCompare1 = Int32.Parse(newString1);

            if (hddCompare >= hddCompare1)
            {
                textBox6.ForeColor = System.Drawing.Color.Green;
                textBox7.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                textBox6.ForeColor = System.Drawing.Color.Red;
                textBox7.ForeColor = System.Drawing.Color.Red;
            }

            string DXstring = textBox8.Text;
            string newDXstring = DXstring.Split(' ').Last();
            int compareDXstring = Int32.Parse(newDXstring);
            int compareDXstring1 = Int32.Parse(textBox5.Text);

            if (compareDXstring1 >= compareDXstring)
            {
                textBox5.ForeColor = System.Drawing.Color.Green;
                textBox8.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                textBox5.ForeColor = System.Drawing.Color.Red;
                textBox8.ForeColor = System.Drawing.Color.Red;
            }

            string compareRam = textBox3.Text.Split(' ').First();
            string compareRam1 = textBox10.Text.Split(' ').First();
            int compareRamPC = Int32.Parse(compareRam);
            int compareRamGm = Int32.Parse(compareRam1);
            if (compareRamPC >= compareRamGm)
            {
                textBox3.ForeColor = System.Drawing.Color.Green;
                textBox10.ForeColor = System.Drawing.Color.Green;
            }
            else if (compareRam1.Length > 2)
            {
                textBox3.ForeColor = System.Drawing.Color.Green;
                textBox10.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                textBox3.ForeColor = System.Drawing.Color.Red;
                textBox10.ForeColor = System.Drawing.Color.Red;
            }

            string core2 = "i3";
            string core4 = "i5";
            string core4s = "i7";


            if (Regex.IsMatch(textBox2.Text, core2))
            {
                if (Regex.IsMatch(textBox11.Text, core2))
                {
                    textBox2.ForeColor = System.Drawing.Color.Green;
                    textBox11.ForeColor = System.Drawing.Color.Green;
                }
                else if (Regex.IsMatch(textBox11.Text, "Pentium"))
                {
                    textBox2.ForeColor = System.Drawing.Color.Green;
                    textBox11.ForeColor = System.Drawing.Color.Green;
                }
                else if (Regex.IsMatch(textBox11.Text, "Celeron"))
                {
                    textBox2.ForeColor = System.Drawing.Color.Green;
                    textBox11.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    textBox2.ForeColor = System.Drawing.Color.Red;
                    textBox11.ForeColor = System.Drawing.Color.Red;
                }
            }
            else if (Regex.IsMatch(textBox2.Text, core4))
            {
                if (Regex.IsMatch(textBox11.Text, core2))
                {
                    textBox2.ForeColor = System.Drawing.Color.Green;
                    textBox11.ForeColor = System.Drawing.Color.Green;
                }
                else if (Regex.IsMatch(textBox11.Text, "Duo"))
                {
                    textBox2.ForeColor = System.Drawing.Color.Green;
                    textBox11.ForeColor = System.Drawing.Color.Green;
                }
                else if (Regex.IsMatch(textBox11.Text, core4))
                {
                    textBox2.ForeColor = System.Drawing.Color.Green;
                    textBox11.ForeColor = System.Drawing.Color.Green;
                }
                else if (Regex.IsMatch(textBox11.Text, "Pentium"))
                {
                    textBox2.ForeColor = System.Drawing.Color.Green;
                    textBox11.ForeColor = System.Drawing.Color.Green;
                }
                else if (Regex.IsMatch(textBox11.Text, "Celeron"))
                {
                    textBox2.ForeColor = System.Drawing.Color.Green;
                    textBox11.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    textBox2.ForeColor = System.Drawing.Color.Red;
                    textBox11.ForeColor = System.Drawing.Color.Red;
                }

            }
            else if (Regex.IsMatch(textBox2.Text, core4s))
            {

                textBox2.ForeColor = System.Drawing.Color.Green;
                textBox11.ForeColor = System.Drawing.Color.Green;
            }

            string win7 = "Windows 7";
            string win8 = "Windows 8";
            string win81 = "Windows 8.1";
            string win10 = "Windows 10";
            string winold = "Windsows Vista";
            string winxp = "Windows Xp";

            if (Regex.IsMatch(textBox1.Text, win10))
            {
                textBox1.ForeColor = System.Drawing.Color.Green;
                textBox12.ForeColor = System.Drawing.Color.Green;
            }
            else if (Regex.IsMatch(textBox1.Text, win8))
            {
                if (Regex.IsMatch(textBox12.Text, "10"))
                {
                    textBox1.ForeColor = System.Drawing.Color.Red;
                    textBox12.ForeColor = System.Drawing.Color.Red;
                }
                else if (Regex.IsMatch(textBox12.Text, "8.1"))
                {
                    textBox1.ForeColor = System.Drawing.Color.Red;
                    textBox12.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    textBox1.ForeColor = System.Drawing.Color.Green;
                    textBox12.ForeColor = System.Drawing.Color.Green;
                }
            }
            else if (Regex.IsMatch(textBox1.Text, win7))
            {
                if (Regex.IsMatch(textBox12.Text, "10"))
                {
                    textBox1.ForeColor = System.Drawing.Color.Red;
                    textBox12.ForeColor = System.Drawing.Color.Red;
                }
                else if (Regex.IsMatch(textBox12.Text, "8.1"))
                {
                    textBox1.ForeColor = System.Drawing.Color.Red;
                    textBox12.ForeColor = System.Drawing.Color.Red;
                }
                else if (Regex.IsMatch(textBox12.Text, "8"))
                {
                    textBox1.ForeColor = System.Drawing.Color.Red;
                    textBox12.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    textBox1.ForeColor = System.Drawing.Color.Green;
                    textBox12.ForeColor = System.Drawing.Color.Green;
                }
            }
            else if (Regex.IsMatch(textBox1.Text, win81))
            {
                if (Regex.IsMatch(textBox12.Text, "10"))
                {
                    textBox1.ForeColor = System.Drawing.Color.Red;
                    textBox12.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    textBox1.ForeColor = System.Drawing.Color.Green;
                    textBox12.ForeColor = System.Drawing.Color.Green;
                }
            }
            else if (Regex.IsMatch(textBox1.Text, winold))
            {
                if (Regex.IsMatch(textBox12.Text, "Vista"))
                {
                    textBox1.ForeColor = System.Drawing.Color.Green;
                    textBox12.ForeColor = System.Drawing.Color.Green;
                }
                else if (Regex.IsMatch(textBox12.Text, "98"))
                {
                    textBox1.ForeColor = System.Drawing.Color.Green;
                    textBox12.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    textBox1.ForeColor = System.Drawing.Color.Red;
                    textBox12.ForeColor = System.Drawing.Color.Red;
                }
            }
            else if (Regex.IsMatch(textBox1.Text, winxp))
            {
                if (Regex.IsMatch(textBox12.Text, "Xp"))
                {
                    textBox1.ForeColor = System.Drawing.Color.Green;
                    textBox12.ForeColor = System.Drawing.Color.Green;
                }
                else if (Regex.IsMatch(textBox12.Text, "98"))
                {
                    textBox1.ForeColor = System.Drawing.Color.Green;
                    textBox12.ForeColor = System.Drawing.Color.Green;
                }
                else if (Regex.IsMatch(textBox12.Text, "Vista"))
                {
                    textBox1.ForeColor = System.Drawing.Color.Green;
                    textBox12.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    textBox1.ForeColor = System.Drawing.Color.Red;
                    textBox12.ForeColor = System.Drawing.Color.Red;
                }
            }

            if (Regex.IsMatch(textBox4.Text, "Intel"))
            {
                if (Regex.IsMatch(textBox9.Text, "Intel"))
                {
                    textBox4.ForeColor = System.Drawing.Color.Green;
                    textBox9.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    textBox4.ForeColor = System.Drawing.Color.YellowGreen;
                    textBox9.ForeColor = System.Drawing.Color.YellowGreen;

                    string warning = "You may face some problem with this";
                    label14.Visible = true;
                    label14.Text = warning;
                }
            }
            else if (Regex.IsMatch(textBox4.Text, "Amd"))
            {

                if (Regex.IsMatch(textBox9.Text, "GeForce"))
                {
                    textBox4.ForeColor = System.Drawing.Color.YellowGreen;
                    textBox9.ForeColor = System.Drawing.Color.YellowGreen;

                    string warning = "You may face some problem with this";
                    label14.Visible = true;
                    label14.Text = warning;
                }
                else if (Regex.IsMatch(textBox9.Text, "Intel"))
                {
                    textBox4.ForeColor = System.Drawing.Color.YellowGreen;
                    textBox9.ForeColor = System.Drawing.Color.YellowGreen;

                    string warning = "You may face some problem with this";
                    label14.Visible = true;
                    label14.Text = warning;
                }
                else
                {
                    textBox4.ForeColor = System.Drawing.Color.Green;
                    textBox9.ForeColor = System.Drawing.Color.Green;
                }

            }
            else if (Regex.IsMatch(textBox4.Text, "Nvidia"))
            {
                textBox4.ForeColor = System.Drawing.Color.Green;
                textBox9.ForeColor = System.Drawing.Color.Green;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            label14.Visible = false;
        }
    }
}
