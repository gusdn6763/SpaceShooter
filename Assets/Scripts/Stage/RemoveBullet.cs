using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    public GameObject sparkEffect;

    private void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == "BULLET")
        {
            Instantiate(sparkEffect, col.gameObject.transform.position, col.gameObject.transform.rotation);
            Destroy(col.gameObject);
        }
    }



}
