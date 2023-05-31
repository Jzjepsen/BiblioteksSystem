using Bibliotek.Domæne;

namespace Bibliotek;

public interface ILånerDatabase
{
     void BødeIkkeBetalt(CPR lånerID, double bøde);
     void BødeBetalt(CPR lånerID, double bøde);
}