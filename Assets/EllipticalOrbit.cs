using UnityEngine;
using System.Collections.Generic;

[ExecuteAlways]
public class EllipticalOrbit : MonoBehaviour
{
    public Transform centralBody;
    public Transform body;
    public float semiMajorAxis = 10f; // a
    public float eccentricity = 0.5f; // e
    public float orbitalPeriod = 5f; // T
    public int resolution = 100; 

    private float meanAnomaly = 0f;
    private float meanMotion; // n
    private float currentTime = 0f;
    private LineRenderer lineRenderer;
    private List<Vector3> orbitPositions = new List<Vector3>();
    private float focalDistance; // f

    void Start()
    {
        InitializeOrbit();
    }

    void Update()
    {
        if (Application.isPlaying)
        {
            CalculateMeanMotion();
            currentTime += Time.deltaTime;
            meanAnomaly = meanMotion * currentTime;
            float eccentricAnomaly = SolveKeplersEquation(meanAnomaly, eccentricity);
            Vector3 position = CalculatePosition(eccentricAnomaly);
            body.transform.position = centralBody.position + position;

            //body.transform.LookAt(centralBody);
        }
        UpdateOrbitPath();
    }

    void OnValidate()
    {
        InitializeOrbit();
        CalculateOrbitPath();
        UpdateOrbitPath();
    }

    void InitializeOrbit()
    {
        CalculateMeanMotion();
        if (lineRenderer == null)
        {
            lineRenderer = GetComponentInChildren<LineRenderer>();
        }
        lineRenderer.positionCount = resolution;
        focalDistance = semiMajorAxis * eccentricity; 
    }

    void CalculateMeanMotion()
    {
        meanMotion = 2 * Mathf.PI / (orbitalPeriod);
        CalculateOrbitPath();
        UpdateOrbitPath();
    }

    float SolveKeplersEquation(float M, float e)
    {
        float E = M; 
        for (int i = 0; i < 5; i++) // 5 iterations Newton-Raphson
        {
            E = E - (E - e * Mathf.Sin(E) - M) / (1 - e * Mathf.Cos(E));
        }
        return E;
    }

    Vector3 CalculatePosition(float E)
    {
        float a = semiMajorAxis;
        float b = a * Mathf.Sqrt(1 - eccentricity * eccentricity); // semi-minor axis
        float x = a * Mathf.Cos(E) - focalDistance; // ensures that the sun is at a focal point
        float y = b * Mathf.Sin(E);
        return new Vector3(x, 0, y);
    }

    /* 
        Line Renderer
     */
    void CalculateOrbitPath()
    {
        orbitPositions.Clear();
        for (int i = 0; i < resolution; i++)
        {
            float M = 2 * Mathf.PI * i / resolution; // mean motion
            float E = SolveKeplersEquation(M, eccentricity);
            Vector3 position = CalculatePosition(E);
            orbitPositions.Add(position);
        }
    }

    /* 
        Line Renderer
     */
    void UpdateOrbitPath()
    {
        Vector3[] positions = new Vector3[orbitPositions.ToArray().Length];

        Vector3[] temp = orbitPositions.ToArray();

        for (int i = 0; i < temp.Length; i++)
        {
            positions[i] = temp[i];
        }
        positions[positions.Length-1] = positions[0];
        if (lineRenderer)
            lineRenderer.SetPositions(positions);
    }
}
