using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Equipo
{
  basica,especial,deSalto,definitiva
}
//public enum Elemento
//{
//    fuego, agua, electro, tierra, aire, oscuridad, luz, veneno
//}
[CreateAssetMenu(menuName = "ObjetosEscriptables/items/Habilidades2")]
public class HabilidadElemental2 : Item
{
    // public Equipo tipoHabilidad;
    //  public Elemento tipoElemento;
    public int danioBase;
    public string animacion;

    public float tiempoInicial;
    public float tiempoFinal;

    public int danioBase2;
    public float tiempoInicial2;
    public float tiempoFinal2;

    public int danioBase3;
    public float tiempoInicial3;
    public float tiempoFinal3;

    public int danioBase4;
    public float tiempoInicial4;
    public float tiempoFinal4;

    public int tipohabilidad;


    public override bool UsarItem()
    {
        HabilidadElemental2 equipamientoActualmenteEquipado = PanelEquipamiento2.instance.EquiparObjeto(this);
        PanelEquipamiento2.instance.RemoverEquipo(equipamientoActualmenteEquipado);
        if (equipamientoActualmenteEquipado)
        {
            Inventario2.instance.AgregarObjeto(equipamientoActualmenteEquipado, 1);
        }
        return true;
    }


}

