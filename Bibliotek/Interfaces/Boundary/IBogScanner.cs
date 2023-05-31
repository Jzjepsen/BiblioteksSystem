using Bibliotek.Domæne;

namespace Bibliotek.Interfaces.Boundary;

public interface IBogscanner
{
    event EventHandler<ScannetEventArgs>? NewIdScan;
}

public class ScannetEventArgs : EventArgs
{
    public BogID Id { get; set; }

    public ScannetEventArgs(BogID newScan)
    {
        Id = newScan;
    }
}