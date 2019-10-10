using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    public GameObject bullet;

    public Transform firePos;

    public ParticleSystem cartridge;

    public ParticleSystem muzzleFlash;


    //선언한 클래스안의 있는 파일 인스펙터로
    [System.Serializable]
    public class PlayerSfx
    {
        public AudioClip[] fire;
        public AudioClip[] reload;
    }

    public enum WeaponType
    {
        RIPLE=0,
        SHOTGUN
    }

    public WeaponType currWeapon = WeaponType.RIPLE;

    private AudioSource _audio;

    public PlayerSfx playerSfx;

    private Shake shake;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        shake = GameObject.Find("CameraRig").GetComponent<Shake>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }


    void Fire()
    {
        StartCoroutine(shake.ShakeCamera());
        Instantiate(bullet, firePos.position, firePos.rotation);
        cartridge.Play();
        muzzleFlash.Play();
        FireSfx();
    }

    void FireSfx()
    {
        var _sfx = playerSfx.fire[(int)currWeapon];

        //적용할 사운드,볼륨 스케일
        _audio.PlayOneShot(_sfx, 1.0f);
    }
}
