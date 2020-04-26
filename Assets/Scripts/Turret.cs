using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("General Info")]
    [SerializeField]
    string turretName;
    [SerializeField]
    string turretDescription;
    [SerializeField]
    Sprite turretIcon;

    [Header("Turret placeholders")]
    [SerializeField]
    Transform rotationJoint = null;
    [SerializeField]
    float rotationJointSpeed = 10.0f;

    [Header("Turret Attributes")]
    [SerializeField]
    Types.AttackTypes damageType;
    [SerializeField]
    float minDamage;
    [SerializeField]
    float maxDamage;
    [SerializeField]
    float range = 15.0f;
    [SerializeField]
    float fireRate = 1f;

    [SerializeField]
    GameObject bullet = null;
    [SerializeField]
    Transform bulletFirePoint = null;

    private Transform target;
    private float fireCountdown = 0f;

    const string enemyTag = "enemy";

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void Start()
    {
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float minimumDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < minimumDistance)
            {
                minimumDistance = distance;
                nearestEnemy = enemy;
            }
        }
        if (minimumDistance < range)
            target = nearestEnemy.transform;
        else target = null;
    }

    private void Update()
    {
        if (target == null)
            return;

        //Target Lock-On
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(rotationJoint.rotation, lookRotation, Time.deltaTime * rotationJointSpeed).eulerAngles;
        rotationJoint.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if(fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject go = Instantiate(bullet, bulletFirePoint.position, bulletFirePoint.rotation);
        go.GetComponent<Bullet>().SeekTarget(target);

    }
}
