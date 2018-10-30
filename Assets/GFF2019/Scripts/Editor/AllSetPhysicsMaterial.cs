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
    public class AllSetPhysicsMaterial : EditorWindow
    {
        private const string TabName = "AllSetPhysicsMaterial";

        private enum SearchCondition
        {
            Tag,
            Layer,
        }

        private SearchCondition _search = SearchCondition.Tag;
        private PhysicMaterial  _physicMaterial;
        private string          _searchTag = "Untagged";
        private int             _searchLayer = 0;
        
        [MenuItem("Village/" + TabName)]
        public static void CreateWindow()
        {
            GetWindow<AllSetPhysicsMaterial>(TabName);
        }

        private void OnGUI()
        {
            //tag or Layer
            _search = (SearchCondition)EditorGUILayout.EnumPopup("検索する種類", _search);

            switch (_search)
            {
                //Search Tag
                case SearchCondition.Tag:   SearchTag();   break;
                //Search Layer
                case SearchCondition.Layer: SearchLayer(); break;
                
                default: throw new ArgumentOutOfRangeException();
            }

            //set PhysicsMaterial
            _physicMaterial =
                (PhysicMaterial) EditorGUILayout.ObjectField("割り当てるPhysicsMaterial", _physicMaterial, typeof(PhysicMaterial));
            
            //Push Button Apply
            if (GUILayout.Button("Apply"))
            {
                ApplyPhysicsMaterial();
            }
        }

        private void SearchTag()
        {
            _searchTag = EditorGUILayout.TagField("検索するタグ", _searchTag);
        }

        private void SearchLayer()
        {
            _searchLayer = EditorGUILayout.LayerField("検索するLayer", _searchLayer);
        }

        private void ApplyObject(GameObject[] objs)
        {
            foreach (var obj in objs)
            {
                if (obj.GetComponent<Collider>() == null) { continue; }
                
                obj.GetComponent<Collider>().material = _physicMaterial;
            }
        }

        private GameObject[] FindGameObjectsWithLayer(int layer)
        {
            var find = FindObjectsOfType(typeof(GameObject)) as GameObject[];
            var ret = new List<GameObject>();
            
            foreach (var obj in find)
            {
                if (obj.layer != layer) { continue; }

                ret.Add(obj);
            }
            
            return ret.ToArray();
        }
        
        private void ApplyPhysicsMaterial()
        {
            switch (_search)
            {
            //Apply Tag
            case SearchCondition.Tag:   ApplyObject(GameObject.FindGameObjectsWithTag(_searchTag));   break;
            //Apply Layer
            case SearchCondition.Layer: ApplyObject(FindGameObjectsWithLayer(_searchLayer)); break;
                
            default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}
