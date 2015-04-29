using System;
using System.Collections.Generic;

namespace ACE.Order.DataLayer
{
  /// <summary>
  /// To sort the objects of the class FeedbackReplyDL by FeedbackReply Description
  /// </summary>
  public class FeedbackReplyComparer_byFeedbackReplyDesc : IComparer<FeedbackReplyDL>
  {
    public int Compare(FeedbackReplyDL x, FeedbackReplyDL y)
    {
      return x.FeedbackReplyDescription.CompareTo(y.FeedbackReplyDescription);
    }
  }

  /// <summary>
  /// To sort the objects of the class FeedbackDL by Reply Date
  /// </summary>
  public class FeedbackReplyComparer_byReplyDate : IComparer<FeedbackReplyDL>
  {
    public int Compare(FeedbackReplyDL x, FeedbackReplyDL y)
    {
      return Convert.ToDateTime(x.ReplyDate).CompareTo(Convert.ToDateTime(y.ReplyDate));
    }
  }
}
