using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollission : MonoBehaviour
{
  public bool knockDownAttack;
  public float attackStrength;
  GameObject otherObject;
  Stats otherStats;
  MollyControllerScript playerState;


  void OnTriggerEnter(Collider other){
    if(gameObject.tag == "PlayerAttackBox" && other.tag == "EnemyHitbox"){
      EnemyTakeDamage(other.gameObject);
    }else if(gameObject.tag == "EnemyAttackBox" && other.tag == "PlayerHitbox"){
      PlayerTakeDamage(other.gameObject);
    }else{
      // playerState = otherObject.GetComponent<MollyControllerScript>();
      //   playerState.animator.SetBool("isHit", false);
      return;
      }
  }

  void EnemyTakeDamage(GameObject other){
    otherObject = other.transform.parent.gameObject;
    Debug.Log("Enemy takes damage");
    Debug.Log(otherObject);
  }

  void PlayerTakeDamage(GameObject other){
    otherObject = other.transform.parent.gameObject;
    playerState = otherObject.GetComponent<MollyControllerScript>();
    otherStats = otherObject.GetComponent<Stats>();
    otherStats.health = otherStats.health - attackStrength;
    if(knockDownAttack == true){
      playerState.knockedDown = true;
    }else{
      playerState.tookDamage = true;
    }
    Debug.Log("Player takes damage");
    Debug.Log(otherObject);

  }
}
