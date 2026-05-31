using UnityEngine;
using System.Collections.Generic;
public class Alineacion : ISteeringBehaviour
{
    public float weight { get; set; }
    public float perception;
    public float maxSpeed;
    public Vector2 velocity;
    public float maxForce;
    public Alineacion(float weight, float perception, Vector2 velocity, float maxSpeed, float maxForce, float force)
    {
        this.weight = weight;
        this.perception = perception;
        this.velocity = velocity;
        this.maxSpeed = maxSpeed;
        this.maxForce = maxForce;   
    }

    public Vector2 Calculate(List<Transform> neighbors, Transform self)
    {
        Vector2 align = Vector2.zero;
        int count = 0;
        foreach (Transform neighbor in neighbors)
        {
            float dist = Vector2.Distance(self.position, neighbor.position);
            if (dist < perception && dist > 0.001f)
            {
                Boid boid = neighbor.GetComponent<Boid>();
                if (boid != null)
                {
                    align += boid.velocity;
                    count++;
                }
            }
            else
            {
                align = (align / count).normalized * maxSpeed;
                align = Vector2.ClampMagnitude(align - velocity, maxForce);
            }
        }
        return align;
    }
}
