/*作成者     ：村上 和樹
 *機能説明   ：
 *初回作成日 ：2018/10/31
 *更新日     ：2018/11/01
*/

namespace Village
{
    public class Count
    {
        public int CountUp(int add)
        {
            return Data += add;
        }

        public int CountDown(int sub)
        {
            return Data += sub;
        }

        public void CountClear()
        {
            Data = 0;
        }
        
        public int Data { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Count()
        {
            Data = 0;
        }

    }
}
