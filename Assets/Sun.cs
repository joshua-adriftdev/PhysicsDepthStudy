using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (StateManager.instance.selectedPlanet != null)
            transform.LookAt(StateManager.instance.selectedPlanet.orbit.body.transform);
    }
}
