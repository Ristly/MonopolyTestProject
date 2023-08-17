using Microsoft.EntityFrameworkCore;
using MonopolyTest.Exceptions;
using MonopolyTest.Models.DBModels;
using MonopolyTest.Models.ServiceModels;

namespace MonopolyTest.Managers;


/// <summary>
/// Менеджер для работы с моделью Pallet
/// </summary>
public class PalletsManager : IPalletsManager
{
    private readonly ApplicationDbContext _context;

    public PalletsManager(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddPalletAsync(ServicePallet servicePallet)
    {
        await _context.Pallets.AddAsync(DBModelConverter(servicePallet));
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePalletAsync(Pallet pallet)
    {
        _context.Pallets.Update(pallet);
        await _context.SaveChangesAsync();
    }

    public async Task RemovePalletAsync(Pallet pallet)
    {
        _context.Pallets.Remove(pallet);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Pallet>?> GetPallets()
    => await _context.Pallets.Include(x=>x.Boxes).ToListAsync();


    public bool PalletValidation(Pallet pallet)
    {
        if (pallet.Boxes is not null && pallet.Boxes is { Count: > 0 })
        {
            if (pallet.Boxes.Any(box => box.Width > pallet.Width || box.Depth > pallet.Depth))
                return false;
        }
        return true;
    }

    private Pallet DBModelConverter(ServicePallet servicePallet)
    {
        Pallet pallet = new Pallet()
        {
            Width = servicePallet.Width,
            Height = servicePallet.Height,
            Depth = servicePallet.Depth,
        };

        if (servicePallet.BoxesIds is not null && servicePallet.BoxesIds is { Count: > 0 })
        {
            pallet.Boxes = _context.Boxes.Where(x => servicePallet.BoxesIds.Any(y => y == x.ID)).ToList();
            if (!PalletValidation(pallet)) throw new BoxSizeException("Pallet can't contain boxes with bigger size");
        }
        return pallet;
    }

}

