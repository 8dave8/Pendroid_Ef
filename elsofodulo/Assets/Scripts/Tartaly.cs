using System;

public class Tartaly
{
    double hossz;
    double szelesseg;
    double magassag;

    public int MaxUrtartalomLiterben()
        => Convert.ToInt32(Math.Round(hossz * szelesseg * magassag * 1000));

    public int ToltottVizLiterben { get; private set; }

    public void Tolt(int mennyiseg)
        => ToltottVizLiterben += mennyiseg;

    public void Elfolyik(int mennyiseg)
        => ToltottVizLiterben -= mennyiseg;

    public Tartaly()
    {
        hossz = 10;
        szelesseg = 10;
        magassag = 10;
        ToltottVizLiterben = Convert.ToInt32(Math.Round(MaxUrtartalomLiterben() * 0.9));
    }
}
