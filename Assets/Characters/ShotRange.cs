using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotRange : MonoBehaviour
{
    public GameObject bullet;
    private Transform tf1;
    // Score score;
    bool canShot;

    private void Awake()
    {
        canShot = true;
        tf1 = GameObject.Find("tf1").transform;
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.tag == "Enemy" || other.gameObject.tag == "Golem") && canShot)
        {
            StartCoroutine(shot(other.transform));
        }
    }

    IEnumerator shot(Transform tfx)
    {
        canShot = false;
        GameObject newBullet = Instantiate(bullet, tf1.position, Quaternion.identity);
        newBullet.GetComponent<BulletManager>().Shot(tfx);
        yield return new WaitForSeconds(.5f);
        if (tfx.gameObject.tag == "Golem")
        {
            // score.AddScore(50);
            // tfx.GetComponent<golemMovement>().Death();
        }
        else if (tfx.gameObject.tag == "Enemy")
        {
            // tfx.GetComponent<EnemyMovement>().GetHit();
            // score.AddScore(50);
            // tfx.GetComponent<Score>().GetDamage((int)score.damage);
        }
        canShot = true;
    }
}
