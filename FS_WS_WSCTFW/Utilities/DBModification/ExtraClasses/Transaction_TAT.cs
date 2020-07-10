using ClinicianAutomation.ExtraClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FS_WS_WSCTFW.Utilities.DBModification.ExtraClasses
{
    public class Transaction_TAT
    {
        public static string GetTransactionTat(string method, string[] data)
        {
            try
            {

                return "Process completed successfuly";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static string Generate_Query(string method, string[] data)
        {
            string query = string.Empty;
            Connections con = new Connections();

            switch (method)
            {
                case "prod_bydate":
                    query = ":connect " + con.get_inline_connection("PBMM") +
                        "\nUSE [PBMM]" +
                        "\nGO" +
                        "\ndeclare @tbl_data table(data nvarchar(100))" +
                        "\ninsert @tbl_data(data) values " +
                         split_array(data) +
                         "\nwhere [date] in (select data from @tbl_data)" +
                        "\nGO";

                    break;
                case "prod_bytransaction":
                    query = ":connect " + con.get_inline_connection("PBMM") +
                        "\nUSE [PBMM]" +
                        "\nGO" +
                        "\ndeclare @tbl_data table(data nvarchar(100))" +
                        "\ninsert @tbl_data(data) values " +
                         split_array(data) +
                         "\nwhere [date] in (select data from @tbl_data)" +
                        "\nGO";

                    break;
                case "qa_bydate":
                    query = ":connect " + con.get_inline_connection("PBMUAT") +
                        "\nUSE [PBMM]" +
                        "\nGO" +
                        "\ndeclare @tbl_data table(data nvarchar(100))" +
                        "\ninsert @tbl_data(data) values " +
                         split_array(data) +
                         "\nwhere [date] in (select data from @tbl_data)" +
                        "\nGO";

                    break;
                case "qa_bytransaction":
                    query = ":connect " + con.get_inline_connection("PBMUAT") +
                        "\nUSE [PBMM]" +
                        "\nGO" +
                        "\ndeclare @tbl_data table(data nvarchar(100))" +
                        "\ninsert @tbl_data(data) values " +
                         split_array(data) +
                         "\nwhere [date] in (select data from @tbl_data)" +
                        "\nGO";

                    break;
            }


            return query;
        }

        static string split_array(string[] memberid)
        {
            string concat = null;
            foreach (string s in memberid)
            {
                concat += "('" + s + "'),";
            }
            return concat.Remove(concat.Length - 1);
        }
    }
}