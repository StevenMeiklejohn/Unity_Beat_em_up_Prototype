using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{

  UnityEngine.AI.NavMeshAgent navMeshAgent;
  EnemySight enemySight;
  EnemyWalk enemyWalk;
  EnemyAttack enemyAttack;


  public enum currentStateEnum {idle=0, walk=1, attack=2};
  public currentStateEnum currentState;
  public GameObject spriteObject;

  // Animation state Machine
  Animator animator;
  AnimatorStateInfo currentStateInfo;
  static int currentAnimState;
  static int idleState = Animator.StringToHash("Base Layer.Idle");
  static int walkState = Animator.StringToHash("Base Layer.Walk");
  static int runState = Animator.StringToHash("Base Layer.Run");
  static int jumpState = Animator.StringToHash("Base Layer.Jump");
  static int takeDamageState = Animator.StringToHash("Base Layer.TakeDamage");
  static int attack1State = Animator.StringToHash("Base Layer.Attack1");
  static int attack2State = Animator.StringToHash("Base Layer.Attack2");
  static int attack3State = Animator.StringToHash("Base Layer.Attack3");
  static int blockState = Animator.StringToHash("Base Layer.Block");
  static int FallState = Animator.StringToHash("Base Layer.Fall");


    void Awake(){
      navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
      enemySight = GetComponent<EnemySight>();
      enemyAttack = GetComponent<EnemyAttack>();
      animator = spriteObject.GetComponent<Animator>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      // Debug.Log(navMeshAgent.velocity.sqrMagnitude);

      if(enemySight.playerInSight == false && enemySight.targetDistance > enemyAttack.attackRange){
        Debug.Log("not inSight");
        animator.SetBool("Walk", false);
        animator.SetBool("Attack", false);
      }

      if(enemySight.playerInSight == true && enemySight.targetDistance > enemyAttack.attackRange){
        Debug.Log("inSight only");
        animator.SetBool("Walk", true);
        animator.SetBool("Attack", false);
      }
      if(enemySight.playerInSight == true && enemySight.targetDistance < enemyAttack.attackRange){
        Debug.Log("Attacking");
        animator.SetBool("Walk", false);
        animator.SetBool("Attack", true);
      }


      // if(enemySight.playerInSight == true && enemySight.targetDistance < enemyAttack.attackRange){
      //   Debug.Log("Attacking");
      //   animator.SetBool("Walk", false);
      //   animator.SetBool("Attack", true);
      // }else if(enemySight.playerInSight == true){
      //   Debug.Log("inSight only");
      //   animator.SetBool("Walk", true);
      //   animator.SetBool("Attack", false);
      // }else if(enemySight.playerInSight == false){
      //   Debug.Log("not inSight");
      //   animator.SetBool("Walk", false);
      //   animator.SetBool("Attack", false);
      // }

      if(currentAnimState == idleState){
        currentState = currentStateEnum.idle;
      }
      if(currentAnimState == walkState){
        currentState = currentStateEnum.walk;
      }
      if(currentAnimState == attack1State){
        currentState = currentStateEnum.attack;
      }
      currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);
      currentAnimState = currentStateInfo.fullPathHash;

    }
}
