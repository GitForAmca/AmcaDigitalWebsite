using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace AMCA_IT_SOLUTIONS.Models
{
    public class SQLConnectivity
    {
        public SqlConnection SqlCon = new SqlConnection(WebConfigurationManager.ConnectionStrings["digitalAmcaCon"].ToString());
        public ConnectionState State { get; set; }
    }
}