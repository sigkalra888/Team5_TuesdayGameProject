using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] angleCollider;
    private string[] angleName = new string[4] { "left", "right", "flont", "back"};

    private struct PushFlag
    {
        public string angleName;
        public bool pushFlag;
    }

    PushFlag[] pFlag = new PushFlag[4];

    private bool isPush = false;
    
    void Start()
    {
    }

    void Update()
    {
        RayCheack();
    }

    private void BoxParamInit()
    {
        for (int i = 0; i < 4; i++)
        {
            pFlag[i].angleName = angleName[i];
            pFlag[i].pushFlag = false;
        }
    }

    public void PushBox(int angleIndex)
    {
        
    }

    /// <summary>
    /// レイを使用した周りのオブジェクトの把握処理
    /// </summary>
    private void RayCheack()
    {
        for (int i = 0; i < 4; i++)
        {
            Ray ray = new Ray();
            switch (i)
            {
                case 0:
                    ray = new Ray(angleCollider[i].transform.position, new Vector3(-1, 0, 0));
                    break;
                case 1:
                    ray = new Ray(angleCollider[i].transform.position, new Vector3(1, 0, 0));
                    break;
                case 2:
                    ray = new Ray(angleCollider[i].transform.position, new Vector3(0, 0, 1));
                    break;
                case 3:
                    ray = new Ray(angleCollider[i].transform.position, new Vector3(0, 0, -1));
                    break;
            }
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
        }
    }
}
