using System;
using System.Collections.Generic;
//using System.Linq;
using System.Windows.Forms;

namespace _3322
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           
          //  Application.Run(new Form1());
            if (GetInfo.logstart_set)
            {
                //Form1.WriteLogo("程序关闭", DateTime.Now, "Stop OK ", "");
                GetInfo newcls = new GetInfo();

                newcls.writeLog("程序启动", DateTime.Now, "Start OK ", "");
            }
            Application.Run(new main());
            
            
        }

       
    }
}
