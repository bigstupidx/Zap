using UnityEditor;
using UnityEngine;
using UI;

[CustomEditor(typeof(WarpStorePanel))]
public class WarpStoreEditor : Editor {

    WarpStorePanel instance;

    private void OnEnable()
    {
        instance = (WarpStorePanel)target;
    }

    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Save Off Screen Position"))
        {
            instance.m_OffScreenPosition = instance.GetComponent<RectTransform>().anchoredPosition;
        }
        if (GUILayout.Button("Save On Screen Position"))
        {
            instance.m_OnScreenPosition = instance.GetComponent<RectTransform>().anchoredPosition;
        }
        base.OnInspectorGUI();
    }
}
