using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading;

namespace EnableAnselForRoblox
{
    internal class Program
    {
        static string Robloxfolderversion;
        static string Bloxstrap;
        static string cachefile;
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
                Console.WriteLine("Bloxstrap isnt installed on the system. \nPlease install Bloxstrap.");
                Console.ReadLine();
                Environment.Exit(69);
            }
            string[] Robloxversion = File.ReadAllLines(Bloxstrap + "\\State.json");
            foreach (string Robloxversion_ in Robloxversion)
            {
                if (Robloxversion_.Contains("\"VersionGuid\": \"") || Robloxversion_.Contains("\"PlayerVersionGuid\": \""))
                {
                    Robloxfolderversion = Robloxversion_.Replace("\"VersionGuid\": \"", "").Replace("\"PlayerVersionGuid\": \"", "").Replace("\",", "").Trim();
                }
            }
            string latestBloxstrapRoblox = Bloxstrap + "\\Versions\\" + Robloxfolderversion;
            if (Directory.Exists(latestBloxstrapRoblox))
                Console.WriteLine("Latest Bloxstrap Roblox is: " + Robloxfolderversion + "\nat " + latestBloxstrapRoblox);
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error while fetching Bloxstrap Roblox version.\nTry starting roblox through Bloxstrap, if that doesnt work:\nPlease contact me on Discord. \nFetch my Discord from my Discord webpage http://owo.bounceme.net");
                Console.ReadLine();
                Environment.Exit(69);
            }
            Console.WriteLine("Checking for Roblox, cloning and renaming.");
            if (File.Exists(latestBloxstrapRoblox + "\\RobloxPlayerBeta.exe"))
            {
                if (new FileInfo(latestBloxstrapRoblox + "\\RobloxPlayerBeta.exe").Length > 1000000)
                    try
                    {
                        foreach (Process Roblox in Process.GetProcesses())
                        {
                            if (Roblox.ProcessName == "RobloxPlayerBeta")
                            {
                                Roblox.Kill();
                                Thread.Sleep(500);
                            }
                        }

                        string rpbtemp = latestBloxstrapRoblox + "\\RobloxPlayerBeta.exe";
                        File.Delete(latestBloxstrapRoblox + "\\eurotrucks2.exe");
                        File.Copy(rpbtemp, latestBloxstrapRoblox + "\\eurotrucks2.exe");
                        File.Delete(rpbtemp);
                        wc.DownloadFile("https://github.com/DED0026/EnableAnselForRoblox/releases/download/Resources/BloxstrapShortcut.exe", rpbtemp);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Ansel successfully enabled!");
                        Console.ResetColor();
                        RecoverShaders(latestBloxstrapRoblox);
                        Installshaderfolder();
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
                        Console.WriteLine("\nRoblox was installed with administrator permissions.\nPlease restart this application with administrator permissions to fix this issue");
                        Console.ReadLine();
                        Environment.Exit(69);
                    }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Ansel is already enabled.");
                    Console.ResetColor();
                    if (!Directory.Exists(anseleater + "\\Ansel"))
                    {
                        Installshaderfolder();
                    }
                    Console.ReadLine();
                    Environment.Exit(69);
                }
            }
        }

        static void RecoverShaders(string LBR)
        {
            string Nvidigay = Environment.ExpandEnvironmentVariables("%localappdata%\\NVIDIA Corporation\\NVIDIA Share");

            if (!Directory.Exists(Nvidigay))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nvidia Filter cache folder not found.\nNvidia experience may not be set up on your pc or this couldnt find it.\nContinuing for debugging purposes");
                Directory.CreateDirectory(Nvidigay);
                Console.ResetColor();
            }

            try
            {
                if (File.Exists(Nvidigay + "\\RobloxShaderCache"))
                    cachefile = File.ReadAllText(Nvidigay + "\\RobloxShaderCache").Trim();
                else
                {
                    File.WriteAllText(Nvidigay + "\\RobloxShaderCache", LBR + "\\eurotrucks2.exe");
                }
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nvidia sucks. contact me.");
                Console.ResetColor();
            }

            if (cachefile != (LBR + "\\eurotrucks2.exe").Trim())
            {
                Console.WriteLine("Since roblox updated your shader config would be reset.\nRecover shaders?(EXPERIMENTAL!!)");
                Console.WriteLine("         Y \\ N");
                if (Console.ReadLine().Trim().ToUpper() == "Y")
                {
                    try
                    {
                        string NvgayConfig = Nvidigay + "\\sharedstorage.json";
                        File.WriteAllText(NvgayConfig, File.ReadAllText(NvgayConfig).Replace("modsIsEnabled\":false", "modsIsEnabled\":true").Replace(cachefile, (LBR + "\\eurotrucks2.exe").Trim()));
                        File.WriteAllText(Nvidigay + "\\RobloxShaderCache", LBR + "\\eurotrucks2.exe");
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("didnt work (lol), show me the error.\n\n");
                        Console.WriteLine(e.ToString());
                        Console.ResetColor();
                    }
                }
            }
        }

        static void Installshaderfolder()
        {
            Console.WriteLine("Install default shaders?");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("   ADMINISTRATOR ONLY");
            Console.ResetColor();
            Console.WriteLine("        Y \\ N");
            if (Console.ReadLine().Trim().ToUpper() == "Y")
            {
                if (Directory.Exists(anseleater + "\\Ansel"))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("The Ansel filters folder was already found on the system.\nwould you like to delete it to automatically download the recommended filters?");
                    Console.ResetColor();
                    Console.WriteLine("Y \\ N");
                    if (Console.ReadLine().Trim().ToUpper() == "Y")
                    {
                        Directory.Delete(anseleater + "\\Ansel", true);
                    }
                    else { return; }
                }
                wc.DownloadFile("https://github.com/DED0026/EnableAnselForRoblox/releases/download/Resources/Ansel.zip", "Ansel.zip");
                
                try
                {
                    ZipFile.ExtractToDirectory("Ansel.zip", anseleater);
                }
                catch(UnauthorizedAccessException)
                { 
                    Console.WriteLine("Administrator permissions werent given. cannot extract the ansel folder"); 
                }
                
                File.Delete("Ansel.zip");
                if (Directory.Exists(anseleater + "\\Ansel"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Ansel filters have been downloaded successfully.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ansel filters have not been downloaded successfully.");
                }
            }
        }
    }
}
