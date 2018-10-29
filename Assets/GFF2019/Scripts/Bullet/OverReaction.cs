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
        private Vector3 _crntMove;
        private Vector3 _prevMove;
        private Vector3 _moveEnergy;
        private Vector3 _deformEnergy;

        public Vector3 CrntMove     { get { return _crntMove;     } }
        public Vector3 MoveEnergy   { get { return _moveEnergy;   } }
        public Vector3 DeformEnergy { get { return _deformEnergy; } }

        ///<summary>
        /// 初期起動時
        ///</summary>
        protected virtual void Awake ()
        {
            _baseMesh     = GetComponent<MeshFilter>().mesh;
            _baseVertices = _baseMesh.vertices;
            _baseBounds   = _baseMesh.bounds;

            _prevPosition = transform.position;
            _crntMove     = Vector3.zero;
            _prevMove     = Vector3.zero;
            _moveEnergy   = Vector3.zero;
            _deformEnergy = Vector3.zero;
            
        }

        public override void FixedRun()
        {
            _crntMove = transform.position - _prevPosition;

            UpdateMoveEnergy();
            UpdateDeformEnergy();
            DeformMesh();

            _prevPosition = transform.position;
            _prevMove     = _crntMove;
        }

        private void UpdateMoveEnergy()
        {
            _moveEnergy = new Vector3()
            {
                x = UpdateMoveEnergy(_crntMove.x,_prevMove.x,_moveEnergy.x),
                y = UpdateMoveEnergy(_crntMove.y,_prevMove.y,_moveEnergy.y),
                z = UpdateMoveEnergy(_crntMove.z,_prevMove.z,_moveEnergy.z),
            };
        }

        private float UpdateMoveEnergy(float crntMove, float prevMove, float moveEnergy)
        {
            int crntMoveSign   = Sign(crntMove);
            int prevMoveSign   = Sign(prevMove);
            int moveEnergySign = Sign(moveEnergy);

            if (crntMoveSign == 0) { return moveEnergy * undeformPower; }

            if (crntMoveSign != prevMoveSign) { return moveEnergy - crntMove; }

            if (crntMoveSign != moveEnergySign) { return moveEnergy + crntMove; }

            if (crntMoveSign < 0) { Mathf.Min(crntMove * deformPower, moveEnergy * undeformPower); }
            
            return Mathf.Max(crntMove * deformPower, moveEnergy * undeformPower); ;
        }

        private void UpdateDeformEnergy()
        {
            float deformEnergyVertical = _moveEnergy.magnitude * Vector3.Dot(_moveEnergy.normalized, _crntMove.normalized);
            float deformEnergyHorizontalRatio = deformEnergyVertical / maxDeformScale;
            float deformEnergyHorizontal = 1 - deformEnergyHorizontalRatio;

            if (deformEnergyVertical < 0f) { deformEnergyVertical = deformEnergyHorizontalRatio; }

            deformEnergyVertical   = Mathf.Clamp(1f + deformEnergyVertical, minDeformScale, maxDeformScale);
            deformEnergyHorizontal = Mathf.Clamp(deformEnergyHorizontal   , minDeformScale, maxDeformScale);

            _deformEnergy = new Vector3(deformEnergyHorizontal,deformEnergyVertical,deformEnergyHorizontal);
            
        }

        private void DeformMesh()
        {
            Vector3[] deformedVertices = new Vector3[_baseVertices.Length];
            Quaternion crntRotation = transform.localRotation;
            Quaternion crntRotationInverse = Quaternion.Inverse(crntRotation);
            Quaternion moveEnergyRotation = Quaternion.FromToRotation(Vector3.up, _moveEnergy.normalized);
            Quaternion moveEnergyRotationInverse = Quaternion.Inverse(moveEnergyRotation);

            for (int i = 0; i < _baseVertices.Length; i++)
            {
                deformedVertices[i] = _baseVertices[i];           
                deformedVertices[i] = crntRotation * deformedVertices[i];
                deformedVertices[i] = moveEnergyRotationInverse * deformedVertices[i];
                deformedVertices[i] = new Vector3(deformedVertices[i].x * _deformEnergy.x,
                                                  deformedVertices[i].y * _deformEnergy.y,
                                                  deformedVertices[i].z * _deformEnergy.z);
                deformedVertices[i] = moveEnergyRotation  * deformedVertices[i];
                deformedVertices[i] = crntRotationInverse * deformedVertices[i];
            }

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
