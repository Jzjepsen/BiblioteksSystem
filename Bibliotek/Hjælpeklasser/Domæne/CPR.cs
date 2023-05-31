namespace Bibliotek.Domæne;

public class CPR
{
    private int cprNummer = 0000000000;
    
    public CPR(int cprNummer)
    {
        this.cprNummer = cprNummer;
    }
    public int CPRNummer
    {
        get { return cprNummer; }
        set
        {
            if (IsValidCPR(value))
            {
                cprNummer = value;
            }
            else
            {
                throw new ArgumentException("Ugyldigt CPR-nummer");
            }
        }
    }

    private bool IsValidCPR(int cpr)
    {
        string cprString = cpr.ToString();

        if (cprString.Length != 10)
        {
            return false; // CPR-nummeret skal være præcis 10 cifre langt
        }
        return true;

    }
}