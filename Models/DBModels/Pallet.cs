namespace MonopolyTest.Models.DBModels;

public class Pallet
{
    public int ID { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Depth { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public double Weight { get; set; } = 30;
    public double? Volume { get; set; }
    public List<Box>? Boxes { get; set; }

}
