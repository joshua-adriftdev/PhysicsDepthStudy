using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager instance;
    public float timeScale = 1f;

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
}

// SUn = directional light that looks at the planet the user is on.