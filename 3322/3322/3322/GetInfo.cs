using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

namespace _3322
{


    class HttpFt
    {
        /// <summary>
        /// Base64编码
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static string Base64Encode(string Message)
        {
            char[] Base64Code = new char[]{'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T',
         'U','V','W','X','Y','Z','a','b','c','d','e','f','g','h','i','j','k','l','m','n',
         'o','p','q','r','s','t','u','v','w','x','y','z','0','1','2','3','4','5','6','7',
         '8','9','+','/','='};
            byte empty = (byte)0;
            System.Collections.ArrayList byteMessage = new System.Collections.ArrayList(System.Text.Encoding.Default.GetBytes(Message));
            System.Text.StringBuilder outmessage;
            int messageLen = byteMessage.Count;
            int page = messageLen / 3;
            int use = 0;
            if ((use = messageLen % 3) > 0)
            {
                for (int i = 0; i < 3 - use; i++)
                    byteMessage.Add(empty);
                page++;
            }
            outmessage = new System.Text.StringBuilder(page * 4);
            for (int i = 0; i < page; i++)
            {
                byte[] instr = new byte[3];
                instr[0] = (byte)byteMessage[i * 3];
                instr[1] = (byte)byteMessage[i * 3 + 1];
                instr[2] = (byte)byteMessage[i * 3 + 2];
                int[] outstr = new int[4];
                outstr[0] = instr[0] >> 2;
                outstr[1] = ((instr[0] & 0x03) << 4) ^ (instr[1] >> 4);
                if (!instr[1].Equals(empty))
                    outstr[2] = ((instr[1] & 0x0f) << 2) ^ (instr[2] >> 6);
                else
                    outstr[2] = 64;
                if (!instr[2].Equals(empty))
                    outstr[3] = (instr[2] & 0x3f);
                else
                    outstr[3] = 64;
                outmessage.Append(Base64Code[outstr[0]]);
                outmessage.Append(Base64Code[outstr[1]]);
                outmessage.Append(Base64Code[outstr[2]]);
                outmessage.Append(Base64Code[outstr[3]]);
            }
            return outmessage.ToString();
        }























        /// <summary>
        /// 返回URL内容,带POST数据提交
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="method">GET/POST(默认)</param>
        /// <returns></returns>

        public static string SendData(string url, string data, string method)
        {
            WebRequest wr = WebRequest.Create(url);//http://192.168.0.179:57/test1.aspx
            wr.Method = method;
            wr.ContentType = "application/x-www-form-urlencoded";
            char[] reserved = { '?', '=', '&' };
            StringBuilder UrlEncoded = new StringBuilder();
            byte[] SomeBytes = null;
            if (data != null)
            {
                ASCIIEncoding EncodedData = new ASCIIEncoding();
                SomeBytes = EncodedData.GetBytes(data);
                wr.ContentLength = SomeBytes.Length;
                Stream newStream = wr.GetRequestStream();
                newStream.Write(SomeBytes, 0, SomeBytes.Length);
                newStream.Close();
            }
            else
            {
                wr.ContentLength = 0;
            }
            string re = "";
            try
            {
                WebResponse result = wr.GetResponse();
                Stream ReceiveStream = result.GetResponseStream();

                Byte[] read = new Byte[512];
                int bytes = ReceiveStream.Read(read, 0, 512);

                re = "";
                while (bytes > 0)
                {

                    // 注意：
                    // 下面假定响应使用 UTF-8 作为编码方式。
                    // 如果内容以 ANSI 代码页形式（例如，932）发送，则使用类似下面的语句：
                    //  Encoding encode = System.Text.Encoding.GetEncoding("shift-jis");
                    Encoding encode = System.Text.Encoding.GetEncoding("gb2312");
                    re += encode.GetString(read, 0, bytes);
                    bytes = ReceiveStream.Read(read, 0, 512);
                }
            }
            catch (Exception e)
            {
                re = e.Message;
            }
            return re;
        }



    }













    class GetInfo
    {






