
using System.ComponentModel.DataAnnotations;
using Bibliotek;
using Bibliotek.Controller;
using Bibliotek.Domæne;
using Bibliotek.Interfaces.Boundary;
using Moq;
using NSubstitute.Exceptions;
using NSubstitute.ReceivedExtensions;

namespace BiblioteksSystem;
using NSubstitute;
using NUnit.Framework;
using NUnit;

public class Tests
{
    private AfleverBog UUT_bog;
    private IBrugerInterface brugerInterface;
    private IPrinter printer;
    private IBødeModul bødeModul;
    private ILånerDatabase lånerDatabase;
    private IBogDatabase bogDatabase;
    private BogID ID;
    private IBogscanner bogscanner;
    private IGetDateTime dateTime;
    
    [SetUp]
    public void Setup()
    {
        brugerInterface = Substitute.For<IBrugerInterface>();
        printer = Substitute.For<IPrinter>();
        bødeModul = Substitute.For<IBødeModul>();
        lånerDatabase = Substitute.For<ILånerDatabase>();
        bogDatabase = Substitute.For<IBogDatabase>();
        bogscanner = Substitute.For<IBogscanner>();
        ID = Substitute.For<BogID>();
        dateTime = Substitute.For<IGetDateTime>();
        UUT_bog = new AfleverBog(brugerInterface, printer,bødeModul, lånerDatabase, bogDatabase, bogscanner, dateTime);
    }

    [Test]
    public void BødeBetalt_ShouldUpdateLånerDatabase()
    {
        // Arrange
        var lånetid = 30;
        var bøde = 50;
        var expectedLånerId = new CPR(2311952266); // Replace with the actual type of CPR

        UUT_bog.UdregnLånetid().Returns(lånetid);
        bødeModul.UdregnBøde(lånetid).Returns(bøde);

        // Act
        UUT_bog.BødeBetalt();

        // Assert
        lånerDatabase.Received(1).BødeBetalt(expectedLånerId, bøde);
    }
    
    [Test]
    public void NewIDScan_event_received()
    {
        // Arrange
        var eventArgs = new ScannetEventArgs(ID); //args til scannet Event
        var lånetid = 30;
        var bøde = 31;
            //Act
        bogscanner.NewIdScan += Raise.EventWith(UUT_bog, eventArgs);

        //Assert - Mock
        bogDatabase.Received(1).FindBog(ID);

        //Assert udregn lånetid returnerer lånetid
        UUT_bog.UdregnLånetid().Returns(lånetid);

        //Assert - state based test. BVA: 30 dage
        bødeModul.UdregnBøde(lånetid).Returns(0);

        // Assert opkrævBøde kaldes når bøde er over 30
        brugerInterface.Received(1).OpkrævBøde(bøde);

        //Assert
        bogDatabase.BogAfleveret(ID);

        //Assert
        printer.UdskrivKvittering(ID, bøde, true);
    }
}