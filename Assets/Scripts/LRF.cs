using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LRF : MonoBehaviour
{
    List<GameObject> radarObjects;
    GameObject[] enemies;
    public Transform player;
    public float angle;
    public enum lrfType
    {
        A,
        B,
        C
    } public lrfType lrftype = lrfType.A;

    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Monster");
        player = GameObject.FindWithTag("Player").transform;
        if(lrftype == lrfType.A)
        {
            angle = 30f;
        }else if(lrftype == lrfType.B)
        {
            angle = 60f;
        }else if(lrftype == lrfType.C)
        {
            angle = 90f;
        }
        Debug.Log(angle);
    }

    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Monster");

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                if (IsEnemyVisible(enemies[i].transform))
                {
                    enemies[i].transform.Find("RadarPoint").gameObject.layer = 8;
                }
                else
                {
                    enemies[i].transform.Find("RadarPoint").gameObject.layer = 11;
                }
            }
        }
        
    }

    bool IsEnemyVisible(Transform enemyTransform)
    {
        Vector3 directionToEnemy = enemyTransform.position - player.position;

        float angleToEnemy = Vector3.Angle(player.forward, directionToEnemy);

        if (angleToEnemy <= angle&&angleToEnemy>=-angle )
        {
            

            return true;
        }

        return false;
    }
}
