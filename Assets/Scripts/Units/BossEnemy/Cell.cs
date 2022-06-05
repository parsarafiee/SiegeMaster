using General;
using Units.Interfaces;
using Units.Types;
using UnityEngine;
using static PuQUEST;

namespace Units.BossEnemy
{
    public class Cell : Unit, ICreatable<Cell.Args>, IHittable
    {
        public float Maxforce;
        public float Minforce;
       [HideInInspector] public Body body;
        [HideInInspector] private PuQUEST puQuest;
        public float maxDistance;
        public float minDistance;
        Rigidbody rigidbody;
        
        public float calculatedForce;
        float randForce ;
        public Transform cellPosition;

        protected override Vector3 AimedPosition => throw new System.NotImplementedException();

        public override void Init()
        {
            calculatedForce = 0;
            base.Init();
            puQuest = FindObjectOfType<PuQUEST>();
             rigidbody = GetComponent<Rigidbody>();

        }
        public void Construct(Args constructionArgs)
        {
            cellPosition = constructionArgs.cellPosition;
            transform.parent = constructionArgs.cellPosition;
            body = constructionArgs.body;
            transform.SetParent(constructionArgs.parentTransfrom);

            //Calculate Random Force 
            randForce = Random.Range(Minforce, Maxforce);
            float randDis = Random.Range(minDistance, maxDistance);
            calculatedForce = Mathf.Clamp(Vector3.Distance(cellPosition.position, transform.position), 0, randDis);
        }
        public override void FixedRefresh()
        {
            
            rigidbody.AddForce( (cellPosition.position - transform.position).normalized * calculatedForce*randForce) ;
          //  Debug.Log("hk");
            base.Refresh();
        }

        public void ForceCalculation()
        {

        }
        protected override void OnDeathEvent()
        {
            CellManager.Instance.Pool(CellType.Normal, this);
        }
        public override void GotShot(float damage)
        {
            stats.health.Current -= damage;
           
           
        }
        public override void Pool()
        {
            body.CellDeath(this);
            gameObject.SetActive(false);
            puQuest.balls.LoseCell();
            base.Pool();
        }

        public class Args : ConstructionArgs
        {
            public Transform cellPosition;
            public Transform parentTransfrom;
            public Body body;
            
            public Args(Vector3 _spawningPosition, Transform _cellPosition, Body _body, Transform _parentTransfrom) : base(_spawningPosition)
            {
                cellPosition = _cellPosition;
                parentTransfrom = _parentTransfrom;
                body= _body;

            }
        }
    }
}