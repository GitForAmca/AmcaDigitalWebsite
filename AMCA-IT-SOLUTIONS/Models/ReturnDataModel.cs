using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace AMCA_IT_SOLUTIONS.Models
{
    public class ReturnDataModel
    {
        //public ReturnDataModel()
        //{

        //}
        public static void returnTable(ValidationModel PL)
        {
            try
            {
                SQLConnectivity SC = new SQLConnectivity();
                SqlCommand sqlCmd = new SqlCommand("PR_Enquiries", SC.SqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.Parameters.Add("@AutoId", SqlDbType.Int).Value = PL.AutoId;
                sqlCmd.Parameters.Add("@OpCode", SqlDbType.Int).Value = PL.OpCode;
                sqlCmd.Parameters.Add("@ServiceType", SqlDbType.Int).Value = PL.ServiceType;
                sqlCmd.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = PL.CompanyName;
                sqlCmd.Parameters.Add("@ConcernPerson", SqlDbType.VarChar).Value = PL.ConcernPerson;
                sqlCmd.Parameters.Add("@EmailId", SqlDbType.VarChar).Value = PL.EmailId;
                sqlCmd.Parameters.Add("@TradeLicenseAuthority", SqlDbType.VarChar).Value = PL.TradeLicenseAuthority;
                sqlCmd.Parameters.Add("@ServiceList", SqlDbType.VarChar).Value = PL.ServiceList;
                sqlCmd.Parameters.Add("@AboutTamca", SqlDbType.VarChar).Value = PL.AboutTamca;
                sqlCmd.Parameters.Add("@CountryCode", SqlDbType.VarChar).Value = PL.CountryCode;
                sqlCmd.Parameters.Add("@ContactNumber", SqlDbType.VarChar).Value = PL.ContactNumber;
                sqlCmd.Parameters.Add("@MessageContact", SqlDbType.Text).Value = PL.MessageContact;
                sqlCmd.Parameters.Add("@txtPageName", SqlDbType.VarChar).Value = PL.txtPageName;
                sqlCmd.Parameters.Add("@BlogTitle", SqlDbType.VarChar).Value = PL.BlogTitle;
                sqlCmd.Parameters.Add("@Usercomment", SqlDbType.Text).Value = PL.Usercomment;

                SqlDataAdapter sqlAdp = new SqlDataAdapter(sqlCmd);
                PL.dt = new DataTable();
                sqlAdp.Fill(PL.dt);

            }
            catch (Exception ex)
            {
                
            }
        }
    }
}