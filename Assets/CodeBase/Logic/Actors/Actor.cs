using UnityEngine;

namespace CodeBase.Logic.Actors
{
    public class Actor : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;

        private void Start()
        {
            _triggerObserver.TriggerEnter += Connect;
        }

        private void Connect(Collider other)
        {
            FreeActor freeActor = other.GetComponent<FreeActor>();
            
            if (freeActor != null) 
                freeActor.Employ(transform.parent);
        }
    }
}