<%@ WebHandler Language="C#" Class="captcha" %>

using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.Web.SessionState;

public class captcha : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "image/jpeg";
        var captcha = new CatpchaImage();
        string str = captcha.DrawNumbers(5);
        if (context.Session[CatpchaImage.SESSION_CAPTCHA] == null)
            context.Session.Add(CatpchaImage.SESSION_CAPTCHA, str);
        else
        {
            context.Session[CatpchaImage.SESSION_CAPTCHA] = str;
        }
        Bitmap bmp = captcha.Result;
        bmp.Save(context.Response.OutputStream, ImageFormat.Jpeg);
    }


    public bool IsReusable
    {
        get { return true; }
    }
}