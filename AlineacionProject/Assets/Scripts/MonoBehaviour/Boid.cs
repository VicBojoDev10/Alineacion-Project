using UnityEngine;
using System.Collections.Generic;

public class Boid : MonoBehaviour
{
    public float maxSpeed = 5f;

    public float maxForce = 12f;

    public float perception = 3f;
    public float sepDistance = 1.2f;
    public float wSep = 1.5f, wAli = 1f, wCoh = 1f;

    public float wSeek = 2f;
    public Vector2 velocity;

    [SerializeField] private List<GameObject> boids;
    [SerializeField] public Separacion sep;
    [SerializeField] public Alineacion ali;
    [SerializeField] public Cohesion coh;
    public void Step(List<Boid> neighbors, Vector2 mousePos, Vector2 flockDirection, float dt)
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

            if (d < sepDistance) 
            {
                sep += offset.normalized / d;
                numSep++;
            }

            ali += neighbor.velocity; 
            numAli++;

            coh += (Vector2)neighbor.transform.position;
            numCoh++;
        }

        Vector2 steer = Vector2.zero;
        if (numSep > 0) steer += wSep * CalculateSteering(sep / numSep);
        if (numAli > 0) steer += wAli * CalculateSteering(ali / numAli);
        if (numCoh > 0) steer += wCoh * CalculateSteering((coh / numCoh) - (Vector2)transform.position);
        
        Vector2 toMouse = mousePos - (Vector2)transform.position;
        steer += wSeek * CalculateSteering(toMouse);
        
        steer += wAli * CalculateSteering(flockDirection);
        velocity = Vector2.ClampMagnitude(velocity + Vector2.ClampMagnitude(steer, maxForce) * dt, maxSpeed);
        transform.position += new Vector3(velocity.x, velocity.y,0) * dt;

        if(velocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private Vector2 CalculateSteering(Vector2 desiredDir)
    {
        var desired = desiredDir.normalized * maxSpeed;
        return Vector2.ClampMagnitude(desired - velocity, maxForce);
    }
}
