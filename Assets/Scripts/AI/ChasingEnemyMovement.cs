using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasingEnemyMovement : MonoBehaviour {

    PlayerController player;
    NavMeshAgent navMesh;
    bool playerInRange;

	void Start ()
    {
        player = PlayerController.Instance;
        navMesh = GetComponent<NavMeshAgent>();
        playerInRange = false;
	}
	
	void Update ()
    {
        //when the player enters the nav mesh, start chasing them
        var path = new NavMeshPath();
        if(navMesh.CalculatePath(player.transform.position, path))
        {
            navMesh.SetPath(path);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            playerInRange = true;
            player.Dead();
            navMesh.ResetPath();
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
