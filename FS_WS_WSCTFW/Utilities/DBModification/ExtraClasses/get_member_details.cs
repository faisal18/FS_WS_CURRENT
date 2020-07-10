using System;
using System.Configuration;


namespace ClinicianAutomation.ExtraClasses
{
    public class get_member_details
    {
        public string memberdetails(string[] memberid)
        {
            create_script cs = new create_script();
            create_batch cb = new create_batch();
            string path = ConfigurationManager.AppSettings["member"] + "Member_" + memberid.ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            try
            {
                // string.Join("_",facilityid)
               // string.Join("_", facilityid).Substring(0, 20)
                    string query = generate_query(memberid);
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "All", "_GetMemberDetails_", "PBMM", 3);
                //cs.script_create(query, path);
                //cb.batch_create(path, "DHPO");
                return "Request Processed Successfully";
                //return "File created successfully in directory " + Environment.NewLine + "" + path + ".sql".ToString();

            }
            catch (Exception ex)
            {
                return "An exception occured " + Environment.NewLine + "" + ex.Message;
            }
        }

        public string generate_query(string[] memberid)
        {
            Connections con = new Connections();
            string query = //":connect 10.156.62.42 -U fazeel -P Dell@123" +
                ":connect " + con.get_inline_connection("ECLAIMLINKPORTAL") + 
                "\nUSE [PbmPayer]" +
                "\nGO" +
                "\ndeclare @memberid table(member nvarchar(100))" +
                "\ninsert @memberid(member) values " +
                 split_array(memberid) +
                "\nselect 'Member details in MemberSyn_Database' as 'DB Name',m.Payerid, (select p.name from Payer P where p.payerid = m.payerid) as payername ,[MemberNo],[FirstName],[LastName],[DOB],[Gender],[MI_memberNo],[xRefMemberNo] ,M.[DivisionId] as Member_Division,M.[GroupNo] as Member_GroupNO,M.[GroupEffectiveDate] as Member_GroupEffectiveDate,M.[GroupTermDate] as Member_GroupTermDate,M.[updated_date] as Member_updated_date,M.[updated_file] as Member_updated_file,G.[DivisionId] as Group_Division,G.[GroupNo] as Group_GroupNO,G.[GroupEffectiveDate] as Group_GroupEffectiveDate,G.[GroupTermDate] as Group_GroupTermDate,G.[updated_date] as Group_updated_date,G.[updated_file] as Group_updated_file" +
                "\nfrom [PbmPayer].[dbo].[MemberRecord] M with (nolock) " +
                "\ninner join [PbmPayer].[dbo].[GroupRecord] G on G.GroupNo = M.GroupNo" +
                "\nwhere [MemberNo] in (select member from @memberid)" +
                "\nGO" +

                "\n\n"+//":connect 10.5.3.42 -U fshaikh -P Dell@123" +
                ":connect " + con.get_inline_connection("PBMM") +
                "\nUSE [PBMM]" +
                "\ndeclare @memberid table(member nvarchar(100))" +
                "\ninsert @memberid(member) values " +
                 split_array(memberid) +
                "\nselect 'Member details in PBM Switch' as 'DB Name',m.Payer_id,(select p.name from Payer P where p.id = m.payer_id) as payername ,[Member_No],[first_name],[last_name],[date_of_birth],[gender],[division_code][group_id],[effective_from],[effective_to],[national_id],[MI_first_name],[MI_last_name],[MI_memberNo], last_update as Member_Last_updated" +
                "\n from [PBMM].[dbo].[Member] M with (nolock) "+
                "\nwhere [Member_No] in (select member from @memberid)" +
                "\nGO";
            return query;
        }

       string split_array(string[] memberid)
        {
            string concat = null;
            foreach(string s in memberid)
            {
                concat += "('" + s + "'),";
            }
            return concat.Remove(concat.Length - 1);
        }
    }
}