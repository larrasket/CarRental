namespace Models.DataModels;

public class Item : BaseModel
{
    public string Name { get; set; }
    public Bill Bill { get; set; }
}