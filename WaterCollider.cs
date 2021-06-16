using Obi;
using UnityEngine;

[RequireComponent(typeof(ObiSolver))]
public class WaterCollider : MonoBehaviour
{
    ObiSolver solver;
    public ObiEmitter WaterEmitter;
    ObiSolver.ObiCollisionEventArgs collisionEvent;

    void Awake(){
        solver = GetComponent<ObiSolver>();
    }

    void OnEnable () {
        solver.OnCollision += Solver_OnCollision;
    }

    void OnDisable(){
        solver.OnCollision -= Solver_OnCollision;
    }

    void Solver_OnCollision (object sender, Obi.ObiSolver.ObiCollisionEventArgs e)
    {
        var world = ObiColliderWorld.GetInstance();

        // just iterate over all contacts in the current frame:
        foreach (Oni.Contact contact in e.contacts)
        {
            // if this one is an actual collision:
            if (contact.distance < 0.01)
            {
                ObiColliderBase col = world.colliderHandles[contact.bodyB].owner;
                if (col && col.gameObject.CompareTag("Solid_Burning"))
                {
                    int particleIndex = solver.simplices[contact.bodyA];
                    WaterEmitter.life[particleIndex] = 0;
                    col.GetComponent<FireProperties>().KillFire();
                    
                }
            }
        }
    }
}
