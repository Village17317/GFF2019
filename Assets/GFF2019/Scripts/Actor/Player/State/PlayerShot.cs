/*作成者     ：村上 和樹
 *機能説明   ：
 *初回作成日 ：
 *更新日     ：
*/

using UnityEngine;

namespace Village
{
    public class PlayerShot : IActorUpperState<Player>
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

        public void Execute()
        {
            var bullet = CreateBullet(Owner.BulletData.FirePos);
            Fire(bullet);
            
            ObserveIdle();
        }

        /// <summary>
        /// Shot -> Idle
        /// </summary>
        private void ObserveIdle()
        {
            if(Owner.IsAttack) { return; }
                     
            Owner.ChengeState(new PlayerUpperIdle(Owner));
        }

        /// <summary>
        /// 弾の生成
        /// </summary>
        /// <param name="pos">射出位置</param>
        private GameObject CreateBullet(Vector3 pos)
        {
            var bullet = Owner.BulletData.BulletInstance;
            bullet.transform.position = pos;
            return bullet;
        }

        /// <summary>
        /// 射出
        /// </summary>
        /// <param name="bullet">発射するもの</param>
        private void Fire(GameObject bullet)
        {
            bullet.GetComponent<Rigidbody>().AddForce(Owner.transform.forward * Owner.BulletData.Force);
        }        
    }
}
