using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//直接に状態にを変更します
public interface Imanipulate
{
    void Change(Element e);
    void ComeBack();
    
}
//温度変更するため
public interface ICelsius
{
    void Change(int grade);
}