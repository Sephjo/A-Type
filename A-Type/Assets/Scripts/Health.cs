using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

  public float healthPoints = 100;
  
  public void reciveDamage(float damage)
  {
    healthPoints -= damage;
    if (healthPoints <= 0)
    {
      die();
    }
  }

  void die()
  {
    
    
    Destroy(gameObject);
  }
}
