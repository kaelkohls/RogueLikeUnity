using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casilla2Equipamiento : Casilla2
{
    public Equipo tipoHabilidad;

    protected override void UsarObjetoEnCasilla()
    {
        DesequiparObjeto();
    }
    private void DesequiparObjeto()
    {

        if (Inventario2.instance.AgregarObjeto(itemAlmacenado, 1))
        {
            EliminarObjeto();
        }
    }
    public override void EliminarObjeto()
    {
        PanelEquipamiento2.instance.RemoverEquipo((HabilidadElemental2)itemAlmacenado);
        resetearCasilla();
    }
}
