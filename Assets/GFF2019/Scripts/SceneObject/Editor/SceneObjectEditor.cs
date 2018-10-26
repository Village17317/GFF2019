/*作成者     ：村上 和樹
 *機能説明   ：SceneObjectのEditor上での振る舞い
 *初回作成日 ：2018/10/26
 *更新日     ：2018/10/26
*/

using UnityEditor;
using UnityEngine;

namespace Village
{
    [CustomPropertyDrawer(typeof(SceneObject))]
    public class SceneObjectEditor : PropertyDrawer
    {
        private const string VariableName = "_sceneName";    // 変数名
        private const string LogMsg       = "can't be used"; // エラーメッセージ
        
        /// <summary>
        /// SceneAssetの取得
        /// </summary>
        /// <param name="sceneObjectName">取得したいシーンの名前</param>
        private SceneAsset GetSceneObject(string sceneObjectName)
        {
            // 値が空、もしくはNullの場合 nullを返す
            if (string.IsNullOrEmpty(sceneObjectName)) { return null; }

            // Scenes In Buildに登録されているかをチェック
            for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
            {
                var scene = EditorBuildSettings.scenes[i];
                if (scene.path.IndexOf(sceneObjectName) != -1)
                {
                    return AssetDatabase.LoadAssetAtPath(scene.path,typeof(SceneAsset)) as SceneAsset;
                }
            }
            //　登録されていない場合 nullを返す
            Debug.Log(sceneObjectName + " " +  LogMsg);
            return null;
        }

        /// <summary>
        /// Inspector上での表示
        /// </summary>
        /// <param name="rect">表示位置</param>
        /// <param name="property">対応するもの</param>
        /// <param name="label">ラベル</param>
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            var prop     = property.FindPropertyRelative(VariableName);
            var sceneObj = GetSceneObject(prop.stringValue);
            var newScene = EditorGUI.ObjectField(rect, label, sceneObj, typeof(SceneAsset), false);
            
            // sceneがsetされていない時
            if (newScene == null)
            {
                prop.stringValue = string.Empty;
            }
            else
            {
                // 現在setされているものと新しくセットしたものが同じとき
                if (newScene.name == prop.stringValue)     { return; }
                // Scenes In Buildに登録されていない場合
                if (GetSceneObject(newScene.name) == null) { return; }

                prop.stringValue = newScene.name;
            }
        }
        
    }
}
