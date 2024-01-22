using System.Net;
using Webtech;

namespace Tourism {
    partial class ApiImpl {
        public static void Auth(HttpListenerRequest req, HttpListenerResponse res) {
            var form = req.ParseForm();
            if(DataBase.ExistUser(form["login"], form["password"])) {
                res.AppendCookie(accessor.Authorize(res));
                res.Redirect("/main");
                res.Close();
                logger.Success("AUTH", $"Successfully logined {form["login"]}.");
            } else {
                res.ResponseError(401, "Non-registered user");
                logger.Warning("AUTH", $"Unknown user \"{form["login"]}\" trying to log in.");
            }
        }
        public static void Register(HttpListenerRequest req, HttpListenerResponse res) {
            var form = req.ParseForm();
            if(DataBase.AddUser(form["login"], form["password"], form["name"])) {
                res.AppendCookie(accessor.Authorize(res));
                res.Redirect("/main");
                res.Close();
                logger.Success("REG", $"Successfully registered {form["login"]}.");
            } else {
                res.ResponseError(500, "User registration failed.");
                logger.Error("REG", $"Fail to register user \"{form["login"]}\"");
            }
        }
    }
}