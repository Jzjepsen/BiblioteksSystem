namespace Bibliotek;

public class GetDateTime : IGetDateTime
{
    public DateTime getDateTime()
    {
        return DateTime.Now;
    }
}