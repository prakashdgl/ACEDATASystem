using System;
using System.Collections.Generic;

namespace ACE.Order.DataLayer
{
  /// <summary>
  /// To sort the objects of the class FeedbackDL by Feedback Description
  /// </summary>
  public class FeedbackComparer_byFeedbackDesc : IComparer<FeedbackDL>
  {
    public int Compare(FeedbackDL x, FeedbackDL y)
    {
      return x.FeedbackDescription.CompareTo(y.FeedbackDescription);
    }
  }

  /// <summary>
  /// To sort the objects of the class FeedbackDL by Created Date
  /// </summary>
  public class FeedbackComparer_byCreatedDate : IComparer<FeedbackDL>
  {
    public int Compare(FeedbackDL x, FeedbackDL y)
    {
      return Convert.ToDateTime(x.CreatedDate).CompareTo(Convert.ToDateTime(y.CreatedDate));
    }
  }

  /// <summary>
  /// To sort the objects of the class FeedbackDL by FeedbackType Description
  /// </summary>
  public class FeedbackComparer_byFeedbackTypeDesc : IComparer<FeedbackDL>
  {
    public int Compare(FeedbackDL x, FeedbackDL y)
    {
      return x.FeedbackTypeDescription.CompareTo(y.FeedbackTypeDescription);
    }
  }

  /// <summary>
  /// To sort the objects of the class FeedbackDL by FeedbackPriority Description
  /// </summary>
  public class FeedbackComparer_byFeedbackPriorityDesc : IComparer<FeedbackDL>
  {
    public int Compare(FeedbackDL x, FeedbackDL y)
    {
      return x.FeedbackPriorityDescription.CompareTo(y.FeedbackPriorityDescription);
    }
  }
}
