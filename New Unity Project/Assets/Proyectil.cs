using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public float speed = 2;
    public int attackDamage = 10;

    GameObject player;
    Rigidbody2D rb2d;
    Vector3 target,dir;

    int direccion = 1;

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Jugador");
        rb2d = GetComponent<Rigidbody2D>();

        if (player != null)
        {
            target = player.transform.position;
            dir = (target - transform.position).normalized;

            if (target.x < transform.position.x) direccion = 1;
            if (target.x > transform.position.x) direccion = -1;
        }
        transform.localScale = new Vector3(1 * direccion, 1, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       if (target != Vector3.zero)
        {
            rb2d.MovePosition(transform.position + (dir * speed) * Time.deltaTime);
        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.CompareTag("Jugador"))
        {
            col.gameObject.GetComponent<ControladordeJugador>().TakeDamage(attackDamage);
        }

        else if (col.tag == "Suelo") Destroy(gameObject);
    }
}
