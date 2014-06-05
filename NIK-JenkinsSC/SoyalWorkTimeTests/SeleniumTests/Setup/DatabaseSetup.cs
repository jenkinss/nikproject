using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SoyalWorkTimeTests
{
    public static class DatabaseSetup
    {
        public static string Name { get; set; }

        public static void Setup()
        {
            Console.WriteLine("test db setup start");
            executeCommand("sqllocaldb p " + Name);
            executeCommand("sqllocaldb d " + Name);
            executeCommand("sqllocaldb c " + Name);
            executeCommand("sqllocaldb s " + Name);
            string address = executeCommand("sqllocaldb i " + Name + " | findstr ^pipe").Substring(20);
            string importcommand = @"CREATE DATABASE [" + Name + @"] ON ( FILENAME = N'C:\VS_workspace\SoyalWorkTimeWebManager\App_Data\aspnet-SoyalWorkTimeWebManager-20140402091553.mdf' ), ( FILENAME = N'C:\VS_workspace\SoyalWorkTimeWebManager\App_Data\aspnet-SoyalWorkTimeWebManager-20140402091553_log.ldf' )  FOR ATTACH ;";
            executeCommand("sqlcmd -S " + address + @" -Q """ + importcommand + @"""");
        }

        private static void XMLStringreplace(string file, string oldStr, string newStr)
        {
            XDocument doc = XDocument.Load(file);
            foreach (XElement cell in doc.Element("Actions").Elements("Action")) { if (cell.Element("ActionDate").Value == oldStr) cell.Element("ActionDate").Value = newStr; }
            doc.Save(file);
        }


        public static void DeSetup()
        {
            executeCommand("sqllocaldb p " + Name);
            executeCommand("sqllocaldb d " + Name);
        }

        private static string executeCommand(string command)
        {
            Process p = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                    FileName = "cmd.exe",
                    Arguments = "/C " + command,
                    UseShellExecute = true, //set to false for non debug.
                    RedirectStandardOutput = true
                }
            };

            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            return output;
        }
    }
}