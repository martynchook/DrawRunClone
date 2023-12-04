using UnityEngine;

namespace CodeBase.Logic.Actors
{
    public class ActorAnimator : MonoBehaviour
    {
        [SerializeField] public Animator _animator;
    
        private static readonly int RunHash = Animator.StringToHash("Run");
        private static readonly int VictoryHash = Animator.StringToHash("Victory");

        public void PlayRun() => 
            _animator.SetTrigger(RunHash);

        public void StartVictory() => 
            _animator.SetTrigger(VictoryHash);
    }
}
