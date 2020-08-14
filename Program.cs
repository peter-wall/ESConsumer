using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ESConsumer
{
    public class RegionInfo
    {
        public string CN;
        public string description;
        public string mfServerStatus;
        public int mfCAS64Bit;
        public int mfCASMTOEnabled;
        public int mfCASBatchEnabled;
        public int mfCASIMSEnabled;
        public int mfCASPLIEnabled;
        public string mfStatusHistory;
        public string mfCASPAC;
        public string mfCASSOR;
    }

    public class RegionProperties
    {
        public int mfStructVersion;
        public string CN;
        public string mfUID;
        public string description;
        public string mfError;
        public string mfStatusHistory;
        public string mfConfig;
        public int mfMajorVersion;
        public int mfMinorVersion;
        public int mfBuildVersion;
        public int mfSearchOrder;
        public string mfServerLocal;
        public string mfServerStatus;
        public int mfServiceCount;
        public int mfListenerCount;
        public int mfPackageCount;
        public int mfHandlerCount;
        public int mfSecondaryCommsServerCount;
        public string mfServerType;
        public string mfDateTimeStatusChanged;
        public string mfDateTimeRegistered;
        public string mfDateTimeQueried;
        public int mfCASStructVersion;
        public int mfCASXRMCount;
        public int mfCASShMemPages;
        public int mfCASSICount;
        public int mfCASShMemCushion;
        public int mfCASTraceTableSize;
        public int mfCASLocalTraceSize;
        public int mfCASTraceMaxSize;
        public int mfCASRequestedLicenses;
        public int mfCASAllocatedLicenses;
        public int mfCASConsoleLogSize;
        public int mfCASHSFEnabled;
        public int mfCASHSFWriteToDisk;
        public int mfCASHSFMaxFileSize;
        public int mfCASHSFMaxDisplayRecords;
        public int mfCASHSFCreateJCLFileRecords;
        public int mfCASDumpOnAbend;
        public int mfCASColdStartTrace;
        public int mfCASAuxiliaryTrace;
        public int mfCASShowConsole;
        public int mfCASRemoteDebug;
        public int mfCASStartOnSystemStart;
        public int mfCAS64Bit;
        public int mfCASMTOEnabled;
        public int mfCASCICSPersonality;
        public int mfCASPurgeLogs;
        public int mfCASPerformanceMonitor;
        public int mfCASEventLogInformational;
        public int mfCASEventLogWarning;
        public int mfCASEventLogError;
        public int mfCASEventLogSevere;
        public int mfCASIRDSWarmStart;
        public int mfCASIRDSMaxSize;
        public int mfCASIRDSBufferQueueCount;
        public string mfCASAutoExecuteUser;
        public int mfCASTraceFlagsTaskControl;
        public int mfCASTraceFlagsStorageControl;
        public int mfCASTraceFlagsTableManagement;
        public int mfCASTraceFlagsApplicationContainer;
        public int mfCASTraceFlagsRequestHandler;
        public int mfCASTraceFlagsRMInterface;
        public int mfCASTraceFlagsCommunications;
        public int mfCASTraceFlagsApplication;
        public int mfCASTraceFlagsExit;
        public string mfCASCICSSIT;
        public string mfCASTXTRANP;
        public string mfCASTXFILEP;
        public string mfCASTXMAPP;
        public string mfCASTXRDTP;
        public int mfCASCICSEZASOKET;
        public int mfCASBatchEnabled;
        public string mfCASJCLPATH;
        public string mfCASMFSYSCAT;
        public string mfCASJCLALLOCLOC;
        public string mfCASJCLSYSPROCLIB;
        public int mfCASJCLMainframePollInterval;
        public int mfCASIMSEnabled;
        public string mfCASIMSCodesetBias;
        public string mfCASIMSMFSAttributeBias;
        public string mfCASIMSMFSNullChar;
        public int mfCASIMSKeypointFrequency;
        public int mfCASIMSDefaultMQName;
        public int mfCASIMSTrailingSpace;
        public string mfCASIMSGEN;
        public string mfCASIMSDBDATPath;
        public int mfCASIMSDBLockedResourceTimeout;
        public int mfCASIMSDBKeyCompression;
        public int mfCASIMSDBDataCompression;
        public int mfCASIMSDBIRLMLocking;
        public int mfCASIMSDBDumpOnLockTimeout;
        public int mfCASIMSDBDumpOnDeadlock;
        public int mfCASIMSDBRollforwardRecovery;
        public int mfCASIMSTMTransThreshold;
        public int mfCASIMSTMMQMaxBlocks;
        public int mfCASIMSTMMQBufferCount;
        public string mfCASIMSTMMQColdStart;
        public int mfCASIMSTMMQPersistColdStart;
        public string mfCASIMSTMMFSPath;
        public string mfCASIMSTMAppPath;
        public string mfCASIMSTMTranDefPath;
        public int mfCASPLIEnabled;
        public int mfCASPLIUseJavaDebug;
        public int mfCASPLIPromptPLITEST;
        public int mfMQLEnabled;
        public int mfESEnableStartScript;
        public int mfESEnableStopScript;
        public int mfESEnableNotRespondingScript;
        public int mfESRetries;
        public string mfCASPAC;
        public string mfCASSOR;
    }

    public class ESCWARegions
    {
        public string MFDSName;
        public string MFDSHost;
        public int MFDSPort;
        public RegionInfo[] RegionList;
    }

    class Program
    {
        static string cookie = string.Empty;
        static string content = string.Empty;
        static void Main(string[] args)
        {
            //XMLHttpRequest 
            //HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://localhost:10086/native/v1/regions/127.0.0.1/Validate");
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://localhost:10086/validate");
            req.Method = "GET";
            req.Headers.Set("X-Requested-With", "XMLHttpRequest");
            req.Referer = "http://localhost:10086";
            req.ContentType = "application/json";
            req.Accept = "application/json";

            
            //logoff.Method = "GET";
            //logoff.Headers.Set("X-Requested-With", "XMLHttpRequest");
            //// req.Host = "localhost:10086";
            //logoff.Referer = "http://localhost:10086";
            //logoff.ContentType = "application/json";
            //logoff.Accept = "application/json";

            //string content = string.Empty;

            HttpWebResponse response;
            response = MakeGETRequest(req);
            {
                int a = response.Cookies.Count;
                cookie = response.Headers.Get("Set-Cookie");
                GetResponse(response, true);

                req = (HttpWebRequest)WebRequest.Create("http://localhost:10086/server/v1/info");
                response = MakeGETRequest(req);
                GetResponse(response, true);

                req = (HttpWebRequest)WebRequest.Create("http://localhost:10086/server/v1/config/mfds/1");
                response = MakeGETRequest(req);
                GetResponse(response, true);

                req = (HttpWebRequest)WebRequest.Create("http://localhost:10086/native/v1/regions/127.0.0.1/86");
                response = MakeGETRequest(req);
                GetResponse(response);
                string jsonValue = content;
                ESCWARegions regions = JsonConvert.DeserializeObject<ESCWARegions>(jsonValue);

                for (int i = 0; i < regions.RegionList.Length; i++)
                {
                    Console.WriteLine(regions.RegionList[i].CN);
                }

                req = (HttpWebRequest)WebRequest.Create("http://localhost:10086/logoff");
                response = MakeGETRequest(req);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Console.WriteLine("Logged off OK");
                }
            }
        }

        static void GetResponse(HttpWebResponse response, bool doDisplay = false)
        {
            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            content = sr.ReadToEnd();
            if (doDisplay)
                Console.WriteLine(content);
        }
        static HttpWebResponse MakeGETRequest(HttpWebRequest req)
        {
            req.Method = "GET";
            req.Headers.Set("X-Requested-With", "XMLHttpRequest");
            if (cookie.Length > 0)
                req.Headers.Set("Set-Cookie", cookie);
            req.Referer = "http://localhost:10086";
            req.ContentType = "application/json";
            req.Accept = "application/json";

            return (HttpWebResponse)req.GetResponse();
        }
    }
}
