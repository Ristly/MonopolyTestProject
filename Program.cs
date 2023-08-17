using MonopolyTest;
using MonopolyTest.Managers;
using MonopolyTest.Models.DBModels;



//Начало программы
await Main();

static async Task Main()
{
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    var context = new ApplicationDbContext();

    var boxManager = new BoxesManager(context);
    var palletManager = new PalletsManager(context);


    PrintLine();
    Console.WriteLine();
    Console.WriteLine("Сгруппировать все паллеты по сроку годности, отсортировать по возрастанию срока годности, в каждой группе отсортировать паллеты по весу.");
    Console.WriteLine();
    PrintLine();

    var pallets = await palletManager.GetPallets();
    var palletGroups = pallets.GroupBy(x => x.ExpirationDate, y => y).OrderBy(x => x.Key).ToList();

    foreach (var group in palletGroups)
    {
        var orderedPallets = group.OrderBy(x => x.Weight).ToList();
        ConsolePalletsDisplay(orderedPallets);
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
    }

    PrintLine();
    Console.WriteLine();
    Console.WriteLine("3 паллеты, которые содержат коробки с наибольшим сроком годности, отсортированные по возрастанию объема.");
    Console.WriteLine();
    PrintLine();

    var threePallets = pallets.OrderByDescending(x => x.ExpirationDate)
        .Take(3).OrderBy(x => x.Volume)
        .ToList();

    ConsolePalletsDisplay(threePallets);

}




static void ConsolePalletsDisplay(List<Pallet> pallets)
{

    PrintLine();
    PrintRow("Id", "Width", "Height", "Depth", "Volume", "Weight", "ExpirationDate");
    PrintLine();

    foreach (var pallet in pallets)
    {
        string temp = string.Empty;
        string expirationDate = pallet.ExpirationDate is null ? "-" : pallet.ExpirationDate.Value.ToString("d");
        temp += pallet.ID.ToString() + "," + pallet.Width.ToString() + "," + pallet.Height.ToString() +
            "," + pallet.Depth.ToString() + "," + pallet.Volume.ToString() + "," + pallet.Weight.ToString() +
            "," + expirationDate;
        PrintRow(temp.Split(','));
        PrintLine();
    }
    Console.ReadLine();

}
static void ConsoleBoxesDisplay(List<Box> boxes)
{

    PrintLine();
    PrintRow("Id", "Width", "Height", "Depth", "Volume", "Weight", "Created", "PalletId");
    PrintLine();



    foreach (var box in boxes)
    {
        string temp = string.Empty;
        string palletId = box.Pallet is null ? "-" : box.Pallet.ID.ToString();
        temp += box.ID.ToString() + "," + box.Width.ToString() + "," + box.Height.ToString() +
            "," + box.Depth.ToString() + "," + box.Volume.ToString() + "," + box.Weight.ToString() +
            "," + box.Created.ToString("d") + "," + palletId;
        PrintRow(temp.Split(','));
        PrintLine();
    }
    Console.ReadLine();

}

static void PrintRow(params string[] columns)
{
    int tableWidth = 173;
    int width = (tableWidth - columns.Length) / columns.Length;
    string row = "|";

    foreach (string column in columns)
    {
        row += AlignCentre(column, width) + "|";
    }

    Console.WriteLine(row);
}

static void PrintLine()
{
    int tableWidth = 173;
    Console.WriteLine(new string('-', tableWidth));
}

static string AlignCentre(string text, int width)
{
    text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

    if (string.IsNullOrEmpty(text))
    {
        return new string(' ', width);
    }
    else
    {
        return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
    }
}