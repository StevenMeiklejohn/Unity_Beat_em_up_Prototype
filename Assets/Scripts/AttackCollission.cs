using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollission : MonoBehaviour
{
  public bool knockDownAttack;
  public float attackStrength;
  GameObject otherObject;
  MollyControllerScript playerState;


  void OnTriggerEnter(Collider other){
    if(gameObject.tag == "PlayerAttackBox" && other.tag == "EnemyHitbox"){
      EnemyTakeDamage(other.gameObject);
    }else if(gameObject.tag == "EnemyAttackBox" && other.tag == "PlayerHitbox"){
      PlayerTakeDamage(other.gameObject);
    }else return;
  }

  void EnemyTakeDamage(GameObject other){
    Debug.Log("Enemy takes damage");
  }

  void PlayerTakeDamage(GameObject other){
    otherObject = other.transform.parent.gameObject;
    Debug.Log("Player takes damage");
    Debug.Log(otherObject);

  }
}
