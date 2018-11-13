/*作成者     ：村上 和樹
 *機能説明   ：
 *初回作成日 ：
 *更新日     ：
*/
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Village
{
    public class OneMeshConverter : EditorWindow
    {
        private const string TabName = "OneMeshConverter";

        // 結合させたいメッシュのリスト
        private GameObject   _parentObj;
        private List<MeshFilter> _combineMeshes = new List<MeshFilter>();
		
        // 保存先のパス
        private string _path = "";
        // 保存ファイル名
        private string _fileName = "";
		
		
        [MenuItem("Village/" + TabName)]
        public static void CreateWindow()
        {
            GetWindow<OneMeshConverter>(TabName);
        }
		
        private void OnGUI()
        {		
            // 
            _parentObj = EditorGUILayout.ObjectField("Parent",_parentObj,typeof(GameObject)) as GameObject;
			
            // 保存先の指定
            _path = EditorGUILayout.TextField("保存先のパスを入力", _path);
			
            // 保存ファイル名を指定
            _fileName = EditorGUILayout.TextField("ファイル名を入力", _fileName);
			
            // 合体
            if (GUILayout.Button("Export"))
            {
                ExportCombineMesh();	
            }
        }

        private void AddList()
        {
            _combineMeshes.Clear();
            
            foreach (var obj in _parentObj.GetComponentsInChildren<MeshFilter>())
            {
                if(!obj.gameObject.activeInHierarchy) { continue; }
                if(!obj.GetComponent<MeshRenderer>().enabled) { continue; }
                
                
                _combineMeshes.Add(obj);
            }
        }
        
        private void ExportCombineMesh()
        {
			
            AddList();
			
            var combineList = new List<CombineInstance>();
			
            foreach (var filter in _combineMeshes)
            {
                var cmesh = new CombineInstance();
                cmesh.transform = filter.transform.localToWorldMatrix;
                cmesh.mesh      = filter.sharedMesh;
                combineList.Add(cmesh);
            }

            var mesh = new Mesh();
            mesh.CombineMeshes(combineList.ToArray());
            Unwrapping.GenerateSecondaryUVSet(mesh);
			
            AssetDatabase.CreateAsset(mesh,"Assets/" + _path + "/" + _fileName + ".asset");
            AssetDatabase.Refresh();
        }

    }
}
