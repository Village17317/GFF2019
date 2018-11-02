/*作成者     ：村上 和樹
 *機能説明   ：指定したメッシュを反転し、保存
 *初回作成日 ：2018/11/02
 *更新日     ：2018/11/02
*/

using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Village
{
    public class ReverseMeshConverter : EditorWindow
    {
        private const string TabName = "ReverseMeshConverter";

        private Mesh   _mesh;
        private string _path     = "";
        private string _fileName = "";
        
        
        
        /// <summary>
        /// Windowを作成
        /// </summary>
        [MenuItem("Village/" + TabName)]
        public static void ShowWindow()
        {
            GetWindow<ReverseMeshConverter>(TabName);
        }

        private void OnGUI()
        {
            //Meshを指定
            _mesh = EditorGUILayout.ObjectField("Meshを指定",_mesh,typeof(Mesh)) as Mesh;
            
            //保存先を入力（Asset以下）
            _path = EditorGUILayout.TextField("保存先のパスを入力", _path);

            //ファイルの名前を入力
            _fileName = EditorGUILayout.TextField("ファイル名を入力", _fileName);
            
            //ボタンが押されたときに作成
            if (GUILayout.Button("作成"))
            {
                CreateAsset();
            }
            
        }

        private void CreateAsset()
        {
            var reverseMesh = new Mesh
                              {
                                  vertices  = _mesh.vertices,
                                  uv        = _mesh.uv,
                                  triangles = _mesh.triangles.Reverse().ToArray()
                              };

            AssetDatabase.CreateAsset(reverseMesh,"Assets/" + _path + "/" + _fileName + ".asset");
            AssetDatabase.SaveAssets();
        }
    }
}
