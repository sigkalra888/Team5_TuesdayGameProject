using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorS : MonoBehaviour
{
    public MeshRenderer mesh;
    private Material m;
    public GameObject msg;
    public GameObject button;

    private Color black;

    void Start()
    {
        m = mesh.material;
        black = m.color;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            if (button.activeSelf)
            {
                button.SetActive(false);
            }
            else
            {
                button.SetActive(true);
            }
        }
    }

    public void RandomColor()
    {
        if (m.color != Color.red)
        {
            m.color = Color.red;
        }
        else
        {
            m.color = black;
        }
    }

}
