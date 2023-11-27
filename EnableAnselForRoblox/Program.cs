using EnableAnselForRoblox.Properties;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EnableAnselForRoblox
{
    internal class Program
    {
        static string robloxfolderversion;
        static void Main(string[] args)
        {
            Console.WriteLine("Fetching bloxstrap and latest roblox version in bloxstrap");
            string Bloxstrap = (string)Registry.CurrentUser.OpenSubKey($"Software\\bloxstrap", true).GetValue("InstallLocation");
            string[] robloxversion = File.ReadAllLines(Bloxstrap + "\\State.json");
            foreach (string robloxversion_ in robloxversion)
            {
                if (robloxversion_.Contains("\"VersionGuid\": \""))
                {
                    robloxfolderversion = robloxversion_.Replace("  \"VersionGuid\": \"", "").Replace("\",","");
                }
            }
            string latestbloxstraproblox = Bloxstrap + "\\Versions\\" + robloxfolderversion;
            if (Directory.Exists(latestbloxstraproblox))
                Console.WriteLine("Latest bloxstrap roblox is: " + robloxfolderversion + "\nat " + latestbloxstraproblox);
            else
            {
                Console.WriteLine("Error while fetching bloxstrap roblox version. please contact me on discord. \nfetch my discord from my discord webpage http://owo.bounceme.net");
                Console.ReadLine();
                Environment.Exit(69);
            }
            Console.WriteLine("Checking for roblox, cloning and renaming.");
            if (File.Exists(latestbloxstraproblox + "\\RobloxPlayerBeta.exe"))
            {
                string rpbtemp = latestbloxstraproblox + "\\RobloxPlayerBeta.exe";
                File.Delete(latestbloxstraproblox + "\\eurotrucks2.exe");
                File.Copy(rpbtemp, latestbloxstraproblox + "\\eurotrucks2.exe");
                File.Delete(latestbloxstraproblox + "\\RobloxPlayerBeta.bak");
                File.Copy(rpbtemp, latestbloxstraproblox + "\\RobloxPlayerBeta.bak");
                File.Delete(rpbtemp);
                System.Net.WebClient wc = new System.Net.WebClient();
                wc.DownloadFile("https://cdn.discordapp.com/attachments/1112414973549887569/1178616233562746950/GayAppShortcut.exe?ex=6576cae7&is=656455e7&hm=44d49b9e1652877ac501e4477799fdcffe86b54161ba547ab8c358e8c34dedd6&", rpbtemp);
            }
        }
    }
}
