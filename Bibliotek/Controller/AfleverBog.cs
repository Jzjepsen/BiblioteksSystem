using Bibliotek.Domæne;
using Bibliotek.Interfaces.Boundary;
namespace Bibliotek.Controller;

public class AfleverBog : IAfleverBog
{
    private IBogscanner _bogscanner;
    private IBrugerInterface _brugerInterface;
    private IPrinter _printer;
    private IBødeModul _bødeModul;
    private ILånerDatabase _lånerDatabase;
    private IBogDatabase _bogDatabase;
    private BogID ID;
    private CPR _lånerId;
    private IGetDateTime _dateTime;

    public AfleverBog(IBrugerInterface brugerInterface,
        IPrinter printer,
        IBødeModul bødeModul,
        ILånerDatabase lånerDatabase,
        IBogDatabase bogDatabase,
        IBogscanner bogscanner,
        IGetDateTime dateTime)
    {
        _brugerInterface = brugerInterface;
        _printer = printer;
        _bødeModul = bødeModul;
        _lånerDatabase = lånerDatabase;
        _bogDatabase = bogDatabase;
        _bogscanner = bogscanner;
        _dateTime = dateTime;
        // subscribe på event fra scanner
        _bogscanner.NewIdScan += HandleBogScannetEvent;
    }
    
    public int UdregnLånetid()
    {
        var Bog = new BogID();
        DateTime senesteUdtjekning = Bog.LastCheckedOutDate;     //hent seneste dato udtjekket
        var now = _dateTime.getDateTime();

        TimeSpan tidsforskel = now.Subtract(senesteUdtjekning);    //udregner tidsforskellen
        var lånetid = (int)tidsforskel.TotalDays;                  //konverterer til hele dage og caster til int

        return lånetid;
    }

    public void HandleBogScannetEvent(object sender, ScannetEventArgs e )
    {
        ID = e.Id;         // assign newScan ID fra IScanner til ID 
        _bogDatabase.FindBog(ID);
        var lånetid = UdregnLånetid();
        var bøde = _bødeModul.UdregnBøde(lånetid);
        bool bødestatus = false;
        
        if (bøde > 0)
        {
            _brugerInterface.OpkrævBøde(bøde);
            bødestatus = true;
        }
        _bogDatabase.BogAfleveret(ID);
        _printer.UdskrivKvittering(ID, bøde, bødestatus);
    }

    public void BødeIkkeBetalt()
    {
       var bøde = _bødeModul.UdregnBøde(UdregnLånetid());
        _lånerDatabase.BødeIkkeBetalt(_lånerId, bøde);
    }
    
    public void BødeBetalt()
    {
        var bøde = _bødeModul.UdregnBøde(UdregnLånetid());
        _lånerDatabase.BødeBetalt(_lånerId, bøde);
    }
    
}