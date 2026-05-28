using System.Collections.Generic;
using UnityEngine;

public class Separacion : ISteeringBehaviour
{
    public float weight { get; set; }
    public int count = 0;
    public Vector2 sep = Vector2.zero;

    public float sepDist;
    public List<GameObject> neighbors;
    public float self;
    public float distance;

    public Separacion(float weight, int count, Vector2 sep, float sepDist,List<GameObject> neighbors, float distance)
    {
        this.weight = weight;
        this.count = count;
        this.sep = sep;
        this.sepDist = sepDist;
        this.neighbors = neighbors;
        this.distance = distance;

    }

    private Vector2 Calculate(List<Transform> neighbors, Transform self)
    {
        foreach (Transform neighbor in neighbors)
        {
            for (int i = 0; i < count; i++)
            {
                if (distance < sepDist)
                {
                    Vector2 away = Vector2.Normalize(self.position - neighbor.position);
                    sep += away / distance;
                    count++;
                }
            }
        }

        return sep;
    }
}
