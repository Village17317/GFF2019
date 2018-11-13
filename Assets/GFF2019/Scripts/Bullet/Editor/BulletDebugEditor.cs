/*作成者     ：村上 和樹
 *機能説明   ：
 *初回作成日 ：2018/11/09
 *更新日     ：2018/11/09
*/

using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Village
{
    public class BulletDebugEditor : EditorWindow
    {
        private const string TabName = "BulletDebugger";

        private List<Bullet> _bullets = new List<Bullet>();
        private StringBuilder    _builder;
        
        [MenuItem("Village/Debug/" + TabName)]
        public static void CreateWindow()
        {
            GetWindow<BulletDebugEditor>(TabName);
        }

        private void OnGUI()
        {
            //_builderがNullの時、生成
            if(_builder == null) { _builder = new StringBuilder(); }

            //いったん中身を空にする
            _builder.Remove(0, _builder.Length);
            
            foreach (var bullet in _bullets)
            {
                if(bullet == null)                        { continue; }
                
                _builder.Append(bullet.GetComponent<Bullet>().DataToString() + "\n");
            }
            
            GUILayout.Label(_builder.ToString());
            Repaint();
        }

        private void Update()
        {
            if(EditorApplication.isPaused) { return; }

            _bullets = new List<Bullet>(FindObjectsOfType<Bullet>().ToArray());
        }
    }
}
