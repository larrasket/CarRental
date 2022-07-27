namespace Models.DataModels;

public class Fine : BaseModel
{
    public string Location { get; set; }
    public string Types { get; set; }
    public Bill? Bill { get; set; }
}