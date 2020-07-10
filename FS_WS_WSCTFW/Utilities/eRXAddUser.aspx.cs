using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using System.Web.Security;


namespace FS_WS_WSCTFW.Utilities
{
    public partial class eRXAddUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            txtFullLog.ReadOnly = true;
            if (!IsPostBack)
            {
                chkExecuteScript.Checked = true;
            }
        }

        protected void btnAdderxUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProviderLicense.Text.Length < 1)
                {
                    txtFullLog.Text = "Please provide valid Provider License";
                }
                else
                    if (txtProviderUsername.Text.Length < 1)
                {
                    txtFullLog.Text = "Please provide valid Provider License";

                }
                else
                {

                    string useremail = Helpers.EmailHelper.getUsernamefromEmail(User.Identity.Name.ToString());
                    if ( useremail != null )
                    {

                        string server1 = ClinicianAutomation.ExtraClasses.Connections.run_singlevalue("ERX", "server");
                        string DB1 = ClinicianAutomation.ExtraClasses.Connections.run_singlevalue("ERX", "database");
                        string username1 = ClinicianAutomation.ExtraClasses.Connections.run_singlevalue("ERX", "username");
                        string password1 = ClinicianAutomation.ExtraClasses.Connections.run_singlevalue("ERX", "password");

                        string sqlScript = @":out stdout" + Environment.NewLine + "GO" + Environment.NewLine;
                        sqlScript += ":connect "+server1+" -U "+username1+" -P "+password1+"" + Environment.NewLine + "GO" + Environment.NewLine;
                        sqlScript += "use eclaimlinkportal" + Environment.NewLine + "GO" + Environment.NewLine;
                        sqlScript += "declare @LicenseID varchar(8000)" + Environment.NewLine + "";
                        sqlScript += "set @LicenseID = '" + txtProviderLicense.Text + "'" + Environment.NewLine + "";
                        sqlScript += ":out C:\\tmp\\License.txt" + Environment.NewLine + "";
                        sqlScript += "Print ' Declare @LicenseID varchar(8000)'" + Environment.NewLine + "";
                        sqlScript += "Print ' Set @LicenseID = ''" + txtProviderLicense.Text + "'' '" + Environment.NewLine + "GO" + Environment.NewLine;
                        sqlScript += ":out stdout" + Environment.NewLine + "GO " + Environment.NewLine;
                        sqlScript += " :r C:\\tmp\\License.txt " + Environment.NewLine + "";
                     //   sqlScript += "SELECT *   FROM [eclaimlinkportal].[dbo].[Facilities]  where license = @LicenseID " + Environment.NewLine + "";
                      //  sqlScript += "Print 'Facility exists in [eclaimlinkportal]'" + Environment.NewLine + "";
                     //   sqlScript += "Select u.Name,u.Username,u.Email,u.phone,* from users u where FacilityId in   (SELECT ID FROM [eclaimlinkportal].[dbo].[Facilities] where license = @LicenseID )" + Environment.NewLine + "";
                        sqlScript += ":out stdout" + Environment.NewLine + "GO " + Environment.NewLine;
                        sqlScript += " :r C:\\tmp\\License.txt" + Environment.NewLine + "";
                        sqlScript += "Declare @Username varchar(8000)" + Environment.NewLine + "";
                        sqlScript += "Declare @Name varchar(200)" + Environment.NewLine + "";
                        sqlScript += "Declare @Password varchar(200)" + Environment.NewLine + "";
                        sqlScript += "Declare @Email varchar(200)" + Environment.NewLine + "";
                        sqlScript += "Declare @Phone varchar(200)" + Environment.NewLine + "";
                        sqlScript += "Set @Username = '" + txtProviderUsername.Text + "'" + Environment.NewLine + "";
                        sqlScript += "Set @Name = (SElect u.Name from users u where FacilityId in (SELECT ID FROM [eclaimlinkportal].[dbo].[Facilities] where license = @LicenseID) and u.username = @Username  )" + Environment.NewLine + "";
                        sqlScript += " Set @Password =  (  SElect u.Password from users u where FacilityId in (SELECT ID FROM [eclaimlinkportal].[dbo].[Facilities] where license = @LicenseID)and u.username = @Username )" + Environment.NewLine + "";
                        sqlScript += "Set @Email =( SElect u.Email from users u where FacilityId in (SELECT ID FROM [eclaimlinkportal].[dbo].[Facilities] where license = @LicenseID)and u.username = @Username)" + Environment.NewLine + "";
                        sqlScript += " SEt @Phone = ( SElect u.phone from users u where FacilityId in (SELECT ID FROM [eclaimlinkportal].[dbo].[Facilities] where license = @LicenseID)and u.username = @Username)" + Environment.NewLine + "";
                        sqlScript += ":out C:\\tmp\\fir.txt" + Environment.NewLine + "";
                        sqlScript += "Print ' Declare @Username varchar(8000)'" + Environment.NewLine + "";
                        sqlScript += "Print ' Declare @LicenseID varchar(8000)'" + Environment.NewLine + "";
                        sqlScript += "Print ' Declare @Name varchar(200)'" + Environment.NewLine + "";
                        sqlScript += " Print ' Declare @Password varchar(200)'" + Environment.NewLine + "";
                        sqlScript += "  Print ' Declare @Email varchar(200)'" + Environment.NewLine + "";
                        sqlScript += " Print ' Declare @Phone varchar(200)'" + Environment.NewLine + "";
                        sqlScript += "Print ' Declare @UpdateRecord int'" + Environment.NewLine + "";
                        sqlScript += " Print ' Set @Username = '''+@Username+''''" + Environment.NewLine + "";
                        sqlScript += "Print ' Set @LicenseID = '''+@LicenseID+''''" + Environment.NewLine + "";
                        sqlScript += " Print ' Set @Name = '''+@Name+''''" + Environment.NewLine + "";
                        sqlScript += "Print ' Set @Password = '''+@Password+''''" + Environment.NewLine + "";
                        sqlScript += "Print ' Set @Email = '''+@Email+''''" + Environment.NewLine + "";
                        sqlScript += "Print ' Set @Phone = '''+@Phone+''''" + Environment.NewLine + "";
                        sqlScript += "Print ' Set @UpdateRecord = " + (chkExecuteScript.Checked ? 0 : 1) + "'" + Environment.NewLine + "GO";
                        sqlScript += "" + Environment.NewLine + "";
                        sqlScript += ":out stdout" + Environment.NewLine + "GO";
                        sqlScript += "" + Environment.NewLine + "";
                        sqlScript += "use erx" + Environment.NewLine + "";
                        sqlScript += ":r C:\\tmp\\fir.txt" + Environment.NewLine + "";
                        sqlScript += "Print 'License - ' +  @LicenseID" + Environment.NewLine + "";
                        sqlScript += "Print 'Username - ' +  @Username" + Environment.NewLine + "";
                     //   sqlScript += "Print 'Password - ' +  @Password" + Environment.NewLine + "";
                        sqlScript += "Print 'Name - ' +  @Name" + Environment.NewLine + "";
                        sqlScript += "Print 'Email - ' +  @Email" + Environment.NewLine + "";
                        sqlScript += "Print 'Phone - ' +  @Phone" + Environment.NewLine + "";
                        sqlScript += "" + Environment.NewLine + "";
                        sqlScript += "" + Environment.NewLine + "";
                        sqlScript += "" + Environment.NewLine + "";
                        sqlScript += "" + Environment.NewLine + "";
                        sqlScript += @"





 -- *%*%*%*%*%*%*%*%*%*%*%*%*%%%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%%%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%
 -- FOr multiple users in eclaimlink, please check username first and update username below
 -- *%*%*%*%*%*%*%*%*%*%*%*%*%%%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%%%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%*%
 
use eclaimlinkportal
 if exists (SELECT *   FROM [eclaimlinkportal].[dbo].[Facilities]  where license = @LicenseID )
Begin 
Print 'Facility exists in [eclaimlinkportal]'
-- Select u.Name,u.Username,u.Email,u.phone,* from users u where FacilityId in   (SELECT ID FROM [eclaimlinkportal].[dbo].[Facilities] where license = @LicenseID )
end 
else
Begin 
Print 'Facility DOES NOT exists in [eclaimlinkportal]'
end 

 if exists(SElect u.Name from [eclaimlinkportal].[dbo].[users] u where FacilityId in (SELECT ID FROM [eclaimlinkportal].[dbo].[Facilities] where license = @LicenseID) and u.username = @Username)
	begin
	Print ' -- User exists in [eclaimlinkportal]'
	

use erx


  --checking if provider, group and user available in eRX database
  Declare @ProviderID int
  Declare @GroupID int
  
  if exists(select * from erx.dbo.[Provider] where License = @LicenseID)
		  Begin
			Print '  --- Provider exists in [eRX] Database --- '
			--select * from erx.dbo.[Provider] where License = @LicenseID
			Set @ProviderID = (Select ID from [Provider] where License = @LicenseID)
			Print 'ProviderID = ' + cast(@ProviderID as varchar(100))

			 if exists(Select * from erx.dbo.[Group] where Provider = @ProviderID and [Name] = 'Admin' and [Admin] = 1 and [Active] = 1 and [Deleted] = 0)
				  Begin
					Print '  --- Group exists in [eRX] Database --- '
					Set @GroupID = ( Select ID from erx.dbo.[Group] where Provider = @ProviderID and [Name] = 'Admin' and [Admin] = 1 and [Active] = 1 and [Deleted] = 0)
			--  Select * from erx.dbo.[Group] where Provider = @ProviderID and [Name] = 'Admin' and [Admin] = 1 and [Active] = 1 and [Deleted] = 0
					Print 'GroupID = ' + cast(@GroupID as varchar(100))

					



					if exists(select * from erx.dbo.[User] where [group] = @GroupID and   [Username] = @Username and [Password]=@Password)
						  Begin
							Print ' --- User exists in [eRX] Database --- '
							Print ' --- User Information ::: ---'
							Print ' --- [Id] ,[Group] ,[Name] ,[Username],[Email] ,[Phone] ,[Active] ---'
                        select [Id] ,[Group] ,[Name] ,[Username],[Email] ,[Phone] ,[Active] from erx.dbo.[User] where [group] = @GroupID and   [Username] = @Username and [Password]=@Password
						  end
						 else
						  begin
							Print ' User Does NOT exists in [eRX] Database'
							
							if (@UpdateRecord = 1)
							Begin
							
							
							
								INSERT INTO [eRx].[dbo].[User]
										   ([Group]
										   ,[Name]
										   ,[Username]
										   ,[Password]
										   ,[Email]
										   ,[Phone]
										   ,[Active]
										   ,[Deleted]
										   )
									 VALUES
										   (
											@GroupID
										   ,@Name
										   ,@Username
										   ,@Password
										   , @Email
										   , @Phone
										   ,1
										   ,0
										   )

								Print '--- User Added Successfully ---'
                                
                                Print '---   Updating DHPO Credentials as eclaimlink portal---'
								
                                 SET QUOTED_IDENTIFIER OFF

                                update top (1) [Provider]
								set 
								[POUsername] = @Username
								,[POPassword] = @Password
								where [License] = @LicenseID

                                 SET QUOTED_IDENTIFIER ON


								Print '---   Updating DHPO Credentials as eclaimlink portal---'
							end 
							else
							Begin
							Print ' @UpdateRecord set as 0 ... NO DATA UPDATED For user'
							end
							
						  end




				  end
			  else
				  begin
					Print ' Group  Does NOT exists in [eRX] Database'
					
					if (@UpdateRecord = 1)
							Begin
							
							
							
								  INSERT INTO [eRx].[dbo].[Group]
										   ([Provider]
										   ,[Name]
										   ,[Admin]
										   ,[Active]
										   ,[Deleted]
										  )
									 VALUES
										    
										   (@ProviderID
										   , 'Admin' 
										   ,1
										   ,1
										   ,0
										  )
										  
							Set @GroupID = ( Select ID from erx.dbo.[Group] where Provider = @ProviderID and [Name] = 'Admin' and [Admin] = 1 and [Active] = 1 and [Deleted] = 0)
							Print '--- Group Added Successfully ---'

							
							
							Print '--- Adding User  ---'
										INSERT INTO [eRx].[dbo].[User]
										   ([Group]
										   ,[Name]
										   ,[Username]
										   ,[Password]
										   ,[Email]
										   ,[Phone]
										   ,[Active]
										   ,[Deleted]
										   )
									 VALUES
										   (
											@GroupID
										   ,@Name
										   ,@Username
										   ,@Password
										   , @Email
										   , @Phone
										   ,1
										   ,0
										   )
								Print '--- User Added Successfully ---'

							end 
							else
							Begin
							Print ' @UpdateRecord set as 0 ... NO DATA UPDATED For Group'
							end
					
				  end


		 end
  else
		  begin
			Print 'Provider Does NOT exists in [eRX] Database'
			
							
		  end  
   

 end
else
	begin
	Print '  --  User DOES NOT exists in [eclaimlinkportal]'
	end


";
                        string fullpath = Helpers.BatchFIleCaller.getBatchFileGenerationPath();

                       // string filename = useremail + "_" + txtProviderLicense.Text + "_" + txtProviderUsername.Text + "_" + DateTime.Now.ToString("yyMMdd-hhmmss");
                        string filename = useremail + "_" + txtProviderLicense.Text + "_"  + DateTime.Now.ToString("yyMMdd-hhmmss");

                        string outputfilename = System.Configuration.ConfigurationManager.AppSettings["EmailReportPath"] + filename ;

                        sqlScript += Environment.NewLine;
                        sqlScript += ":out  " + outputfilename + ".CSV";



                        txtFullLog.Text = sqlScript;

                        txtFullLog.Text += Environment.NewLine;

                        txtFullLog.Text +=  useremail;

                   
                        txtFullLog.Text += Environment.NewLine;

                        txtFullLog.Text += filename;

                       if( Helpers.BatchFIleCaller.SaveAnyFile(filename, sqlScript, fullpath , false, "SQL"))
                        {
                            txtFullLog.Text += Environment.NewLine;

                            txtFullLog.Text += " SQL File saved Successfully";

                 
                            txtFullLog.Text += Environment.NewLine;

                            string server = ClinicianAutomation.ExtraClasses.Connections.run_singlevalue("ERX", "server");
                            string DB = ClinicianAutomation.ExtraClasses.Connections.run_singlevalue("ERX", "database");
                            string username = ClinicianAutomation.ExtraClasses.Connections.run_singlevalue("ERX", "username");
                            string password = ClinicianAutomation.ExtraClasses.Connections.run_singlevalue("ERX", "password");
                            string sqlcmd = Helpers.BatchFIleCaller.GenerateSQLCMDCommand(1, server, DB, username, password, fullpath + "\\" + filename, outputfilename, "");
                            //string sqlcmd = Helpers.BatchFIleCaller.GenerateSQLCMDCommand(1, "10.156.62.42", "erx","fazeel","Dell@123", fullpath + "\\" + filename, outputfilename, "");

                            txtFullLog.Text += sqlcmd;

                            if (Helpers.BatchFIleCaller.SaveAnyFile (filename,sqlcmd,fullpath,false,"BAT"))
                            {
                                txtFullLog.Text += Environment.NewLine;

                                txtFullLog.Text += " Bat File saved Successfully";

                                txtFullLog.Text += Environment.NewLine;

                                txtFullLog.Text += Helpers.BatchFIleCaller.ExecuteBatchFileeHDF(fullpath + "\\" + filename + ".BAT");


                            }

                            else
                            {
                                txtFullLog.Text += Environment.NewLine;

                                txtFullLog.Text += "BAT Script not saved successfully !!!";
                            }
                        }
                        else
                        {
                            txtFullLog.Text += Environment.NewLine;

                            txtFullLog.Text += "SQL Script not saved successfully !!!";


                        }

                    }
                }

                txtFullLog.Text = " Script Executed and Results will be emailed to you shortly!!!!  ";
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}