using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class waterControll : MonoBehaviour
{
    //ToStart
    public GameObject bekeres;
    public GameObject Simulation;
    public Text atfolyoszam;
    public Text atfolyomerett;

    private static int atfolyok;
    private static float atfolyomeret;
    
    //Simulation
    public Text SimTime;
    public Text Vizmagassag;
    public Text OsszViz;
    public Text NyitottLefolyoSzam;
    public Slider SLWaterIN;
    public Text CurrentWaterIn;
    public Transform Water;

    private float maxVizy;
    private float ido = 0f;
    private const float maxViz = 1000f;
    private float CurrentWater = 0;

    void Start()
    {
        maxVizy = Water.transform.localScale.y;
        Water.localPosition = new Vector2(Water.localPosition.x, Water.localPosition.y-50);
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
            CurrentWater += SLWaterIN.value * Time.deltaTime;
            float vizmagassag = CurrentWater / 100000;

            vizszintControll(vizmagassag);
            

            //Viz img beállitása
            Water.localScale = new Vector2(Water.localScale.x, CurrentWater/20000*Time.deltaTime);
            if (SLWaterIN.value != 0)  Water.localPosition = new Vector2(Water.localPosition.x, Water.localPosition.y+CurrentWater / 100000*Time.deltaTime);

            Kiiras(vizmagassag);
        }
    }

    private void vizszintControll(float vizmagassag)
    {
        if (vizmagassag < 8.9f)
        {

        }
        else if (vizmagassag > 9.1f)
        {
            float minusViz = atfolyok * (atfolyomeret * 1000) * Time.deltaTime;
            CurrentWater -= atfolyok;
            Water.localPosition = new Vector2(Water.localPosition.x, Water.localPosition.y - atfolyok*Time.deltaTime);
        }

        if (CurrentWater >= 1000000)
        {
            SLWaterIN.value = 0;
            CurrentWater = 1000000;
        }
    }

    private void Kiiras(float vizmagassag)
    {
        SimTime.text = $"Szimuláció ideje: {Mathf.Round(ido)}";
        CurrentWaterIn.text = SLWaterIN.value.ToString() + " L/mp";
        OsszViz.text = "Vizmennyiseg: " + CurrentWater.ToString();
        NyitottLefolyoSzam.text = "Bekapcsolt lefolyók száma: " + atfolyok.ToString();
        Vizmagassag.text = "Vizmagasság: " + vizmagassag.ToString();
    }
    public void kezdes()
    {
        try
        {
            atfolyok = int.Parse(atfolyoszam.text);
            string x = atfolyomerett.text.Replace(',', '.');
            atfolyomeret = float.Parse(x);
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
            catch (System.Exception e)
            {
                Debug.Log(e);
                throw;
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
            throw;
        }
    }
    public static void StartSim(GameObject bekeres, GameObject Simulation)
    {
        bekeres.SetActive(false);
        Simulation.SetActive(true);
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
