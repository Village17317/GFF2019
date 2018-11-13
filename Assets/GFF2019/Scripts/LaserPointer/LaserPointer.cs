/*作成者     ：村上 和樹
 *機能説明   ：
 *初回作成日 ：2018/11/09
 *更新日     ：2018/11/09
*/
using UnityEngine;
using UnityEngine.UI;

namespace Village
{
    public class LaserPointer : MonoBehaviour
    {
        [SerializeField] private Gradient     _color;
        [SerializeField] private LineRenderer _line;
        [SerializeField] private Image        _cursor;       
        
        ///<summary>
        /// 初期起動時
        ///</summary>
        private void Awake ()
        {
            _line.colorGradient = _color;
            SetLineVisible(false);
            SetCursorVisible(false);
        }

        public void SetLaser(params Vector3[] startPoints)
        {
            //頂点数を渡された数分にする
            _line.positionCount = startPoints.Length;
            //各頂点の位置を設定
            _line.SetPositions(startPoints);
        }

        public void SetLineVisible(bool isVisible)
        {
            _line.enabled = isVisible;
        }

        public void SetCursorVisible(bool isVisible)
        {
            _cursor.enabled = isVisible;
        }
        
        public void SetColor(Gradient color)
        {
            _color = color;
        }

        public void SetColor(Color color)
        {
            var gck      = new GradientColorKey[2];
            gck[0].color = color;
            gck[0].time  = 0.0F;
            gck[1].color = color;
            gck[1].time  = 1.0F;
            
            var gak      = new GradientAlphaKey[2];
            gak[0].alpha = 1.0F;
            gak[0].time  = 0.0F;
            gak[1].alpha = 0.0F;
            gak[1].time  = 1.0F;

            _color.SetKeys(gck, gak);
        }
    }
}
