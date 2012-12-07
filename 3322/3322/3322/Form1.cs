using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Xml;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading; 

namespace _3322
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        public string ip;
        public int pgnum;
        public int nowpg;
        public string Domain = "";
        public string uname = "";
        public string upass = "";
        public static bool autorun_set;//定義是否自動運行
        public static bool logstart_set; //定義是否記錄程序啟動
        public static bool logstop_set; //定義是否記錄程序退出
        public static bool notifyIcon_set;//定義是否開啟右下角圖標
        public static bool hidden_set;//定義是否啟動隱藏運行模式 
        public static string gxpl;


        //private void button1_Click(object sender, EventArgs e)
        //{
        //    if (this.textBox1.Text == "" || textBox2.Text == "")
        //    {

        //        MessageBox.Show("出错,用户名或密码不能为空!", "出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }

        //    ip = GetClientInternetIP();
        //    //用户名:密码
        //    string url = "http://" + textBox1.Text + ":" + textBox2.Text + "@members.3322.net/dyndns/update?system=dyndns&hostname=" + Domain + "&myip=" + ip;
        //    webKitBrowser1.Navigate(url);





        //}


        /// <summary>
        /// 获得客户端外网IP地址
        /// </summary>
        /// <returns>IP地址</returns>
        public static string GetClientInternetIP()
        {
            string ipAddress = "";
            WebClient webClient = new WebClient();
            ipAddress = webClient.DownloadString("http://ip.changeip.com/");//站获得IP的网页
            //判断IP是否合法


            string[] sArray = ipAddress.Split('=');
            string ip1 = sArray[1].ToString();
            //ipAddress = ip1.Substring(0, ip1.Length - 5);
            ipAddress = ip1.Replace("-->", "").Trim();
            //-->

            return ipAddress;
        }

        //private void webKitBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        //{

        //    //將更新時間，更新ip，更新狀態寫入log.xml數據庫文件內

        //    DateTime now = DateTime.Now;


        //    string msg = "";
        //    string stat;
        //    if (this.webKitBrowser1.DocumentText != "")
        //    {
        //        if (this.webKitBrowser1.DocumentText.Contains("nochg"))
        //        {

        //            msg = "IP没有更改.";
        //        }
        //        else
        //        {

        //            if (this.webKitBrowser1.DocumentText.Contains("good"))
        //            {
        //                msg = "IP更新成功.";
        //            }
        //            else
        //            {
        //                msg = "更新失败，请看域名官网说明.";

        //            }
        //        }

        //        stat = webKitBrowser1.DocumentText;
        //        WriteLogo(msg, now, stat, ip);



        //    }









        //}






        public static void writeLog(string msg, DateTime tm, string stat, string ip)
        {

            try {

                int id = 0;
                XmlDocument doc = new XmlDocument();
                //如果存在該文件，這裡指增加記錄但非覆蓋.
                doc.Load("log.xml");
                XmlNodeList xnl = doc.SelectNodes("/logs");
              
                if (xnl != null)
                {
                    if (xnl.Item(0).HasChildNodes)
                    {
                        XmlNode xn2 = doc.SelectSingleNode("/logs").LastChild.Attributes["id"];
                        string noid = xn2.Value;

                        id = int.Parse(noid) + 1;

                        XmlElement root = doc.DocumentElement;
                        XmlNode node = doc.CreateElement("msginfo");
                        node.Attributes.Append(CreateAttribute(node, "id", id.ToString()));
                        //创建节点（三级）
                        XmlElement element1 = doc.CreateElement("Time");
                        element1.InnerText = tm.ToString();
                        node.AppendChild(element1);

                        XmlElement element2 = doc.CreateElement("msg");
                        element2.InnerText = msg;
                        node.AppendChild(element2);

                        XmlElement element3 = doc.CreateElement("ip");
                        element3.InnerText = ip;
                        node.AppendChild(element3);

                        XmlElement element4 = doc.CreateElement("stat");
                        element4.InnerText = stat;
                        node.AppendChild(element4);

                        root.AppendChild(node);
                        doc.Save("log.xml");
                    }
                    else
                    {


                        id = 1;

                     
                        //如果沒有該文件,這裡指第一次執行程序，生成xml
                       // XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
                      //  doc.AppendChild(dec);
                        //创建一个根节点（一级）
                        //XmlElement root = doc.CreateElement("logs");
                       // doc.AppendChild(root);
                        //创建节点（二级）

                        XmlElement root = doc.DocumentElement;
                        XmlNode node = doc.CreateElement("msginfo");
                        node.Attributes.Append(CreateAttribute(node, "id", id.ToString()));
                        //创建节点（三级）
                        XmlElement element1 = doc.CreateElement("Time");
                        element1.InnerText = tm.ToString();
                        node.AppendChild(element1);

                        XmlElement element2 = doc.CreateElement("msg");
                        element2.InnerText = msg;
                        node.AppendChild(element2);

                        XmlElement element3 = doc.CreateElement("ip");
                        element3.InnerText = ip;
                        node.AppendChild(element3);

                        XmlElement element4 = doc.CreateElement("stat");
                        element4.InnerText = stat;
                        node.AppendChild(element4);

                        root.AppendChild(node);
                        doc.Save("log.xml");



                    }
                }
                else
                {
                   


                
                
                }



               
            
            
            
            
            
            
            }
            catch {

              
               

                
            }


        
        
        }



        public static  XmlAttribute CreateAttribute(XmlNode node, string attributeName, string value)
        {
            try
            {
                XmlDocument doc = node.OwnerDocument;
                XmlAttribute attr = null;
                attr = doc.CreateAttribute(attributeName);
                attr.Value = value;
                node.Attributes.SetNamedItem(attr);
                return attr;
            }
            catch (Exception err)
            {
                string desc = err.Message;
                return null;
            }
        } 




        /*
         * 
         * 
         *  准备生成的XML文件格式如下:

        <?xml version="1.0" encoding="utf-8" ?>
        <Update>
          <Soft Name="BlogWriter">
            <Verson>1.0.1.2</Verson>
            <DownLoad>http://www.csdn.net/BlogWrite.rar</DownLoad>
          </Soft>
        </Update>

 

        详细代码为:

                    XmlDocument doc = new XmlDocument();
                    XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
                    doc.AppendChild(dec);
                    //创建一个根节点（一级）
                    XmlElement root = doc.CreateElement("Update");
                    doc.AppendChild(root);
                    //创建节点（二级）
                    XmlNode node = doc.CreateElement("Soft");
                    node.Attributes.Append(CreateAttribute(node, "Name", "BlogWriter"));
                    //创建节点（三级）
                    XmlElement element1 = doc.CreateElement("Verson");
                    element1.InnerText = "1.0.1.2";
                    node.AppendChild(element1);

                    XmlElement element2 = doc.CreateElement("DownLoad");
                    element2.InnerText = "http://www.csdn.net/BlogWrite.rar";
                    node.AppendChild(element2);

                    root.AppendChild(node);
                    doc.Save(@"C:\web\bb.xml");
                    Console.Write(doc.OuterXml);
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
                 //利用XmlNode往XML文件写入数据,可以实现追加
            public static string SaveXMLData_XmlNode(BaseInfo.Phonebook pb)
            {
                try
                {
                    //创建一个XmlDocument 对象，用于载入存储信息的XML文件
                    System.Xml.XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(System.Web.HttpContext.Current.Server.MapPath("phonebook.xml"));
                    //创建一个menber节点并将它添加到根节点下
                    XmlElement parentNode = xdoc.CreateElement("member");
                    xdoc.DocumentElement.PrependChild(parentNode);
                    //创建所有用于存储信息的节点
                    XmlElement nameNode = xdoc.CreateElement("name");
                    XmlElement telphoneNode = xdoc.CreateElement("telphone");
            
                    ///获取文本信息
                    XmlText nameText = xdoc.CreateTextNode(pb.name);
                    XmlText telphoneText = xdoc.CreateTextNode(pb.telphone);
           
                    ///将上面的各个存储信息节点添加到menber节点下，但并不包含最终的值
                    parentNode.AppendChild(nameNode);
                    parentNode.AppendChild(telphoneNode);
          
                    ///将上面获取的文本信息添加到与之相对应的节点中
                    nameNode.AppendChild(nameText);
                    telphoneNode.AppendChild(telphoneText);
            
                    ///保存存储信息的XML文件
                    xdoc.Save(System.Web.HttpContext.Current.Server.MapPath("phonebook.xml"));
                    return "true";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }



        */



        #region   將日志写入log.xml
        /// <summary>
        /// 將日志写入log.xml
        /// </summary>
        /// <param name="msg">提示信息，自定义</param>
        /// <param name="tm">当前日期及时间</param>
        /// <param name="stat">服务器返回状态</param>
        /// <param name="ip">ip</param>

        public static void WriteLogo(string msg, DateTime tm, string stat, string ip)
        {
            DataSet ds = new DataSet();
            ds.ReadXml("log.xml");
            int id = 0;
            if (ds.Tables["msginfo"].Rows.Count == 1 && ds.Tables["msginfo"].Rows[0]["ID"].ToString() == "")
            {

               
                    id = ds.Tables["msginfo"].Rows.Count;
                    ds.Tables["msginfo"].Rows[0]["ID"] = id;
                    ds.Tables["msginfo"].Rows[0]["Time"] = tm;
                    ds.Tables["msginfo"].Rows[0]["msg"] = msg;
                    ds.Tables["msginfo"].Rows[0]["ip"] = ip;
                    ds.Tables["msginfo"].Rows[0]["stat"] = stat;
                    ds.AcceptChanges();
                    ds.WriteXml("log.xml");
                                /*
                                DataRow newRow = newdtb.NewRow();
                                newRow["ProName"] = "pro" + i.ToString();
                                newRow["ProPrice"] = 12.3m;
                                newdtb.Rows.Add(newRow);
                                 */
                                // ds.AcceptChanges();
                
            }
            else
            {
                id = ds.Tables["msginfo"].Rows.Count + 1;
                DataTable dt = ds.Tables["msginfo"];
                DataRow newRow = dt.NewRow();
                newRow["ID"] = id;
                newRow["Time"] = tm;
                newRow["msg"] = msg;
                newRow["ip"] = ip;
                newRow["stat"] = stat;
                dt.Rows.Add(newRow);
                ds.WriteXml("log.xml");
            }
        }

        #endregion



        #region  讀取數據
        /// <summary>
        ///  讀取數據
        /// </summary>

        /// 


      /* 
             *                  <config>
                                <autorun>     
                                </autorun>
                                <logstart>      
                                </logstart>
                                <logstop></logstop>
                                <notifyIcon></notifyIcon>
                                <hidden></hidden>    
                                </config>
             * 
             */

        public void readconfig()
        {

            DataSet ds = new DataSet();
            ds.ReadXml("configs.xml");
            DataTable dt01 = ds.Tables["config"];

            if (dt01.Rows[0]["autorun"].ToString() == "True")
            {
                autorun_set = true;
            }
            else {

                autorun_set = false;
            }

            if (dt01.Rows[0]["logstart"].ToString() == "True")
            {
                logstart_set = true;
            }
            else {

                logstart_set = false;
            }

            if (dt01.Rows[0]["logstop"].ToString() == "True")
            {
                logstop_set = true;

            }
            else {

                logstop_set = false;
            }

            if (dt01.Rows[0]["notifyIcon"].ToString() == "True")
            {

                notifyIcon_set = true;
            }
            else {

                notifyIcon_set = false;
            
            }
            if (dt01.Rows[0]["hidden"].ToString() == "True")
            {
                hidden_set = true;

            }
            else {

                hidden_set = false;
            }


            if (dt01.Rows[0]["gxpl"].ToString() != "")
            {
                gxpl = dt01.Rows[0]["gxpl"].ToString();

            }
            else
            {

                gxpl = "2";
            }


            this.timer1.Interval = int.Parse(gxpl) * 60000;

        }


        #endregion







        #region  讀取數據
        /// <summary>
        ///  讀取數據
        /// </summary>

        /// 
        public DataView reddt(int pagenum, ref int nowpg)
        {
            int pgindex = 0;
            int stindex = 0;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ds.ReadXml("log.xml");

            if (ds.Tables["msginfo"].Rows[0]["id"].ToString() == "")
            {
                throw new Exception("暂时没有记录!");
                

            }


            if (pagenum <= 1)
            {
                //當前頁碼等於1
                pagenum = 1;
                //查詢記錄到的條數
                pgindex = 1 * 8 + 1;


            }
            else
            {

                pgindex = pagenum * 8 + 1;
                stindex = pgindex - 8 - 1;
                // aa  = pgindex;
                //bb  = stindex;
                if (pgindex > ds.Tables["msginfo"].Rows.Count)
                {
                    if (pagenum == 99999)
                    {
                        int c = ds.Tables["msginfo"].Rows.Count / 8;


                        int d = ds.Tables["msginfo"].Rows.Count % 8;

                        if (d != 0)
                        {
                            c = c + 1;
                            pagenum = c;
                            nowpg = c;

                        }
                        else
                        {
                            pagenum = c;
                            nowpg = c;
                        }
                        pgindex = pagenum * 8;
                        stindex = pgindex - 8;


                    }
                    else
                    {
                        if ((pgindex - ds.Tables["msginfo"].Rows.Count) <= 8)
                        {


                        }
                        else
                        {
                            throw new Exception("索引超出限制");
                        }
                    }
                }
            }






            //"].Columns.Add("MasterID", typeof(int));


            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("时间", typeof(string));
            dt.Columns.Add("消息", typeof(string));
            dt.Columns.Add("IP", typeof(string));
            dt.Columns.Add("状态", typeof(string));

            foreach (DataRow row in ds.Tables["msginfo"].Rows)
            {
                DataRow newRow = dt.NewRow();
                newRow["ID"] = row["id"];
                newRow["时间"] = row["Time"];
                newRow["消息"] = row["msg"];
                newRow["IP"] = row["ip"];
                newRow["状态"] = row["stat"];
                dt.Rows.Add(newRow);


            }

            DataView dv = dt.DefaultView;

            //  dv.RowFilter = "'ID'<" + bb +" and 'ID'>"+aa;
            dv.RowFilter = string.Format("(ID < {0}) and (ID > {1})", pgindex, stindex);

            return dv;



        }
        #endregion





        private void button2_Click(object sender, EventArgs e)
        {

            DateTime now = DateTime.Now;


            string msg;
            string stat;

            msg = "服务器成更新动作成功.";
            stat = "good";
            //WriteLogo(msg, now, stat, ip);

            writeLog(msg, now, stat, ip);




        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Width = 569;
            //569, 349
      
            pgnum = 1;
            try
            {
                dataGridView1.DataSource = reddt(pgnum, ref nowpg);
                this.linkLabel2.Enabled = true;
                this.linkLabel3.Enabled = true;
                this.linkLabel5.Enabled = true;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message.ToString());
                label3_Click(sender, e);
            
            
            }
           


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.Width = 400;
          
            this.Width = 155;
            try
            {
                readUserinfo();
                readconfig();
               //120000
                this.timer1.Interval = int.Parse(gxpl) * 60000;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            
            }

              if(logstart_set){
                  //WriteLogo("启动成功",DateTime.Now,"Start OK","");
                  writeLog("启动成功", DateTime.Now, "Start OK", "");
            } 




        }



        public void readUserinfo()
        {

            DataSet ds = new DataSet();
            ds.ReadXml("configs.xml");
            if (ds.Tables["password"].Rows[0]["Domain"].ToString() != "" && ds.Tables["password"].Rows[0]["uname"].ToString() != "" && ds.Tables["password"].Rows[0]["pass"].ToString() != "")
            {
                Domain = ds.Tables["password"].Rows[0]["Domain"].ToString().Trim();
                uname = ds.Tables["password"].Rows[0]["uname"].ToString();
                upass = _3des.DES3Decrypt(ds.Tables["password"].Rows[0]["pass"].ToString(), "F6T8SCV5", "C5DFX2S5", "G5X2D7C2");
                this.label1.Text = "[ " + Domain + " ]";

            }
        
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //if (this.checkBox1.Checked == true)
            //{

            //    if (textBox1.Text != "" && textBox2.Text != "")
            //    {

            //        string password = _3des.DES3Encrypt(textBox2.Text, "F6T8SCV5", "C5DFX2S5", "G5X2D7C2");
            //        DataSet ds = new DataSet();
            //        ds.ReadXml("log.xml");

            //        ds.Tables["password"].Rows[0]["uname"] = textBox1.Text;
            //        ds.Tables["password"].Rows[0]["pass"] = password;
            //        ds.Tables["password"].Rows[0]["cheaked"] = "true";
            //        ds.AcceptChanges();
            //        ds.WriteXml("log.xml");

            //    }


            //}

            //else
            //{

            //    DataSet ds = new DataSet();
            //    ds.ReadXml("log.xml");
            //    ds.Tables["password"].Rows[0]["pass"] = "";
            //    ds.Tables["password"].Rows[0]["cheaked"] = false;
            //    ds.AcceptChanges();
            //    ds.WriteXml("log.xml");


            //}



        }

        private void Form1_MinimumSizeChanged(object sender, EventArgs e)
        {

            // this.Hide();


        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {


            if (this.dataGridView1.Rows.Count != 0)
            {

                this.dataGridView1.Columns["ID"].Width = 32;
                for (int i = 0; i < this.dataGridView1.Rows.Count; )
                {
                    this.dataGridView1.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
                    i += 2;
                }
            }

            if (pgnum <= 1)
            {

                this.linkLabel3.Enabled = false;
            }
            else
            {
                this.linkLabel3.Enabled = true;
            }

        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (pgnum >= 2)
            {

                pgnum = pgnum - 1;
                dataGridView1.DataSource = reddt(pgnum, ref nowpg);
                this.linkLabel2.Enabled = true;
                this.linkLabel5.Enabled = true;
            }
            else
            {

                return;

            }


        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            try
            {
                pgnum = pgnum + 1;
                this.linkLabel3.Enabled = true;
                dataGridView1.DataSource = reddt(pgnum, ref nowpg);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.linkLabel2.Enabled = false;
                pgnum = pgnum - 1;
                this.linkLabel5.Enabled = true;


            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pgnum = 1;
            dataGridView1.DataSource = reddt(pgnum, ref nowpg);
            this.linkLabel5.Enabled = true;
            this.linkLabel2.Enabled = true;
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pgnum = 99999;
            dataGridView1.DataSource = reddt(pgnum, ref nowpg);
            pgnum = nowpg;
            this.linkLabel5.Enabled = false;
            this.linkLabel2.Enabled = false;
            this.linkLabel3.Enabled = true;


        }




        private void timer1_Tick(object sender, EventArgs e)
        {
             button1_Click(sender, e);






        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Width = 155;
        }

        

       

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
           

            
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form aa = new Form2();
            aa.ShowDialog();
            if(aa.DialogResult == DialogResult.Abort){
                //最小化，隱藏任務欄,開啟右下角圖標

                this.WindowState = FormWindowState.Minimized;
               
            
            }
            readUserinfo();
            readconfig();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //如是第一次使用，请先在高級設置界面內輸入相關的用戶名及密碼和域名參數。

            if (Domain == "" || uname == "" || upass == "")
            {

                MessageBox.Show("第一次登陆，请先到【高级设置】界面內设置用户名及密码和相对应的域名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;


            }


            this.button1.BackColor = System.Drawing.SystemColors.GrayText;
            this.button1.Enabled = false;



            string aa = "";


            DataSet ds = new DataSet();
            ds.ReadXml("configs.xml");
            if (ds.Tables["password"].Rows[0]["Domain"].ToString() != "" && ds.Tables["password"].Rows[0]["uname"].ToString() != "" && ds.Tables["password"].Rows[0]["pass"].ToString() != "")
            {
                Domain = ds.Tables["password"].Rows[0]["Domain"].ToString().Trim();
                uname = ds.Tables["password"].Rows[0]["uname"].ToString();
                upass = _3des.DES3Decrypt(ds.Tables["password"].Rows[0]["pass"].ToString(), "F6T8SCV5", "C5DFX2S5", "G5X2D7C2");

            }


            try
            {

                string resmsg = getmsg(uname, upass, Domain, ref aa);
                string res = @"[a-z]*";
                Regex reg = new Regex(res, RegexOptions.IgnoreCase);
                Match bbb = reg.Match(resmsg);
                string sat = bbb.Value;

                reg = new Regex(@"[0-9](.)*", RegexOptions.IgnoreCase);
                bbb = reg.Match(resmsg);
                string ip = bbb.Value;
                DateTime now = DateTime.Now;
                switch (sat)
                {
                    case "good":
                        //WriteLogo("IP更新成功", now, sat, ip);
                        writeLog("更新成功", DateTime.Now, sat, ip);
                        break;
                    case "nochg":
                        //WriteLogo("IP沒有更改", now, sat, ip);
                        writeLog("IP沒有更改", DateTime.Now, sat, ip);
                        break;
                    case "badauth":
                       // WriteLogo("服务器验证失败", now, sat, ip);
                        writeLog("服务器验证失败", DateTime.Now, sat, ip);
                        break;
                    case "badagent":                        
                       // WriteLogo("严重错误,该域名被封杀", now, sat, ip);
                        writeLog("严重错误,该域名被封杀", DateTime.Now, sat, ip);
                        break;

                }


                this.label1.Text = aa;

                linkLabel5_LinkClicked(null,null);
                // textBox1.Text = getmsg2();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message.ToString());


            }


            //System.Drawing.SystemColors.GrayText


            this.button1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.Enabled = true; //Control




        }











        public string getmsg(string uname, string upass, string domains, ref string stat)
        {
            


            try
            {
                Process cmd = new Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.RedirectStandardError = true;
                cmd.Start();
                string cmds = "lynx -mime_header -auth=" + uname + ":" + upass + " \"http://members.3322.net/dyndns/update?system=dyndns&hostname=" + domains + " \"";
                //lynx -mime_header -auth=用户名:密码 "http://members.3322.net/dyndns/update?system=dyndns&hostname=域名"
                cmd.StandardInput.WriteLine(cmds);
                cmd.StandardInput.WriteLine("exit");
                string info = cmd.StandardOutput.ReadToEnd();
                string m;

                if (info.Contains("HTTP/1.1 200 OK"))
                {
                    string[] mm = Regex.Split(info, "close", RegexOptions.IgnoreCase);
                    char[] separator = { 'G' };
                    string[] b = mm[1].Split(separator);
                    m = b[0].Trim();


                }
                else
                {
                    throw new Exception("访问域名服务器失败，请查看设置是否错误!");


                }
                //int startPoint = info.IndexOf("close");//獲取該關鍵字的行數及起始點          
                //string m = info.Substring(startPoint + 5, 26).Trim();//獲取info內容的包含mac address的那一行的第8個字符開始，到後面的17個字符
                cmd.WaitForExit();
                cmd.Close();
                stat = "就绪";
                return m;
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.Width < 200)
            {
                this.Text = "";

            }
            else
            {
                this.Text = "东谷域名更新系统";

            }

            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                this.notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
                this.Activate();
                this.notifyIcon1.Visible = false;
                this.ShowInTaskbar = true;
            }
        }

        private void navigationBar1_Click(object sender, EventArgs e)
        {

        }

        













    }


}

