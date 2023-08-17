namespace MonopolyTest.Models.DBModels;

public class Box
{
    public int ID { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Depth { get; set; }
    public double Volume { get; set; }
    public double Weight { get; set; }
    public DateTime Created { get; set; }
    public Pallet? Pallet { get; set; }

}
