using Bibliotek.Domæne;

namespace Bibliotek;

public interface IPrinter
{
    bool UdskrivKvittering(BogID id, double bøde, bool bødeStatus);
}

