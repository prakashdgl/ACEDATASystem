using System.Collections.Generic;

namespace ACE.Order.DataLayer
{
  /// <summary>
  /// To sort the objects of the class EmployeePresentEmployerProjectDL by Project Name
  /// </summary>
  public class EmployeePresentEmployerProjectComparer_byProjectName : IComparer<EmployeePresentEmployerProjectDL>
  {
    public int Compare(EmployeePresentEmployerProjectDL x, EmployeePresentEmployerProjectDL y)
    {
      return x.ProjectName.CompareTo(y.ProjectName);
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeePresentEmployerProjectDL by Client Name
  /// </summary>
  public class EmployeePresentEmployerProjectComparer_byClientName : IComparer<EmployeePresentEmployerProjectDL>
  {
    public int Compare(EmployeePresentEmployerProjectDL x, EmployeePresentEmployerProjectDL y)
    {
      return x.ClientName.CompareTo(y.ClientName);
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeePresentEmployerProjectDL by Technology
  /// </summary>
  public class EmployeePresentEmployerProjectComparer_byTechnology : IComparer<EmployeePresentEmployerProjectDL>
  {
    public int Compare(EmployeePresentEmployerProjectDL x, EmployeePresentEmployerProjectDL y)
    {
      return x.Technology.CompareTo(y.Technology);
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeePresentEmployerProjectDL by Domain
  /// </summary>
  public class EmployeePresentEmployerProjectComparer_byDomain : IComparer<EmployeePresentEmployerProjectDL>
  {
    public int Compare(EmployeePresentEmployerProjectDL x, EmployeePresentEmployerProjectDL y)
    {
      return x.Domain.CompareTo(y.Domain);
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeePresentEmployerProjectDL by FromDate
  /// </summary>
  public class EmployeePresentEmployerProjectComparer_byFromDate : IComparer<EmployeePresentEmployerProjectDL>
  {
    public int Compare(EmployeePresentEmployerProjectDL x, EmployeePresentEmployerProjectDL y)
    {
      if (x.FromDate < y.FromDate)
        return -1;
      if (x.FromDate > y.FromDate)
        return 1;
      else
        return 0;
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeePresentEmployerProjectDL by ToDate
  /// </summary>
  public class EmployeePresentEmployerProjectComparer_byToDate : IComparer<EmployeePresentEmployerProjectDL>
  {
    public int Compare(EmployeePresentEmployerProjectDL x, EmployeePresentEmployerProjectDL y)
    {
      if (x.ToDate < y.ToDate)
        return -1;
      if (x.ToDate > y.ToDate)
        return 1;
      else
        return 0;
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeePresentEmployerProjectDL by JobRole Description
  /// </summary>
  public class EmployeePresentEmployerProjectComparer_byJobRoleDesc : IComparer<EmployeePresentEmployerProjectDL>
  {
    public int Compare(EmployeePresentEmployerProjectDL x, EmployeePresentEmployerProjectDL y)
    {
      return x.JobRoleDescription.CompareTo(y.JobRoleDescription);
    }
  }
}
