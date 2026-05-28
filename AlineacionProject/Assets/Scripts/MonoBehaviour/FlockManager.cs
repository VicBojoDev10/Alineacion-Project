using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class FlockManager : MonoBehaviour
{
    public int count;

    public float spawnRadius;
    

    [SerializeField] private List<Boid> boid;
    //[SerializeField] private SpatialHash hash;
    void Start()
    {
        foreach (Boid boids in boid)
        {
             for (int i = 0; i < count; i++) 
             {
                boid.Add(Instantiate(boids, transform.position, transform.rotation)); 
             }
        }
    }
    
    //Seek en flocking Manager o crear los gameobjects de 0 para que hagan el behavior

    
    void Update()
    {
        
    }
}
