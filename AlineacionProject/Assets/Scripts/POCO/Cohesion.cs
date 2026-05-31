using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Cohesion : ISteeringBehaviour
{
    public float weight { get; set; }
    public float perception;
    public Vector2 centroid;
    public float maxSpeed;
    public Vector2 desired;
    public float maxForce;
    private Vector2 velocity;

public Cohesion(float weight, float perception, float maxSpeed, Vector2 centroid, float maxForce, Vector2 velocity)
    {
        this.weight = weight;
        this.perception = perception;
        this.maxSpeed = maxSpeed;
        this.centroid = centroid;
        this.maxForce = maxForce;
        this.velocity = velocity;
    }

    public Vector2 Calculate(List<Transform> neighbors, Transform self)
    {
        Vector2 cohesion = Vector2.zero;
        int count = 0;
        foreach (Transform neighbor in neighbors)
        {
            float dist = Vector2.Distance(self.position, neighbor.position);
            Vector2 position = neighbor.position;
            Vector2 selfPos = self.position;
            if (dist < perception && dist > 0.001f)
            {
                cohesion += (Vector2)neighbor.position;
                count++;
            }
            else
            {
                centroid = cohesion / count;
                desired = ((centroid - selfPos).normalized * (maxSpeed));
                return Vector2.ClampMagnitude(desired - velocity, maxForce);
            }
        }
        return cohesion;
    }
}
