using System;
using System.Linq;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace FS_WS_WSCTFW.WSCaller
{
    public partial class ManageEnvironments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }

            LoadGrid();

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try {
                if (!User.Identity.IsAuthenticated)
                {
                    Response.Redirect("~/Account/Login.aspx");
                }

                FS_WS_WSCTFW.Models.WSEnvironmentsModel WSEnvMode = new Models.WSEnvironmentsModel();

                WSEnvMode.WSEnvName = txtEnvName.Text.ToString();
                WSEnvMode.WSURL = txtEnvUrl.Text.ToString();
                WSEnvMode.WSEnvEndPoint = txtEnvEndPoint.Text.ToString();
                WSEnvMode.WSEnvWSDL = txtEnvWSDL.Text.ToString();
                WSEnvMode.WSUsername = txtUseName.Text.ToString();
                WSEnvMode.WSPasswd = txtUserPsswrd.Text.ToString();
                WSEnvMode.isPublic = chkPublic.Checked;
                WSEnvMode.isActive = checkisActive.Checked;
                WSEnvMode.isDeleted = false;
                WSEnvMode.CreatedBy = User.Identity.GetUserId();

                WSEnvMode.CreatedDate = DateTime.Now;



                FS_WS_WSCTFW.Models.ApplicationDbContext db = new Models.ApplicationDbContext();

                db.WSEnvironmentsModels.Add(WSEnvMode);
                int result = db.SaveChanges();
                if (result > 0)
                {
                    if (Helpers.WSHelper.AddWSDetails(txtEnvWSDL.Text, WSEnvMode.WSEnvID, User.Identity.GetUserId()))
                          {
                        Response.Write("<script>alert('Record Save Successfully');</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Error Occured Please try again.');</script>");
                    }
                    LoadGrid();
                }
            }
            catch (Exception ex)
            {
                Helpers.Logger.Error(ex);
            }
        }





        public void LoadGrid()
        {

            string currentUserId = User.Identity.GetUserId();
            
            FS_WS_WSCTFW.Models.ApplicationDbContext db = new Models.ApplicationDbContext();
            var WSEnvironments = from w in db.WSEnvironmentsModels
                                 where w.isActive == true && w.isDeleted == false && w.CreatedBy == currentUserId
                                 select new
                                 {
                                    Environment_Name = w.WSEnvName,
                                    URL =  w.WSURL,
                                    EndPoint = w.WSEnvEndPoint,
                                    WSDL =  w.WSEnvWSDL,
                                    Username =   w.WSUsername,
                                    Password =  w.WSPasswd
                                 };


            DG_AllData.DataSource = WSEnvironments.ToList();
            DG_AllData.DataBind();

        }
    }
}