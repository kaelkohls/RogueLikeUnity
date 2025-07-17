using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palanca1 : MonoBehaviour
{
    float distanciadelJugador;
    public GameObject jugador;

    private Animator anim;
   // private float tiempo = 0;
    public GameObject objetointeract;
    void Start()
    {
      jugador = GameObject.FindGameObjectWithTag("Jugador");
        // tiempo -= Time.deltaTime;
        anim = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        distanciadelJugador = Mathf.Abs(jugador.transform.position.x - transform.position.x);
        if (distanciadelJugador < 2)
        {
            if ( Input.GetKeyDown(KeyCode.E) & objetointeract.GetComponent<plataforma2>().transform.position == objetointeract.GetComponent<plataforma2>().StartPoint.position)
            {
                objetointeract.GetComponent<plataforma2>().Activacion(1);
                anim.Play("Palanactiva");
                anim.Play("Palaninactiva");
                // tiempo = 1;
            }

            if (Input.GetKeyDown(KeyCode.E) & objetointeract.GetComponent<plataforma2>().transform.position == objetointeract.GetComponent<plataforma2>().EndPoint.position)
            {
                objetointeract.GetComponent<plataforma2>().Activacion(2);
                anim.Play("Palanactiva");
                anim.Play("Palaninactiva");
                // tiempo = 1;
            }
        }
    }
   // void OnTriggerStay2D(Collider2D col)
    //{
       // if (col.tag == "Jugador" & Input.GetKeyDown(KeyCode.E) & objetointeract.GetComponent<plataforma2>().transform.position == objetointeract.GetComponent<plataforma2>().StartPoint.position)
      //  {
       //     objetointeract.GetComponent<plataforma2>().Activacion(1);
      //      anim.Play("Palanactiva");
            // tiempo = 1;
      //  }

      //  if (col.tag == "Jugador" & Input.GetKeyDown(KeyCode.E) & objetointeract.GetComponent<plataforma2>().transform.position == objetointeract.GetComponent<plataforma2>().EndPoint.position)
       // {
      //      objetointeract.GetComponent<plataforma2>().Activacion(2);
       //     anim.Play("Palanactiva");
            // tiempo = 1;
     //   }

  //  }

    void OnGUI()
    {
        Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);

        GUI.Box(
            new Rect(
                pos.x,
                Screen.height - pos.y - 150,
                130,
                96
            ), "Presione la tecla E"
            );
    }
}
