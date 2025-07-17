using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer),typeof(BoxCollider2D))]
public class Objeto : MonoBehaviour
{
    //private Animator anim;

    float distanciadelJugador;
    public GameObject jugador;

    public Item item;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    public int cantidad = 1;

    private void OnValidate()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameObject.name = item.nombre;
        spriteRenderer.sprite = item.artwork;
    }
    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Jugador");
        //anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer.sortingLayerName = "Drop";
        boxCollider.isTrigger = true;
        boxCollider.size = new Vector2(1, 1);
        gameObject.layer = 13;
    }
    void FixedUpdate()
    {
        
        distanciadelJugador = Mathf.Abs(jugador.transform.position.x - transform.position.x);
        if (distanciadelJugador < 2 )// & Input.GetKeyDown(KeyCode.E))
        {

            if (item.habilidad1Pasiva2 < 2)
            {
                //anim.Play("ObjetoTomado");
                if (Inventario.instance.AgregarObjeto(item, cantidad))
                {
                    Destroy(gameObject);
                }
            }
            else if (item.habilidad1Pasiva2 >= 2)
            {
                if (Inventario2.instance.AgregarObjeto(item, cantidad))
                {
                    Destroy(gameObject);
                }
            }

        }


    }
}

//void OnTriggerEnter2D(Collider2D col)
// {
//   if (col.CompareTag("Jugador")) //&& Input.GetKeyDown(KeyCode.E))
//  {

//      if (item.habilidad1Pasiva2 < 2)
//      {
//anim.Play("ObjetoTomado");
//          if (Inventario.instance.AgregarObjeto(item, cantidad))
//      {
//          Destroy(gameObject);
//      }
//      }
//     else if (item.habilidad1Pasiva2 >= 2)
//       {
//           if (Inventario2.instance.AgregarObjeto(item, cantidad))
//           {
//               Destroy(gameObject);
//          }
//       }

//    }

//  }
//}
