using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class waterControll : MonoBehaviour
{
    public static Text atfolyoszam;
    public static Text atfolyomerett;
    public static int atfolyok;
    public static double atfolyomeret;
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void kezdes()
    {
        if (int.Parse(atfolyomerett.text) != 0)
        {
            atfolyok = int.Parse(atfolyoszam.text);
        }
        else if (double.Parse(atfolyomerett.text) != 0)
        {
            atfolyomeret = double.Parse(atfolyomerett.text);
        }
        try
        {
        Debug.Log(atfolyok + " " + atfolyomeret);
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
            throw;
        }
    }
}

class Szelep
{
    public bool bekapcsolva;

    public double folyamMennyiseg;

    public Szelep(double meret)
    {
        folyamMennyiseg = meret;
    }
}
