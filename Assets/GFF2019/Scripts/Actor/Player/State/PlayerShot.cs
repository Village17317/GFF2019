/*作成者     ：村上 和樹
 *機能説明   ：
 *初回作成日 ：
 *更新日     ：
*/

using UnityEditor;
using UnityEngine;

namespace Village
{
    public class PlayerShot : IActorUpperState<Player>
    {
        private const int   ChargeMaxLevel  = 3;  //チャージレベルの最大
        private const float ChargingMaxTime = 1f; //チャージが完了するタイム
        
        
        public Player Owner     { get; set; }
        public string StateName { get { return "Shot " + _chargeLevel;} }

        private GameObject _bullet;
        private int        _chargeLevel;
        private CountFloat _timeCount;
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PlayerShot(Player owner)
        {
            Owner        = owner;
            _timeCount   = new CountFloat();
            _chargeLevel = 1;//初期レベル
            
            //弾を生成
            _bullet = CreateBullet(Owner.BulletData.FirePos);
            _bullet.GetComponent<Bullet>().IsVisible(false);
        }

        public void Execute()
        {
            Reflection.ReflectionVector(Owner.BulletData.FirePos, Direction);
            
            Fire(_bullet.GetComponent<Bullet>());
            
            ObserveIdle();
        }

        /// <summary>
        /// Shot -> Idle
        /// </summary>
        private void ObserveIdle()
        {
            if(Owner.IsCharge) { return; }
                     
            Owner.ChangeUpperState(new PlayerUpperIdle(Owner));
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
        private void Fire(Bullet bullet)
        {           
            if (!Owner.IsAttack)
            {
                Charge(Owner.BulletData.FirePos);
                return;
            }
                        
            bullet.IsVisible(true);

            bullet.Shot(Direction, Owner.BulletData.Force, _chargeLevel);
        }

        /// <summary>
        /// チャージ処理
        /// </summary>
        private void Charge(Vector3 pos)
        {      
            _bullet.transform.position = pos;
            _timeCount.CountUp(Time.deltaTime);

            if (!_timeCount.ValueGreaterEqual(ChargingMaxTime)) { return; }
            
            _timeCount.CountClear();
            _chargeLevel = Mathf.Min(++_chargeLevel, ChargeMaxLevel);
        }
        
        /// <summary>
        /// 発射方向を決める
        /// </summary>
        private Vector3 Direction
        {
            get
            {                                                
                Ray ray = Owner.MyCamera.ScreenCenterPointToRay();
                RaycastHit hit;

                if (Physics.Raycast(ray,out hit))
                {                    
                    return (hit.point - Owner.BulletData.FirePos).normalized;
                }
                     
                return Owner.gameObject.transform.forward;
            }
        }
        
    }
}
