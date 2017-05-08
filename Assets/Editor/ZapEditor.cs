using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameCritical;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(Zap))]
[CanEditMultipleObjects]
public class ZapEditor : Editor {

	// Use this for initialization
	public override void OnInspectorGUI () {
        Zap zapScript = target as Zap;

        zapScript.m_HasPoints = EditorGUILayout.Toggle("Has Points", zapScript.m_HasPoints);

        if (zapScript.m_HasPoints)
            zapScript.m_Points = EditorGUILayout.IntField(zapScript.m_Points);
    }
}
#endif
