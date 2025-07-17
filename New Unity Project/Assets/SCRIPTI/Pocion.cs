using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "ObjetosEscriptables/itemS/Pociones/Salud")]
public class Pocion : Item 
{

    public override bool UsarItem()
    {

        ControladordeJugador saludJugador = GameManager.instance.jugador.GetComponent<ControladordeJugador>();
       if(saludJugador.hp >= saludJugador.maxhp)
        {
            Debug.Log("Salud Llena,no se usara la pocima");
            return false;
            
        }
        else 
        {
            saludJugador.hp = saludJugador.maxhp;
            return true;
        }
    }

}
