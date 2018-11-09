/*作成者     ：村上 和樹
 *機能説明   ：
 *初回作成日 ：2018/11/07
 *更新日     ：2018/11/07
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village
{
    public class CountFloat : ICount<float>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CountFloat(float beginValue = 0)
        {
            Value = beginValue;
        }

        public float CountUp(float add)
        {
            return Value += add;
        }

        public float CountDown(float sub)
        {
            return Value -= sub;
        }

        public void CountClear()
        {
            Value = 0f;
        }

        public bool ValueGreater(float other)
        {
            return Value > other;
        }

        public bool ValueGreaterEqual(float other)
        {
            return Value >= other;
        }

        public bool ValueLess(float other)
        {
            return Value < other;
        }

        public bool ValueLessEqual(float other)
        {
            return Value <= other;
        }

        public float Value { get; private set; }
    }
}
