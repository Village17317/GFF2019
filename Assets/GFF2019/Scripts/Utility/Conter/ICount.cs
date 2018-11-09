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
    interface ICount<T> where T : struct
    {
        /// <summary>
        /// 値を増やす
        /// </summary>
        /// <param name="add">増やす値</param>
        /// <returns>増やした結果</returns>
        T CountUp(T add);

        /// <summary>
        /// 値を減らす
        /// </summary>
        /// <param name="sub">減らす値</param>
        /// <returns>減らした結果</returns>
        T CountDown(T sub);

        /// <summary>
        /// 値の初期化
        /// </summary>
        /// <returns></returns>
        void CountClear();

        /// <summary>
        /// 指定した値より大きいかどうか
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        bool ValueGreater(T other);

        /// <summary>
        /// 指定した値以上かどうか
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        bool ValueGreaterEqual(T other);
        
        /// <summary>
        /// 指定した値未満かどうか
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        bool ValueLess(T other);

        /// <summary>
        /// 指定した値以下かどうか
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        bool ValueLessEqual(T other);
        
        /// <summary>
        /// 値
        /// </summary>
        T Value { get; }
    }
}
