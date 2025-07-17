using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Casilla2 : MonoBehaviour, IPointerDownHandler
{
    public int cantidadStock;
    public Item itemAlmacenado;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (itemAlmacenado == null)
        {
            image.enabled = false;
        }
    }
    public void AddObjeto(Item item, int cantidad)
    {
        itemAlmacenado = item;
        image.enabled = true;
        image.sprite = item.artwork;
        cantidadStock = cantidad;
        gameObject.name = itemAlmacenado.name;
    }

    public virtual void EliminarObjeto()
    {
        Inventario2.instance.RemoverObjeto(itemAlmacenado);
        resetearCasilla();
    }
    protected void resetearCasilla()
    {
        image.sprite = null;
        cantidadStock = 0;
        image.enabled = false;
        itemAlmacenado = null;
    }

    protected virtual void UsarObjetoEnCasilla()
    {
        if (itemAlmacenado)
        {
            if (itemAlmacenado.UsarItem())
            {
                ReducirStock(1);
            }
        }
    }

    void ReducirStock(int cantidade)
    {
        cantidadStock -= cantidade;
        if (cantidadStock <= 0)
        {
            EliminarObjeto();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        UsarObjetoEnCasilla();
    }

}