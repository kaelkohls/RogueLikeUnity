using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario2 : MonoBehaviour
{
    public bool inventarioLleno;
    public static Inventario2 instance;
    private Casilla2[] casillas;
    private List<Item> objetos = new List<Item>();
    private int casillaVacia = 0;

    private void Awake()
    {
        instance = this;
        casillas = GetComponentsInChildren<Casilla2>();
    }
    void DeterminarSiguienteCasillaVacia()
    {
        casillaVacia = 0;
        foreach (Casilla2 casilla in casillas)
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
        if (casillaVacia >= casillas.Length)
        {
            inventarioLleno = true;
        }
    }
    public bool AgregarObjeto(Item item, int cantidad)
    {
        //El Inventario está lleno? el objeto a agregar es apilable?si es apilable,tengo una copia de este en mi inventario?
        DeterminarSiguienteCasillaVacia();
        if ((item.apilable == true && !objetos.Contains(item) && !inventarioLleno) || (!item.apilable && !inventarioLleno))
        {
            // Nuestro item es apilable y no tenemos copia de el o nuestrio objeto no es apilable(tenemos espacio en el inventario)
            Casilla2 casillaAdd = casillas[casillaVacia];
            objetos.Add(item);
            casillaAdd.AddObjeto(item, cantidad);
            return true;
        }
        else if (item.apilable == true && objetos.Contains(item))
        {
            //Nuestro objeto es apilable y tenemos una copia de el en alguna casilla
            for (int i = 0; i < casillas.Length; i++)
            {
                if (item == casillas[i].itemAlmacenado)
                {
                    casillas[i].cantidadStock += cantidad;
                    break;
                }
            }
            return true;
        }
        else
        {
            Debug.Log("InventarioLleno");
            return false;
        }
    }
    public void RemoverObjeto(Item item)
    {
        objetos.Remove(item);
    }
}
