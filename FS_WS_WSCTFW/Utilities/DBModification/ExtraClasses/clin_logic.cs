using System;

namespace ClinicianAutomation.ExtraClasses
{
    public class clin_logic
    {
        wsdl soap = new ExtraClasses.wsdl();
        runUtilities utilities = new ExtraClasses.runUtilities();
        getLMUValue lmu = new ExtraClasses.getLMUValue();
        EmailSender email = new ExtraClasses.EmailSender();

        public string clnician_logic (string license,string username,string password)
        {
            string res_email=null;
            string result = null;

            string res_soap = soap.service(username,password,license);

            //Clinician does not Exist.
            if (res_soap == "")
                {
                int res_lmu = lmu.getdate(license);
                
                //inActive
                if (res_lmu == 0)
                {
                    int res_util = utilities.util(license,username,password);
                    if (res_util == 1)
                    {
                        res_email = email.SendEmail(0, license,username,password);
                        result = "Clinician is inActive in LMU!!! " + Environment.NewLine + " Clinician has been updated. " + Environment.NewLine + " Email Sent:" + res_email.ToString();
                    }
                    else if (res_util == -1)
                    {
                        result = "UTIL crashed";
                    }

                }

                //Active
                else if (res_lmu == 1)
                {
                    int res_util = utilities.util(license, username,password);
                    if (res_util == 1)//UtilRun
                    {
                        //file saved
                        res_email = email.SendEmail(1, license, username, password);
                        result = "Clinician has been Updated. " + Environment.NewLine + " Email Sent:" + res_email.ToString() + " " + Environment.NewLine + " Last updated at:" + DateTime.Now.ToString();
                    }
                    else if (res_util == -1)
                    {
                        result = "UTIL crashed";
                    }
                }
                else if (res_lmu == -1)
                {
                    result = "LMU crashed";
                }
                else
                {
                    result = "License:" + license + " not found !!";
                }
            }

            //Clinician exists
            else if (res_soap == license)
            {
                int res_lmu = lmu.getdate(license);

                //inActive
                if (res_lmu == 0)
                {
                    res_email = email.SendEmail(3, license,username,password);
                    result = "Clinician is inActive in LMU and Exist in eClaimLink." + Environment.NewLine + " Please ask them to update in LMU " + Environment.NewLine + " Email Sent:" + res_email.ToString();
                }

                //Active
                else if (res_lmu == 1)
                {
                    //file saved
                    res_email = email.SendEmail(4, license, username, password);
                    result =  "Clinician is Active and Credentials are correct. " + Environment.NewLine + " Please ask them to hard reset browser. " + Environment.NewLine + " Email Sent:" + res_email.ToString() + "\nLast updated at:" + DateTime.Now.ToString();
                }
                else if (res_lmu == -1)
                {
                    result = "LMU crashed";
                }
                else
                {
                    result = "Clinician exist in EclaimLink !! " + Environment.NewLine + " Not found in LMU. " + Environment.NewLine + " Email Sent:" + res_email.ToString();
                }
            }
            
            //Old license of Clinician exists
            else if (res_soap != license && res_soap != "")
            {
                int res_util = utilities.util(res_soap);
                if (res_util == 1)
                {
                    res_email = email.SendEmail(5, res_soap, username, password);
                    result = "Clinician had an OLD license: "+ res_soap +" .It has been nullified.Run it again after 5 minutes." + Environment.NewLine + "Email Sent:" + res_email.ToString() + "\nLast updated at:" + DateTime.Now.ToString();
                }
                else if (res_util == -1)
                {
                    result = "LMU crashed";
                }
            }
            else
            {
                result = "Soap validation failed";
            }

            return result;
        }
    }
}