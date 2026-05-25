using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class FlockManager : MonoBehaviour
{
    public int count;

    public float spawnRadius;

    public Boid boid;
    [SerializeField] private List<Boid> boids = new List<Boid>();
    [SerializeField] private SpatialHash hash;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(boid);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
