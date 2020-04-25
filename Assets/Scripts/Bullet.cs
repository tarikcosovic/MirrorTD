using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float speed = 70f;
    [SerializeField]
    GameObject impactParticle;

    private Transform target;

    public void SeekTarget(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            TargetHit();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void TargetHit()
    {
        GameObject impactEffect = Instantiate(impactParticle, transform.position, transform.rotation);
        Destroy(impactEffect, 3);
        //Spawn Particles
        //Update Enemy Health 
        //etc..
        Destroy(gameObject);
    }
}
