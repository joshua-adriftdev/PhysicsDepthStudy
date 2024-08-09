using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager instance;
    public float timeScale = 1f;
    public float planetScale = 1f;

    public Planet[] planets;
    public Planet selectedPlanet = null;

    public Camera issCamera;

    private void Start()
    {
        instance = this;
    }

    public void UpdateTimeScale(float s)
    {
        int timeScale = Mathf.RoundToInt(s);
        Debug.Log("Time Scale: " + timeScale);
        this.timeScale = Mathf.Pow(10, timeScale);
        Time.timeScale = Mathf.Pow(10, timeScale);
        Debug.Log("Time Scale: " + Mathf.Pow(10, timeScale));
    }

    public void UpdateScale(float s)
    {
        int scale = Mathf.RoundToInt(s);
        Debug.Log("Planet Scale: " + scale);
        this.planetScale = Mathf.Pow(10, scale);
        Debug.Log("Planet Scale: " + Mathf.Pow(10, scale));
    }

    public void SetPlanetIndex(int index)
    {
        Planet planet = planets[index];
        
        OnSelectPlanet(planet, selectedPlanet);
        selectedPlanet = planet;
    }

    public void OnSelectPlanet(Planet planet, Planet old)
    {
        if (old != null) { 
            old.cam.enabled = false;
            if (old == planets[3])
            {
                planets[2].lineRenderer.startWidth = 10f;
            }
            else
            {
                old.lineRenderer.startWidth = 10f;
            }
        }

        planet.cam.enabled = true;
        if (planet == planets[3])
        {
            planets[2].lineRenderer.startWidth = 0.005f;
        } else
        {
            planet.lineRenderer.startWidth = 0.005f;
        }
        //planet.orbit.resolution = 5000;

        //Invoke(nameof(UpdateLine), 10);
    }
    
    public void GoToISS()
    {
        Time.timeScale = 0.00001f;
        //Time.timeScale = 0.05f;
        issCamera.enabled = true;
    }
}

// SUn = directional light that looks at the planet the user is on.