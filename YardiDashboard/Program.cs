
using System;
using System.IO;
using System.Windows.Forms;

namespace YardiDashboard
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(
                new FileInfo(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "log4net.config"));
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmDash());
        }
    }
}
