using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager instance;
    public float timeScale = 1f;

    public Planet[] planets;
    public Planet selectedPlanet = null;
    

    private void Start()
    {
        instance = this;
    }

    public void UpdateTimeScale(float s)
    {
        int timeScale = Mathf.RoundToInt(s);
        Debug.Log("Time Scale: " + timeScale);
        this.timeScale = timeScale;
        Time.timeScale = Mathf.Pow(10, timeScale);
        Debug.Log("Time Scale: " + Mathf.Pow(10, timeScale));
    }

    public void SetPlanetIndex(int index)
    {
        Planet planet = planets[index];
        selectedPlanet = planet;
        OnSelectPlanet(planet, selectedPlanet);
        
    }

    public void OnSelectPlanet(Planet planet, Planet old)
    {
        if (old != null) { 
            old.cam.enabled = false;
            old.orbit.resolution = 1000;
        }

        planet.cam.enabled = true;
        planet.orbit.resolution = 5000;

        Invoke(nameof(UpdateLine), 10);
    }

    public void UpdateLine()
    {
        Debug.Log("Check");
        selectedPlanet.orbit.resolution = 5002;
        selectedPlanet.orbit.CalculateOrbitPath();
        selectedPlanet.orbit.UpdateOrbitPath();
    }
}

// SUn = directional light that looks at the planet the user is on.