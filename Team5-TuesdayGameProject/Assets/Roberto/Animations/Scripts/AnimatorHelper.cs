using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHelper : MonoBehaviour
{
    Locomotion player;
    private void Start()
    {
        player = GetComponentInParent<Locomotion>();
    }
    public void SetPosition()
    {
        player.setTheTopPos();
    }
}
