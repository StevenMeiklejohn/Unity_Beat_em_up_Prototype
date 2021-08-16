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
  bool facingRight;
    EnemyState enemyState;



    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemySight = GetComponent<EnemySight>();
        animator = spriteObject.GetComponent<Animator>();
        enemyState = GetComponent<EnemyState>();

        navMeshAgent.speed = enemySpeed;
    }


    void Update()
    {
      if(enemyState.currentState == EnemyState.currentStateEnum.walk){
        Walk();
      }else if(enemyState.currentState == EnemyState.currentStateEnum.idle){
        Stop();
      }
      // if(enemySight.playerInSight == true){
      //
      //   animator.SetBool("Walk", true);
      //
      //   if(enemySight.targetDistance < 0.6f){
      //     animator.SetBool("Walk", false);
      //   }
      // }

    }

    void Walk(){
      if(enemySight.playerOnRight == true && facingRight){
        Flip();
      }else if(enemySight.playerOnRight != true && !facingRight){
        Flip();
      }
      navMeshAgent.speed = enemySpeed;
      enemyCurrentSpeed = navMeshAgent.velocity.sqrMagnitude;
      navMeshAgent.SetDestination(enemySight.target.transform.position);
      navMeshAgent.updateRotation = false;
    }

    void Stop(){
      navMeshAgent.ResetPath();
    }

    void Flip(){
      facingRight = !facingRight;
      Vector3 thisScale = transform.localScale;
      thisScale.x *= -1;
      transform.localScale = thisScale;
    }
}
