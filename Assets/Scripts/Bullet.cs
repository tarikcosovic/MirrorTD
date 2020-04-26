using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float speed = 70f;
    [SerializeField]
    GameObject impactParticle = null;
    [SerializeField]
    float explosionRadius = 0f;

    private Transform target;

    public void SeekTarget(Transform _target)
    {
        target = _target;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
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
        transform.LookAt(target);
    }

    void TargetHit()
    {
        GameObject impactEffect = Instantiate(impactParticle, transform.position, transform.rotation);
        Destroy(impactEffect, 5);

        if (explosionRadius > 0f)
            Explode();
        else HitEnemy(target.gameObject);

        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "enemy")
            {
                HitEnemy(collider.gameObject);
            }
        }
    }

    void HitEnemy(GameObject go)
    {
        //Spawn Particles
        //Update Enemy Health
        //target.GetComponent<Enemy>().UpdateHealth()
        //etc..
        Destroy(go);
    }
}
