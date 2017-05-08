using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCritical;

namespace Obstacles
{
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class Obstacle : MonoBehaviour {

        [SerializeField]
        private int m_Damage;

        private int m_Row;
        private int m_Col;

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "Player")
            {
                ApplyObstacleEffect();
            }
        }

        public void SetPosition(int row, int col)
        {
            m_Row = row;
            m_Col = col;
            Zap zapAtPosition = GameMaster.Instance.m_ZapManager.GetZapGrid().GetZap(m_Row, m_Col);
            this.transform.position = zapAtPosition.GetOffsetPosition();
        }

        public virtual void ApplyObstacleEffect() { }
    }
}
