using System.Collections.Generic;

namespace ACE.Order.DataLayer
{
  /// <summary>
  /// To sort the objects of the class EmployeeExperienceDL by Organization Name 
  /// </summary>
  public class EmployeeExperienceComparer_byOrganizationName : IComparer<EmployeeExperienceDL>
  {
    public int Compare(EmployeeExperienceDL x, EmployeeExperienceDL y)
    {
      return x.OrganizationName.CompareTo(y.OrganizationName);
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeeExperienceDL by Location 
  /// </summary>
  public class EmployeeExperienceComparer_byLocation : IComparer<EmployeeExperienceDL>
  {
    public int Compare(EmployeeExperienceDL x, EmployeeExperienceDL y)
    {
      return x.Location.CompareTo(y.Location);
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeeExperienceDL by Designation
  /// </summary>
  public class EmployeeExperienceComparer_byDesignation : IComparer<EmployeeExperienceDL>
  {
    public int Compare(EmployeeExperienceDL x, EmployeeExperienceDL y)
    {
      return x.Designation.CompareTo(y.Designation);
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeeExperienceDL by FromMonthAndYear 
  /// </summary>
  public class EmployeeExperienceComparer_byFromMonthAndYear : IComparer<EmployeeExperienceDL>
  {
    public int Compare(EmployeeExperienceDL x, EmployeeExperienceDL y)
    {
      return x.FromMonthAndYear.CompareTo(y.FromMonthAndYear);
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeeExperienceDL by ToMonthAndYear 
  /// </summary>
  public class EmployeeExperienceComparer_byToMonthAndYear : IComparer<EmployeeExperienceDL>
  {
    public int Compare(EmployeeExperienceDL x, EmployeeExperienceDL y)
    {
      return x.ToMonthAndYear.CompareTo(y.ToMonthAndYear);
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeeExperienceDL by CTC 
  /// </summary>
  public class EmployeeExperienceComparer_byCTC : IComparer<EmployeeExperienceDL>
  {
    public int Compare(EmployeeExperienceDL x, EmployeeExperienceDL y)
    {
      if (x.CTC < y.CTC)
        return -1;
      if (x.CTC > y.CTC)
        return 1;
      else
        return 0;
    }
  }
}
