/*作成者     ：村上 和樹
 *機能説明   ：配列のindexを別名で描画
 *更新日     ：2018/10/25
 *初回作成日 ：2018/10/25
*/

using UnityEditor;
using UnityEngine;

namespace Village
{
    [CustomPropertyDrawer(typeof(NamedArrayAttribute))]
    public class NamedArrayDrawer : PropertyDrawer
    {
        /// <summary>
        /// Inspectorの描画
        /// </summary>
        /// <param name="rect">描画範囲</param>
        /// <param name="property">対応するもの</param>
        /// <param name="label">通常時のラベル</param>
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            try
            {
                int pos = int.Parse(property.propertyPath.Split('[', ']')[1]);
                EditorGUI.PropertyField(rect, property, new GUIContent(Attribute.IndexNames[pos]));
            }
            catch
            {
                EditorGUI.PropertyField(rect, property, label);
            }
        }

        /// <summary>
        /// Attributeの変換した値
        /// </summary>
        private NamedArrayAttribute Attribute
        {
            get{ return attribute as NamedArrayAttribute; }
        }
    }
}
