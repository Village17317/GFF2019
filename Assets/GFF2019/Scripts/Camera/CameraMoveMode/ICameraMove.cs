/*作成者     ：村上 和樹
 *機能説明   ：
 *初回作成日 ：2018/11/01
 *更新日     ：2018/11/01
*/

using UnityEngine;

namespace Village
{
    public interface ICameraMove
    {
        /// <summary>
        /// 利用者
        /// </summary>
        Transform Owner { get; }
        
        /// <summary>
        /// 移動
        /// </summary>
        /// <param name="pos">到達位置</param>
        /// <param name="backLength">到達位置から後ろの距離</param>
        void Move(Vector3 pos,Vector3 backLength);
    }
}
