
namespace ACE.PurchaseOrder.DataLayer
{
  public class MailingAddressDL : CommonForAllDL
  {
    #region Properties

    public string Address1 { get; set; }

    public string Address2 { get; set; }

    public string Address3 { get; set; }

    public string CityDescription { get; set; }

    public int CityID { get; set; }

    public string CityName { get; set; }

    public int DistrictID { get; set; }

    public string DistrictName { get; set; } 

    public int StateID { get; set; } 

    public string StateName { get; set; } 

    public int CountryID { get; set; } 

    public string CountryName { get; set; } 

    public string PostalCode { get; set; }

    public CityDL CityDetails { get; set; }

    public DistrictDL DistrictDetails { get; set; }

    public StateDL StateDetails { get; set; }

    public CountryDL CountryDetails { get; set; }

    #endregion

    #region Constructors

    public MailingAddressDL()
    {
      CityDetails = new CityDL();
      DistrictDetails = new DistrictDL();
      StateDetails = new StateDL();
      CountryDetails = new CountryDL();
    }

    #endregion
  }
}
