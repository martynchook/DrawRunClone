using CodeBase.Logic.Actors;
using UnityEngine;

namespace CodeBase.Logic
{
    public class FinishTrigger : MonoBehaviour
    {
        [SerializeField] private Finish _finish;
        
        private bool _isFinish = false;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Actor>() && (_isFinish == false))
            {
                GetComponent<Collider>().enabled = false;
                _isFinish = true;
                _finish.FinishRace(other);
            }
        }
        
    }
}
