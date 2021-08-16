using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWalk : MonoBehaviour
{

  NavMeshAgent navMeshAgent;
  EnemySight enemySight;
  Animator animator;
  public float enemyCurrentSpeed;
  public float enemySpeed;
  public GameObject spriteObject;



    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemySight = GetComponent<EnemySight>();
        animator = spriteObject.GetComponent<Animator>();

        navMeshAgent.speed = enemySpeed;
    }


    void Update()
    {
      if(enemySight.playerInSight == true){
        navMeshAgent.SetDestination(enemySight.target.transform.position);
        navMeshAgent.updateRotation = false;
        animator.SetBool("Walk", true);

        if(enemySight.targetDistance < 1.0f){
          animator.SetBool("Walk", false);
        }
      }
    }
}
