using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//player movement by click
public class PlayerController : MonoBehaviour
{
    private Animator anim; //links to animations
    private NavMeshAgent agent; //links to navmesh agent
    
	// Use this for initialization
	void Awake ()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
	}

    void Update()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude); //updates animation based of Speed of agent when it reaches set magnitude
    }
}
