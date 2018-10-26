/*作成者     ：村上 和樹
 *機能説明   ：
 *初回作成日 ：2018/10/26
 *更新日     ：2018/10/26
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village
{
    [System.Serializable]
    public class SceneObject
    {
        [SerializeField] private string _sceneName;

        /// <summary>
        /// 暗黙的にSceneObjectをstringに変更
        /// </summary>
        public static implicit operator string(SceneObject sceneObject)
        {
            return sceneObject._sceneName;
        }

        /// <summary>
        /// 暗黙的にstringをSceneObjectに変更
        /// </summary>
        public static implicit operator SceneObject(string sceneName)
        {
            return new SceneObject(){ _sceneName = sceneName };
        }

    }
}
