using Bibliotek.Domæne;

namespace Bibliotek;

public interface IBogDatabase
{
    void FindBog(BogID id);
    void BogAfleveret(BogID id);
}