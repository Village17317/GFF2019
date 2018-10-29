/*作成者     ：村上 和樹
 *機能説明   ： Playerの処理
 *初回作成日 ： 2018/10/26
 *更新日     ：2018/10/29
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village
{
    public class Player : Actor<Player>
    {
        [SerializeField] private BulletParams _bulletParams;
        
        
        public BulletParams BulletData{ get {return _bulletParams; } }
        
        /// <summary>
        /// 移動中か
        /// </summary>
        public bool IsMove
        {
            get
            {
                return !Input.GetAxis("Horizontal").Equals(0f) || !Input.GetAxis("Vertical").Equals(0f);
            }
        }

        /// <summary>
        /// 地面接触中か
        /// </summary>
        public bool IsGround
        {
            get
            {
                float   length = 1.1f;
                Vector3 center = transform.position + Vector3.up;
                
                Ray ray = new Ray(center, Vector3.down);
                RaycastHit hit;
                Debug.DrawRay(center,Vector3.down * length,Color.red);
                
                return Physics.Raycast(ray, out hit, length);
            }
        }

        /// <summary>
        /// 攻撃中か
        /// </summary>
        public bool IsAttack
        {
            get { return false; }
        }
        
        
        
        ///<summary>
        /// 初期起動時
        ///</summary>
        private void Awake ()
        {
            ChengeState(new PlayerIdle(this));
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        public override void Run ()
        {
            nowAction.Run();
            
            Debug.Log(nowAction.StateName);
        }


    }
}
