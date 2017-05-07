using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCritical
{
public class DeathStar : MonoBehaviour
{

    [SerializeField]
    private float m_Speed;

    public bool m_CanKillPlayer;

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(0, m_Speed, 0) * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (m_CanKillPlayer)
            {
                Destroy(col.gameObject);
            }
        }
    }
}
}