/*作成者     ：村上 和樹
 *機能説明   ：
 *初回作成日 ：
 *更新日     ：
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village
{
    public class PlayerShot : IActorState<Player>
    {
        public Player Owner     { get; set; }
        public string StateName { get { return "Shot";} }
        
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PlayerShot(Player owner)
        {
            Owner = owner;
        }

        public void Run()
        {
            var bullet = CreateBullet(Owner.BulletData.FirePos);
            Fire(bullet);
            
            ObserveIdle();
        }

        private void ObserveIdle()
        {
            if(Owner.IsAttack) { return; }
            
            
            Owner.ChengeState(new PlayerIdle(Owner));
        }

        private GameObject CreateBullet(Vector3 pos)
        {
            var bullet = Owner.BulletData.BulletInstance;
            bullet.transform.position = pos;
            return bullet;
        }

        private void Fire(GameObject bullet)
        {
            bullet.GetComponent<Rigidbody>().AddForce(Owner.transform.forward * Owner.BulletData.Force);
        }        
    }
}
