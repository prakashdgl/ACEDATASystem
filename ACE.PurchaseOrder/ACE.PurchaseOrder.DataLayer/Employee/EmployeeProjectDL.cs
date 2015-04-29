
namespace ACE.PurchaseOrder.DataLayer
{
  public class EmployeeProjectDL : CommonForAllDL
  {
    #region Properties

    public int EmployeeProjectID { get; set; }

    public int ProjectID { get; set; }

    public string ProjectName { get; set; }

    public int FromMonth { get; set; }

    public int FromYear { get; set; }

    public int ToMonth { get; set; }

    public int ToYear { get; set; }

    public int JobRoleID { get; set; }

    public string JobRoleDescription { get; set; }

    #endregion    
  }
}
