﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class Zap : MonoBehaviour {

    [SerializeField]
    private int m_Points;
    [SerializeField]
    private Color m_Color;
    
    public float Width { get { return m_SpriteRenderer.bounds.size.x; } }
    public float Height { get { return m_SpriteRenderer.bounds.size.y; } }
    private Vector3 m_OffsetPosition;
    private int m_Row;
    private int m_Col;

    private SpriteRenderer m_SpriteRenderer;

    void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.color = m_Color;
    }

    public virtual void ApplyEffect()
    {
        GameMaster.Instance.m_ZapScore.AddToScore(m_Points);
    }

    public void SetOffsetDistance(float distanceFromZap)
    {
        m_OffsetPosition = new Vector3(Width / 2.0f, Height / 2.0f - distanceFromZap, 0) + this.transform.position;
    }

    public Vector3 GetOffsetPosition()
    {
        return m_OffsetPosition;
    }

    public int GetRow()
    {
        return m_Row;
    }

    public int GetCol()
    {
        return m_Row;
    }

    public void SetRow(int row)
    {
        m_Row = row;
    }

    public void SetCol(int col)
    {
        m_Col = col;
    }

    public void SetWidth(float width)
    {
        Vector3 currScale = this.transform.localScale;
        this.transform.localScale = new Vector3(width, currScale.y, currScale.z);
    }
}
