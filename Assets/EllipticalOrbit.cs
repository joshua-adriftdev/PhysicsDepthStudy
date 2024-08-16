using UnityEngine;
using System.Collections.Generic;

[ExecuteAlways]
public class EllipticalOrbit : MonoBehaviour
{
    public Transform centralBody;
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
            CalculateMeanMotion(); // re-calculate the fixed mean motion value
                                   // ensures the orbital path responds to value changes at runtime

            currentTime += Time.deltaTime; // Update time

            // Calculate meanAnomaly based on time
            meanAnomaly = meanMotion * currentTime;
            float eccentricAnomaly = SolveKeplersEquation(meanAnomaly, eccentricity);

            Vector3 position = CalculatePosition(eccentricAnomaly);
            transform.position = centralBody.position + position;
        }
    }

    void OnValidate()
    {
        InitializeOrbit();
        CalculateOrbitPath();
        UpdateOrbitPath();
    }

    // Setup Line Renderers
    void InitializeOrbit()
    {
        CalculateMeanMotion(); // Calculate the fixed mean motion value
        if (lineRenderer == null)
        {
            lineRenderer = GetComponentInChildren<LineRenderer>();
        }
        if (lineRenderer)
            lineRenderer.positionCount = resolution;

        focalDistance = semiMajorAxis * eccentricity; // calculate the focalDistance
    }

    void CalculateMeanMotion()
    {
        meanMotion = 2 * Mathf.PI / (orbitalPeriod);
    }

    float SolveKeplersEquation(float m, float e)
    {
        float E = m; 
        for (int i = 0; i < 5; i++) // 5 iterations Newton-Raphson
        {
            E = E - (E - e * Mathf.Sin(E) - m) / (1 - e * Mathf.Cos(E));
        }
        return E;
    }

    Vector3 CalculatePosition(float E)
    {
        float a = semiMajorAxis;
        float b = a * Mathf.Sqrt(1 - eccentricity * eccentricity); // calculate the semi-minor axis

        float x = a * Mathf.Cos(E) - focalDistance; // ensures that the sun is at a focal point
        float y = b * Mathf.Sin(E);
        return new Vector3(x, 0, y); // the position in global coordinates
    }

    /* 
        Line Renderer - Uses a resolution to precalculate the orbital path, using X (resolution) points.
                        This code runs exclusively to render to orbital path in the scene, and does not impact the movement of bodies.
     */
    public void CalculateOrbitPath()
    {
        orbitPositions.Clear();
        for (int i = 0; i < resolution; i++) // 'i' becomes 'time'
        {
            float M = 2 * Mathf.PI * i / resolution; // Calculate mean motion based on the resolution.
            float E = SolveKeplersEquation(M, eccentricity);
            Vector3 position = CalculatePosition(E);
            orbitPositions.Add(position);
        }
    }

    /* 
        Line Renderer
     */
    public void UpdateOrbitPath()
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
