using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

namespace EnableAnselForRoblox
{
    internal class Program
    {
        static string Robloxfolderversion;
        static string Bloxstrap;
        readonly static string anseleater = @"C:\Program Files\NVIDIA Corporation";
        readonly static System.Net.WebClient wc = new System.Net.WebClient();

        static void Main()
        {
            Console.WriteLine("Fetching Bloxstrap and latest Roblox version in Bloxstrap");
            try
            {
                Bloxstrap = (string)Registry.CurrentUser.OpenSubKey($"Software\\Bloxstrap", true).GetValue("InstallLocation");
            }
            catch (NullReferenceException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Bloxstrap isnt installed on the system. \nplease install Bloxstrap.");
                Console.ReadLine();
                Environment.Exit(69);
            }
            string[] Robloxversion = File.ReadAllLines(Bloxstrap + "\\State.json");
            foreach (string Robloxversion_ in Robloxversion)
            {
                if (Robloxversion_.Contains("\"VersionGuid\": \""))
                {
                    Robloxfolderversion = Robloxversion_.Replace("  \"VersionGuid\": \"", "").Replace("\",", "");
                }
            }
            string latestBloxstrapRoblox = Bloxstrap + "\\Versions\\" + Robloxfolderversion;
            if (Directory.Exists(latestBloxstrapRoblox))
                Console.WriteLine("Latest Bloxstrap Roblox is: " + Robloxfolderversion + "\nat " + latestBloxstrapRoblox);
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error while fetching Bloxstrap Roblox version. please contact me on Discord. \nfetch my Discord from my Discord webpage http://owo.bounceme.net");
                Console.ReadLine();
                Environment.Exit(69);
            }
            Console.WriteLine("Checking for Roblox, cloning and renaming.");
            if (File.Exists(latestBloxstrapRoblox + "\\RobloxPlayerBeta.exe"))
            {
                if (new FileInfo(latestBloxstrapRoblox + "\\RobloxPlayerBeta.exe").Length > 1000000)
                    try
                    {
                        string rpbtemp = latestBloxstrapRoblox + "\\RobloxPlayerBeta.exe";
                        File.Delete(latestBloxstrapRoblox + "\\eurotrucks2.exe");
                        File.Copy(rpbtemp, latestBloxstrapRoblox + "\\eurotrucks2.exe");
                        File.Delete(latestBloxstrapRoblox + "\\RobloxPlayerBeta.bak");
                        File.Copy(rpbtemp, latestBloxstrapRoblox + "\\RobloxPlayerBeta.bak");
                        File.Delete(rpbtemp);
                        wc.DownloadFile("https://github.com/DED0026/EnableAnselForRoblox/releases/download/Resources/BloxstrapShortcut.exe", rpbtemp);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Ansel successfully enabled!");
                        Console.WriteLine("Install default shaders?");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("   ADMINISTRATOR ONLY");
                        Console.ResetColor();
                        Console.WriteLine("        Y \\ N");
                        if (Console.ReadLine().Trim().ToUpper() == "Y")
                        {
                            Installshaderfolder();
                        }
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Thank you for using Ansel enabler for Roblox.\nPress any key to open roblox.");
                        Console.ReadLine();
                        Process.Start(Bloxstrap + "\\Bloxstrap.exe");
                        Environment.Exit(69);
                    }
                    catch (UnauthorizedAccessException meow)
                    {
                        Console.Write(meow);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Roblox was installed with administrator permissions.\nPlease restart this application with administrator permissions to fix this issue");
                        Console.ReadLine();
                        Environment.Exit(69);
                    }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Ansel is already enabled.");
                    Console.ReadLine();
                    Console.ResetColor();
                    if (Directory.Exists(anseleater + "\\Ansel"))
                    {
                        Installshaderfolder();
                    }
                    Environment.Exit(69);
                }
            }
        }

        static void Installshaderfolder()
        {
            if (Directory.Exists(anseleater + "\\Ansel"))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("The Ansel filters folder was already found on the system.\nwould you like to delete it to automatically download the recommended filters?");
                Console.ResetColor();
                Console.WriteLine("Y \\ N");
                if (Console.ReadLine().Trim().ToUpper() == "Y")
                {
                    Directory.Delete(anseleater, true);
                }
                else { return; }
            }
            wc.DownloadFile("https://github.com/DED0026/EnableAnselForRoblox/releases/download/Resources/Ansel.zip", "Ansel.zip");
            ZipFile.ExtractToDirectory("Ansel.zip", anseleater);
            File.Delete("Ansel.zip");
            if (Directory.Exists(anseleater + "\\Ansel"))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Ansel filters have been downloaded successfully.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("RAHHHH");
            }
        }
    }
}
