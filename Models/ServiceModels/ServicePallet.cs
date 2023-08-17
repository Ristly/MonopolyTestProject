using MonopolyTest.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyTest.Models.ServiceModels;

public class ServicePallet
{
    public double Width { get; set; }
    public double Height { get; set; }
    public double Depth { get; set; }
    public List<int>? BoxesIds { get; set; }

}
