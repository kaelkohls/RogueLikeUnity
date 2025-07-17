using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class estela : MonoBehaviour
{
    

    private int danio = 50;
    GameObject player;
    EdgeCollider2D edgec;
    // Start is called before the first frame update
    void Start()
    {

        edgec = this.GetComponent<EdgeCollider2D>();
        player = GameObject.FindGameObjectWithTag("Jugador");
        //player.GetComponent<ControladordeJugador>().edgeC = this.GetComponent<EdgeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<ControladordeJugador>().trailR.enabled == true)
        {
            edgec.enabled = true;
        }
        else edgec.enabled = false;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemigo"))
        {
            col.gameObject.GetComponent<ENEMIGO>().TakeDamage(danio);
        }

    }


}
