/*作成者     ：村上 和樹
 *機能説明   ：Up
 *初回作成日 ：2018/10/26
 *更新日     ：2018/10/26
*/

using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Village
{
    [CustomEditor(typeof(UpdateManager))]
    public class UpdateManagerEditor : Editor
    {
        private const float  ElementHeight = 25f;
        private const string LabelName     = "一覧";
        private const string ListName      = "_updateList";
        
        private       ReorderableList _mList;

        private void OnEnable()
        {
            var prop = serializedObject.FindProperty(ListName);
            _mList               = new ReorderableList(serializedObject,prop);
            _mList.elementHeight = ElementHeight;
            _mList.drawElementCallback = (rect, index, isActive, isFocused) => {
                var element = prop.GetArrayElementAtIndex(index);
                rect.height -= 4f;
                rect.y      += 2f;
                EditorGUI.PropertyField(rect,element);
            };
            _mList.drawHeaderCallback = (rect) => { EditorGUI.LabelField(rect,LabelName); };
            _mList.drawElementBackgroundCallback = (rect, index, isActive, isFocused) => {
                GUI.backgroundColor = Color.cyan;
            };
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            serializedObject.Update();
            _mList.DoLayoutList(); //見れるようになる
            serializedObject.ApplyModifiedProperties();

            if(GUILayout.Button("SetButton"))
            {
                if (Target != null)
                {
                    Target.SetObject();
                }
            }

            if (GUILayout.Button("ReSetButton"))
            {
                if (Target != null)
                {
                    Target.DeleteAll();
                }
            }
        }

        private UpdateManager Target { get { return target as UpdateManager; } }
        
    }
}
