/*作成者     ：村上 和樹
 *機能説明   ：
 *初回作成日 ：
 *更新日     ：
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Village
{
    public class ScriptableObjectCreateWindow : EditorWindow
    {
        private enum ClassTab
        {
            ActorState,
        }
        
        private const string TabName = "ScriptableObjectCreateWindow";

        private ClassTab _tab      = ClassTab.ActorState;
        private string   _path     = "";
        private string   _fileName = "";
        
        [MenuItem("Village/" + TabName)]
        public static void ShowWindow()
        {
            GetWindow<ScriptableObjectCreateWindow>(TabName);
        }

        private void OnGUI()
        {
            // 作りたいクラスを選択
            _tab = (ClassTab) EditorGUILayout.EnumPopup("作成するクラスを選択", _tab);

            //保存先を入力（Asset以下）
            _path = EditorGUILayout.TextField("保存先のパスを入力", _path);

            //ファイルの名前を入力
            _fileName = EditorGUILayout.TextField("ファイル名を入力", _fileName);
            
            //ボタンが押されたときに作成
            if (GUILayout.Button("作成"))
            {
                switch (_tab)
                {
                case ClassTab.ActorState: CreateAsset<ActorState>(); break;
                default: break;
                }
            }
        }

        private void CreateAsset<T>() where T : ScriptableObject
        {
            var asset = CreateInstance<T>();
                
            AssetDatabase.CreateAsset(asset,"Assets/" + _path + "/" + _fileName + ".asset");
            AssetDatabase.Refresh();
        }
        
    }
}
