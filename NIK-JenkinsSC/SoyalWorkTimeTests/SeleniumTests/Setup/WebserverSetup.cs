using System;
 using System.Collections.Generic;
 using System.Diagnostics;
 using System.Linq;
 using System.Text;
 using System.Threading;
 using System.Threading.Tasks;


namespace SoyalWorkTimeTests
{
    public static class WebServerSetup
    {
        public static string sitePath { get; set; }
        public static int portNumber { get; set; }
        private static Process _iisProcess;

        public static void StartIIS()
        {
            if (_iisProcess == null)
            {
                var thread = new Thread(IISExpressThread) { IsBackground = true };
                thread.Start();
            }
        }

        public static void StopIIS()
        {
            if (_iisProcess != null) { Stop(); }
        }

        private static void Stop()
        {
            try
            {
                _iisProcess.CloseMainWindow();
                _iisProcess.Dispose();
            }
            catch
            {   
                //noproblem
            }
        }

        private static void IISExpressThread()
        {
            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Normal,
                ErrorDialog = true,
                LoadUserProfile = true,
                CreateNoWindow = false,
                UseShellExecute = false,
                Arguments = string.Format("/path:\"{0}\" /port:{1}", sitePath, portNumber.ToString())
            };
            var programfiles = string.IsNullOrEmpty(startInfo.EnvironmentVariables["programfiles"])
                                                  ? startInfo.EnvironmentVariables["programfiles(x86)"]
                                                  : startInfo.EnvironmentVariables["programfiles"];
            startInfo.FileName = programfiles + "\\IIS Express\\iisexpress.exe";
            try
            {
                _iisProcess = new Process { StartInfo = startInfo };
                _iisProcess.Start();
                _iisProcess.WaitForExit();
            }
            catch { Stop(); }
        }
    }
}