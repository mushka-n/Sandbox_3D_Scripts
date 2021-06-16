using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class FireProperties : MonoBehaviour
{
    // 0 - none; 1 - burning; 2 - destroyed
    public int state = 0;
    
    public bool canBurn = false;
    public float burningSpeed = 1f;
    public float destroySpeed = 1f;
    public float burnPower = 1f;
    public bool canExplode = false;
    public float explodePower = 0f;
    public GameObject destroyedFBX;
    
    public Renderer defaultRenderer;

    private GameObject Fire;
    private Transform BoxInsides;
    

    private void Start()
    {
        BoxInsides = GameObject.Find("BoxInsides").transform;
    }

    public void Ignite(GameObject GivenFire)
    {
        Fire = GivenFire;
        state = 1;
        if (canBurn) StartCoroutine(WaitForFireSpread());
    }
    
    public void KillFire()
    {
        if (transform.gameObject)
        {
            transform.gameObject.tag = transform.gameObject.tag.Substring(0, transform.gameObject.tag.Length - 8);
            StopAllCoroutines();
            Destroy(transform.GetChild(1).gameObject);
        }
    }
    
    
    IEnumerator WaitForFireSpread()
    {
        float burnSpeedMin = burningSpeed;
        float burnSpeedMax = burningSpeed*5;
        float time = Random.Range(burnSpeedMin, burnSpeedMax);
        yield return new WaitForSeconds(time);
        CastFire();
    }
    
    private void CastFire()
    {
        Instantiate(
            Fire,
            transform.position,
            Quaternion.Euler(transform.rotation.eulerAngles),
            transform
        );
        transform.gameObject.tag = transform.gameObject.tag + "_Burning";
        StartCoroutine(WaitForFireDestroy());
    }
    
    IEnumerator WaitForFireDestroy()
    {
        float destroySpeedMin = destroySpeed*2;
        float destroySpeedMax = destroySpeed*10;
        float time = Random.Range(destroySpeedMin, destroySpeedMax);
        for (int i = 1; i < time; i++)
        {
            Color now = defaultRenderer.material.color;
            Color result = new Color(now.r-now.r/time,now.g-now.g/time,now.b-now.b/time);
            defaultRenderer.material.color = Color.Lerp(now, result, Time.time/time);
            yield return new WaitForSeconds(1f);
        }
        DestroyByFire();
    }

    private void DestroyByFire()
    {
        Destroy(transform.gameObject);
        Instantiate(
            destroyedFBX,
            transform.position,
            Quaternion.Euler(transform.rotation.eulerAngles),
            BoxInsides.transform
        );
    }
}
