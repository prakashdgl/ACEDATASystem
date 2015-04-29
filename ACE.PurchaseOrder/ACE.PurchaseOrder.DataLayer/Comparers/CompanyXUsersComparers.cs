using System.Collections.Generic;

namespace ACE.PurchaseOrder.DataLayer
{
    /// <summary>
    /// To sort the objects of the class CompanyXUsersDL by Company Name
    /// </summary>
    public class CompanyXUsersComparer_byCompanyName : IComparer<CompanyXUsersDL>
    {
        public int Compare(CompanyXUsersDL x, CompanyXUsersDL y)
        {
            return x.CompanyName.CompareTo(y.CompanyName);
        }
    }

    /// <summary>
    /// To sort the objects of the class CompanyXUsersDL by Role Name
    /// </summary>
    public class CompanyXUsersComparer_byRoleName : IComparer<CompanyXUsersDL>
    {
        public int Compare(CompanyXUsersDL x, CompanyXUsersDL y)
        {
            return x.RoleName.CompareTo(y.RoleName);
        }
    }    
}
