using System;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Diagnostics;
using Microsoft.Win32;

namespace UWFTool.Installer {
    class Program {
        private static readonly string AppDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                                                Path.DirectorySeparatorChar;

        static void Main(String[] args) {
            if (args.Length == 2) {
                switch (args[0]) {
                    case "/install":
                        try {
                            NTAccount account = new NTAccount(args[1]);
                            SecurityIdentifier securityIdentifier =
                                (SecurityIdentifier) account.Translate(typeof(SecurityIdentifier));
                            string sid = securityIdentifier.ToString();
                            RegistryKey run =
                                Registry.Users.OpenSubKey(sid + @"\Software\Microsoft\Windows\CurrentVersion\Run",
                                    true);
                            run.SetValue("UWFTool", AppDir + "UWFTool.exe");
                        } catch (Exception e) {
                            Console.Error.WriteLine(e);
                        }

                        break;
                    case "/uninstall":
                        try {
                            NTAccount account = new NTAccount(args[1]);
                            SecurityIdentifier securityIdentifier =
                                (SecurityIdentifier) account.Translate(typeof(SecurityIdentifier));
                            string sid = securityIdentifier.ToString();
                            RegistryKey run =
                                Registry.Users.OpenSubKey(sid + @"\Software\Microsoft\Windows\CurrentVersion\Run",
                                    true);
                            run.DeleteValue("UWFTool");
                        } catch (Exception e) {
                            Console.WriteLine(e);
                            throw;
                        }
                        break;
                }
            }
            if(args.Length == 1)
            {
                string cmd = "";
                switch (args[0])
                {
                    case "/installUWF":
                        cmd = "DISM /Online /enable-Feature /FeatureName:client-UnifiedWriteFilter /all /Quiet";
                        break;
                    case "/uninstallUWF":
                        cmd = "DISM /Online /disable-Feature /FeatureName:client-UnifiedWriteFilter /Quiet";
                        break;
                }
                ProcessStartInfo pStartInfo = new ProcessStartInfo("cmd.exe", "/c " + cmd);
                Process p = new Process();
                p.StartInfo = pStartInfo;
                p.Start();
            }
        }
    }
}