using System;
using UnityEngine;
using UnityEngine.UI;


public class InputHandler : MonoBehaviour
{
    [SerializeField]
    private InputField InSzelepSzam;
    [SerializeField]
    private InputField InSzelepSebesseg;
    [SerializeField]
    private GameObject InputField;
    [SerializeField]
    private GameObject SimulationField;

    public static Szelep[] szelepek;

    public void Kezdes()
    {
        try
        {
            int szelepSzam = int.Parse(InSzelepSzam.text);
            double szelepSebesseg = double.Parse(InSzelepSebesseg.text);

            if (szelepSzam == 0 || (szelepSebesseg == 0 || szelepSebesseg > 20)) throw new Exception();
            szelepek = new Szelep[szelepSzam];

            for (int i = 0; i < szelepek.Length; i++) 
                szelepek[i] = new Szelep(szelepSebesseg);

            InputField.SetActive(false);
            SimulationField.SetActive(true);

            Simulation.SzelepSet(szelepek);
        }
        catch (Exception e) { Debug.Log(e); }
    }
}
