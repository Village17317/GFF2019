/*作成者     ：村上 和樹
 *機能説明   ：Meshを柔らかく変形させる
 *初回作成日 ： 2018/10/29
 *更新日     ： 2018/10/29
*/

using UnityEngine;

namespace Village
{
    [RequireComponent(typeof(MeshFilter))]
    public abstract class OverReaction : Inheritor
    {

        [SerializeField] protected float maxDeformScale = 3f;
        [SerializeField] protected float minDeformScale = 0.2f;
        [SerializeField] protected float deformPower    = 5f;
        [SerializeField] protected float undeformPower  = 0.5f;

        private Mesh      _baseMesh;
        private Vector3[] _baseVertices;
        private Bounds    _baseBounds;

        private Vector3 _prevPosition;
        
        private Vector3 _prevMove;
        
        public Vector3 NowMove      { get; private set; }
        public Vector3 MoveEnergy   { get; private set; }
        public Vector3 DeformEnergy { get; private set; }

        ///<summary>
        /// 初期起動時
        ///</summary>
        protected virtual void Awake ()
        {
            _baseMesh     = GetComponent<MeshFilter>().mesh;
            _baseVertices = _baseMesh.vertices;
            _baseBounds   = _baseMesh.bounds;

            _prevPosition = transform.position;
            _prevMove     = Vector3.zero;

            NowMove       = Vector3.zero;
            MoveEnergy    = Vector3.zero;
            DeformEnergy  = Vector3.zero;
            
        }

        public override void FixedRun()
        {
            NowMove = transform.position - _prevPosition;

            UpdateMoveEnergy();
            UpdateDeformEnergy();
            DeformMesh();

            // 前フレームのPositionとして格納
            _prevPosition = transform.position;
            // 前フレームの動きとして格納
            _prevMove     = NowMove;
        }

        /// <summary>
        /// 運動エネルギーの算出
        /// </summary>
        private void UpdateMoveEnergy()
        {
            MoveEnergy = new Vector3()
            {
                x = UpdateMoveEnergy(NowMove.x, _prevMove.x, MoveEnergy.x),
                y = UpdateMoveEnergy(NowMove.y, _prevMove.y, MoveEnergy.y),
                z = UpdateMoveEnergy(NowMove.z, _prevMove.z, MoveEnergy.z),
            };
        }

        /// <summary>
        /// XYZ各成分ごとに運動エネルギーを算出する
        /// </summary>
        /// <param name="nowMove">   現在の動き</param>
        /// <param name="prevMove">  直前の動き</param>
        /// <param name="moveEnergy">運動エネルギー</param>
        /// <returns></returns>
        private float UpdateMoveEnergy(float nowMove, float prevMove, float moveEnergy)
        {
            int nowMoveSign    = Sign(nowMove);
            int prevMoveSign   = Sign(prevMove);
            int moveEnergySign = Sign(moveEnergy);

            // 動きがない時は現存する運動エネルギーを減衰させていく
            if (nowMoveSign == 0) { return moveEnergy * undeformPower; }

            // 現在の動きと直前の動きが反転しているときは、運動エネルギーを反転させる
            if (nowMoveSign != prevMoveSign) { return moveEnergy - nowMove; }

            // 現在の動きと運動エネルギーが運動エネルギーを小さくする
            if (nowMoveSign != moveEnergySign) { return moveEnergy + nowMove; }

            // 上記の条件以外の場合、現在の動きと運動エネルギーが同じ方向の場合は
            // 現在の動きと現存するエネルギーとを比較して、大きい方を採用する
           
            // （nowMoveSignがマイナスの時）
            if (nowMoveSign < 0) { return Mathf.Min(nowMove * deformPower, moveEnergy * undeformPower); }
            // （nowMoveSignがプラスの時）
            return Mathf.Max(nowMove * deformPower, moveEnergy * undeformPower); ;
        }

        /// <summary>
        /// 変形エネルギーの算出
        /// </summary>
        private void UpdateDeformEnergy()
        {
            float deformEnergyVertical = MoveEnergy.magnitude 
                                         * Vector3.Dot(MoveEnergy.normalized, NowMove.normalized);
            float deformEnergyHorizontalRatio = deformEnergyVertical / maxDeformScale;
            float deformEnergyHorizontal = 1f - deformEnergyHorizontalRatio;

            if (deformEnergyVertical < 0f) { deformEnergyVertical = deformEnergyHorizontalRatio; }

            deformEnergyVertical   = Mathf.Clamp(1f + deformEnergyVertical, minDeformScale, maxDeformScale);
            deformEnergyHorizontal = Mathf.Clamp(deformEnergyHorizontal   , minDeformScale, maxDeformScale);

            DeformEnergy = new Vector3(deformEnergyHorizontal,deformEnergyVertical,deformEnergyHorizontal);
            
        }

        /// <summary>
        /// メッシュの変形
        /// </summary>
        private void DeformMesh()
        {
            Vector3[]  deformedVertices          = new Vector3[_baseVertices.Length];
            Quaternion nowRotation               = transform.localRotation;
            Quaternion nowRotationInverse        = Quaternion.Inverse(nowRotation);
            Quaternion moveEnergyRotation        = Quaternion.FromToRotation(Vector3.up, MoveEnergy.normalized);
            Quaternion moveEnergyRotationInverse = Quaternion.Inverse(moveEnergyRotation);

            for (int i = 0; i < _baseVertices.Length; i++)
            {
                // 各頂点のコピーを取る
                deformedVertices[i] = _baseVertices[i];
                
                // 現在の回転行列を回転していないメッシュの頂点に乗算して回転
                deformedVertices[i] = nowRotation * deformedVertices[i];
                
                // 移動方向を示す回転行列の逆行列を頂点に乗算して回転
                deformedVertices[i] = moveEnergyRotationInverse * deformedVertices[i];
                
                // 頂点をDeformEnergyに従ってスケーリング
                deformedVertices[i] = new Vector3(deformedVertices[i].x * DeformEnergy.x,
                                                  deformedVertices[i].y * DeformEnergy.y,
                                                  deformedVertices[i].z * DeformEnergy.z);
                
                // 移動方向を示す回転行列を頂点に乗算して回転を元に戻す
                deformedVertices[i] = moveEnergyRotation  * deformedVertices[i];
                
                // 現在の回転行列の逆行列を頂点に乗算して回転を元に戻す
                deformedVertices[i] = nowRotationInverse * deformedVertices[i];
            }

            // 変形結果として再格納
            _baseMesh.vertices = deformedVertices;
        }

        private static int Sign(float value)
        {
            if (value.Equals(0f)) { return 0; }

            if (value > 0) { return 1; }
            return -1;
        }
    }
}