        public string Domain = "";
        public string uname = "";
        public string upass = "";
        public static bool autorun_set;//定義是否自動運行
        public static bool logstart_set; //定義是否記錄程序啟動
        public static bool logstop_set; //定義是否記錄程序退出
        public static bool notifyIcon_set;//定義是否開啟右下角圖標
        public static bool hidden_set;//定義是否啟動隱藏運行模式 
        public static string gxpl;

        public static string Nowsat; //定義更新狀態
        public static string Nowip; //定義當前Ip;



        public void writeLog(string msg, DateTime tm, string stat, string ip)
        {

            try
            {

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
            catch
            {





            }




        }



        public static XmlAttribute CreateAttribute(XmlNode node, string attributeName, string value)
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





        public static string getmsg(string uname, string upass, string domains)
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
                cmd.Dispose();

                return m;
                
                


            }
            catch (Exception err)
            {
                throw err;
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


            }

        }



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
            else
            {

                autorun_set = false;
            }

            if (dt01.Rows[0]["logstart"].ToString() == "True")
            {
                logstart_set = true;
            }
            else
            {

                logstart_set = false;
            }

            if (dt01.Rows[0]["logstop"].ToString() == "True")
            {
                logstop_set = true;

            }
            else
            {

                logstop_set = false;
            }

            if (dt01.Rows[0]["notifyIcon"].ToString() == "True")
            {

                notifyIcon_set = true;
            }
            else
            {

                notifyIcon_set = false;

            }
            if (dt01.Rows[0]["hidden"].ToString() == "True")
            {
                hidden_set = true;

            }
            else
            {

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




        }


        #endregion

        #region
        /// <summary>
        /// 讀取日誌記錄
        /// </summary>
        /// <param name="tm1">起始日期</param>
        /// <param name="tm2">結束日期</param>
        /// <returns>記錄表DataView</returns>

        public DataView redLog(string tm1, string tm2, int lx)
        {




            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ds.ReadXml("log.xml");

            if (ds.Tables["msginfo"].Rows[0]["id"].ToString() == "")
            {
                throw new Exception("暂时没有记录!");


            }



            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("时间", typeof(DateTime));
            dt.Columns.Add("消息", typeof(string));
            dt.Columns.Add("IP", typeof(string));
            dt.Columns.Add("状态", typeof(string));

            foreach (DataRow row in ds.Tables["msginfo"].Rows)
            {
                DataRow newRow = dt.NewRow();
                newRow["ID"] = row["id"].ToString();
                newRow["时间"] = Convert.ToDateTime(row["Time"].ToString());
                newRow["消息"] = row["msg"].ToString();
                newRow["IP"] = row["ip"].ToString();
                newRow["状态"] = row["stat"].ToString();
                dt.Rows.Add(newRow);


            }

            DataView dv = dt.DefaultView;
            //2012/8/29 10:53:47

            //  dv.RowFilter = "'ID'<" + bb +" and 'ID'>"+aa;

            //  tm2 = "2010/8/29 10:53:47";
            // tm1 = "2012/11/29 10:53:47";

            DateTime aa = Convert.ToDateTime(tm2);
            DateTime bb = Convert.ToDateTime(tm1);
            if (tm1 == "" && tm2 == "")
            {

            }
            else
            {
                string sql = "";
                sql += string.Format("( 时间< '{0}') and (时间 > '{1}')", aa, bb);

                switch (lx)
                {

                    case 1: //選擇更新记录
                        sql += string.Format("and ((状态= '{0}') or (状态 = '{1}'))", "nochg", "good");

                        break;

                    case 2: //运行记录
                        //Stop OK
                        sql += string.Format("and (( 状态= '{0}') or (状态 = '{1}'))", "Stop OK", "Start OK");

                        break;

                    case 3://维护记录

                        // 清除成功

                        sql += string.Format("and  (状态= '{0}')", "清除成功");
                        break;




                }



                dv.RowFilter = sql;



            }


            //dv.RowFilter = "时间 < '"+bb +"'";

            return dv;



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










    }
}
