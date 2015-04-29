using System.Collections.Generic;

namespace ACE.Order.DataLayer
{
  /// <summary>
  /// To sort the objects of the class EmployeeLanguageKnownDL by Language Description
  /// </summary>
  public class EmployeeLanguageKnownComparer_byLanguageDesc : IComparer<EmployeeLanguageKnownDL>
  {
    public int Compare(EmployeeLanguageKnownDL x, EmployeeLanguageKnownDL y)
    {
      return x.LanguageDescription.CompareTo(y.LanguageDescription);
    }
  }
}
