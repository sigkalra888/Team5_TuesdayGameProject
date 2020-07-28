using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : Singleton<BlockManager>
{
    public GameObject[] changeBlocks = new GameObject[2];
   

    //ブロックの温度交換
    public void ChangeBlock(GameObject block)
    {
        if (changeBlocks[1] == null)
        {
            if (changeBlocks[0] == null) { changeBlocks[0] = block; }
            else { changeBlocks[1] = block; }
        }
        else
        {
            string tmp = changeBlocks[1].GetComponent<Block>().temperature;
            changeBlocks[1].GetComponent<Block>().temperature = changeBlocks[0].GetComponent<Block>().temperature;
            changeBlocks[0].GetComponent<Block>().temperature = tmp;
            
            for (int i = 0; i < changeBlocks.Length; i++)
            {
                changeBlocks[i].GetComponent<Block>().BlockColorChange();
                changeBlocks[i].GetComponent<Block>().changeTemperature = true;
                changeBlocks[i] = null;
            }
        }
    }
}
