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
        [SerializeField] private BulletParams     _bulletParams;
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private LaserPointer     _laser;
        
        public BulletParams     BulletData { get { return _bulletParams;     } }
        public CameraController MyCamera   { get { return _cameraController; } }
        public LaserPointer     Laser      { get { return _laser;            } }
        
        /// <summary>
        /// 移動中か
        /// </summary>
        public bool IsMove
        {
            get { return !Controller.Instance.LeftAxis().Equals(Vector2.zero); }
        }

        /// <summary>
        /// 地面接触中か
        /// </summary>
        public bool IsGround
        {
            get
            {
                const float Length = 1.1f;
                Vector3     center = transform.position + Vector3.up;
                
                Ray ray = new Ray(center, Vector3.down);
                RaycastHit hit;
                Debug.DrawRay(center,Vector3.down * Length,Color.red);
                
                return Physics.Raycast(ray, out hit, Length);
            }
        }

        /// <summary>
        /// 地面接触中にジャンプボタンが押されたとき
        /// </summary>
        public bool IsJump
        {
            get { return IsGround && Controller.Instance.IsJump(); }
        }

        /// <summary>
        /// チャージ中かどうか
        /// </summary>
        public bool IsCharge
        {
            get { return Controller.Instance.IsCharge(); }
        }
           
        /// <summary>
        /// ショットボタンが押されたとき
        /// </summary>
        public bool IsAttack
        {
            get { return Controller.Instance.IsShot(); }
        }
        
        ///<summary>
        /// 初期起動時
        ///</summary>
        private void Awake ()
        {
            ChangeUpperState(new PlayerUpperIdle(this));
            ChangeLowerState(new PlayerLowerIdle(this));
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        public override void Run ()
        {
            //Aim時は右スティックの横軸でY軸回転
            if (Controller.Instance.IsTargetAiming())
            {
                transform.Rotate(0,Controller.Instance.RightAxis().x,0);
            }
            
            _laser.SetLineVisible  (Controller.Instance.IsCharge());
            _laser.SetCursorVisible(Controller.Instance.IsTargetAiming());
            
            nowUpperAction.Execute();
            nowLowerAction.Execute();
        }

        /// <summary>
        /// Debug用
        /// </summary>
        private void OnGUI()
        {
            GUILayout.Label(StringBuildManager.Build("Upper : ",nowUpperAction.StateName));
            GUILayout.Label(StringBuildManager.Build("Lower : ",nowLowerAction.StateName));            
        }
    }
}
