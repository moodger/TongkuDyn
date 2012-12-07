using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace _3322
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        GetInfo Newcls = new GetInfo();


        private void Form3_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now.Date;
            string  tm01 = DateTime.Now.AddDays(-7).ToString();
            string wz = DateTime.Now.AddHours(3).ToString();
            dateTimeInput1.Text = tm01;
            dateTimeInput2.Text = wz;
            //comboBoxEx1
            comboBoxEx1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxEx1.Items.Add("更新记录");
            comboBoxEx1.Items.Add("运行记录");
            comboBoxEx1.Items.Add("维护记录"); 

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

         

            string srdt = dateTimeInput1.Text;

            string todt = dateTimeInput2.Text;

            if (comboBoxEx1.Text != "")
            {

                dataGridViewX1.DataSource = Newcls.redLog(srdt, todt, comboBoxEx1.SelectedIndex + 1);


            }
            else
            {

                MessageBox.Show("日志类型不可以为空！","系统提醒您",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            
            }



          



            





        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {



        }

       
    }
}
