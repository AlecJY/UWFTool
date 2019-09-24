using System;
using System.Windows.Forms;
using log4net;
using System.Management;
using Microsoft.Management.Infrastructure;
using Microsoft.Management.Infrastructure.Options;
using System.Management.Automation;

namespace UWFTool.UWFController {
    public class UWFManagement
    {
        private static readonly ILog Logger = LogManager.GetLogger(AppInfo.AppName);
        private CimInstance _cimInstance;
        private CimSession _cimSession;
        private bool _noPermission;


        public UWFManagement()
        {
            try
            {
                if (_cimSession != null)
                {
                    _cimSession.Close();
                }

                var sessionOptions = new DComSessionOptions();
                sessionOptions.Timeout = new TimeSpan(0, 2, 0);
                _cimSession = CimSession.Create(@".", sessionOptions);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            UpdateCimInstance();
        }

        private void UpdateCimInstance()
        {
            try
            {
                var enumerateInstances = _cimSession.EnumerateInstances(@"root\standardcimv2\embedded", "UWF_Filter");
                foreach (var enumerateInstance in enumerateInstances)
                {
                    _cimInstance = enumerateInstance;
                    break;
                }
            }
            catch (Exception e)
            {
                if (e.HResult == -2146233088)
                {
                    try {
                        _cimSession.GetClass(@"root\standardcimv2\embedded", "UWF_Filter");
                        _noPermission = true;
                        return;
                    } catch (Exception) { }
                    Console.Error.WriteLine(e);
                    Logger.Error(e);
                    MessageBox.Show("Cannot find Unified Write Filter.\nPlease install Unified Write Filter then run this program again.", AppInfo.AppName, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    Environment.Exit(1);
                }
                else
                {
                    Console.Error.WriteLine(e);
                    Logger.Fatal(e);
                }
            }
        }

        public bool NoPermission() {
            return _noPermission;
        }

        public bool UWFCurrentEnabled()
        {
            UpdateCimInstance();
            return (bool)_cimInstance.CimInstanceProperties["CurrentEnabled"].Value;
        }

        public bool UWFNextEnabled()
        {
            UpdateCimInstance();
            return (bool)_cimInstance.CimInstanceProperties["NextEnabled"].Value;
        }

        public void UWFEnable()
        {
            var scope = new ManagementScope(@"root\standardcimv2\embedded");
            var uwfClass = new ManagementClass(scope.Path.Path, "UWF_Filter", null);
            var instances = uwfClass.GetInstances();

            foreach (ManagementObject instance in instances)
            {
                var result = instance.InvokeMethod("Enable", null);
                break;
            }
        }

        public void UWFDisable()
        {
            var scope = new ManagementScope(@"root\standardcimv2\embedded");
            var uwfClass = new ManagementClass(scope.Path.Path, "UWF_Filter", null);
            var instances = uwfClass.GetInstances();

            foreach (ManagementObject instance in instances)
            {
                var result = instance.InvokeMethod("Disable", null);
                break;
            }
        }

        public void UWFRestartSystem()
        {
            var scope = new ManagementScope(@"root\standardcimv2\embedded");
            var uwfClass = new ManagementClass(scope.Path.Path, "UWF_Filter", null);
            var instances = uwfClass.GetInstances();

            foreach (ManagementObject instance in instances)
            {
                var result = instance.InvokeMethod("RestartSystem", null);
                break;
            }
        }
    }
}
