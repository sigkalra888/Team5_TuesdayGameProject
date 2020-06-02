using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Imanipulate
{
    void Change(Element e);
    void ComeBack();
    
}

public interface ICelsius
{
    void Change(int grade);
}