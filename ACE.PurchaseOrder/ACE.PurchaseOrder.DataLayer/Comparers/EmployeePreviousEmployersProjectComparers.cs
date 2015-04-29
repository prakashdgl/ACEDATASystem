using System.Collections.Generic;

namespace ACE.Order.DataLayer
{
  /// <summary>
  /// To sort the objects of the class EmployeePreviousEmployersProjectDL by Project Name
  /// </summary>
  public class EmployeePreviousEmployersProjectComparer_byProjectName : IComparer<EmployeePreviousEmployersProjectDL>
  {
    public int Compare(EmployeePreviousEmployersProjectDL x, EmployeePreviousEmployersProjectDL y)
    {
      return x.ProjectName.CompareTo(y.ProjectName);
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeePreviousEmployersProjectDL by Client Name
  /// </summary>
  public class EmployeePreviousEmployersProjectComparer_byClientName : IComparer<EmployeePreviousEmployersProjectDL>
  {
    public int Compare(EmployeePreviousEmployersProjectDL x, EmployeePreviousEmployersProjectDL y)
    {
      return x.ClientName.CompareTo(y.ClientName);
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeePreviousEmployersProjectDL by Technology
  /// </summary>
  public class EmployeePreviousEmployersProjectComparer_byTechnology : IComparer<EmployeePreviousEmployersProjectDL>
  {
    public int Compare(EmployeePreviousEmployersProjectDL x, EmployeePreviousEmployersProjectDL y)
    {
      return x.Technology.CompareTo(y.Technology);
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeePreviousEmployersProjectDL by Domain
  /// </summary>
  public class EmployeePreviousEmployersProjectComparer_byDomain : IComparer<EmployeePreviousEmployersProjectDL>
  {
    public int Compare(EmployeePreviousEmployersProjectDL x, EmployeePreviousEmployersProjectDL y)
    {
      return x.Domain.CompareTo(y.Domain);
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeePreviousEmployersProjectDL by FromMonthAndYear
  /// </summary>
  public class EmployeePreviousEmployersProjectComparer_byFromMonthAndYear : IComparer<EmployeePreviousEmployersProjectDL>
  {
    public int Compare(EmployeePreviousEmployersProjectDL x, EmployeePreviousEmployersProjectDL y)
    {
      return x.FromMonthAndYear.CompareTo(y.FromMonthAndYear);
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeePreviousEmployersProjectDL by ToMonthAndYear
  /// </summary>
  public class EmployeePreviousEmployersProjectComparer_byToMonthAndYear : IComparer<EmployeePreviousEmployersProjectDL>
  {
    public int Compare(EmployeePreviousEmployersProjectDL x, EmployeePreviousEmployersProjectDL y)
    {
      return x.ToMonthAndYear.CompareTo(y.ToMonthAndYear);
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeePreviousEmployersProjectDL by TeamSize 
  /// </summary>
  public class EmployeePreviousEmployersProjectComparer_byTeamSize : IComparer<EmployeePreviousEmployersProjectDL>
  {
    public int Compare(EmployeePreviousEmployersProjectDL x, EmployeePreviousEmployersProjectDL y)
    {
      if (x.TeamSize < y.TeamSize)
        return -1;
      if (x.TeamSize > y.TeamSize)
        return 1;
      else
        return 0;
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeePreviousEmployersProjectDL by RolePlayed
  /// </summary>
  public class EmployeePreviousEmployersProjectComparer_byRolePlayed : IComparer<EmployeePreviousEmployersProjectDL>
  {
    public int Compare(EmployeePreviousEmployersProjectDL x, EmployeePreviousEmployersProjectDL y)
    {
      return x.RolePlayed.CompareTo(y.RolePlayed);
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeePreviousEmployersProjectDL by OnsiteLocation
  /// </summary>
  public class EmployeePreviousEmployersProjectComparer_byOnsiteLocation : IComparer<EmployeePreviousEmployersProjectDL>
  {
    public int Compare(EmployeePreviousEmployersProjectDL x, EmployeePreviousEmployersProjectDL y)
    {
      return x.OnsiteLocation.CompareTo(y.OnsiteLocation);
    }
  }
}
