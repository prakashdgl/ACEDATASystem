using System.Collections.Generic;

namespace ACE.Order.DataLayer
{
  /// <summary>
  /// To sort the objects of the class EmployeeEducationDL by Qualification Description
  /// </summary>
  public class EmployeeEducationComparer_byQualificationDesc : IComparer<EmployeeEducationDL>
  {
    public int Compare(EmployeeEducationDL x, EmployeeEducationDL y)
    {
      return x.QualificationDescription.CompareTo(y.QualificationDescription);
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeeEducationDL by Major Description
  /// </summary>
  public class EmployeeEducationComparer_byMajorDesc : IComparer<EmployeeEducationDL>
  {
    public int Compare(EmployeeEducationDL x, EmployeeEducationDL y)
    {
      return x.MajorDescription.CompareTo(y.MajorDescription);
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeeEducationDL by YearOfPass
  /// </summary>
  public class EmployeeEducationComparer_byYearOfPass : IComparer<EmployeeEducationDL>
  {
    public int Compare(EmployeeEducationDL x, EmployeeEducationDL y)
    {
      if (x.YearOfPass < y.YearOfPass)
        return -1;
      if (x.YearOfPass > y.YearOfPass)
        return 1;
      else
        return 0;
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeeEducationDL by University Description
  /// </summary>
  public class EmployeeEducationComparer_byUniversityDesc : IComparer<EmployeeEducationDL>
  {
    public int Compare(EmployeeEducationDL x, EmployeeEducationDL y)
    {
      return x.UniversityDescription.CompareTo(y.UniversityDescription);
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeeEducationDL by Class Obtained
  /// </summary>
  public class EmployeeEducationComparer_byClassObtained : IComparer<EmployeeEducationDL>
  {
    public int Compare(EmployeeEducationDL x, EmployeeEducationDL y)
    {
      return x.ClassObtained.CompareTo(y.ClassObtained);
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeeEducationDL by Institution Description
  /// </summary>
  public class EmployeeEducationComparer_byInstitutionDesc : IComparer<EmployeeEducationDL>
  {
    public int Compare(EmployeeEducationDL x, EmployeeEducationDL y)
    {
      return x.InstitutionDescription.CompareTo(y.InstitutionDescription);
    }
  }

}
