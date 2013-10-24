using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Motorlam.WebForms
{
    public partial class ReportViewerBuiltinForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string reportPath = this.Request["reportPath"];
            if (!string.IsNullOrEmpty(reportPath))
            {
                if (!this.IsPostBack)
                    InitializeReportViewer(reportPath);
            }
        }

        private void InitializeReportViewer(string reportPath)
        {
            string reportServerUrl = ConfigurationManager.AppSettings["ReportServerUrl"];
            this.ReportViewer1.ServerReport.ReportServerUrl = new Uri(reportServerUrl);

            if (!string.IsNullOrEmpty(reportPath))
            {
                this.ReportViewer1.ServerReport.ReportPath = reportPath;
                var reportParametersInfo = this.ReportViewer1.ServerReport.GetParameters();
                var reportParameters = new List<Microsoft.Reporting.WebForms.ReportParameter>();
                foreach (var parameter in reportParametersInfo)
                {
                    if(!string.IsNullOrEmpty(Request[parameter.Name]))
                        reportParameters.Add(new Microsoft.Reporting.WebForms.ReportParameter(parameter.Name, Request[parameter.Name]));
                }
                if (reportParameters.Count > 0) this.ReportViewer1.ServerReport.SetParameters(reportParameters);
            }
        }
    }
}