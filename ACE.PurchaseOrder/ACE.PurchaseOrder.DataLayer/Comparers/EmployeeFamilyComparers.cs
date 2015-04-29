using System.Collections.Generic;

namespace ACE.Order.DataLayer
{
  /// <summary>
  /// To sort the objects of the class EmployeeFamilyDL by Name
  /// </summary>
  public class EmployeeFamilyComparer_byName : IComparer<EmployeeFamilyDL>
  {
    public int Compare(EmployeeFamilyDL x, EmployeeFamilyDL y)
    {
      return x.Name.CompareTo(y.Name);
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeeFamilyDL by Relationship Description
  /// </summary>
  public class EmployeeFamilyComparer_byRelationshipDesc : IComparer<EmployeeFamilyDL>
  {
    public int Compare(EmployeeFamilyDL x, EmployeeFamilyDL y)
    {
      return x.RelationshipDescription.CompareTo(y.RelationshipDescription);
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeeFamilyDL by Gender Description
  /// </summary>
  public class EmployeeFamilyComparer_byGenderDesc : IComparer<EmployeeFamilyDL>
  {
    public int Compare(EmployeeFamilyDL x, EmployeeFamilyDL y)
    {
      return x.GenderDescription.CompareTo(y.GenderDescription);
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeeFamilyDL by DOB
  /// </summary>
  public class EmployeeFamilyComparer_byDOB : IComparer<EmployeeFamilyDL>
  {
    public int Compare(EmployeeFamilyDL x, EmployeeFamilyDL y)
    {
      if (x.DOB < y.DOB)
        return -1;
      if (x.DOB > y.DOB)
        return 1;
      else
        return 0;
    }
  }
}
