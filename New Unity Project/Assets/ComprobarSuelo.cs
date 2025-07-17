using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComprobarSuelo : MonoBehaviour
{
    private ControladordeJugador player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<ControladordeJugador>();
        
    }

     void OnCollisionStay2D(Collision2D colision)
    {
        if(colision.gameObject.tag == "Suelo") { 
        player.estoyenSuelo = true;
        }
        if (colision.gameObject.tag == "Plataforma")
        {
            player.transform.parent = colision.transform;
            player.estoyenSuelo = true;
        }
    }
     void OnCollisionExit2D(Collision2D colision1)
    {
        if (colision1.gameObject.tag == "Suelo")
        {
            player.estoyenSuelo = false;
        }
        if (colision1.gameObject.tag == "Plataforma")
        {
            player.transform.parent = null;
            player.estoyenSuelo = false;
        }
    }

}
