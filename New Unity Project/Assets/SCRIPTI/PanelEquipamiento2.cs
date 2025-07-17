using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelEquipamiento2 : MonoBehaviour
{
    private int casillaVacia = 0;
    public bool inventarioLleno;

    public static PanelEquipamiento2 instance;
    public Casilla2Equipamiento[] casillaEquipamientos;
    public List<HabilidadElemental2> habilidades = new List<HabilidadElemental2>();

    public void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        casillaEquipamientos = GetComponentsInChildren<Casilla2Equipamiento>();
    }
    void DeterminarSiguienteCasillaVacia()
    {
        casillaVacia = 0;
        foreach (Casilla2 casilla in casillaEquipamientos)
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
    public HabilidadElemental2 EquiparObjeto(HabilidadElemental2 habilidadElemental)
    {
        DeterminarSiguienteCasillaVacia();
        foreach (Casilla2Equipamiento casillaEquipo in casillaEquipamientos)
        {
            //if(habilidadElemental.tipoHabilidad == casillaEquipo.tipoHabilidad)
            // {
            if (!casillaEquipo.itemAlmacenado && !inventarioLleno)
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
    private void AgregarEquipo(HabilidadElemental2 habilidadElemental, Casilla2Equipamiento casillaEquipo)
    {
        casillaEquipo.AddObjeto(habilidadElemental, 1);
        habilidades.Add(habilidadElemental);
        GameManager.instance.jugador.GetComponent<ControladordeJugador>().ActualizarEquipamiento2(habilidades);
    }
    public void RemoverEquipo(HabilidadElemental2 habilidadElemental)
    {
        habilidades.Remove(habilidadElemental);
        GameManager.instance.jugador.GetComponent<ControladordeJugador>().ActualizarEquipamiento2(habilidades);
    }
}