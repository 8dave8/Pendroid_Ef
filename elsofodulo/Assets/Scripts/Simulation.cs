using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Simulation : MonoBehaviour
{

    [SerializeField]
    private InputField sliderText;
    [SerializeField]
    private GameObject warning;

    int befolyoLiterSzam = 0;

    Tartaly tartaly = new Tartaly();

    static Szelep[] szelepek;
    public static void SzelepSet(Szelep[] input) => szelepek = input;
    public void BeIrBefolyoKobMeter()
    {
        try
        {
            double val = float.Parse(sliderText.text);
            befolyoLiterSzam = (int)Math.Round(val * 1000);

            if (val > szelepek.Sum(x => x.folyamSebesseg)) warning.SetActive(true);
            else warning.SetActive(false);
        }
        catch { }
    }
    
    public void Elteltperc()
    {
        tartaly.Tolt(befolyoLiterSzam); // osztani 60al majd megoldom
    }

}