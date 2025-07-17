using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelEquipamiento : MonoBehaviour
{
    private int casillaVacia = 0;
    public bool inventarioLleno;

    public static PanelEquipamiento instance;
    public CasillaEquipamiento[] casillaEquipamientos;
    public List<HabilidadElemental> habilidades = new List<HabilidadElemental>();

    public void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        casillaEquipamientos = GetComponentsInChildren<CasillaEquipamiento>();
    }
    void DeterminarSiguienteCasillaVacia()
     {
         casillaVacia = 0;
         foreach (Casilla casilla in casillaEquipamientos)
          {
     if (casilla.itemAlmacenado)
              {
      casillaVacia++;
    }
              else
            {
                break;
            }
    }
    if (casillaVacia >= casillaEquipamientos.Length)
          {
      inventarioLleno = true;
    }
    }
    public HabilidadElemental EquiparObjeto(HabilidadElemental habilidadElemental)
    {
         DeterminarSiguienteCasillaVacia();
        foreach (CasillaEquipamiento casillaEquipo in casillaEquipamientos)
        {
            //if(habilidadElemental.tipoHabilidad == casillaEquipo.tipoHabilidad)
           // {
                if(!casillaEquipo.itemAlmacenado && !inventarioLleno)
                {
                    Debug.Log("Casilla está vacia");
                    AgregarEquipo(habilidadElemental, casillaEquipo);
                    return null;
                }
                //else
              //  {
                //    HabilidadElemental objetoEquipado = casillaEquipo.itemAlmacenado as HabilidadElemental;
               //     AgregarEquipo(habilidadElemental, casillaEquipo);
                 //   return objetoEquipado;
              //  }
           // }
        }
        return null;
    }
    private void AgregarEquipo(HabilidadElemental habilidadElemental, CasillaEquipamiento casillaEquipo)
    {
        casillaEquipo.AddObjeto(habilidadElemental, 1);
        habilidades.Add(habilidadElemental);
        GameManager.instance.jugador.GetComponent<ControladordeJugador>().ActualizarEquipamiento(habilidades);
        if(GameManager.instance.jugador.GetComponent<ControladordeJugador>().hp > GameManager.instance.jugador.GetComponent<ControladordeJugador>().maxhp)
        {
            GameManager.instance.jugador.GetComponent<ControladordeJugador>().hp = GameManager.instance.jugador.GetComponent<ControladordeJugador>().maxhp;
        }
    }
    public void RemoverEquipo(HabilidadElemental habilidadElemental)
    {
        habilidades.Remove(habilidadElemental);
        GameManager.instance.jugador.GetComponent<ControladordeJugador>().ActualizarEquipamiento(habilidades);
        if (GameManager.instance.jugador.GetComponent<ControladordeJugador>().hp > GameManager.instance.jugador.GetComponent<ControladordeJugador>().maxhp)
        {
            GameManager.instance.jugador.GetComponent<ControladordeJugador>().hp = GameManager.instance.jugador.GetComponent<ControladordeJugador>().maxhp;
        }
    }
}
