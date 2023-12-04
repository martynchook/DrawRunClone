using CodeBase.Logic.Actors;
using UnityEngine;

namespace CodeBase.Logic.Obstacles
{
    public class ObstacleMine : Obstacle
    {
        [SerializeField] public float _explosionRadius = 1.5f;
        [SerializeField]  public ParticleSystem _particleSystem;

        protected override void Connect(ActorElimination other)
        {
            FreeActor freeActor = other.GetComponent<FreeActor>();
            
            if (freeActor != null)
                return;
            
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explosionRadius);
            foreach (var hitCollider in hitColliders)
            {
                ActorElimination actor = hitCollider.GetComponent<ActorElimination>();
                if (actor != null)
                {
                    actor.Elimination();
                    AddExplosionForce(actor);
                }
            }
            _particleSystem.transform.SetParent(null);
            _particleSystem.Play();
            Destroy(gameObject);
        }

        private static void AddExplosionForce(ActorElimination actor) => 
            actor.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(10f, 20f), GetExplosionPos(actor.transform), 5f, 10f, ForceMode.Impulse);

        private static Vector3 GetExplosionPos(Transform actor) => 
            actor.position + new Vector3(Random.Range(0f, 1f), -1f, Random.Range(0f, 1f));
    }
}
