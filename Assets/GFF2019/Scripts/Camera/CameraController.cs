/*作成者     ：村上 和樹
 *機能説明   ：カメラの移動
 *初回作成日 ：2018/11/01
 *更新日     ：2018/11/01
*/

using UnityEngine;

namespace Village
{
    [RequireComponent(typeof(AudioListener))]
    [RequireComponent(typeof(Camera))]
    [RequireComponent(typeof(FlareLayer))]
    public class CameraController : Inheritor
    {  
        [SerializeField] private Transform    _target;     //注視するターゲット
        [SerializeField] private Vector3      _correction; //補正 
        [SerializeField] private Range        _aimLimit;   //
        
        private float _backLength = -10f;
        
        private int _rotateSignX = 1;
        private int _rotateSignY = 1;
        
        private ICameraMove _chaseMode;
              
        /// <summary>
        /// カメラから見た正面
        /// </summary>
        public Vector3 Forwerd
        {
            get
            {
                return transform.TransformDirection(Vector3.forward);             
            }
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
        /// ターゲットを追尾する
        /// </summary>
        private void ChaseMove()
        {            
            _chaseMode.Move(_target.position + _correction, Forwerd * _backLength);
        }
 
        /// <summary>
        /// ターゲットを中心に回転
        /// </summary>
        private void AimRotate()
        {
            float x = _rotateSignX * Controller.Instance.RightAxis().y;
            float y = _rotateSignY * Controller.Instance.RightAxis().x;
            
            transform.localEulerAngles += new Vector3(x, y);
                                         
            transform.localEulerAngles = RotationLimitX(transform.localEulerAngles,_aimLimit.Min,_aimLimit.Max);            
        }

        /// <summary>
        /// X軸の補正
        /// </summary>
        /// <param name="rotation">回転情報</param>
        /// <param name="min">最小値</param>
        /// <param name="max">最大値</param>
        private static Vector3 RotationLimitX(Vector3 rotation,float min,float max)
        {
            rotation.x = RotationValueLimit(rotation.x, min, max);
            
            return rotation;
        }

        /// <summary>
        /// 回転座標の補正
        /// </summary>
        /// <param name="value">補正したい軸の値</param>
        /// <param name="min">最小値</param>
        /// <param name="max">最大値</param>
        private static float RotationValueLimit(float value, float min, float max)
        {
            float angle = 180f <= value ? value - 360f : value;
            return Mathf.Clamp(angle, min, max);
        }
        
        
    }
}
