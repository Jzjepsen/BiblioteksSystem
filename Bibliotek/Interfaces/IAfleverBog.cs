using Bibliotek.Interfaces.Boundary;

namespace Bibliotek;

public interface IAfleverBog
{
    int UdregnLånetid();
    void HandleBogScannetEvent(object sender, ScannetEventArgs e );
    void BødeIkkeBetalt();
    void BødeBetalt();
}