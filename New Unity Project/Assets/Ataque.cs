using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public LayerMask enemyLayers2;
    public int attackDamage = 40;

    public int attackDamage2 = 40;
    public int attackDamage3 = 40;
    public int attackDamage4 = 40;

    public int extraDamage = 0;
    public int extraDamage2 = 0;
    public int extraDamage3 = 0;
    public int extraDamage4 = 0;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemigo"))
        {
            col.gameObject.GetComponent<ENEMIGO>().TakeDamage(attackDamage);
        }
       // Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        //Collider2D[] hitEnemies2 = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers2);
       // foreach (Collider2D enemy in hitEnemies)
      //  {
       //     enemy.GetComponent<ENEMIGO>().TakeDamage(attackDamage + extraDamage);

      //  }
        // if (col.tag == "Enemigo") col.SendMessage("Atacado");
    }

    // private void OnTriggerStay2D(Collider2D col)
    // {
    //     if (col.tag == "Enemigo") col.SendMessage("Atacado");
    // }
    // void Attack()
    //  {
    //  Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

    //  foreach(Collider2D enemy in hitEnemies)
    //  {
    //     enemy.GetComponent<EnemyControler>().TakeDamage(attackDamage);
    //  }
    //}

    // private void OnDrawGizmosSelected()
    //{
    //if (attackPoint == null)
    //  return;
    //  Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    // }

}
