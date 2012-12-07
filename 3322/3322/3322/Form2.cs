using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace _3322
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }




        GetInfo Newcls = new GetInfo();



        private void Form2_Load(object sender, EventArgs e)
        {
           //讀取用戶保存的用戶名及密碼，域名信息

            DataSet ds = new DataSet();
            ds.ReadXml("configs.xml");
            if (ds.Tables["password"].Rows[0]["Domain"].ToString() != "" && ds.Tables["password"].Rows[0]["uname"].ToString() != "" && ds.Tables["password"].Rows[0]["pass"].ToString() != "" && ds.Tables["config"].Rows[0]["gxpl"].ToString() != "")
            {
                DomainTxt.Text = ds.Tables["password"].Rows[0]["Domain"].ToString().Trim();
                uname.Text = ds.Tables["password"].Rows[0]["uname"].ToString();
                upass.Text= _3des.DES3Decrypt(ds.Tables["password"].Rows[0]["pass"].ToString(), "F6T8SCV5", "C5DFX2S5", "G5X2D7C2");
                gxpl.Text = ds.Tables["config"].Rows[0]["gxpl"].ToString();

            }
            Newcls.readconfig();



                            if (GetInfo.autorun_set)
                            {

                                this.checkBox1.Checked = true;
                            }

                            if (GetInfo.logstart_set)
                            {

                                checkBox2.Checked = true;
                            }

                            if (GetInfo.logstop_set)
                            {
                                
                                checkBox3.Checked = true;
                            }
                            if (GetInfo.notifyIcon_set)
                            {
                                checkBox4.Checked = true;

                            }
                            if (GetInfo.hidden_set)
                            {

                                checkBox5.Checked = true;
                            }




            //讀取用戶設置信息
            /*
             * 
             * 
             * 
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
             * 
             * 
             * 
             * 
             * 
             * 
             * */
            

           
       


            //顯示格言信息
            linkLabel1.Text = GetBywords();

            










        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }




        private void button1_Click(object sender, EventArgs e)
        {

            //寫入用戶名，密碼，域名信息,自動更新頻率 

            if (uname.Text != "" && upass.Text != "" && DomainTxt.Text != "" && gxpl.Text != "") 
            {

                string password = _3des.DES3Encrypt(upass.Text, "F6T8SCV5", "C5DFX2S5", "G5X2D7C2");
                DataSet ds = new DataSet();
                ds.ReadXml("configs.xml");

                ds.Tables["password"].Rows[0]["uname"] = uname.Text;
                ds.Tables["password"].Rows[0]["pass"] = password;
                ds.Tables["password"].Rows[0]["domain"] = DomainTxt.Text;
                if (this.gxpl.Text == "1" || double.Parse(this.gxpl.Text) < 1)
                {
                    MessageBox.Show("更新频率不能低于或等于1分钟!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }
                else
                {
                    ds.Tables["config"].Rows[0]["gxpl"] = gxpl.Text.Trim();
                }

                if (this.checkBox1.Checked == true)
                {
                    ds.Tables["config"].Rows[0]["autorun"] = true;


                }
                else
                {
                      ds.Tables["config"].Rows[0]["autorun"] = false;
                
                }


                if (this.checkBox2.Checked == true)
                {
                     ds.Tables["config"].Rows[0]["logstart"] = true;


                }
                else
                {
                      ds.Tables["config"].Rows[0]["logstart"] = false;

                }



                if (this.checkBox3.Checked == true)
                {
                     ds.Tables["config"].Rows[0]["logstop"] = true;


                }
                else
                {
                     ds.Tables["config"].Rows[0]["logstop"] = false;

                }

                if (this.checkBox4.Checked == true)
                {
                    ds.Tables["config"].Rows[0]["notifyIcon"] = true;


                }
                else
                {
                     ds.Tables["config"].Rows[0]["notifyIcon"] = false;

                }


                if (this.checkBox5.Checked == true)
                {
                      ds.Tables["config"].Rows[0]["hidden"] = true;


                }
                else
                {
                     ds.Tables["config"].Rows[0]["hidden"] = false;

                }


                ds.AcceptChanges();
                ds.WriteXml("configs.xml");
                MessageBox.Show("数据保存成功，开始按照您设定的参数更新您的域名！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else {

                MessageBox.Show("请正确填写相关数据！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            
            }

           






        }

        private void button2_Click(object sender, EventArgs e)
        {
           




           
           // //跟随系統啟動-否
           // checkBox1.Checked = false;
           // //記錄系統啟動 -是
           // checkBox2.Checked = true;
           // //記錄系統關閉 -是
           // checkBox3.Checked = true;
           // //顯示圖標 - 是
           // checkBox4.Checked = true;
           // //隱藏運行程序 - 否
           // checkBox5.Checked = false;
           // this.gxpl.Text = "2";

           //// WriterXmlDoc();
           
       


        }



    //      public static string SaveXMLData_XmlTextWriter()
    //{
    //    try
    //    {
    //        //创建一个XmlTextWriter类的实例对象
    //        System.IO.FileInfo file = new FileInfo("log.xml");
    //        //if (!file.Exists)
          
    //        XmlTextWriter textWriter = new XmlTextWriter("log.xml", System.Text.Encoding.UTF8);
            
    //        //开始写过程，调用WriterStartDocument方法写入文件头信息
    //        //例如<?xml version="1.0" encoding="utf-8"?>
    //        textWriter.WriteStartDocument();
    //        //写入根节点<logs></logs>
    //        textWriter.WriteStartElement("logs");
    //        //写入节点name
    //        textWriter .WriteElementString("id", "1");             
           
         
    //        //为name节点写入用户输入的值
    //        textWriter.WriteString(pb.name);
    //        //写入name节点的结束符
    //        textWriter.WriteEndElement();
       
 
    //        //写入文档结束，调用WriteEndDocument方法
    //        textWriter.WriteEndDocument();
    //        //关闭textWriter
    //        textWriter.Close();
    //        return "true";        
    //    }
    //    catch (Exception ex)
    //    {
    //        return ex.ToString();
    //    }
    //}

        public void Clsxml()
        {
            XmlTextWriter xmlWrite = new XmlTextWriter("log.xml", null);    //实例化时要提tw或者文件名
            xmlWrite.WriteStartDocument();                      //写开头 ,需对应后面的WriteEndDocument()
            xmlWrite.WriteStartElement("logs");       //写根节点,对应后面的WriteEndElement()

          //  xmlWrite.WriteStartElement("msginfo");  //子logs的子節點msginfo
           // xmlWrite.WriteAttributeString("id", "1");//为上一根接点创建属性
          //  xmlWrite.WriteElementString("Time", "");
           // xmlWrite.WriteElementString("msg", "");
         //   xmlWrite.WriteElementString("ip", "");            
           // xmlWrite.WriteElementString("stat", "");
        //    xmlWrite.WriteEndElement();   //結束msginfo
         //   xmlWrite.WriteEndElement();    //結束logs
            xmlWrite.WriteEndDocument();
            xmlWrite.Formatting = Formatting.Indented;
            xmlWrite.Close();
        }



        public void WriterXmlDoc()
        {
            // 在此处放置用户代码以初始化页面
            XmlTextWriter xmlWrite = new XmlTextWriter("d://mltest.xml", null);    //实例化时要提tw或者文件名
            xmlWrite.WriteStartDocument();                      //写开头 ,需对应后面的WriteEndDocument()
            xmlWrite.WriteStartElement("Folder");       //写节点,对应后面的WriteEndElement()
            xmlWrite.WriteElementString("name", "MyFolder");        //写具体的属性名与值
            xmlWrite.WriteElementString("open", "1");


            xmlWrite.WriteStartElement("Placemark");
            xmlWrite.WriteElementString("name", "Myplace");
            xmlWrite.WriteElementString("description", "This my Home .");
            xmlWrite.WriteStartElement("Polygon");
            xmlWrite.WriteElementString("tessellate", "1");
            xmlWrite.WriteStartElement("outerBoundaryIs");
            xmlWrite.WriteStartElement("LinearRing");
            xmlWrite.WriteElementString("coordinates", "113.384699976597,23.13109492384194,0 113.3847009682283,23.13079559342177,0 113.3847104767035,23.1307956122293,0 113.3847484787173,23.1307956872297,0 113.3848385302343,23.13079586504226,0 113.3849139017068,23.13079591392106,0 113.3849750530701,23.13080025741056,0 113.3849751763442,23.13083484711034,0 113.3849707708473,23.13095568255526,0 113.384970788045,23.13100743069212,0 113.3849704611595,23.13107232425702,0 113.384970300658,23.13109403100412,0 113.3849041444355,23.1310942495685,0 113.3848520557927,23.13109442156901,0 113.3847761184067,23.13109467229479,0 113.384699976597,23.13109492384194,0");

            xmlWrite.WriteEndElement();
            xmlWrite.WriteEndElement();
            xmlWrite.WriteEndElement();
            xmlWrite.WriteEndElement();
            xmlWrite.WriteEndElement();
            xmlWrite.WriteEndDocument();
            xmlWrite.Formatting = Formatting.Indented;
            xmlWrite.Close();
        }



        public string  GetBywords()
        {
            DataSet ds = new DataSet();
            ds.ReadXml("configs.xml");
            DataTable dt01 = ds.Tables["byword"];
            //將格言信息，導入數組。
            DataRow row1;
            row1 = dt01.NewRow();
            row1["id"] = 13;
            row1["bytxt"] = "欢迎来访问我的技术博客【http://moodger.lofter.com/】";
            dt01.Rows.Add(row1);  

            
            int aa = 0;

            Random ra = new Random();
           aa= ra.Next(1,14);      
           string geyan = "";
           foreach (DataRow row in dt01.Rows)
           {
               if (int.Parse(row["id"].ToString()) == aa)
               {
                   geyan = row["bytxt"].ToString();
                   break;
               
               }



           }

           return geyan;



        
        }


        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkLabel1.Text.Contains("博客"))
            {

                System.Diagnostics.Process.Start("http://moodger.lofter.com");
            }

        }

        private void linkLabel1_TextChanged(object sender, EventArgs e)
        {

            if (linkLabel1.Text.Contains("博客"))
            {
                this.linkLabel1.LinkBehavior = LinkBehavior.HoverUnderline;


            }



        }

      

        private void button4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://moodger.lofter.com/_3322");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Clsxml();
                MessageBox.Show("历史日志清理成功,此清理事件将记录到新的日志內！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            { 
            
            
            }
            
            
            
            //.logstop_set = false;
           // Form1.writeLog("清除历史日誌記錄", DateTime.Now, "清除成功", "");















        }

        private void Form2_SizeChanged(object sender, EventArgs e)
        {


            if (this.WindowState == FormWindowState.Minimized)
            {
                //返回狀態給Form1
                this.Close();
                this.DialogResult = DialogResult.Abort;
               
                
            }
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox9.Checked == true)
            {


                this.smtpAdd.ReadOnly = true;
                this.smtpUser.ReadOnly = true;
                this.smtpPass.ReadOnly = true;


            }

            else
            {
                this.smtpAdd.ReadOnly = false;
                this.smtpUser.ReadOnly = false;
                this.smtpPass.ReadOnly = false;

            
            
            }


        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            linkLabel1.Text = GetBywords();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(this.checkBox2.Checked == true)
            {

                GetInfo.logstart_set = true;

            
            }
            else
            {

                GetInfo.logstart_set = false;
            }


        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox2.Checked == true)
            {

                GetInfo.logstop_set = true;


            }
            else
            {

                GetInfo.logstop_set = false;
            }
            
           

        }

       
    }
}
