using System;

namespace WriteOnImage
{
    public partial class Certificate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                imgCertificate.ImageUrl = "~/Certificates/" + Request.QueryString["id"] + ".jpg";
                lblMessage.Text = "Dynamically generated certificate:";
            }
            else
            {
                Response.Redirect("~/Default.aspx?error=id_parameter_missing");
            }
        }
    }
}