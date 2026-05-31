using System.Collections.Generic;
using UnityEngine;

public interface ISteeringBehaviour
{
    public float weight { get; set; }

    public Vector2 Calculate(List<Transform> neighbors, Transform self);
}
