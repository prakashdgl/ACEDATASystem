using System.Collections.Generic;

namespace ACE.Order.DataLayer
{
  /// <summary>
  /// To sort the objects of the class DepartmentDL by Department Description
  /// </summary>
  public class DepartmentComparer_byDepartmentDesc : IComparer<DepartmentDL>
  {
    public int Compare(DepartmentDL x, DepartmentDL y)
    {
      return x.DepartmentDescription.CompareTo(y.DepartmentDescription);
    }
  }
}
