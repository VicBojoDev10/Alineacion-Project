using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class FlockManager : MonoBehaviour
{
    public float maxSpeed = 5f;

    public float maxForce = 12f;

    public float perception = 3f;
    public float sepDistance = 1.2f;
    public float wSep = 1.5f, wAli = 1f, wCoh = 1f;

    public float wSeek = 2f;
    public int count = 50;
    public float spawnRadius;
    [SerializeField] private Boid boidsPrefab;
    private List<Boid> boids = new List<Boid>();

    public Vector2 velocity;
    
    public float pointsDistance = 3f;
    public float rotationSpeed = 90f;
    
    private float currentAngle = 0f;

    private Vector2 originPoint;
    private Vector2 targetPoint;

    private Camera cam;
    //[SerializeField] private SpatialHash hash;
    void Start()
    {
        cam = Camera.main;
        for (int i = 0; i < count; i++)
        {
            Boid boid = Instantiate(boidsPrefab,transform.position, Quaternion.identity);
            boid.velocity = Random.insideUnitCircle * boid.maxSpeed;
            boids.Add(boid);
        }
    }
    
    //Seek en flocking Manager o crear los gameobjects de 0 para que hagan el behavior

    
    void Update()
    {
        foreach (var boid in boids)
        {
            boid.maxSpeed = maxSpeed;
            boid.maxForce = maxForce;
            boid.perception = perception;
            boid.sepDistance = sepDistance;
            boid.wSep = wSep;
            boid.wAli = wAli;
            boid.wCoh = wCoh;
            boid.wSeek = wSeek;
        }
        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0;
        originPoint = new Vector2(mouseWorld.x, mouseWorld.y);
        
        if(Input.GetKey(KeyCode.LeftArrow)) currentAngle += rotationSpeed * Time.deltaTime;
        if(Input.GetKey(KeyCode.RightArrow)) currentAngle -= rotationSpeed * Time.deltaTime;
        
        float rad = currentAngle * Mathf.Deg2Rad;
        targetPoint = originPoint + new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * pointsDistance;
        
        Vector2 flockDirection = (targetPoint - originPoint).normalized;
        
        foreach (var boid in boids)
            boid.Step(boids, originPoint, flockDirection, Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(new  Vector3(originPoint.x, originPoint.y, 0), 0.2f);
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(new Vector3(targetPoint.x, targetPoint.y, 0), 0.15f);
        
        Gizmos.color = Color.white;
        Gizmos.DrawLine(new Vector3(originPoint.x, originPoint.y, 0), new Vector3(targetPoint.x, targetPoint.y, 0));    
    }
}
