using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCritical;
using UnityEngine.UI;

namespace UI
{
    public class CharacterUnlockButton :  UnlockableButton
    {
        [SerializeField]
        private SpriteRenderer m_CharacterSprite;

        public override void Init()
        {
            base.Init();
            m_MainImage.sprite = m_CharacterSprite.sprite;
            m_MainImage.color = m_CharacterSprite.color;
        }

        public override void equip()
        {
            base.equip();

            GameMaster.Instance.m_PlayerDecorations.SetCharacter(
                m_CharacterSprite.sprite, 
                m_CharacterSprite.color);
        }
    }
}
