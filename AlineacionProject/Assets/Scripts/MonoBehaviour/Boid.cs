using UnityEngine;
using System.Collections.Generic;

public class Boid : MonoBehaviour
{
    public float maxSpeed;

    public float maxForce;

    public float perception;
    public float sepDistance;
    public Vector2 velocity;

    public void Step(IList<Boid> neighbor, float dt)
    {
        Vector2 sep = Vector2.zero;
        Vector2 ali = Vector2.zero;
        Vector2 coh = Vector2.zero;
    }

    private Vector2 CalculateSteering(Vector2 desiredDir)
    {
        var desired = desiredDir.normalized * maxSpeed;
        return Vector2.ClampMagnitude(desired - velocity, maxForce);
    }
}
