using System;
using System.Net;
using System.Reflection;
using System.Diagnostics;
using Webtech;
using static System.IO.File;

#nullable disable

namespace Tourism {
    partial class ApiImpl {
        public static Logger logger;
        public static string WorkDir;
        public static AccessManager accessor;
        private static DBManager DataBase;

        static ApiImpl() {
            WorkDir = "F:\\Свят\\Documents\\Programming\\C#\\Tourism\\"; //Assembly.GetExecutingAssembly().Location;
            logger = new Logger{LogFile = $"{WorkDir}/logs/{DateTime.Today.Day}{DateTime.Today.Month}{DateTime.Today.Year}.log"};
            accessor = new(600);
            DataBase = new($"{WorkDir}/resources/data/DataBase.db");
        }
        public static void Resource(HttpListenerRequest req, HttpListenerResponse res) {
            if (!Exists($"{WorkDir}{req.RawUrl}")) {
                logger.Error("API-Resource", $"Non-existed or not founded resource requested {req.RawUrl}");
                res.ResponseError(404, "Non-existed or not founded resource requested");
                return;
            }
            logger.Info("API-Resource", "Resource requested " + req.RawUrl);
            res.ResponseFile($"{WorkDir}/{req.RawUrl}");
        }
        public static void TechInfo(HttpListenerRequest req, HttpListenerResponse res) {
            logger.Warning("API-TechInfo", "Tech info requested");
            var server = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            var result = $@"Assembly: {Assembly.GetExecutingAssembly().Location}
Company: {server.CompanyName}
Version: {server.FileVersion}
Comments: {server.Comments}

[Request]:
    User-Agent: {req.UserAgent}
    Method: {req.HttpMethod}
    Auth: {req.IsAuthenticated}
    HasData: {req.HasEntityBody}

    -Headers-
    
{req.Headers}
    Cookies: {string.Join('\n', req.Cookies)}";
            res.ResponseText(result);
        }
        public static void Page(HttpListenerRequest req, HttpListenerResponse res) {
            if (!Exists($"{WorkDir}resources/pages{req.RawUrl}.html")) {
                logger.Error("API-Page", $"Non-existed or not founded page requested {req.RawUrl}");
                res.ResponseError(404, "Non-existed or not founded page requested");
                return;
            }
            logger.Info("API-Page", "Page requested " + req.RawUrl);
            res.ResponseFile($"{WorkDir}resources/pages{req.RawUrl}.html");
        }
    }
}