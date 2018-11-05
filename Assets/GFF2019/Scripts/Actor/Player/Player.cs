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
        
        
        public BulletParams BulletData{ get {return _bulletParams; } }
        
        /// <summary>
        /// 移動中か
        /// </summary>
        public bool IsMove
        {
            get
            {
                return !Controller.Instance.LeftAxis().Equals(Vector2.zero);
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
        /// 地面接触中にジャンプボタンが押されたとき
        /// </summary>
        public bool IsJump
        {
            get { return IsGround && Controller.Instance.IsJump(); }
        }
        
        /// <summary>
        /// ショットボタンが押されたとき
        /// </summary>
        public bool IsAttack
        {
            get { return Controller.Instance.IsShot(); }
        }

        /// <summary>
        /// カメラから見た正面
        /// </summary>
        public Vector3 CameraForwerd
        {
            get { return _cameraController.Forwerd; }
        }

        /// <summary>
        /// カメラから見た右
        /// </summary>
        public Vector3 CameraRight
        {
            get { return _cameraController.Right; }
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
            nowUpperAction.Execute();
            nowLowerAction.Execute();
            
            Debug.Log(StringBuildManager.Build("<color=orange>",nowUpperAction.StateName,"</color>"));
            Debug.Log(StringBuildManager.Build("<color=green>" ,nowLowerAction.StateName,"</color>"));
        }


    }
}
