using Microsoft.EntityFrameworkCore;
using MonopolyTest.Exceptions;
using MonopolyTest.Models.DBModels;
using MonopolyTest.Models.ServiceModels;

namespace MonopolyTest.Managers;

public class BoxesManager
{

    private readonly ApplicationDbContext _context;

    public BoxesManager(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddBoxAsync(ServiceBox serviceBox)
    {
        await _context.Boxes.AddAsync(ToDBModelConverter(serviceBox));
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBoxAsync(Box box)
    {
        _context.Boxes.Update(box);
        await _context.SaveChangesAsync();
    }

    public async Task SetPalletForBoxAsync(Box box, int palletId)
    {
        var pallet = _context.Pallets.FirstOrDefault(x => x.ID == palletId);
        box.Pallet = pallet;

        if (!BoxValidation(box)) throw new BoxSizeException("Box size doesn't match the pallet");

        _context.Boxes.Update(box);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveBoxAsync(Box box)
    {
        _context.Boxes.Remove(box);
        await _context.SaveChangesAsync();
    }


    public async Task<List<Box>> GetBoxes()
        => await _context.Boxes.ToListAsync();

    public bool BoxValidation(Box box)
    {
        if (box.Pallet is not null)
        {
            if (box.Width > box.Pallet.Width || box.Depth > box.Pallet.Depth)
                return false;
        }

        return true;
    }

    private Box ToDBModelConverter(ServiceBox serviceBox)
    {
        Box box = new Box()
        {
            Width = serviceBox.Width,
            Height = serviceBox.Height,
            Depth = serviceBox.Depth,
            Weight = serviceBox.Weight,
            Created = serviceBox.Created,
        };

        if (serviceBox.PalletId is not null)
        {
            box.Pallet = _context.Pallets.FirstOrDefault(x => x.ID == serviceBox.PalletId);
            if (!BoxValidation(box)) throw new BoxSizeException("Box size doesn't match the pallet");

        }
        return box;
    }



}


