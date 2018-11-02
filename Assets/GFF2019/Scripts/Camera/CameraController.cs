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
        
        [SerializeField] private Transform _target;     //注視するターゲット
        [SerializeField] private Vector3   _correction; //補正 
        private ICameraMove _chaseMode;
               
        /// <summary>
        /// カメラから見た正面
        /// </summary>
        public Vector3 Forwerd
        {
            get { return transform.forward; }
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Awake()
        {
            ChaseModeChange(new CameraConstantMove(transform));
        }

        ///<summary>
        ///更新処理
        ///</summary>
        public override void Run ()
        {
            if(_target == null) { return; }
            
            AimRotate();            
            //ChaseMove();
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
            _chaseMode.Move(_target.position,-1 * _target.forward * 10);
        }
 
        /// <summary>
        /// ターゲットを中心に回転
        /// </summary>
        private void AimRotate()
        {
            float x = _target.position.x;
            float y = transform.position.y;
            float z = _target.position.z;
            
            transform.LookAt(new Vector3(x,y,z));
            transform.RotateAround(_target.localPosition, Vector3.up,0);
        }
        
        
    }
}
