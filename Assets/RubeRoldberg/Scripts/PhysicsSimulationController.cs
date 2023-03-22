using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsSimulationController : MonoBehaviour
{
    private void Awake()
    {
        // DON'T simulate physics unless I tell you to!
        Physics.autoSimulation = false;
    }

    private void FixedUpdate()
    {
        // Step the physics simulation one frame forward, consuming "delta time" amount of time.
        Physics.Simulate(Time.fixedDeltaTime);
    }
}
