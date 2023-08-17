using MonopolyTest.Models.DBModels;
using MonopolyTest.Models.ServiceModels;

namespace MonopolyTest.Interfaces;

public interface IBoxesManager
{
    public Task AddBoxAsync(ServiceBox serviceBox);
    public Task UpdateBoxAsync(Box box);
    public Task SetPalletForBoxAsync(Box box, int palletId);
    public Task RemoveBoxAsync(Box box);
    public Task<List<Box>?> GetBoxes();
    public bool BoxValidation(Box box);

}
