using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : Interactable
{
    Color red = Color.red;
    Color blue = Color.blue;
    Renderer rend;

    public override void INIT()
    {
        rend = GetComponent<Renderer>();
        changeColor();
    }
    public override void Effect()
    {
        base.Effect();
        changeColor();
    }
    public override void Ripristinate()
    {
        base.Ripristinate();
        changeColor();
    }

    void changeColor()
    {
        switch (currentElement)
        {
            case Element.cold:
                rend.material.color = blue;
                break;
            case Element.hot:
                rend.material.color = red;
                break;
        }
    }
}
