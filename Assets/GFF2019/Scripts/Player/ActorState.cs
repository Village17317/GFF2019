/*作成者     ：村上 和樹
 *機能説明   ：
 *初回作成日 ： 2018/10/26
 *更新日     ： 2018/10/26
*/

using UnityEngine;

namespace Village
{
    public class ActorState : ScriptableObject
    {
        [SerializeField] private float hp      = 1.0f;
        [SerializeField] private float speed   = 1.0f;
        [SerializeField] private float power   = 1.0f;
        [SerializeField] private float defense = 0.0f;

        /// <summary>
        /// 体力
        /// </summary>
        public float Hp
        {
            get { return hp; }
        }

        /// <summary>
        /// 移動速度
        /// </summary>
        public float Speed
        {
            get { return speed; }
        }

        /// <summary>
        /// 攻撃力
        /// </summary>
        public float Power
        {
            get { return power; }
        }

        /// <summary>
        /// 防御力
        /// </summary>
        public float Defense
        {
            get { return defense; }
        }
    }
}
