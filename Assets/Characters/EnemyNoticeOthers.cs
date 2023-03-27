using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyNoticeOthers : MonoBehaviour
{
    EnemyMovement enemyMovement;
    [SerializeField] List<GameObject> Enemies = new List<GameObject>();
    [SerializeField] GameObject tempFocus;

    private void Awake()
    {
        enemyMovement = transform.parent.GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        if (Enemies.Count() > 0)
        {
            try
            {
                enemyMovement.NoticeHumanMove(FindEnemy());
            }
            catch (System.Exception)
            {
                enemyMovement.NoticeHumanExit(FindEnemy());
            }
        }
    }

    private Transform FindEnemy()
    {
        float distance = 100f;
        foreach (var item in Enemies)
        {
            if (item)
            {
                if (Vector3.Distance(item.transform.position, this.GetComponentInParent<Transform>().position) < distance)
                {
                    tempFocus = item.gameObject;
                    distance = Vector3.Distance(item.transform.position, this.GetComponentInParent<Transform>().position);
                }
            }
        }
        if (tempFocus)
        {
            return tempFocus.transform;
        }
        return null;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "Golem")
        {
            Enemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "Golem")
        {
            Enemies.Remove(other.gameObject);
        }
    }
}
