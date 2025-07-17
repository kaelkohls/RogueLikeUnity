using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("ObjetosEscriptables/Items/ItemGenerico"))]
public class Item : ScriptableObject
{
    public int  habilidad1Pasiva2;

    public Sprite artwork;
    public string nombre;
    public bool apilable;
    [TextArea(1, 3)]
    public string descripcion;
    public virtual bool UsarItem()
    {
        Debug.Log("Utilizando" + nombre);
        return true;
    }
}
