/*作成者     ：村上 和樹
 *機能説明   ：
 *初回作成日 ：2018/10/31
 *更新日     ：2018/11/07
*/

namespace Village
{
    public class CountInt : ICount<int>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CountInt(int beginValue = 0)
        {
            Value = beginValue;
        }

        public int CountUp(int add)
        {
            return Value += add;
        }

        public int CountDown(int sub)
        {
            return Value -= sub;
        }

        public void CountClear()
        {
            Value = 0;
        }

        public bool ValueGreater(int other)
        {
            return Value > other;
        }

        public bool ValueGreaterEqual(int other)
        {
            return Value >= other;
        }

        public bool ValueLess(int other)
        {
            return Value < other;
        }

        public bool ValueLessEqual(int other)
        {
            return Value <= other;
        }

        public int Value { get; private set; }
    }
}
