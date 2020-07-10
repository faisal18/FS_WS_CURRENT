using System;
using System.Text;

namespace FS_WS_WSCTFW.Utilities.DBModification
{
    public partial class Generate_CustomQuery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        protected void btn_generate_query_Click(object sender, EventArgs e)
        {
            txt_richbox.InnerText = create_custom(txt_input.Text.ToString(), txt_query.Text.ToString());
        }

        private string create_custom(string input, string query)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                string[] differences = { "\t", "\n", "\r" };
                string[] carry = input.Split(differences, StringSplitOptions.RemoveEmptyEntries);

                foreach (string parameters in carry)
                {
                    string dummy_query = query;
                    string[] diff = { "," };
                    string[] parameters_array = parameters.Split(diff, StringSplitOptions.RemoveEmptyEntries);

                    //for (int i = 1; i < parameters_array.Length + 1; i++)
                    //{
                    //    dummy_query = dummy_query.Replace("@" + i, parameters_array[i - 1]);
                    //}

                    for (int i = 1; i < parameters_array.Length + 1; i++)
                    {

                        if (i <= 9)
                            dummy_query = dummy_query.Replace("@" + i, parameters_array[i - 1]);
                        else if (i > 9 && i <= 19)
                            dummy_query = dummy_query.Replace("#" + i, parameters_array[i - 1]);
                        else if(i>19 && i<=29)
                            dummy_query = dummy_query.Replace("$" + i, parameters_array[i - 1]);



                        dummy_query = dummy_query.Replace("'NULL'", "NULL");

                    }

                    sb.Append("\n\n");
                    sb.Append(dummy_query);
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}