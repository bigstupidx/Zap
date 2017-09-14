using UnityEditor;
using UnityEngine;
using UI;

[CustomEditor(typeof(BoostLoading))]
public class BoostLoadingEditor : Editor
{

    BoostLoading instance;

    private void OnEnable()
    {
        instance = (BoostLoading)target;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Save In Game Position"))
        {
            instance.m_InGamePosition = instance.GetComponent<RectTransform>().anchoredPosition;
        }
        if (GUILayout.Button("Save Warp Store Position"))
        {
            instance.m_WarpStorePosition = instance.GetComponent<RectTransform>().anchoredPosition;
        }
        base.OnInspectorGUI();
    }
}
