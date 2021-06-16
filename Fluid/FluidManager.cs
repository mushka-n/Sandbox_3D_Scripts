using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using Random = UnityEngine.Random;

public class FluidManager : MonoBehaviour
{
    public GameObject Prefab;
    public float Radius;
    public float Mass;
    public float RestDensity;
    public float Viscosity;
    public float Drag;

    public bool WallsUp;
    public List<GameObject> Walls = new List<GameObject>();

    private float smoothingRadius = 1f;
    private Vector3 gravity = new Vector3(0f, -9.81f, 0f);
    private float gravityMultiplicator = 2000f;
    private float gas = 2000f;
    private float deltaTime = 0.0008f;
    private float damping = -0.5f;

    private Transform BoxInsides;
    
    private List<Particle> particles = new List<Particle>();
    private static int particlesLength = 0;
    private List<ParticleCollider> colliders = new List<ParticleCollider>();
    private static int collidersLength = 5;
    private bool clearing;

    private void Start()
    {
        BoxInsides = GameObject.Find("BoxInsides").transform;
        colliders.Add(GameObject.Find("Floor").GetComponent<ParticleCollider>());
        colliders.Add(GameObject.Find("Wall1").GetComponent<ParticleCollider>());
        colliders.Add(GameObject.Find("Wall2").GetComponent<ParticleCollider>());
        colliders.Add(GameObject.Find("Wall3").GetComponent<ParticleCollider>());
        colliders.Add(GameObject.Find("Wall4").GetComponent<ParticleCollider>());
        Initialize();
        
       
    }
    
    IEnumerator MoveParticles() {
        while (true) {
            yield return new WaitForSeconds(0.02f);
            MyUpdate();
        }
    }  

    private void MyUpdate()
    {
        CalculateForces();
        CalculateColissions();
        ParticleMovement();
    }

    private void Initialize()
    {
        //clearing = true;
        StartCoroutine(MoveParticles());
    }

    public void CreateParticle(Vector3 position)
    {
        for (int i = 0; i < 1; i++)
        {
            GameObject currentGo = Instantiate(Prefab);
            currentGo.transform.parent = BoxInsides;
            Particle currentParticle = currentGo.AddComponent<Particle>();
            currentParticle.transform.parent = BoxInsides;
            particles.Add(currentParticle);
            particlesLength++;
            
            currentGo.transform.localScale = Vector3.one * Radius;
            currentGo.transform.position = position;

            currentParticle.Go = currentGo;
            currentParticle.Position = currentGo.transform.position;
            print(particlesLength);
        }
        
    }

    private float ParticleDensity(Particle _currentParticle, float _distance)
    {
        if (_distance < smoothingRadius)
        {
            return _currentParticle.Density += Mass * (315f / (64f * Mathf.PI * Mathf.Pow(smoothingRadius, 9.0f))) *
                                               Mathf.Pow(smoothingRadius - _distance, 3f);
        }

        return _currentParticle.Density;
    }

    private void CalculateForces()
    {
        for (int i = 0; i < particlesLength; i++)
        {
            if (clearing) return;
            for (int j = 0; j < particlesLength; j++)
            {
                Vector3 direction = particles[j].Position - particles[i].Position;
                float distance = direction.magnitude;

                particles[i].Density = ParticleDensity(particles[i], distance);
                particles[i].Pressure = gas * (particles[i].Density - RestDensity);
            }
        }
    }

    private void ParticleMovement()
    {
        for (int i = 0; i < particlesLength; i++)
        {
            if (clearing) return;
            
            Vector3 forcePressure = Vector3.zero;
            Vector3 forceViscosity = Vector3.zero;

            for (int j = 0; j < particlesLength; j++)
            {
                if (i == j) continue;
                Vector3 direction = particles[j].Position - particles[i].Position;
                float distance = direction.magnitude;

                forcePressure += ParticlePressure(particles[i], particles[j], direction, distance);
                forceViscosity += ParticleViscosity(particles[i], particles[j], distance);
            }

            Vector3 forceGravity = gravity * particles[i].Density * gravityMultiplicator;

            particles[i].CombinedForce = forcePressure + forceViscosity + forceGravity;
            particles[i].Velocity += deltaTime * (particles[i].CombinedForce) / particles[i].Density;
            particles[i].Position += deltaTime * (particles[i].Velocity);
            particles[i].Go.transform.position = particles[i].Position;
        }
    }

    private Vector3 ParticlePressure(Particle _currentParticle, Particle _nextParticle, Vector3 _direction,
        float _distance)
    {
        if (_distance < smoothingRadius)
        {
            return -1 * _direction.normalized * Mass * (_currentParticle.Pressure + _nextParticle.Pressure) /
                   (2f * _nextParticle.Density) * (-45f / (Mathf.PI * Mathf.Pow(smoothingRadius, 6f))) *
                   Mathf.Pow(smoothingRadius - _distance, 2f);
        }
        
        return Vector3.zero;
    }

    private Vector3 ParticleViscosity(Particle _currentParticle, Particle _nextParticle, float _distance)
    {
        if (_distance < smoothingRadius)
        {
            return Viscosity * Mass * (_nextParticle.Velocity - _currentParticle.Velocity) / _nextParticle.Density *
                   (45f / (Mathf.PI * Mathf.Pow(smoothingRadius, 6f))) * (smoothingRadius - _distance);
        }
        return Vector3.zero;
    }

    private void CalculateColissions()
    {
        for (int i = 0; i < particlesLength; i++)
        {
            for (int j = 0; j < collidersLength; j++)
            {
                if (clearing || collidersLength == 0) return;
                
                Vector3 penetrationNormal;
                Vector3 penetrationPosition;
                float penetrationLength;

                if (Collision(colliders[j], particles[i].Position, Radius, out penetrationNormal,
                    out penetrationPosition, out penetrationLength))
                {
                    particles[i].Position = penetrationPosition - penetrationNormal * Mathf.Abs(penetrationLength);
                    particles[i].Velocity =
                        DampenVelocity(colliders[j], particles[i].Velocity, penetrationNormal, 1f - Drag);
                }
            }
        }
    }

    private static bool Collision(ParticleCollider collider, Vector3 position, float radius,
        out Vector3 penetrationNormal, out Vector3 penetrationPosition, out float penetrationLength)
    {
        Vector3 colliderProjection = collider.Position - position;

        penetrationNormal = Vector3.Cross(collider.Right, collider.Up);
        penetrationLength = Mathf.Abs(Vector3.Dot(colliderProjection, penetrationNormal)) - (radius / 2f);
        penetrationPosition = collider.Position - colliderProjection;

        return penetrationLength < 0f 
               && Mathf.Abs(Vector3.Dot(colliderProjection, collider.Right)) < collider.Scale.x 
               && Mathf.Abs(Vector3.Dot(colliderProjection, collider.Up)) < collider.Scale.y;
    }

    private Vector3 DampenVelocity(ParticleCollider collider, Vector3 velocity, Vector3 penetrationNormal, float drag)
    {
        Vector3 newVelocity = (Vector3.Dot(velocity, penetrationNormal) * penetrationNormal * damping) +
                              (Vector3.Dot(velocity, collider.Right) * collider.Right * drag) +
                              (Vector3.Dot(velocity, collider.Up) * collider.Up * drag);

        return Vector3.Dot(newVelocity, Vector3.forward) * Vector3.forward +
               Vector3.Dot(newVelocity, Vector3.right) * Vector3.right +
               Vector3.Dot(newVelocity, Vector3.up) * Vector3.up;
    }
}
