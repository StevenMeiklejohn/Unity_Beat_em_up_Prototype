using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWalk : MonoBehaviour
{

  NavMeshAgent navMeshAgent;
  EnemySight enemySight;
  public float enemyCurrentSpeed;
  public float enemySpeed;


    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemySight = GetComponent<EnemySight>();

        navMeshAgent.speed = enemySpeed;
    }


    void Update()
    {
      if(enemySight.playerInSight == true){
        navMeshAgent.SetDestination(enemySight.target.transform.position);
        navMeshAgent.updateRotation = false;
      }
    }
}
