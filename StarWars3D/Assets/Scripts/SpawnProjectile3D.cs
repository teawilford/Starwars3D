using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectile3D
    : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject projectTilePrefab;
    public float projectTileSpeed = 1;
    DateTime lastSpawnTime = DateTime.Now;
    //tags of gameobjects to shoot at
    public string[] TagsOfObjectsToAttack;
    public float DistanceToStartAttacking = 10f;
    public double secondsBetweenProjectiles = 1;
    public bool AutoFire = false;
    public float secondsBeforeProjectileDies = 10;
    public float secondsBeforeShotEffectsDestroy = 1;

    public GameObject shotEffectsPrefab;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            gameObject.GetComponentInParent<Rigidbody>();
        if (spawnPoint == null)
            spawnPoint = gameObject;
    }

    //if you come with in the specified range of an object, start attacking.

    // Update is called once per frame
    void Update()
    {
        

        if ((AutoFire || Input.GetAxis("Fire1") > 0) && (DateTime.Now-lastSpawnTime).TotalSeconds > secondsBetweenProjectiles)
        {
            //Debug.Log(rb.velocity.magnitude);
            //spawn the projectile
            GameObject projectile = Instantiate(projectTilePrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
            //add force to the projectile so it moves
            Rigidbody projectileRigidBody = projectile.GetComponent<Rigidbody>();

            if (projectileRigidBody != null)
            {
                if(rb != null)
                    projectile.GetComponent<Rigidbody>().AddForce(spawnPoint.transform.forward * (projectTileSpeed + (rb.velocity.magnitude)), ForceMode.Impulse);
                else
                    projectile.GetComponent<Rigidbody>().AddForce(spawnPoint.transform.forward * (projectTileSpeed), ForceMode.Impulse);
            }
            //set the destroy time on the projectile
            Destroy(projectile, secondsBeforeProjectileDies);
            lastSpawnTime = DateTime.Now;

            //do we have a "fire" image to play?
            if (shotEffectsPrefab != null)
            {
                GameObject image = Instantiate(shotEffectsPrefab, spawnPoint.transform);
                Destroy(image, secondsBeforeShotEffectsDestroy);
            }
        }

    }
}
