/*作成者     ：村上 和樹
 *機能説明   ：
 *初回作成日 ：2018/11/11
 *更新日     ：2018/11/11
*/
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Village
{
    [CustomPropertyDrawer(typeof(PreviewOnlyAttribute))]
    public class PreviewOnlyDrawer : PropertyDrawer
    {
   
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            string name = Attribute.IndexName == "" ? property.name : Attribute.IndexName;
            EditorGUI.BeginDisabledGroup(true);
            EditorGUI.PropertyField(rect, property, new GUIContent(name));
            EditorGUI.EndDisabledGroup();
        }

        private PreviewOnlyAttribute Attribute
        {
            get { return attribute as PreviewOnlyAttribute; }
        }
    }
}
