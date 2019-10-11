using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateTest : MonoBehaviour
{
    public delegate  void CalNumDelegate(int num);

    CalNumDelegate calNum;

    private void Start()
    {
        //함수연결
        calNum = OnePlusNum;

        //함수 호출
        calNum(4);

        calNum = PowerNum;

        calNum(5);
    }

    void OnePlusNum(int num)
    {
        int result = num + 1;
        Debug.Log("One Plus =" + result);
    }

    void PowerNum(int num)
    {
        int result = num * num;
        Debug.Log("Power = " + result);
    }
}
