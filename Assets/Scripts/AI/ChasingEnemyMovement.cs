using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasingEnemyMovement : MonoBehaviour {

    PlayerController player;
    NavMeshAgent navMesh;

	void Start ()
    {
        player = PlayerController.Instance;
        navMesh = GetComponent<NavMeshAgent>();
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
}
