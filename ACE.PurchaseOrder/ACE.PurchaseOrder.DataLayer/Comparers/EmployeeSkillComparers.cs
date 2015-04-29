using System.Collections.Generic;

namespace ACE.Order.DataLayer
{
  /// <summary>
  /// To sort the objects of the class EmployeeSkillDL by Technology Description
  /// </summary>
  public class EmployeeSkillComparer_byTechnologyDesc : IComparer<EmployeeSkillDL>
  {
    public int Compare(EmployeeSkillDL x, EmployeeSkillDL y)
    {
      return x.TechnologyDescription.CompareTo(y.TechnologyDescription);
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeeSkillDL by SkillLevel Description
  /// </summary>
  public class EmployeeSkillComparer_bySkillLevelDesc : IComparer<EmployeeSkillDL>
  {
    public int Compare(EmployeeSkillDL x, EmployeeSkillDL y)
    {
      return x.SkillLevelDescription.CompareTo(y.SkillLevelDescription);
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeeSkillDL by ExperienceInYears
  /// </summary>
  public class EmployeeSkillComparer_byExpInYrs : IComparer<EmployeeSkillDL>
  {
    public int Compare(EmployeeSkillDL x, EmployeeSkillDL y)
    {
      if (x.ExperienceInYears < y.ExperienceInYears)
        return -1;
      if (x.ExperienceInYears > y.ExperienceInYears)
        return 1;
      else
        return 0;
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeeSkillDL by ExperienceInMonths
  /// </summary>
  public class EmployeeSkillComparer_byExpInMons : IComparer<EmployeeSkillDL>
  {
    public int Compare(EmployeeSkillDL x, EmployeeSkillDL y)
    {
      if (x.ExperienceInMonths < y.ExperienceInMonths)
        return -1;
      if (x.ExperienceInMonths > y.ExperienceInMonths)
        return 1;
      else
        return 0;
    }
  }
}
