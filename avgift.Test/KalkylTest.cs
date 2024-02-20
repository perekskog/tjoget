using Avgift;

namespace avgift.Test;

public class KalkylTest
{
    Konstant konstant;
    Kalkyl kalkyl;

    public KalkylTest()
    {
        konstant = new Konstant
        {
            Avgift_kvartal = 1640,
            Fondering_kvartal = 280,
            Moms_ut = 0.25,
            Vatten_rörlig_m3 = 16,
            Vatten_fast_år = 12,
            Vatten_moms = 0.25,
            Vatten_förbetalt_år = 5,
            El_rörlig_kWh = 0.8,
            El_ingår = 10,
            El_moms = 0.25,
            Städdag_hus = 160,
            Städdag_moms = 0.25
        };

        kalkyl = new Avgift.Kalkyl();
    }


    [Fact]
    public void CalculateVatten_ElAndStäddagShouldBeZero()
    {
        // Arrange
        var förbrukning = new Avgift.Förbrukning
        {
            Vatten = 1,
            El = 2,
            Städdag = 3
        };

        // Act
        var kostnad = kalkyl.Vatten(förbrukning, konstant, new Kostnad());

        //Assert
        var expected = new Kostnad() { Vatten_rörlig = 16, Vatten_fast = 12, Vatten_brutto = 28, Vatten_netto = 23, El_justerad = 0, El_netto = 0, Städdag_netto = 0, Moms = 0, AttBetala = 0 };
        Assert.Equivalent(kostnad, expected);
    }

    [Fact]
    public void CalculateEl_VattenAndStäddagShouldBeZero()
    {
        // Arrange
        var förbrukning = new Avgift.Förbrukning
        {
            Vatten = 1,
            El = 12,
            Städdag = 3
        };

        // Act
        var kostnad = kalkyl.El(förbrukning, konstant, new Kostnad());

        //Assert
        var expected = new Kostnad() { Vatten_rörlig = 0, Vatten_fast = 0, Vatten_brutto = 0, Vatten_netto = 0, El_justerad = 2, El_netto = 1.6, Städdag_netto = 0, Moms = 0, AttBetala = 0 };
        Assert.Equivalent(kostnad, expected);
    }

    [Fact]
    public void CalculateStäddag_ElAndVattenShouldBeZero()
    {
        // Arrange
        var förbrukning = new Avgift.Förbrukning
        {
            Vatten = 1,
            El = 2,
            Städdag = 3
        };

        // Act
        var kostnad = kalkyl.Städdag(förbrukning, konstant, new Kostnad());

        //Assert
        var expected = new Kostnad() { Vatten_rörlig = 0, Vatten_fast = 0, Vatten_brutto = 0, Vatten_netto = 0, El_justerad = 0, El_netto = 0, Städdag_netto = -480, Moms = 0, AttBetala = 0 };
        Assert.Equivalent(kostnad, expected);
    }


}