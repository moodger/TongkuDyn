using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Diagnostics;

namespace _3322
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();

        }



        private bool isMouseDown = false;
        private Point FormLocation;     //form的location
        private Point mouseOffset;
        public int aaaa;


        //private Point mouseOffset; //记录鼠标指针的坐标 
        //private bool isMouseDown = false; //记录鼠标按键是否按下 

        private System.Drawing.Point mousePoint;

        GetInfo Newcls = new GetInfo();


        private void navigationPane1_Load(object sender, EventArgs e)
        {

        }

        private void main_Load(object sender, EventArgs e)
        {



            Newcls.readconfig();
            Newcls.readUserinfo();

            ///1、獲取更新頻率值
            ///
                        if(GetInfo.gxpl!=""){

                            if (int.Parse(GetInfo.gxpl) > 1)
                            {
                                this.timer1.Interval = int.Parse(GetInfo.gxpl) * 60000;
                                
                            }
                            else {

                                MessageBox.Show("更新频率不可低于2分钟！", "系统提醒您", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                pictureBox3_Click(sender,e);

                               
                            }

                        
                        
                        }



        ///获取状态设定值
        ///

            if(GetInfo.hidden_set){


            
            }

     





        }

        private void navigationBar1_Click(object sender, EventArgs e)
        {




        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {

            this.pictureBox1.Image = global::_3322.Properties.Resources.btn01a;
            this.pictureBox1.Enabled = true;

        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox1.Image = global::_3322.Properties.Resources.btn01b;


        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {

            this.pictureBox2.Image = global::_3322.Properties.Resources.btn00a;
            this.pictureBox2.Enabled = true;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            if(this.backgroundWorker1.IsBusy)
            {

                this.pictureBox2.Image = global::_3322.Properties.Resources.btn00a;
            }
            else{
                this.pictureBox2.Image = global::_3322.Properties.Resources.btn00b;
            }
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            this.pictureBox3.Image = global::_3322.Properties.Resources.btn02a;
            this.pictureBox3.Enabled = true;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox3.Image = global::_3322.Properties.Resources.btn02b;

        }

        private void navigationPane1_Load_1(object sender, EventArgs e)
        {

        }

        private void navigationBar1_Click_1(object sender, EventArgs e)
        {

        }


        //public delegate string MethodCaller(string uname, string upass, string domains);//定义个代理 



        private void updnspod() {


            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.UseShellExecute = false;
           　cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.RedirectStandardError = true;
            cmd.Start();
            string cmds = "SimpleDDNS.exe -up";
            cmd.StandardInput.WriteLine(cmds);            
            cmd.StandardInput.WriteLine("exit");
            cmd.Dispose();
        

        
        }


        private void RunNow()
        {

              
          
          
           
               





                //如是第一次使用，请先在高級設置界面內輸入相關的用戶名及密碼和域名參數。

                if (Newcls.Domain == "" || Newcls.uname == "" || Newcls.upass == "")
                {

                    MessageBox.Show("第一次登陆，请先到【高级设置】界面內设置用户名及密码和相对应的域名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;


                }








                //DataSet ds = new DataSet();
                //ds.ReadXml("configs.xml");
                //if (ds.Tables["password"].Rows[0]["Domain"].ToString() != "" && ds.Tables["password"].Rows[0]["uname"].ToString() != "" && ds.Tables["password"].Rows[0]["pass"].ToString() != "")
                //{
                //    Newcls.Domain = ds.Tables["password"].Rows[0]["Domain"].ToString().Trim();
                //    Newcls.uname = ds.Tables["password"].Rows[0]["uname"].ToString();
                //    Newcls.upass = _3des.DES3Decrypt(ds.Tables["password"].Rows[0]["pass"].ToString(), "F6T8SCV5", "C5DFX2S5", "G5X2D7C2");

                //}



                try
                {



                    //Thread th = new Thread(Calc);//要引入using System.Threading;命名空间
                    // th.Start();




                    // string resmsg = Newcls.getmsg(Newcls.uname, Newcls.upass, Newcls.Domain, ref aa);
                    string resmsg = GetInfo.getmsg(Newcls.uname, Newcls.upass, Newcls.Domain);
                    // MethodCaller mc = new MethodCaller(GetInfo.getmsg);
                    //IAsyncResult result = mc.BeginInvoke(Newcls.uname, Newcls.upass, Newcls.Domain, null, null); 
                    //string resmsg= mc.EndInvoke(result);
                    string res = @"[a-z]*";
                    Regex reg = new Regex(res, RegexOptions.IgnoreCase);
                    Match bbb = reg.Match(resmsg);
                    string sat = bbb.Value;

                    reg = new Regex(@"[0-9](.)*", RegexOptions.IgnoreCase);
                    bbb = reg.Match(resmsg);
                    string ip = bbb.Value;
                    DateTime now = DateTime.Now;
                    GetInfo.Nowsat = sat;
                    GetInfo.Nowip = ip;
                    switch (sat)
                    {
                        case "good":
                            //Newcls.writeLogo("IP更新成功", now, sat, ip);
                            Newcls.writeLog("更新成功", DateTime.Now, sat, ip);



                            break;
                        case "nochg":
                            //Newcls.writeLogo("IP沒有更改", now, sat, ip);
                            Newcls.writeLog("IP沒有更改", DateTime.Now, sat, ip);

                            break;
                        case "badauth":
                            // Newcls.writeLogo("服务器验证失败", now, sat, ip);
                            Newcls.writeLog("服务器验证失败", DateTime.Now, sat, ip);

                            break;
                        case "badagent":
                            // Newcls.writeLogo("严重错误,该域名被封杀", now, sat, ip);
                            Newcls.writeLog("严重错误,该域名被封杀", DateTime.Now, sat, ip);

                            break;

                    }

                    // labelItem1.Text = DateTime.Now.ToString()+"    更新完成/n";


                    // textBox1.Text = Newcls.getmsg2();
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message.ToString());


                }

            }

    



    


        


        public void RunDone()
        {
            this.pictureBox2.Image = global::_3322.Properties.Resources.btn00b;
            textBox1.AppendText(GetInfo.Nowsat + " " + GetInfo.Nowip + "\n");
            textBox1.Focus();
            GetInfo.Nowsat = null;
            GetInfo.Nowip = null;

            if (textBox1.Lines.GetUpperBound(0) >10) //當數值超過10行，便清除
            {
                textBox1.Clear();
            }


        }






        private void pictureBox2_Click(object sender, EventArgs e)
        {

    




          


         

                                //  Thread th = new Thread(RunNow);//要引入using System.Threading;命名空间
                                // th.Start();
                                if(this.backgroundWorker1.IsBusy){

                                  //  MessageBox.Show("更新线程已运行，不可重复执行!");
                                   // this.backgroundWorker1.Dispose();
                                    return;
                                }
                                else
                                {
                                    //更新3322.org
                                    this.backgroundWorker1.RunWorkerAsync();
                                    //更新dnspod
                                   
                                   




                                }
                                //System.Drawing.SystemColors.GrayText


                      
            //else //測試代碼，用於測試http請求
            //{

            //    Newcls.readUserinfo();
            //    string dmin = Newcls.Domain;
            //    string username = Newcls.uname;
            //    string passwd = Newcls.upass;


            //    string url22 = @"http://members.3322.net/dyndns/update ";
            //    string data22 = @"GET /dyndns/update?hostname="+dmin+" HTTP/1.1 \r\n";
            //    data22 += @"Host: members.3322.net \r\n";
            //    data22 += @"Authorization: Basic "+username+":"+passwd+" \r\n";
            //    data22 += @"User-Agent: myclient/1.0 me@null.net \r\n";





            //    string ooo =HttpFt.SendData(url22, data22, "POST");
            //    MessageBox.Show(ooo.ToString());
                

                



            //}












        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form aa = new Form2();
            aa.ShowDialog();
            if (aa.DialogResult == DialogResult.Abort)
            {
                //最小化，隱藏任務欄,開啟右下角圖標

                this.WindowState = FormWindowState.Minimized;


            }

            Newcls.readconfig();
            Newcls.readUserinfo();
        }

        private void miniBtn_MouseEnter(object sender, EventArgs e)
        {
            this.miniBtn.BackColor = Color.Crimson;
        }

        private void miniBtn_MouseLeave(object sender, EventArgs e)
        {
            this.miniBtn.BackColor = Color.Transparent;

        }

        private void clsBtn_MouseEnter(object sender, EventArgs e)
        {
            this.clsBtn.BackColor = Color.Crimson;
        }

        private void clsBtn_MouseLeave(object sender, EventArgs e)
        {
            this.clsBtn.BackColor = Color.Transparent;

        }

        private void clsBtn_Click(object sender, EventArgs e)
        {

            Application.Exit();


        }

        private void miniBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panelEx1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                this.mousePoint.X = e.X;
                this.mousePoint.Y = e.Y;

            }

        }



        private void main_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {

                isMouseDown = true;
                FormLocation = this.Location;
                mouseOffset = Control.MousePosition;
                this.Opacity = 0.7;

            }

        }





        private void main_MouseMove(object sender, MouseEventArgs e)
        {
            int _x = 0;

            int _y = 0;

            if (isMouseDown)
            {

                Point pt = Control.MousePosition;

                _x = mouseOffset.X - pt.X;

                _y = mouseOffset.Y - pt.Y;



                this.Location = new Point(FormLocation.X - _x, FormLocation.Y - _y);


            }




        }

        private void main_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            this.Opacity = 1;

        }

        private void panelEx1_Paint(object sender, PaintEventArgs e)
        {







            //   Point[] points = list.ToArray();
            //points = this.panelEx1.po

            // GraphicsPath shape = new GraphicsPath();
            // shape.AddPolygon(points);


            //  this.Region = new System.Drawing.Region(shape);
            this.Region = panelEx1.Region;

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // this.textBox1.Text =    "This text was set safely by BackgroundWorker.";


            RunDone();
           
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
           
            RunNow();
            updnspod();

          
           


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form aa = new Form3();
            aa.ShowDialog();


        }

        private void main_FormClosed(object sender, FormClosedEventArgs e)
        {
            
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy)
            {

                MessageBox.Show("更新线程已运行，不可重复执行!");
            }
            else
            {
                this.backgroundWorker1.RunWorkerAsync();

            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.ScrollToCaret();
        }

        private void panelEx1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)    //最小化到系统托盘
            {
                notifyIcon1.Visible = true;    //显示托盘图标
                this.Hide();    //隐藏窗口
            }

        }

        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;    //取消"关闭窗口"事件
                this.WindowState = FormWindowState.Minimized;    //使关闭时窗口向右下角缩小的效果
                notifyIcon1.Visible = true;
                this.Hide();
                return;
            }


        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            this.Show();
            WindowState = FormWindowState.Normal;
            this.Focus();

        }












    }
}
