using System;
using System.Configuration;
using System.Net;

namespace WebAuditorias.Views
{
    public partial class VistaArchivo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string filePath = Server.MapPath("~") + ConfigurationManager.AppSettings["PathDocs"] + @"\" +Request.QueryString["archivo"];

                WebClient User = new WebClient();
                Byte[] FileBuffer = User.DownloadData(filePath);
                if (FileBuffer != null)
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", FileBuffer.Length.ToString());
                    Response.BinaryWrite(FileBuffer);
                }
            }
        }
    }
}