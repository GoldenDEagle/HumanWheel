using UnityEditor;
using UnityEditor.UI;

namespace Assets.Codebase.Utils.UI.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(CustomButton), true)]
    public class CustomButtonEditor : ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_playSoundOnClick"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_soundId"));
            serializedObject.ApplyModifiedProperties();

            base.OnInspectorGUI();
        }
    }
}