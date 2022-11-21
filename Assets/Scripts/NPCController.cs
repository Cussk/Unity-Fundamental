using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public float patrolTime = 10f; //10 second patrol time, ends with f for float
    public float aggroRange = 10f; //10 second aggro time
    public Transform[] waypoints; //array of transform data

    private int index; //what index in array
    private float speed, agentSpeed; //what current speed is and what compnent(agent) speed is
    private Transform player; //transform indicating where player is

    private Animator anim; //links to animations
    private NavMeshAgent agent; //links to navmesh agent

    private void Awake()
    {
        anim = GetComponent<Animator>(); // gets animations if available
        agent = GetComponent<NavMeshAgent>(); //gets navmeshagent if available for movement
        if (agent != null) { agentSpeed = agent.speed; } //if there is an agent set the agentSpeed variable to the designated speed for the agent
        player = GameObject.FindGameObjectWithTag("Player").transform; //looks for player object position
        index = Random.Range(0, waypoints.Length); //random movement betweent 0(start point) and waypints length(end point)

        InvokeRepeating("Tick", 0, 0.5f); // initiates tick function and then repeats tick function every 0.5 seconds

        if(waypoints.Length > 0) //if we have waypoints
        {
            InvokeRepeating("Patrol", 0, patrolTime); //invokes patrol function based off patrolTime araible
        }
    }

    void Patrol()
    {
        index = index == waypoints.Length - 1 ? 0 : index + 1; //index sets waypoint length array to 0, if it is 0 increaes index by 1
    }

    void Tick()
    {
        agent.destination = waypoints[index].position; //everytime agent reaches a waypoint position it will move to the next postiion in the index
        agent.speed = agentSpeed / 2; //when patrolling move at half speed

        if(player != null && Vector3.Distance(transform.position, player.position) < aggroRange) //if the player is within the aggro range of NPC
        {
            agent.destination = player.position; //NPC will move towards player
            agent.speed = agentSpeed; //move toward player at normal speed
        }
    }

    void Update()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude); //updates animation based of Speed of agent when it reaches set magnitude
    }
}
