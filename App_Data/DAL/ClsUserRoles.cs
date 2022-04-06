using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClsUserRoles
/// </summary>
/// 
namespace PrepumaWebApp.App_Data.DAL
{ 
public class ClsUserRoles
{
    public ClsUserRoles()
    {

    }


    public string UserName { get; set; }
    public string ActiveDirectoryName { get; set; }
    public string RoleName { get; set; }
    public string EncryptedPassword { get; set; }


    public List<ClsUserRoles> GetListClsAppUsers(string appname)
    {
        PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
        List<ClsUserRoles> oAppUserlist = (from data in prepumaContext.GetTable<vw_UserRole>()
                                           where data.ApplicationName == appname
                                           orderby data.UserName
                                           select new ClsUserRoles
                                           {
                                               UserName = data.UserName,
                                               ActiveDirectoryName = data.ActiveDirectoryName,
                                               RoleName = data.RoleName
                                           }).ToList();
        return oAppUserlist;
    }

    public ClsUserRoles GetAppUserWithPassword(string appname, string username)
    {
        PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
        ClsUserRoles oAppUser = (from data in prepumaContext.GetTable<vw_UserRole>()
                                 where data.ApplicationName == appname
                                 where data.ActiveDirectoryName == username
                                 orderby data.UserName
                                 select new ClsUserRoles
                                 {
                                     UserName = data.UserName,
                                     ActiveDirectoryName = data.ActiveDirectoryName,
                                     RoleName = data.RoleName,
                                     EncryptedPassword = data.EncryptedPassword
                                 }).SingleOrDefault<ClsUserRoles>();
        return oAppUser;
    }

   
}
}