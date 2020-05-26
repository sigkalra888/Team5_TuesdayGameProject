using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour,Imanipulate
{
    [SerializeField]
    protected Element startElement = Element.cold;
    protected Element currentElement;
    [SerializeField]
    float delay = 5;
    [SerializeField]
    float counter = 0;
    bool go = false;
    
    // Start is called before the first frame update
    void Start()
    {
        currentElement = startElement;
        INIT();
    }

    // Update is called once per frame
    void Update()
    {
        TICK();
    }
    #region INIT/UPDATE
    public virtual void INIT() { }
    public virtual void TICK()
    {
        timerUpdate();
    }
    #endregion

    #region TIMER
    void timerUpdate()
    {
        if (!go) return;
        counter += Time.deltaTime;
        if (counter >= delay)
        {
            go = false;
            ComeBack();
        }
    }
    void timerStart()
    {
        counter = 0;
        go = true;
    }
    #endregion

    #region CHANGE
    //change in new state and active timer
    public void Change(Element e)
    {
        
        currentElement = e;
        timerStart();
        Effect();
    }

    //return the first state
    public void ComeBack()
    {
        currentElement = startElement;
        Ripristinate();
    }
    #endregion

    #region PHYSICALLY CHANGE
    //change condition when change the temperature
    public virtual void Effect()
    {

    }
    //return to the standard conditions
    public virtual void Ripristinate()
    {

    }
    #endregion

}

public enum Element
{
    cold, hot
}