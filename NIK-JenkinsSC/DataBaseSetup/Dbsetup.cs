using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SoyalWorkTimeDatabaseSetup
{
    public static class DatabaseSetup
    {
        static void Main(string[] args)
        {
            if (args[0] == "Setup") {
                Console.WriteLine("Setup");
                Name = args[1];
                Console.WriteLine(Name);
                databasefile = (args.Length>2) ? args[2] : @"C:\VS_workspace\SoyalWorkTimeWebManager\App_Data\aspnet-SoyalWorkTimeWebManager-20140402091553";
                Console.WriteLine(databasefile);
                xmlfile = (args.Length>3) ? args[3] : @"C:\jenkins\workspace\NIK-JenkinsSC\Results\_PublishedWebsites\SoyalWorkTimeWebManager\Web.config";
                Console.WriteLine(xmlfile);
                Setup();

            } else if  (args[0] == "DeSetup") {
                Name = args[1];
                DeSetup();
            } else {
                Environment.Exit(100);
            }
        }

        private static string Name;
        private static string databasefile;
        private static string xmlfile;
        
        private static void Setup()
        {
            //Console.WriteLine(executeCommand("sqllocaldb p " + Name));
            //Console.WriteLine(executeCommand("sqllocaldb d " + Name));
            //Console.WriteLine(executeCommand("sqllocaldb c " + Name));
            //Console.WriteLine(executeCommand("sqllocaldb s " + Name));
            
            Console.WriteLine("Start sleep");
            Console.WriteLine("-----");
            Thread.Sleep(20000);
            Console.WriteLine("Wake up, go away");

            string address = executeCommand("sqllocaldb i " + Name + " | findstr ^pipe").Substring(20);
            string importcommand = @"CREATE DATABASE [" + Name + @"] ON ( FILENAME = N'" + databasefile + ".mdf' ), ( FILENAME = N'" + databasefile + "_log.ldf' )  FOR ATTACH ;";
            Console.WriteLine(executeCommand("sqlcmd -S " + address + @" -i .\commandfile.sql"));
            Replaceinfile(xmlfile, "SERVERADDRESS", address);
            Replaceinfile(xmlfile, "DATABASENAME", Name);
            Replaceinfile(xmlfile, "DefaultConnection", "OldConnection");
            Replaceinfile(xmlfile, "SecondConnection", "DefaultConnection");
            Console.WriteLine("xml changed");
            
        }

        private static void Replaceinfile(string file, string oldtext, string newtext)
        {
            string text = File.ReadAllText(file);
            text = text.Replace(oldtext, newtext);
            File.WriteAllText(file, text);
        }

        private static void DeSetup()
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
                    UseShellExecute = false,
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