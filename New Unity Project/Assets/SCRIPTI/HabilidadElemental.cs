using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum Equipo
//{
  //  basica,especial,deSalto,definitiva
//}
//public enum Elemento
//{
//    fuego, agua, electro, tierra, aire, oscuridad, luz, veneno
//}
[CreateAssetMenu(menuName ="ObjetosEscriptables/items/Habilidades")]
public class HabilidadElemental : Item
{
       // public Equipo tipoHabilidad;
      //  public Elemento tipoElemento;
        public int danio;
        public int salud;
        public bool fireball;
        public bool estela;
        public int money;
    public int resistenciaFuego;
    public int resistenciaRayo;
    public int resistenciaAgua;
    public int resistenciaVeneno;


    //public float tiempoInicial;
    //public float tiempoFinal;

       // public int tipohabilidad;

    
    public override bool UsarItem()
    {
        HabilidadElemental equipamientoActualmenteEquipado = PanelEquipamiento.instance.EquiparObjeto(this);
        PanelEquipamiento.instance.RemoverEquipo(equipamientoActualmenteEquipado);
        if(equipamientoActualmenteEquipado)
        {
            Inventario.instance.AgregarObjeto(equipamientoActualmenteEquipado, 1); 
        }
        return true;
    }
 

}
