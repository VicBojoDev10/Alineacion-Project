using UnityEngine;
using System.Collections.Generic;

public class Boid : MonoBehaviour
{
    public float maxSpeed;

    public float maxForce;

    public float perception;
    public float sepDistance;
    public Vector2 velocity;
    public float wSep = 1.5f, wAli = 1f, wCoh = 1f;

    [SerializeField] private List<GameObject> boids;
    [SerializeField] public Separacion separacion;
    [SerializeField] public Alineacion alineacion;
    [SerializeField] public Cohesion cohesion;
    public void Step(List<Boid> neighbors, float dt)
    {
        Vector2 sep = Vector2.zero;
        Vector2 ali = Vector2.zero;
        Vector2 coh = Vector2.zero;

        
        int numSep = 0, numAli = 0, numCoh = 0;

        foreach (var neighbor in neighbors)
        {
            if (neighbor == this) continue;
            Vector2 offset = transform.position - neighbor.transform.position;
            var d = offset.magnitude;
            if(d >= perception || d < 0.001f) continue;

           // if (d < sepDistance) { separacion += offset.normalized / d;
                numSep++;
           // }

           // alineacion += neighbor.velocity; 
            numAli++;

            //cohesion += neighbor.transform.position;
            numCoh++;
        }

        Vector2 steer = Vector2.zero;
        if (numSep > 0) steer += wSep * CalculateSteering(sep / numSep);
        if (numAli > 0) steer += wAli * CalculateSteering(ali / numAli);
       // if (numCoh > 0) steer += wCoh * CalculateSteering((coh / numCoh) - transform.position);
        
        
        velocity = Vector2.ClampMagnitude(velocity + Vector2.ClampMagnitude(steer, maxForce) * dt, maxSpeed);
        transform.position += new Vector3(velocity.x, velocity.y,0) * dt;

    }

    private Vector2 CalculateSteering(Vector2 desiredDir)
    {
        var desired = desiredDir.normalized * maxSpeed;
        return Vector2.ClampMagnitude(desired - velocity, maxForce);
    }
}
