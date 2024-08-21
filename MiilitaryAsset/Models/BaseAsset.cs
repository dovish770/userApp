namespace MiilitaryAsset.Models
{
    public abstract class BaseAsset
    {
        public int id { get; set; }
        public string name { get; set; }
        public string assetType { get; set; }
        public enum status { Active, Inactive, Maintenance }
    }
}
