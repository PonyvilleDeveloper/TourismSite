using System.Net;
using System.Text;
using System.Web;
using Webtech;

namespace Tourism;

partial class ApiImpl {
    public static void Tours(HttpListenerRequest req, HttpListenerResponse res) {
        var data = Encoding.UTF8.GetBytes(DataBase.GetTours());
        res.ContentLength64 = data.Length;
        res.ContentType = "text/json";
        res.OutputStream.Write(data);
        res.Close();
    }
    public static void Places(HttpListenerRequest req, HttpListenerResponse res) {
        var data = Encoding.UTF8.GetBytes(DataBase.GetPlaces());
        res.ContentLength64 = data.Length;
        res.ContentType = "text/json";
        res.OutputStream.Write(data);
        res.Close();
    }
    public static void Comments(HttpListenerRequest req, HttpListenerResponse res) {
        var data = Encoding.UTF8.GetBytes(DataBase.GetComments(1));
        res.ContentLength64 = data.Length;
        res.ContentType = "text/json";
        res.OutputStream.Write(data);
        res.Close();
    }
    public static void AddComment(HttpListenerRequest req, HttpListenerResponse res) {
        var data = req.ParseForm();
        if(DataBase.AddComment(data["name"], Convert.ToInt32(data["tour_id"]), data["comment"])) {
            res.StatusCode = 200;
            res.Close();
            logger.Success("COMMENT", $"User {data["name"]} comment tour #{data["tour_id"]}");
        } else {
            res.ResponseError(500, "Something wrong with comment func");
            logger.Error("COMMENT", $"Error of comment tour #{data["tour_id"]} from user {data["name"]}");
        }
        HttpUtility.ParseQueryString(req.Url!.Query);
    }
}