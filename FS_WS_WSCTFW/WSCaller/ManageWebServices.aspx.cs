using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace FS_WS_WSCTFW.WSCaller
{
    public partial class ManageWebServices : System.Web.UI.Page
    {

        protected void Page_init(object sender, EventArgs e)
        {


            //if (Session["getControls"] != null)
            //{
            //    Panel getcontrol = (Panel)Session["getControls"];
            //    pnldHtml = getcontrol;
            //    //      Session.Remove("getControls");
            //}
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }


            if (IsPostBack)
            {

                DynamicFormRenderer(int.Parse(wsMethod.SelectedValue));
            }



            if (!IsPostBack)
            {
                FillEnvironmentCombos();
                FillMethodCombos(int.Parse(WSEnv.SelectedValue));


                //if (Session["getControls"] != null)
                //{
                //    //Panel getcontrol = (Panel)Session["getControls"];
                //    //pnldHtml = getcontrol;
                //    //      Session.Remove("getControls");
                //}


            }


        }


        public void FillEnvironmentCombos()
        {

            string userid = User.Identity.GetUserId();
            Models.ApplicationDbContext db = new Models.ApplicationDbContext();
            var WSEnvironments = from Environments in db.WSEnvironmentsModels
                                     //      where Environments.isDeleted == false && Environments.isActive == true ||  Environments.isPublic == true
                                 where (Environments.CreatedBy == userid && Environments.isActive == true && Environments.isDeleted == false)
                                 || (Environments.isPublic == true && Environments.isActive == true && Environments.isDeleted == false)
                                 orderby Environments.WSEnvName
                                 select new { Environments.WSEnvID, Environments.WSEnvName };
            WSEnv.DataSource = WSEnvironments.ToList();
            WSEnv.DataValueField = "WSEnvID";
            WSEnv.DataTextField = "WSEnvName";

            WSEnv.DataBind();
            WSEnv.SelectedValue = (WSEnv.Items.Count - (WSEnv.Items.Count - 1)).ToString();


        }
        public void FillMethodCombos(int EnvironmentID)
        {

            string userid = User.Identity.GetUserId();
            Models.ApplicationDbContext db = new Models.ApplicationDbContext();
            var Wsmethods = from objMethods in db.WSMethodsModels
                                //      where Environments.isDeleted == false && Environments.isActive == true ||  Environments.isPublic == true
                            where (objMethods.isActive == true && objMethods.isDeleted == false && objMethods.WSEnvID == EnvironmentID)
                            orderby objMethods.MethodName
                            select new { objMethods.WSMethodsID, MethodaBindingName = objMethods.MethodName + "-" + objMethods.Binding };
            wsMethod.DataSource = Wsmethods.ToList();
            wsMethod.DataValueField = "WSMethodsID";
            wsMethod.DataTextField = "MethodaBindingName";

            wsMethod.DataBind();

        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                Page.EnableViewState = true;


                if (!User.Identity.IsAuthenticated)
                {
                    Response.Redirect("~/Account/Login.aspx");
                }

                //foreach (var control in pnldHtml.Controls)
                //{
                //    Helpers.Logger.Info(control.GetType());
                //}

                //      Session["getControls"] = pnldHtml;
                int envId = int.Parse(WSEnv.SelectedValue);




                Models.ApplicationDbContext envLookup = new Models.ApplicationDbContext();
                var WSEnvironments = (from Environments in envLookup.WSEnvironmentsModels
                                          //      where Environments.isDeleted == false && Environments.isActive == true ||  Environments.isPublic == true
                                      where (Environments.WSEnvID == envId)
                                      select Environments.WSEnvWSDL).SingleOrDefault();



                string WSWSDL = WSEnvironments.ToString();
                string methoditemname = wsMethod.SelectedItem.ToString();
                string[] methodaname = methoditemname.Split('-');
                //Call Webservice
                //     object[] paramvalues = getParameterValues(int.Parse(wsMethod.SelectedValue));
                object[] dynparamvalues = getFormValues();

                txtWSResponse.Visible = true;
                txtWSResponse.Text = Helpers.WSHelper.CallWS(WSWSDL, txtWSRequest.Text.ToString(), methodaname[0], dynparamvalues);

                Models.WSWebServicesModel WSCaller = new Models.WSWebServicesModel();

                WSCaller.WSEnvID = int.Parse(WSEnv.SelectedValue);
                WSCaller.RequestDetails = txtWSRequest.Text.ToString();
                WSCaller.ResponseDetails = txtWSResponse.Text.ToString();
                WSCaller.isActive = true;
                WSCaller.isDeleted = false;
                WSCaller.CreatedBy = User.Identity.GetUserId();
                WSCaller.WSMethodID = int.Parse(wsMethod.SelectedValue);
                WSCaller.CreatedDate = DateTime.Now;



                Models.ApplicationDbContext db = new Models.ApplicationDbContext();

                db.WSWebServicesModels.Add(WSCaller);
                int result = db.SaveChanges();
                if (result > 0)
                {
                    //    Response.Write("<script>alert('Response Received Successfully');</script>");

                    int insertParamResult = InsertParameterValues(WSCaller.WSMethodID,WSCaller.WSWebServiceRequestID);

                    if (insertParamResult != -1)
                    {

                    }
                    else
                    {
                        Response.Write("<script>alert('Error Occured while inserting parameter values. Please try again.');</script>");
                    }
                }
                else
                {
                    //     Response.Write("<script>alert('Error Occured Please try again.');</script>");
                }
                if (Session["getControls"] != null)
                {
                    Panel getcontrol = (Panel)Session["getControls"];
                    pnldHtml = getcontrol;
                    //        Session.Remove("getControls");
                }
            }
            catch (Exception ex)
            {
                Helpers.Logger.Error(ex);
            }
        }

        protected void wsMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtWSRequest.Text = "";
            txtWSResponse.Text = "";
            getParameterDetails(int.Parse(wsMethod.SelectedValue));
            //  int paramcount = getParameterCount(int.Parse(wsMethod.SelectedValue));
            //Session.Remove("getControls");
            pnldHtml.Controls.Clear();
            DynamicFormRenderer(int.Parse(wsMethod.SelectedValue));
        }

        protected void WSEnv_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillMethodCombos(int.Parse(WSEnv.SelectedValue));
            txtWSRequest.Text = "";
            txtWSResponse.Text = "";
            // Session.Remove("getControls");
            pnldHtml.Controls.Clear();
        }

        public void getParameterDetails(int MethodID)
        {
            try
            {
                string userid = User.Identity.GetUserId();
                Models.ApplicationDbContext db = new Models.ApplicationDbContext();
                var WsParameters = from objParameters in db.WSMethodParametersModels
                                       //      where Environments.isDeleted == false && Environments.isActive == true ||  Environments.isPublic == true
                                   where (objParameters.isActive == true && objParameters.isDeleted == false && objParameters.WSMethodID == MethodID && objParameters.ParameterType == "Input")
                                   select new { objParameters.ParameterName };

                txtWSRequest.Text = "";

                foreach (var parametername in WsParameters.ToList())
                {
                    txtWSRequest.Text = txtWSRequest.Text + Environment.NewLine + parametername.ParameterName + " = [" + parametername.ParameterName + "] ";

                }
            }

            catch (Exception ex)
            {
                Helpers.Logger.Error(ex);

            }
        }

        public Dictionary<string, string> getParameterTypes(int MethodID)
        {
            try
            {
                Dictionary<string, string> mParamsType = new Dictionary<string, string>();

                Models.ApplicationDbContext db = new Models.ApplicationDbContext();
                var WsParameters = from objParameters in db.WSMethodParametersModels
                                       //      where Environments.isDeleted == false && Environments.isActive == true ||  Environments.isPublic == true
                                   where (objParameters.isActive == true && objParameters.isDeleted == false && objParameters.WSMethodID == MethodID && objParameters.ParameterType == "Input")
                                   select new { objParameters.ParameterName, objParameters.ParameterDataType };

                //   txtWSRequest.Text = "";

                foreach (var parametername in WsParameters.ToList())
                {
                    mParamsType.Add(parametername.ParameterName, parametername.ParameterDataType);
                }

                return mParamsType;
            }

            catch (Exception ex)
            {
                Helpers.Logger.Error(ex);
                return null;
            }
        }


        public int getParameterDBID(int MethodID, string ParameterName)
        {
            try
            {
                int getParameterDBID = 0;

                Models.ApplicationDbContext db = new Models.ApplicationDbContext();
                var WsParameters = (from objParameters in db.WSMethodParametersModels
                                       //      where Environments.isDeleted == false && Environments.isActive == true ||  Environments.isPublic == true
                                   where (objParameters.isActive == true && objParameters.isDeleted == false && objParameters.WSMethodID == MethodID && objParameters.ParameterType == "Input" && objParameters.ParameterName == ParameterName)
                                   select new { objParameters.WSMethodParametersID });

                //   txtWSRequest.Text = "";

                var item = WsParameters.Single();
    
                getParameterDBID = int.Parse(item.WSMethodParametersID.ToString());
                return getParameterDBID;
            }

            catch (Exception ex)
            {
                Helpers.Logger.Error(ex);
                return -1;
            }
        }


        public object[] getConstantParameterValues(int MethodID)
        {
            try
            {


                string userid = User.Identity.GetUserId();
                Models.ApplicationDbContext db = new Models.ApplicationDbContext();
                var WsParameters = from objParameters in db.WSMethodParametersModels
                                       //      where Environments.isDeleted == false && Environments.isActive == true ||  Environments.isPublic == true
                                   where (objParameters.isActive == true && objParameters.isDeleted == false && objParameters.WSMethodID == MethodID)
                                   select objParameters.WSMethodParametersID;


                int totalCount = WsParameters.Count();




                WsParameters = from objParameters in db.WSMethodParametersModels
                                   //      where Environments.isDeleted == false && Environments.isActive == true ||  Environments.isPublic == true
                               where (objParameters.isActive == true && objParameters.isDeleted == false && objParameters.WSMethodID == MethodID && objParameters.ParameterType == "Input")
                               select objParameters.WSMethodParametersID;




                int inputparameters = WsParameters.Count();



                //   object[] myobj = new object[totalCount];
                object[] myobj = new object[inputparameters];


                for (int i = 0; i < inputparameters; i++)
                {
                    myobj[i] = "1";
                }

                //for (int i = totalCount-1 ; i > totalCount-inputparameters-1; i--)
                //{
                //    myobj[i] = "1";
                //}
                return myobj;
            }

            catch (Exception ex)
            {
                Helpers.Logger.Error(ex);
                return null;

            }
        }


        public int getParameterCount(int MethodID)
        {
            try
            {
                string userid = User.Identity.GetUserId();
                Models.ApplicationDbContext db = new Models.ApplicationDbContext();
                var WsParameters = from objParameters in db.WSMethodParametersModels
                                       //      where Environments.isDeleted == false && Environments.isActive == true ||  Environments.isPublic == true
                                   where (objParameters.isActive == true && objParameters.isDeleted == false && objParameters.WSMethodID == MethodID && objParameters.ParameterType == "Input")
                                   select new { objParameters.ParameterName };

                return WsParameters.Count();
            }

            catch (Exception ex)
            {
                Helpers.Logger.Error(ex);
                return -1;

            }
        }
        public void DynamicFormRenderer(int MethodID)
        {
            try
            {
                Dictionary<string, string> mParamTypes = new Dictionary<string, string>();

                mParamTypes = getParameterTypes(MethodID);
                pnldHtml.Controls.Clear();
                for (int i = 0; i < mParamTypes.Count; i++)
                {
                    var item = mParamTypes.ElementAt(i);


                    Label newLbl = new Label();
                    newLbl.ID = "CTRLlbl_" + item.Key + "_" + item.Value + "_DYClbl_" + i;
                    newLbl.Text = item.Key;
                    newLbl.EnableViewState = true;
                    pnldHtml.Controls.Add(newLbl);
                    pnldHtml.Controls.Add(new LiteralControl("<br />"));

                    TextBox newtxtBox = new TextBox();
                    newtxtBox.ID = "CTRLtxt_" + item.Key + "_" + item.Value + "_DYCtxt_" + i;
                    newtxtBox.EnableViewState = true;
                    pnldHtml.Controls.Add(newtxtBox);
                    pnldHtml.Controls.Add(new LiteralControl("<br />"));
                }




                if (Session["getControls"] != null)
                {
                    Session.Remove("getControls");
                    Session.Add("getControls", pnldHtml);
                }
                else
                {
                    Session.Add("getControls", pnldHtml);
                }

            }

            catch (Exception ex)
            {
                Helpers.Logger.Error(ex);


            }
        }



        public object[] getFormValues()
        {
            try
            {

                object[] paramvalues = new object[pnldHtml.Controls.Count];
                //if (Session["getControls"] != null)
                //{
                //    Panel getcontrol = (Panel)Session["getControls"];
                //    pnldHtml = getcontrol;
                //    //      Session.Remove("getControls");
                //}

                if (pnldHtml.Controls.Count > 0)
                {
                    for (int i = 0; i < pnldHtml.Controls.Count; i++)
                    {
                        if (pnldHtml.Controls[i].GetType().ToString() == "System.Web.UI.WebControls.TextBox")

                        {
                            TextBox txtboxval = (TextBox)pnldHtml.Controls[i];
                            paramvalues[i] = txtboxval.Text.ToString();
                        }
                        else
                        {

                            paramvalues[i] = "";
                        }
                    }



                }

                paramvalues = paramvalues.Where(x => !string.IsNullOrEmpty(x.ToString())).ToArray();
                //Helpers.Logger.Info(Request.ToString());
                //string[] ctrls = Request.Form.ToString().Split('&');

                return paramvalues;

            }

            catch (Exception ex)
            {
                Helpers.Logger.Error(ex);
                return null;

            }
        }


        public void getControlValuesusingForm(Object ctrl)
        {
            try
            {
                TextBox ctrltoValidate = new TextBox();
                if (ctrl != null)
                {
                    ctrltoValidate = (TextBox)ctrl;
                }

                string[] request = Request.Form.ToString().Split('&');
                string searchkey = "" + ctrltoValidate.ID.ToString();

                foreach (string seg in request)
                {
                    if (seg.Contains(searchkey))
                    {
                        string ctrlName = seg.Split('=')[0];
                        string ctrlValue = seg.Split('=')[1];
                        string value = Server.UrlDecode(ctrlValue);

                    }
                }







            }

            catch (Exception ex)
            {
                Helpers.Logger.Error(ex);


            }
        }


        public int InsertParameterValues(int MethodID, int WSRequestID)
        {
            try
            {
                int insertionResult = 0;

                


                if (pnldHtml.Controls.Count > 0)
                {
                    for (int i = 0; i < pnldHtml.Controls.Count; i++)
                    {
                        if (pnldHtml.Controls[i].GetType().ToString() == "System.Web.UI.WebControls.TextBox")

                        {
                            TextBox txtboxval = (TextBox)pnldHtml.Controls[i];
                            string textBoxParamName = txtboxval.ID.Split('_')[1].ToString();
                            string textBoxParamType = txtboxval.ID.Split('_')[2].ToString();
                            string textBoxParamValue = txtboxval.Text.ToString();
                            int paramID = getParameterDBID(MethodID, textBoxParamName);

                            if (textBoxParamType == "string")
                            {


                            }
                            else if (textBoxParamType == "int")
                            {


                            }
                            else if (textBoxParamType == "base64Binary")
                            {


                            }


                            Models.WSRequestParameters WSReqParamCaller = new Models.WSRequestParameters();


                            WSReqParamCaller.ParameterValue = textBoxParamValue;
                            WSReqParamCaller.WSMethodParametersID = paramID;
                            WSReqParamCaller.WSRequestID = WSRequestID;
                            


                            WSReqParamCaller.isActive = true;
                            WSReqParamCaller.isDeleted = false;
                            WSReqParamCaller.CreatedBy = User.Identity.GetUserId();
                            WSReqParamCaller.CreatedDate = DateTime.Now;



                            Models.ApplicationDbContext db = new Models.ApplicationDbContext();

                            db.WSRequestParametersModels.Add(WSReqParamCaller);
                            int result = db.SaveChanges();
                            if (result > 0)
                            {
                                //    Response.Write("<script>alert('Response Received Successfully');</script>");

                             
                            }
                            else
                            {
                                //     Response.Write("<script>alert('Error Occured Please try again.');</script>");
                            }


                        }

                    }

                }

                return insertionResult;
            }
            catch (Exception ex)
            {
                Helpers.Logger.Error(ex);
                return -1;
            }
        }

     
    }
}