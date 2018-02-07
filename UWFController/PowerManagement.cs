using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Microsoft.Management.Infrastructure;
using Microsoft.Management.Infrastructure.Options;

namespace UWFTool.UWFController {
    class PowerManagement {
        private static readonly ILog Logger = LogManager.GetLogger(AppInfo.AppName);
        private CimInstance _cimInstance;
        private CimSession _cimSession;

        public PowerManagement() {
            try {
                if (_cimSession != null) {
                    _cimSession.Close();
                }

                var sessionOptions = new DComSessionOptions();
                sessionOptions.Timeout = new TimeSpan(0, 2, 0);
                _cimSession = CimSession.Create(@".", sessionOptions);
            } catch (CimException e) {
                Console.WriteLine(e);
                throw;
            }

            UpdateCimInstance();
        }

        private void UpdateCimInstance() {
            try {
                var enumerateInstances = _cimSession.EnumerateInstances(@"root\cimv2", "Win32_OperatingSystem");
                foreach (var enumerateInstance in enumerateInstances) {
                    _cimInstance = enumerateInstance;
                    break;
                }
            } catch (Exception e) {
                if (e.HResult == -2146233088) {
                    Console.Error.WriteLine(e);
                    Logger.Error(e);
                } else {
                    Console.Error.WriteLine(e);
                    Logger.Fatal(e);
                }
            }
        }

        public void PowerOff() {
            CimMethodParametersCollection parametersCollection = new CimMethodParametersCollection();
            parametersCollection.Add(CimMethodParameter.Create("Flags", 12, CimType.SInt32, 0));
            var result = _cimSession.InvokeMethod(_cimInstance, "Win32Shutdown", parametersCollection);
            if ((uint) result.ReturnValue.Value != 0) {
                Logger.Error("Power off computer failed. Error code: " + result.ReturnValue.Value);
            }
        }

        public void Reboot() {
            CimMethodParametersCollection parametersCollection = new CimMethodParametersCollection();
            parametersCollection.Add(CimMethodParameter.Create("Flags", 6, CimType.SInt32, 0));
            var result = _cimSession.InvokeMethod(_cimInstance, "Win32Shutdown", parametersCollection);
            if ((uint) result.ReturnValue.Value != 0) {
                Logger.Error("Reboot computer failed. Error code: " + result.ReturnValue.Value);
            }
        }
    }
}