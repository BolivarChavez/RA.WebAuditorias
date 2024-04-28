using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAuditorias.Views
{
    public partial class PlantillaMutuos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializedView();
            }
        }

        private void InitializedView()
        {
            string[] arrayParametros;
            arrayParametros = Request.QueryString["plantilla"].Split('|');

            Auditoria.Value = arrayParametros[0] + "-" + arrayParametros[1];
            Codigo.Value = "0";
            Tarea.Value = arrayParametros[2] + "-" + arrayParametros[3];
            Plantilla.Value = arrayParametros[4] + "-" + arrayParametros[5];
        }

        protected void BtnNuevo_ServerClick(object sender, EventArgs e)
        {
            InitializedView();
        }

        protected void BtnBuscar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "document.getElementById('profile-tab').click(); LlenaGrid();", true);
        }

        protected void BtnGrabar_ServerClick(object sender, EventArgs e)
        {

        }

        protected void BtnEliminar_ServerClick(object sender, EventArgs e)
        {

        }

        protected void BtnCargar_ServerClick(object sender, EventArgs e)
        {

        }
    }
}