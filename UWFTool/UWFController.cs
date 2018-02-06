using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using Microsoft.Management.Infrastructure;
using Microsoft.Management.Infrastructure.Options;

namespace UWFTool {
    class UWFController {
        private static readonly ILog Logger = LogManager.GetLogger(AppInfo.AppName);
        private CimInstance _cimInstance;
        private CimSession _cimSession;

        public UWFController() {
            try {
                if (_cimSession != null) {
                    _cimSession.Close();
                }
                var sessionOptions = new DComSessionOptions();
                sessionOptions.Timeout = new TimeSpan(0, 2, 0);
                _cimSession = CimSession.Create(@".", sessionOptions);
            } catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
            UpdateCimInstance();
        }

        private void UpdateCimInstance() {
            try {
                var enumerateInstances = _cimSession.EnumerateInstances(@"root\standardcimv2\embedded", "UWF_Filter");
                foreach (var enumerateInstance in enumerateInstances) {
                    _cimInstance = enumerateInstance;
                    break;
                }
            }
            catch (Exception e) {
                if (e.HResult == -2146233088) {
                    Console.Error.WriteLine(e);
                    Logger.Error(e);
                    MessageBox.Show("Cannot find Unified Write Filter.\nPlease install Unified Write Filter then run this program again.", AppInfo.AppName, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    Environment.Exit(1);
                }
                else {
                    Console.Error.WriteLine(e);
                    Logger.Fatal(e);
                }
            }
        }

        public bool UWFCurrentEnabled() {
            UpdateCimInstance();
            return (bool)_cimInstance.CimInstanceProperties["CurrentEnabled"].Value;
        }

        public bool UWFNextEnabled() {
            UpdateCimInstance();
            return (bool)_cimInstance.CimInstanceProperties["NextEnabled"].Value;
        }

        public void UWFEnable() {
            CimMethodParametersCollection parametersCollection = new CimMethodParametersCollection();
            var result = _cimSession.InvokeMethod(_cimInstance, "Enable", parametersCollection);
            if ((uint)result.ReturnValue.Value != 0)
            {
                Logger.Error("Enable UWF Failed. Error code: " + result.ReturnValue.Value);
            }
        }

        public void UWFDisable() {
            CimMethodParametersCollection parametersCollection = new CimMethodParametersCollection();
            var result = _cimSession.InvokeMethod(_cimInstance, "Disable", parametersCollection);
            if ((uint)result.ReturnValue.Value != 0)
            {
                Logger.Error("Disable UWF Failed. Error code: " + result.ReturnValue.Value);
            }
        }

        public void UWFRestartSystem() {
            CimMethodParametersCollection parametersCollection = new CimMethodParametersCollection();
            var result = _cimSession.InvokeMethod(_cimInstance, "RestartSystem", parametersCollection);
            if ((uint)result.ReturnValue.Value != 0)
            {
                Logger.Error("Restart System Failed. Error code: " + result.ReturnValue.Value);
            }
        }
    }
}
