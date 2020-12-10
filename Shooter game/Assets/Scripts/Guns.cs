using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public Transform muzzleflashPosition;
    public float impactForce = 30;

    public GameObject muzzleflash;
    public ParticleSystem impactEffect;
    public Camera FPScamera;

    private float nextTimeTofire = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1")&& Time.time>=nextTimeTofire)
        {
            nextTimeTofire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject shootEffect = Instantiate(muzzleflash, muzzleflashPosition);
        ParticleSystem muzzle = shootEffect.GetComponent<ParticleSystem>();
        float totalTime = muzzle.main.duration + muzzle.main.startLifetimeMultiplier;
        Destroy(shootEffect, totalTime);
        //muzzleflash.Play();

        RaycastHit hit;
        if(Physics.Raycast(FPScamera.transform.position,FPScamera.transform.forward,out hit,range))
        {
           Debug.Log(hit.transform.name);
           Enemy enemy=hit.transform.GetComponent<Enemy>();

            if(enemy!=null)
            {
                enemy.TakeDamage(damage);
            }
            if(hit.rigidbody!=null)
            {
                hit.rigidbody.AddForce(-hit.normal*impactForce);
            }
            ParticleSystem impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        
        }
    }
}
