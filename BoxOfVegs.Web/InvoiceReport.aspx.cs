using BoxOfVegs.Database;
using BoxOfVegs.Entities;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BoxOfVegs.Web
{
    public partial class InvoiceReport : System.Web.UI.Page
    {
        public BOVContext context = new BOVContext();
        
        //public BoxOfVegsConnection db = new BoxOfVegsConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                var rs = context.Users.OrderByDescending(a => a.UserID).Select(a=>a.UserID).First();
                GetReport(Convert.ToInt32(rs));
            }
        }

        private void GetReport(int invoice)
        {
            SqlParameter param = new SqlParameter("@InvoiceID", invoice);
            var report = context.Database.SqlQuery<Invoice>("InvoiceDetails").ToList();
           // var r = (from a in context.InvoiceDetails() select a);
            ReportDataSource rdata=new ReportDataSource("InvoiceDataSet",report);
            ReportViewer1.LocalReport.DataSources.Add(rdata);
            ReportViewer1.LocalReport.Refresh();
        }
    }
}