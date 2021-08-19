using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

  public float attackStartDelay;
  public float attackRange;
  public GameObject spriteObject;
  public GameObject attack1Box, attack2Box, attack3Box;

  public Sprite currentSprite;
  public Sprite attack1SpriteHitFrame, attack2SpriteHitFrame, attack3SpriteHitFrame;



  UnityEngine.AI.NavMeshAgent navMeshAgent;
  EnemySight enemySight;
  EnemyWalk enemyWalk;
  Animator animator;
  EnemyState enemyState;
    // Start is called before the first frame update


    void Awake(){
      navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
      enemySight = GetComponent<EnemySight>();
      enemyWalk = GetComponent<EnemyWalk>();
      animator = spriteObject.GetComponent<Animator>();
      enemyState = GetComponent<EnemyState>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      currentSprite = spriteObject.GetComponent<SpriteRenderer>().sprite;
      if(enemyState.currentState == EnemyState.currentStateEnum.attack){
        Attack();
      }
      // if(enemySight.playerInSight == true && enemySight.targetDistance < attackRange){
      //
      //
      //
      // }
    }

    void Attack(){
      navMeshAgent.ResetPath();
      if(attack1SpriteHitFrame == currentSprite){
        attack1Box.gameObject.SetActive(true);
      }else{
        attack1Box.gameObject.SetActive(false);
      }
      if(attack2SpriteHitFrame == currentSprite){
        attack2Box.gameObject.SetActive(true);
      }else{
        attack2Box.gameObject.SetActive(false);
      }
      if(attack3SpriteHitFrame == currentSprite){
        attack3Box.gameObject.SetActive(true);
      }else{
        attack3Box.gameObject.SetActive(false);
      }

    }

}
