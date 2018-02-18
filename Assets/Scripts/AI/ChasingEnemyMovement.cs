using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasingEnemyMovement : MonoBehaviour {

    PlayerController player;
    NavMeshAgent navAgent;
    bool playerInRange;

	void Start ()
    {
        //get a reference to the player
        player = PlayerController.Instance;
        navAgent = GetComponent<NavMeshAgent>();
        playerInRange = false;
	}
	
	void Update ()
    {
        //when the player enters the nav mesh, start chasing them
        var path = new NavMeshPath();
        if(navAgent.CalculatePath(player.transform.position, path))
        {
            navAgent.SetPath(path);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //if the ai contacts the player, kill the player and stop chasing
        if (other.gameObject == player.gameObject)
        {
            playerInRange = true;
            player.Dead(); //note: doesnt have to kill player
            navAgent.ResetPath();
            this.enabled = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }
}
