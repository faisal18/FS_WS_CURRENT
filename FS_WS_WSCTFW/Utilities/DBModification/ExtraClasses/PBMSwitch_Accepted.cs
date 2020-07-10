using System;
using System.Text;

namespace ClinicianAutomation.ExtraClasses
{
    public class PBMSwitch_Accepted
    {
        public string update(string[] data)
        {
            try
            {
                string query = generate_query(data);
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "HyperV", "PBMSWITCH_Accepted_TRNS_"+data[0], "PBMM", 2);
                return "File created successfully!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string generate_query(string[] data)
        {
            try
            {
                string query = "\n\ndeclare @datatable table(data nvarchar(200))" +
                                "\ninsert @datatable(data) values" +
                                    split_array(data) +
                                "\nupdate top(" + count(data) + ") PRIOR_AUTHORIZATION" +
                                "\nset result = 'ACCEPTED' " +
                                "\nwhere " +
                                "\nPRIOR_AUTHORIZATION.id in " +
                                "\n(" +
                                "\nselect id from " +
                                "\nAUTHORIZATION_TRANSACTION " +
                                "\nwhere request_id " +
                                "\nin (  select data from @datatable  )" +
                                "\n)" +
                                "\nand result = 'Rejected'";
                return query;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        string split_array(string[] data)
        {
            try
            {
                string concat = null;
                foreach (string s in data)
                {
                    concat += "('" + s + "'),";
                }
                return concat.Remove(concat.Length - 1);

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        int count(string[] data)
        {
            int count = 0;
            foreach (string s in data)
            {
                count++;
            }
            return count;
        }
    }
}