/*作成者     ：村上 和樹
 *機能説明   ：Inheritorを継承したクラスを格納し、
 *            更新処理等を行う
 *初回作成日 ： 2018/10/26
 *更新日     ： 2018/10/26
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

namespace Village
{
    public class UpdateManager : MonoBehaviour
    {
        [Header("Inheritorクラスを継承したクラスを格納する"),SerializeField]
        private List<Inheritor> _updateList = new List<Inheritor>();

        private static UpdateManager _instance;

        public static UpdateManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<UpdateManager>();
                }

                return _instance;
            }
        }

        ///<summary>
        /// 初期生成時
        ///</summary>
		private void Start ()
        {
            for(int i = 0;i < _updateList.Count;i++)
            {
                if (_updateList[i] == null)
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
            for(int i = 0;i < _updateList.Count;i++)
            {
                if (_updateList[i] == null)
                {
                    DeleteAt(i);
                }
                else if (_updateList[i].gameObject.activeInHierarchy)
                {
                    _updateList[i].Run();
                }
            }
        }

        ///<summary>
        ///定期更新
        ///</summary>
        private void FixedUpdate ()
        {
            for(int i = 0;i < _updateList.Count;i++)
            {
                if (_updateList[i] == null)
                {
                    DeleteAt(i);
                }
                else if (_updateList[i].gameObject.activeInHierarchy)
                {
                    _updateList[i].FixedRun();
                }
            }
        }

        /// <summary>
        /// Inheritorオブジェクトの追加
        /// </summary>
        public void Add(Inheritor inheritorObj) 
        {
            _updateList.Add(inheritorObj);
        }

        /// <summary>
        /// Inheritorオブジェクトの削除
        /// </summary>
        public void DeleteAt(int deleteNumber) 
        {
            _updateList.RemoveAt(deleteNumber);
        }

        /// <summary>
        /// Inheritorオブジェクトの全削除
        /// </summary>
        public void DeleteAll() 
        {
            _updateList.Clear();
        }

        /// <summary>
        /// Scene上のInheritorオブジェクトの全追加
        /// </summary>
        public void SetObject() 
        {
            _updateList.Clear();
            foreach(var obj in FindObjectsOfType(typeof(Inheritor)))
            {
                var addObj = obj as Inheritor;
                Add(addObj);
            }
            _updateList.Sort((a,b) => b.GetInstanceID() - a.GetInstanceID());
        }
    }
}