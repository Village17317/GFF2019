/*作成者     ：村上 和樹
 *機能説明   ：
 *初回作成日 ：2018/11/05
 *更新日     ：2018/11/05
*/
using UnityEngine;

namespace Village
{
    [System.Serializable]
    public class Range
    {
        [SerializeField] private float _min;
        [SerializeField] private float _max;
        
        public float Max { get{ return _max; } }
        public float Min { get{ return _min; } }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Range(float min, float max)
        {
            Set(min,max);
        }

        /// <summary>
        /// 渡した値が最小値を下回った場合
        /// </summary>
        /// <param name="value">比較したい値</param>
        public bool IsMinOver(float value)
        {
            return value < Min;
        }
        
        /// <summary>
        /// 渡した値が最大値を上回った場合
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsMaxOver(float value)
        {
            return value > Max;
        }

        /// <summary>
        /// 値をセット
        /// </summary>
        /// <param name="min">最小値</param>
        /// <param name="max">最大値</param>
        public void Set(float min, float max)
        {
            _min = min;
            _max = max;
        }
    }
}
