/*作成者     ：村上 和樹
 *機能説明   ：Inheritorを継承したクラスを格納し、
 *            更新処理等を行う
 *初回作成日 ： 2018/10/26
 *更新日     ： 2018/10/26
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

namespace Village
{
    public class UpdateManager : MonoBehaviour
    {
        [Header("Inheritorクラスを継承したクラスを格納する"),SerializeField]
        private List<Inheritor> updateList = new List<Inheritor>();

        ///<summary>
        /// 初期生成時
        ///</summary>
		private void Start ()
        {
            for(int i = 0;i < updateList.Count;i++)
            {
                if (updateList[i] == null)
                {
                    DeleteAt(i);
                }
            }
        }

        ///<summary>
        ///更新処理
        ///</summary>
        private void Update ()
        {
            for(int i = 0;i < updateList.Count;i++)
            {
                if (updateList[i] == null)
                {
                    DeleteAt(i);
                }
                else if (updateList[i].gameObject.activeInHierarchy)
                {
                    updateList[i].Run();
                }
            }
        }

        ///<summary>
        ///定期更新
        ///</summary>
        private void FixedUpdate ()
        {
            for(int i = 0;i < updateList.Count;i++)
            {
                if (updateList[i] == null)
                {
                    DeleteAt(i);
                }
                else if (updateList[i].gameObject.activeInHierarchy)
                {
                    updateList[i].FixedRun();
                }
            }
        }

        /// <summary>
        /// Inheritorオブジェクトの追加
        /// </summary>
        public void Add(Inheritor inheritorObj) 
        {
            updateList.Add(inheritorObj);
        }

        /// <summary>
        /// Inheritorオブジェクトの削除
        /// </summary>
        public void DeleteAt(int deleteNumber) 
        {
            updateList.RemoveAt(deleteNumber);
        }

        /// <summary>
        /// Inheritorオブジェクトの全削除
        /// </summary>
        public void DeleteAll() 
        {
            updateList.Clear();
        }

        /// <summary>
        /// Scene上のInheritorオブジェクトの全追加
        /// </summary>
        public void SetObject() 
        {
            updateList.Clear();
            foreach(Inheritor obj in FindObjectsOfType(typeof(Inheritor)))
            {
                Add(obj);
            }
            updateList.Sort((a,b) => b.GetInstanceID() - a.GetInstanceID());
        }
    }
}