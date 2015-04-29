using System.Collections.Generic;

namespace ACE.Order.DataLayer
{
  /// <summary>
  /// To sort the objects of the class EmployeeCertificationDL by Certification
  /// </summary>
  public class EmployeeCertificationComparer_byCertification : IComparer<EmployeeCertificationDL>
  {
    public int Compare(EmployeeCertificationDL x, EmployeeCertificationDL y)
    {
      return x.Certification.CompareTo(y.Certification);
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeeCertificationDL by Technology Description
  /// </summary>
  public class EmployeeCertificationComparer_byTechnologyDesc : IComparer<EmployeeCertificationDL>
  {
    public int Compare(EmployeeCertificationDL x, EmployeeCertificationDL y)
    {
      return x.TechnologyDescription.CompareTo(y.TechnologyDescription);
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeeCertificationDL by YearOfPass
  /// </summary>
  public class EmployeeCertificationComparer_byYearOfPass : IComparer<EmployeeCertificationDL>
  {
    public int Compare(EmployeeCertificationDL x, EmployeeCertificationDL y)
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
  /// To sort the objects of the class EmployeeCertificationDL by IssuedBy
  /// </summary>
  public class EmployeeCertificationComparer_byIssuedBy : IComparer<EmployeeCertificationDL>
  {
    public int Compare(EmployeeCertificationDL x, EmployeeCertificationDL y)
    {
      return x.IssuedBy.CompareTo(y.IssuedBy);
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeeCertificationDL by Class Obtained
  /// </summary>
  public class EmployeeCertificationComparer_byClassObtained : IComparer<EmployeeCertificationDL>
  {
    public int Compare(EmployeeCertificationDL x, EmployeeCertificationDL y)
    {
      return x.ClassObtained.CompareTo(y.ClassObtained);
    }
  }

  /// <summary>
  /// To sort the objects of the class EmployeeCertificationDL by TranscriptID
  /// </summary>
  public class EmployeeCertificationComparer_byTranscriptID : IComparer<EmployeeCertificationDL>
  {
    public int Compare(EmployeeCertificationDL x, EmployeeCertificationDL y)
    {
      return x.TranscriptID.CompareTo(y.TranscriptID);
    }
  }
}
