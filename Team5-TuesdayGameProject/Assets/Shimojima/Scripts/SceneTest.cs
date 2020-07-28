using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTest : MonoBehaviour
{
    public GameObject[] buttons;
    public int buttonNum = 0;

    [System.Serializable]
    public struct Blocks
    {
        public GameObject block1;
        public GameObject block2;
    }

    public Blocks[] b;

    private void Start()
    {
    }

    private void Update()
    {
        ChangeBlockTemp();
        PushButton();
        CountUpDown();
    }

    private void ChangeBlockTemp()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            BlockManager.Instance.changeBlocks[0] = b[0].block1;
            BlockManager.Instance.changeBlocks[1] = b[0].block2;
            GameObject o = new GameObject();
            BlockManager.Instance.SetBlock(o);
            Destroy(o);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            BlockManager.Instance.changeBlocks[0] = b[1].block1;
            BlockManager.Instance.changeBlocks[1] = b[1].block2;
            GameObject o = new GameObject();
            BlockManager.Instance.SetBlock(o);
            Destroy(o);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            BlockManager.Instance.changeBlocks[0] = b[2].block1;
            BlockManager.Instance.changeBlocks[1] = b[2].block2;
            GameObject o = new GameObject();
            BlockManager.Instance.SetBlock(o);
            Destroy(o); BlockManager.Instance.SetBlock(b[2].block2);
        }
    }

    private void PushButton()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (!buttons[buttonNum].GetComponent<PressButton>().isPress)
            {
                buttons[buttonNum].GetComponent<PressButton>().Press();
            }
            else
            {
                buttons[buttonNum].GetComponent<PressButton>().Return();
            }
        }
    }

    private void CountUpDown()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            buttonNum++;
            if (buttonNum > buttons.Length - 1) { buttonNum = 0; }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            buttonNum--;
            if (buttonNum < 0) { buttonNum = buttons.Length - 1; }
        }
    }
}
