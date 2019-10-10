using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmos : MonoBehaviour
{
    public Color _color = Color.yellow;
    public float _radius = 0.1f;

    //해당 스크립트를 포함한 오브젝트가 있을때 실행
    private void OnDrawGizmos()
    {
        Gizmos.color = _color;
        //위치,범위
        Gizmos.DrawSphere(this.transform.position, _radius);
    }
}
