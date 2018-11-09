/*作成者     ：村上 和樹
 *機能説明   ：カメラの移動
 *初回作成日 ：2018/11/01
 *更新日     ：2018/11/01
*/

using UnityEngine;

namespace Village
{
    public class CameraController : Inheritor
    {  
        [SerializeField] private Transform    _target;     //注視するターゲット
        [SerializeField] private Transform    _aimTarget;
        [SerializeField] private Vector3      _correction; //補正 
        [SerializeField] private Range        _aimLimit;   //aimの上限下限
        
        private float _backLength = -10f;     
        private int   _rotateSignX = 1;
        private int   _rotateSignY = 1;
        private bool _isAim        = false;
        
        private ICameraMove _chaseMode;
              
        /// <summary>
        /// カメラから見た正面
        /// </summary>
        public Vector3 Forward
        {
            get { return transform.TransformDirection(Vector3.forward); }
        }

        /// <summary>
        /// カメラから見た正面
        /// </summary>
        public Vector3 Right
        {
            get { return transform.TransformDirection(Vector3.right); }   
        }
                
        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Awake()
        {
            ChaseModeChange(new CameraConstantMove(transform));
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        public override void Run ()
        {
            if(_target == null) { return; }
            
            AimRotate();            
            ChaseMove();
        }

        /// <summary>
        /// ターゲットの切替
        /// </summary>
        /// <param name="nextTarget">次のターゲット</param>
        public void SetTarget(Transform nextTarget)
        {
            _target = nextTarget; 
        }

        /// <summary>
        /// ターゲットの追い方の切替
        /// </summary>
        /// <param name="nextMode">次のモード</param>
        public void ChaseModeChange(ICameraMove nextMode)
        {
            _chaseMode = nextMode;
        }

        /// <summary>
        /// カメラからの中心点から伸ばしたRay
        /// </summary>
        /// <returns></returns>
        public Ray ScreenCenterPointToRay()
        {
            var cam = GetComponent<Camera>();
            var ray = cam.ScreenPointToRay(new Vector3(cam.pixelWidth, cam.pixelHeight, 0) * 0.5f);
            
            return ray;
        }
        
        /// <summary>
        /// ターゲットを追尾する
        /// </summary>
        private void ChaseMove()
        {
            Vector3 pos        = _target.position + _correction;
            Vector3 backLength = Forward * _backLength;

            if (Controller.Instance.IsTargetAiming())
            {
                pos = _aimTarget.position;
                backLength = Vector3.zero;
            }
            
            _chaseMode.Move(pos, backLength);
        }


        
        /// <summary>
        /// ターゲットを中心に回転
        /// </summary>
        private void AimRotate()
        {
            float aimX = _rotateSignX * Controller.Instance.RightAxis().y;
            float aimY = _rotateSignY * Controller.Instance.RightAxis().x;
            
            if (Controller.Instance.IsTargetAiming())
            {
                BeginAim();
                
                aimX = aimX * -1;
                aimY = 0f;
                
                transform.localEulerAngles = new Vector3()
                                             {
                                                 x = transform.localEulerAngles.x,
                                                 y = _target.localEulerAngles.y,
                                                 z = transform.localEulerAngles.z,
                                             };
            }
            else
            {
                EndAim();
            }

            transform.localEulerAngles += new Vector3(aimX, aimY);                                    
            transform.localEulerAngles = RotationLimit(transform.localEulerAngles,_aimLimit.Min,_aimLimit.Max);            
        }

        /// <summary>
        /// 軸の補正
        /// </summary>
        /// <param name="rotation">回転情報</param>
        /// <param name="min">     最小値  </param>
        /// <param name="max">     最大値  </param>
        private static Vector3 RotationLimit(Vector3 rotation,float min,float max)
        {
            //X軸の補正
            rotation.x = RotationValueLimit(rotation.x, min, max);
            //Y軸、Z軸はそのまま
            return rotation;
        }

        /// <summary>
        /// 回転座標の補正
        /// </summary>
        /// <param name="value">補正したい軸の値</param>
        /// <param name="min">  最小値         </param>
        /// <param name="max">  最大値         </param>
        private static float RotationValueLimit(float value, float min, float max)
        {
            float angle = 180f <= value ? value - 360f : value;
            return Mathf.Clamp(angle, min, max);
        }
        
        /// <summary>
        /// Aim状態になったとき
        /// </summary>
        private void BeginAim()
        {
            if(_isAim) { return; }
            
            _isAim                     = true;
            transform.localEulerAngles = _target.localEulerAngles;
        }

        /// <summary>
        /// Aim状態から通常状態に戻ったとき
        /// </summary>
        private void EndAim()
        {
            if(!_isAim) { return; }

            _isAim = false;
            transform.localEulerAngles = _target.localEulerAngles;
        }


    }
}
