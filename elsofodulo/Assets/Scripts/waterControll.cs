using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private float maxVizy;
    private float ido = 0f;
    private const float maxViz = 1000f;
    private float CurrentWater = 900000;
    private float mozgas = 500 - 180;

    void Start()
    {
        maxVizy = Water.transform.localScale.y;
        Water.localPosition = new Vector2(Water.localPosition.x, 180);
        bekeres.SetActive(true);
        Simulation.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Simulation.activeSelf)
        {
            //Szamolasok
            ido += Time.deltaTime;
            CurrentWater += SLWaterIN.value / 3.6f; //* Time.deltaTime;
            double vizmagassag = Math.Round((CurrentWater / 100000), 2); //ez legyen double, meg inkább kerekített

            vizszintControll(vizmagassag);
            

            //Viz img beállitása
            Water.localPosition = new Vector2(Water.localPosition.x, 180+(mozgas/1000000)*CurrentWater);

            Kiiras(vizmagassag);
        }
    }

    private void vizszintControll(double vizmagassag)
    {
        Warning.SetActive(((SLWaterIN.value) > (atfolyok * atfolyomeret)));

        CurrentWater -= (float)(atfolyok * atfolyomeret) / 3.6f;
        


        if (CurrentWater >= 1000000)
        {
            SLWaterIN.value = 0;
            CurrentWater = 1000000;
        }
    }

    private void Kiiras(double vizmagassag)
    {
        SimTime.text = $"Szimuláció ideje: {Mathf.Round(ido)}";
        CurrentWaterIn.text = Math.Round(SLWaterIN.value, 2).ToString() + " m^3/h";
        OsszViz.text = CurrentWater.ToString()+" L";
        NyitottLefolyoSzam.text = atfolyok.ToString();
        Vizmagassag.text = vizmagassag.ToString()+" m";
        Bearamlas.text = (SLWaterIN.value * 3.6f).ToString()+" L";
    }
    public void kezdes()
    {
        try
        {
            atfolyok = int.Parse(atfolyoszam.text);
            atfolyomeret = float.Parse(atfolyomerett.text.Replace(',', '.'));
            Debug.Log("atfolyomerett" + atfolyomeret);
            try
            {
                if (atfolyok > 0)
                {
                    if (atfolyomeret > 0f && atfolyomeret <= 100f)
                    {
                        StartSim(bekeres, Simulation);
                    }
                }
                Debug.Log(atfolyok + " " + atfolyomeret);
            }
            catch (Exception) { } // ezeket innen leszedtem mert ennek a debugolásán
        }
        catch (Exception) { }     // rég túl vagyunk azt így rövidebb a kód
    }


    public void StartSim(GameObject bekeres, GameObject Simulation) // a skálázás miatt leszedtem a staticot, nem kell az btw
    {
        bekeres.SetActive(false);
        Simulation.SetActive(true);

        SLWaterIN.maxValue = (float)Math.Round(atfolyok * atfolyomeret * 1.25, 2);
        MaxBearamlas.text = SLWaterIN.maxValue.ToString();
        /*így skálászva lesz, nem lesz olyan lassú a szimuláció
         * mivel skálázva van, nem fog olyan gyorsan megtelni a víz (kifolyni eddig se folyt gyorsan)
         * Szóval nyugodtan kezdődhet 9 méteres vízmagassággal
         * BTW azért van random osztogatás 3,6al mert át van váltva a m^3/h l/s-re
         * tudom hogy még átláthatatlanabb de működik eskü
         */
    }
    public void goBack()
    {
        SceneManager.LoadScene("SampleScene");
        Debug.Log("kilep");
    }
    public void btKilepes()
    {
        Application.Quit();
    }
}

class Szelep
{
    public bool bekapcsolva;
    public double folyamMennyiseg;
    public Szelep(double meret)
    {
        this.folyamMennyiseg = meret;
    }
}
