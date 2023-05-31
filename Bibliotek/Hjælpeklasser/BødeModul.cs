using Bibliotek.Domæne;

namespace Bibliotek;

public class BødeModul : IBødeModul
{
    public BødeModul()
    {
    }

    public double UdregnBøde(int lånetid)
    {
        double _bøde = 0;

        if (lånetid <= 30)
            _bøde = 0;
        else if (lånetid > 30 && lånetid <= 45)
            _bøde = 20;
        else if (lånetid > 45 && lånetid <= 75)
            _bøde = 50;
        else if (lånetid > 75)
            _bøde = 200;
        return _bøde;
    }

}