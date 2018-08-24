using CPQaunt.DataAccess;
using ParkingSqlHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPQaunt.UploadCode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_star_Click(object sender, EventArgs e)
        {
            StarSprider();

            this.timer1.Interval = 1000 * 60 * 10;
            this.timer1.Tick += Timer1_Tick;
            timer1.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            StarSprider();
        }

        public async void StarSprider() {
           
            Tcp_Hiscode tcp = new Tcp_Hiscode();

            if (!tcp.GetLastSingle())
            {
                MessageBox.Show("采集失败！");
                this.timer1.Stop();
            }
            DateTime DBTime = tcp.Datetime.Value;
            DateTime Today = DateTime.Today;

            TimeSpan ts = Today - DBTime;

            int days = ts.Days;

            DateTime endday = DateTime.Today;
            DateTime sarday = DateTime.Today.AddDays(days);


            while (sarday <= endday)
            {


                string today = sarday.ToString("yyyyMMdd");
                string url = string.Format("http://kaijiang.500.com/static/public/ssc/xml/qihaoxml/{0}.xml?_A=IEAKFSZM1534655958600", today);

                sarday = sarday.AddDays(1);

                HttpClient client = new HttpClient();
                string html = await client.GetStringAsync(url);

                SysOpenCode.xml xml = XmlHelper.Deserialize<SysOpenCode.xml>(html);

                for (int j = 0; j < xml.row.Count; j++)
                {
                    Tcp_Hiscode t = new Tcp_Hiscode();
                    t.Expect = xml.row[j].expect;
                    t.Opencode = xml.row[j].opencode;
                    t.Datetime = DateTime.Parse(xml.row[j].opentime);

                    if (t.SelectByExpect(t.Expect))
                    {
                        continue;
                    }


                    if (t.Insert())
                    {
                        this.textBox1.Text += "\r\n" + t.Expect;
                    }


                }


            }


        }
    }
}
