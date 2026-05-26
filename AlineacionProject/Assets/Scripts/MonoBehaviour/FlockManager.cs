using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class FlockManager : MonoBehaviour
{
    public int count;

    public float spawnRadius;
    

    [SerializeField] private List<Boid> boids;
    //[SerializeField] private SpatialHash hash;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            //boids.Add(Instantiate(boids, transform.position, transform.rotation));
        }
    }

    
    void Update()
    {
        
    }
}
