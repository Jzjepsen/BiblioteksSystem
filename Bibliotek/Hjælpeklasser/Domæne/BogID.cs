namespace Bibliotek.Domæne;

public class BogID
{
    public int BookId { get; set; }                     //Bogens Identifikation 
    public string Title { get; set; }                   //titel
    public string Author { get; set; }                  //forfatter
    public string Genre { get; set; }                   //genre
    public bool IsAvailable { get; set; }               //udlånsstatus
    public DateTime LastCheckedOutDate { get; set; }    // sidste udtjekningsdato
    public CPR LastCheckedOutBy { get; set; }           // seneste låners CPR nummer
}

// Lave LastCheckedOutDate om til en metode

