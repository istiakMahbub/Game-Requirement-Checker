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

namespace DemoProjectApplication2
{
    public partial class Form2 : Form
    {
        
        public Form2()
        {
            InitializeComponent();
            button1.BackColor = Color.Navy;
            button2.BackColor = Color.Navy;
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

      
        

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
