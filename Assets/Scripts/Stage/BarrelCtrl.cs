using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    //폭발 오브젝트 넣기
    public GameObject expEffect;

    private int hitCount = 0;

    private Rigidbody rb;

    private Transform trs;


    public Mesh[] meshes;

    private MeshFilter meshFillter;

    public Texture[] textures;

    private MeshRenderer _randerer;

    //폭발할 통의 반경
    public float expRadius = 10.0f;

    private AudioSource _audio;

    public AudioClip expSfx;

    public Shake shake;

    void Start()
    {
        _audio = GetComponent<AudioSource>();
        trs = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        meshFillter = GetComponent<MeshFilter>();
        _randerer = GetComponent<MeshRenderer>();
        _randerer.material.mainTexture = textures[Random.Range(0, textures.Length)];

        shake = GameObject.Find("CameraRig").GetComponent<Shake>();
    }

    // Update is called once per frame

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("BULLET"))
        {
            if(++hitCount==3)
            {
                ExpBarrel();
                rb.mass = 30f;
            }
        }
    }


    void ExpBarrel()
    {
        Instantiate(expEffect, trs.position, Quaternion.identity);

        IndirectDamage(trs.position);

        int idx = Random.Range(0, meshes.Length);

        // 찌그러진 메쉬 적용
        meshFillter.sharedMesh = meshes[idx];

        //메쉬 랜덤적용
        GetComponent<MeshCollider>().sharedMesh = meshes[idx];

        _audio.PlayOneShot(expSfx, 1.0f);

        StartCoroutine(shake.ShakeCamera(1,0.2f,0.3f));

    }

    void IndirectDamage(Vector3 pos)
    {
        //현재 위치 부터 ~, ~반경, 레이어 숫자
        //overlap 원형부터
        Collider[] colls = Physics.OverlapSphere(pos, expRadius, 1 << 9 | 1<< 8);

        foreach(Collider coll in colls)
        {
            var _rb = coll.GetComponent<Rigidbody>();
            if (coll.gameObject.tag == "Barrel")
            {
                _rb.mass = 1.0f;
            }
            _rb.AddExplosionForce(1200.0f, pos, expRadius, 1000.0f);

            if (coll.gameObject.tag =="Enemy")
            {
                coll.gameObject.GetComponent<EnemyAI>().state = EnemyAI.State.DIE;
            }
        }
    }
}
