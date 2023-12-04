using CodeBase.Logic.Actors;
using UnityEngine;

namespace CodeBase.Logic.Obstacles
{
    public abstract class Obstacle : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;

        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;
        }

        protected abstract void Connect(ActorElimination actorElimination);

        private void TriggerEnter(Collider other)
        {
            ActorElimination actorElimination = other.GetComponent<ActorElimination>();
            if (actorElimination != null)
                Connect(actorElimination);
        }

        private void TriggerExit(Collider other) { }
    }
}