using UnityEngine;
using UnityEditor;

public static class HierarchyGUI_ShowComponent
{
    private const int ICON_SIZE = 16;

    [InitializeOnLoadMethod]
    private static void Initialize()
    {
        EditorApplication.hierarchyWindowItemOnGUI += OnGUI;
    }

    private static void OnGUI(int instanceID, Rect selectionRect)
    {
        // instanceID ���I�u�W�F�N�g�Q�Ƃɕϊ�
        var go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (go == null)
        {
            return;
        }

        // �I�u�W�F�N�g���������Ă���R���|�[�l���g�ꗗ���擾
        var components = go.GetComponents<Component>();
        if (components.Length == 0)
        {
            return;
        }

        selectionRect.x = selectionRect.xMax - ICON_SIZE * components.Length;
        selectionRect.width = ICON_SIZE;

        foreach (var component in components)
        {
            // �R���|�[�l���g�̃A�C�R���摜���擾
            var texture2D = AssetPreview.GetMiniThumbnail(component);

            GUI.DrawTexture(selectionRect, texture2D);
            selectionRect.x += ICON_SIZE;
        }
    }
}