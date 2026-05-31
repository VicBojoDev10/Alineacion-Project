using System.Collections.Generic;
using UnityEngine;

public class Separacion : ISteeringBehaviour
{
    public float weight { get; set; }
    public float sepDist;
    public float maxSpeed;
    public Separacion(float weight, float sepDist,  float maxSpeed)
    {
        this.weight = weight;
        this.sepDist = sepDist;
        this.maxSpeed = maxSpeed;
    }

    public Vector2 Calculate(List<Transform> neighbors, Transform self)
    {
        Vector2 sep = Vector2.zero;
        int count = 0;
        foreach (Transform neighbor in neighbors)
        {
            float dist = Vector2.Distance(self.position, neighbor.position);
            if (dist < sepDist && dist > 0.001f)
            {
                Vector2 away = ((self.position - neighbor.position)).normalized;
                sep  += away / dist;
                count++;
            }
        }
        if(count > 0) sep /= count;
        return sep;
    }
}
