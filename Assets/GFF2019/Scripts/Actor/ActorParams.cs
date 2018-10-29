/*作成者     ：村上 和樹
 *機能説明   ： 各Actorのステータス
 *初回作成日 ： 2018/10/26
 *更新日     ： 2018/10/26
*/

using UnityEngine;

namespace Village
{
    public class ActorParams : ScriptableObject
    {
        [SerializeField] private float _hp      = 1.0f;
        [SerializeField] private float _speed   = 1.0f;
        [SerializeField] private float _power   = 1.0f;
        [SerializeField] private float _defense = 0.0f;

        /// <summary>
        /// 体力
        /// </summary>
        public float Hp
        {
            get { return _hp; }
            set
            {
                _hp = value;
                _hp = Mathf.Max(_hp, 0f);
            }
        }

        /// <summary>
        /// 移動速度
        /// </summary>
        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        /// <summary>
        /// 攻撃力
        /// </summary>
        public float Power
        {
            get { return _power; }
            set { _power = value; }
        }

        /// <summary>
        /// 防御力
        /// </summary>
        public float Defense
        {
            get { return _defense; }
            set { _defense = value; }
        }
    }
}
