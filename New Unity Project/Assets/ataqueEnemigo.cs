using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ataqueEnemigo : MonoBehaviour
{
    public int attackDamage = 100;
    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.CompareTag("Jugador"))
        {
            col.gameObject.GetComponent<ControladordeJugador>().TakeDamage(attackDamage);
        }

    }
}
