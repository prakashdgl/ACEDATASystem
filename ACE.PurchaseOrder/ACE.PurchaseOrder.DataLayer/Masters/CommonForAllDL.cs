using ACE.PurchaseOrder.CommonLayer;

namespace ACE.PurchaseOrder.DataLayer
{
  public class CommonForAllDL
  {
    #region Variables

    protected ACEConnection _myConnection = new ACEConnection();

    #endregion

    #region Properties

    public ScreenMode ScreenMode { get; set; }

    public int AddEditOption { get; set; }

    #endregion
  }
}
