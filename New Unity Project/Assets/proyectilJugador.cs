using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proyectilJugador : MonoBehaviour
{
    public BoxCollider2D explosion;

    private Animator anim;
    public float actualespera;

    public Transform attackPoint;
    public float attackRange = 0.5f;


    public float speed = 2;
    public int attackDamage = 10;

    GameObject player;
    Rigidbody2D rb2d;
    Vector3 target, dir;

    int direccion = 1;

    // Start is called before the first frame update
    void Start()
    {
        explosion = transform.GetComponent<BoxCollider2D>();
        explosion.enabled = false;
        explosion.isTrigger = true;

        actualespera = 1.5f;
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Jugador");
        rb2d = GetComponent<Rigidbody2D>();

        if (player != null)
        {

            target = player.transform.GetChild(2).position;
            dir = (target - transform.position).normalized;

            if (target.x < transform.position.x) direccion = 1;
            if (target.x > transform.position.x) direccion = -1;
        }
        transform.localScale = new Vector3(1 * direccion, 1, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        actualespera -= Time.deltaTime;
        if (actualespera < 0) Destroy(gameObject);
        if (target != Vector3.zero)
        {
            rb2d.MovePosition(transform.position + (dir * speed) * Time.deltaTime);
        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
         if (col.gameObject.CompareTag("Enemigo") )//&& speed > 0)
        {
            if (speed > 0)
            {
                anim.Play("Explosion");
                transform.position = attackPoint.transform.position;
                actualespera = 1f;
                explosion.enabled = true;

            }
            
            col.gameObject.GetComponent<ENEMIGO>().TakeDamage(attackDamage);      
            speed = 0;
            
        }

        //if (col.tag == "Suelo") Destroy(gameObject);
    }
}
