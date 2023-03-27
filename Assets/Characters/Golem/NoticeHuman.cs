using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NoticeHuman : MonoBehaviour
{
    GolemMovement golemMovement;
    [SerializeField] List<GameObject> Humans = new List<GameObject>();
    [SerializeField] GameObject tempObj;



    private void Awake()
    {
        golemMovement = transform.parent.GetComponent<GolemMovement>();
    }

    private void Update()
    {
        if (Humans.Count() > 0)
        {
            try
            {
                golemMovement.NoticeHumanMove(FindNearestHuman());
            }
            catch (System.Exception)
            {
                golemMovement.NoticeHumanExit(FindNearestHuman());
            }
        }
    }

    private Transform FindNearestHuman()
    {
        float distance = 100f;
        foreach (var item in Humans)
        {
            if (item)
            {
                if (Vector3.Distance(item.transform.position, this.GetComponentInParent<Transform>().position) < distance)
                {
                    tempObj = item.gameObject;
                    distance = Vector3.Distance(item.transform.position, this.GetComponentInParent<Transform>().position);
                }
            }
        }
        if (tempObj)
        {
            return tempObj.transform;
        }
        return null;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            Humans.Add(other.gameObject);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            Humans.Remove(other.gameObject);
        }
    }
}
