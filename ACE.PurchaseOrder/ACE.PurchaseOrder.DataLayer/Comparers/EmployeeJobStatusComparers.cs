using System;
using System.Collections.Generic;

namespace ACE.Order.DataLayer
{
  /// <summary>
  /// To sort the objects of the class EmployeeJobStatusDL by JobStatus Description
  /// </summary>
  public class EmployeeJobStatusComparer_byJobStatusDesc : IComparer<EmployeeJobStatusDL>
  {
    public int Compare(EmployeeJobStatusDL x, EmployeeJobStatusDL y)
    {
      return x.JobStatusDescription.CompareTo(y.JobStatusDescription);
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeeJobStatusDL by From Date
  /// </summary>
  public class EmployeeJobStatusComparer_byFromDate : IComparer<EmployeeJobStatusDL>
  {
    public int Compare(EmployeeJobStatusDL x, EmployeeJobStatusDL y)
    {
      return Convert.ToDateTime(x.FromDate).CompareTo(Convert.ToDateTime(y.FromDate));
    }
  }
}
