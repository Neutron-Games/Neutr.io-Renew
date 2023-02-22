using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private float shootForce = 10f;
    private float timeBetweenShooting = 3f;
    bool shooting;
    int shotDamage;

    public void Shot(Transform t2)
    {
        Vector3 direction = t2.position - transform.position;
        GetComponent<Rigidbody>().AddForce(direction * shootForce, ForceMode.Impulse);
        GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 0.5f, GetComponent<Rigidbody>().velocity.z);
        Destroy(gameObject, 1);
    }
}
