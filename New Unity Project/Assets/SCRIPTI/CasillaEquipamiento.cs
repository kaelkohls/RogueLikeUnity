using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasillaEquipamiento : Casilla
{
    //public Equipo tipoHabilidad;

    protected override void UsarObjetoEnCasilla()
    {
        DesequiparObjeto();
    }
    private void DesequiparObjeto()
    {

        if (Inventario.instance.AgregarObjeto(itemAlmacenado, 1))
        {
            EliminarObjeto();
        }
    }
    public override void EliminarObjeto()
    {
        PanelEquipamiento.instance.RemoverEquipo((HabilidadElemental)itemAlmacenado);
        resetearCasilla();
    }
}
