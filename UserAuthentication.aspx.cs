using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Security.Principal;
using System.ServiceModel; 
using System.Configuration;
using PrepumaWebApp.App_Data.DAL;



namespace PrepumaWebApp
{
    public partial class UserAuthentication : System.Web.UI.Page
    {
        private string m_sUserName { get; set; }
        private string m_appName = ConfigurationManager.AppSettings["appName"].ToString();
        private string m_endPointAddress = ConfigurationManager.AppSettings["endPointAddress"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (IsWindowUser())
            //{

            //    //BasicHttpBinding binding = new BasicHttpBinding();
            //    //////WSHttpBinding binding = new WSHttpBinding();
            //    //////EndpointAddress address = new EndpointAddress(m_endPointAddress); //("http://picer-dev-app01.purolator.com/PI_ApplicationSecurity/PI_AppSecurityService.svc");
            //    //////var factory = new ChannelFactory<IPI_AppSecurityService>(binding, address);
            //    //////IPI_AppSecurityService channel = factory.CreateChannel();


            //    System.ServiceModel.WSHttpBinding MyHttpBinding = new System.ServiceModel.WSHttpBinding(SecurityMode.Message);

            //    MyHttpBinding.Security.Transport.ClientCredentialType = System.ServiceModel.HttpClientCredentialType.Windows;
            //    //MyHttpBinding.Security.Transport.ClientCredentialType = System.ServiceModel.HttpClientCredentialType.Ntlm;  //works too
            //    System.ServiceModel.EndpointAddress MyAuthASMXWebServiceAddress = new System.ServiceModel.EndpointAddress(new Uri("http://picer-dev-app01/PI_ApplicationSecurity/PI_AppSecurityService.svc"));
            //    PI_AppSecurityService.PI_AppSecurityServiceClient channel = new PI_AppSecurityService.PI_AppSecurityServiceClient(MyHttpBinding, MyAuthASMXWebServiceAddress);
        


            //    var isOk = channel.IsAuthorizedForApp(m_appName, m_sUserName);

            //    //PI_AppSecurityServiceClient serviceClient = new PI_AppSecurityServiceClient();
            //    //var isOk = serviceClient.IsAuthorizedForApp("PISalesAdmin", "Sravanthi.Kanike");


            //    if (isOk)
            //    {
            //        PurolatorApplication PIapplication = channel.GetApplicationForUser(m_appName, m_sUserName);
            //        if (PIapplication != null)
            //        {
            //            List<PurolatorApplicationRole> lst = PIapplication.Roles.ToList();

            //            UserInfo.Username = m_sUserName;
            //            UserInfo.Roles = lst[0].RoleName.ToString();
            //            //Session["isOk"] = isOk;
            //            Session["userName"] = m_sUserName;
            //            Session["appName"] = m_appName;
            //            Session["userRole"] = UserInfo.Roles;
            //        }
            //        Response.Redirect("DashBoard.aspx");
            //    }
            //    else
            //    { lblInvalid.Text = "Invalid user! Please contact Administrator"; }
            //}
        }

        private Boolean IsWindowUser()
        {

            m_sUserName = WindowsIdentity.GetCurrent().Name;
            WindowsPrincipal wp = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            m_sUserName = wp.Identity.Name;
            if (m_sUserName.Contains(@"\"))
            {
                int position = m_sUserName.LastIndexOf(@"\") + 1;
                m_sUserName = m_sUserName.Substring(position);
            }
            if (m_sUserName.Length > 0)
            { return true; }
            else { return false; }

        }
    }
}