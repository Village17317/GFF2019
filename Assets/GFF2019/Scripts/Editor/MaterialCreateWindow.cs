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
    public class MaterialCreateWindow : EditorWindow
    {

        private const string TabName = "MaterialCreateWindow";
        
        private string _path     = "";
        private string _fileName = "";
        private Color _color = Color.white;
        private Shader _shader = new Shader();
        
        [MenuItem("Village/" + TabName)]
        public static void CreateWindow()
        {
            GetWindow<MaterialCreateWindow>(TabName);
        }


        private void OnGUI()
        {
            if (_shader == null)
            {
                _shader = Shader.Find("Standard");
            }

            _shader = (Shader) EditorGUILayout.ObjectField("Shaderを選択",_shader,typeof(Shader));
         
            //色を決める
            _color = EditorGUILayout.ColorField("対応する色を決める", _color);

            
            //保存先を入力（Asset以下）
            _path = EditorGUILayout.TextField("保存先のパスを入力", _path);

            //ファイルの名前を入力
            _fileName = EditorGUILayout.TextField("ファイル名を入力", _fileName);
            
            //ボタンが押されたときに作成
            if (GUILayout.Button("作成"))
            {
                CreateMaterial();
            }
        }

        private void CreateMaterial()
        {
            var asset = new Material(_shader) { color = _color };
            AssetDatabase.CreateAsset(asset,"Assets/" + _path + "/" + _fileName + ".mat");
            AssetDatabase.Refresh();
        }
        
    }
}
