using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//ha ezt valaki meglátja és kiég a clean code teljes hiányától => nem én voltam    -Yiselita

public class waterControll : MonoBehaviour
{
    //ToStart
    public GameObject bekeres;
    public GameObject Simulation;
    public Text atfolyoszam;
    public Text atfolyomerett;

    private int atfolyok;
    private double atfolyomeret;
    
    //Simulation
    public Text SimTime;
    public Text Vizmagassag;
    public Text OsszViz;
    public Text NyitottLefolyoSzam;
    public Slider SLWaterIN;
    public Text CurrentWaterIn;
    public Transform Water;
    public Text Bearamlas;
    public Text MaxBearamlas;
    public GameObject Warning;
    public Text VizmellettiVizmagassag;
    public Text VizmellettiVizmennyiseg;

    private float CurrentWater = 900000;
    private float mozgas = 380 - (-75);
    private int aktivSzelepek;

    private float  minutes , seconds, miliseconds = 0f;

    void Start()
    {
        Water.localPosition = new Vector2(Water.localPosition.x, -75);
        bekeres.SetActive(true);
        Simulation.SetActive(false);
    }
    void FixedUpdate()
    {
        if (Simulation.activeSelf)
        {
            //Szamolasok
            Counter();
            CurrentWater += SLWaterIN.value / 3.6f;
            VizszintControll();
            //Viz img beállitása
            Water.localPosition = new Vector2(Water.localPosition.x, -70+(mozgas/1000000)*CurrentWater);

            Kiiras(Math.Round((CurrentWater / 100000), 2));
        }
    }
    private void Counter()
    {
        if (miliseconds >= 1000)
        {
            if (seconds >= 60)
            {
                minutes++;
                seconds = 0;
            }
            else seconds++;
            miliseconds -= 1000;
        }
        miliseconds += Time.deltaTime * 1000;

        var minutesS = ((minutes < 10) ? "0" : "") + minutes;
        var secondsS = ((seconds < 10) ? "0" : "") + seconds;
        var milisecondsS = (((int)miliseconds < 100) ? "0" : "") + (((int)miliseconds < 10) ? "0" : "") + (int)miliseconds;

        SimTime.text = ($"{minutesS}:{secondsS}:{milisecondsS}");
    } 
    private void VizszintControll()
    {
        var overMax = (SLWaterIN.value > (atfolyok * atfolyomeret));
        Warning.SetActive(overMax);


        aktivSzelepek = (overMax) ? atfolyok : GetAktivSzelepek();

        CurrentWater -= (float)(atfolyomeret * aktivSzelepek) / 3.6f;

        if (CurrentWater >= 1000000) CurrentWater = 1000000;
        
    }

    private int GetAktivSzelepek()
    {
        if (CurrentWater >= 910000) return atfolyok;

        int szelepCount = 0;
        var tmp = SLWaterIN.value;
        while (tmp > atfolyomeret)
        {
            tmp -= (float)atfolyomeret;
            szelepCount++;
        }

        return ((CurrentWater>900000) ? 1 : 0) + szelepCount;
    }

    private void Kiiras(double vizmagassag)
    {
        CurrentWaterIn.text = Math.Round(SLWaterIN.value, 2).ToString() + " m^3/h";
        OsszViz.text = CurrentWater.ToString()+" L";
        VizmellettiVizmennyiseg.text = CurrentWater.ToString()+" L";
        NyitottLefolyoSzam.text = aktivSzelepek.ToString();
        Vizmagassag.text = vizmagassag.ToString()+" m";
        VizmellettiVizmagassag.text = vizmagassag.ToString() + " m";
        Bearamlas.text = (SLWaterIN.value * 3.6f).ToString()+" L";
    }
    public void kezdes()
    {
        try
        {
            atfolyok = int.Parse(atfolyoszam.text);
            atfolyomeret = float.Parse(atfolyomerett.text.Replace(',', '.'));
            if (atfolyok > 0 && (atfolyomeret > 0f && atfolyomeret <= 100f))
            {
                StartSim(bekeres, Simulation);
            }
        }
        catch (Exception) { }    
    }
    public void StartSim(GameObject bekeres, GameObject Simulation)
    {
        bekeres.SetActive(false);
        Simulation.SetActive(true);

        SLWaterIN.maxValue = (float)Math.Round(atfolyok * atfolyomeret * 1.1, 2);
        MaxBearamlas.text = SLWaterIN.maxValue.ToString() + " m^3/h";

    }
    public void Exit() => Application.Quit();
    public void Restart() => SceneManager.LoadScene("SampleScene"); 
}