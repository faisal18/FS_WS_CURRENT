using System.Web.Services.Description;

namespace ClinicianAutomation
{
    public class wsdl
    {
        public string service(string clin_user,string clin_pass,string clin_license)
        {
            string license;
            Service.ClinicianAuthonticationSoapClient soap = new Service.ClinicianAuthonticationSoapClient();
            var response = soap.AuthonticateClinician(clin_user.ToString(), clin_pass.ToString(),"ClinUserProd@!23", "Clinpwd?where2.",out license);
            //if (license == clin_license)
            //    response = 1;
            //else if (license != clin_license && license != "")
            //    response = -2;
            //else if (license == null)
            //    response = -1;

            return license;

        }
    }
}