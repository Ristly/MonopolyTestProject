using MonopolyTest.Models.DBModels;
using MonopolyTest.Models.ServiceModels;

public interface IPalletsManager
{
    public Task AddPalletAsync(ServicePallet servicePallet);
    public Task UpdatePalletAsync(Pallet box);
    public Task RemovePalletAsync(Pallet box);
    public bool PalletValidation(Pallet pallet);
    public Task<List<Pallet>?> GetPallets();
}