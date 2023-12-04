using CodeBase.Logic.Actors;
using UnityEngine;

namespace CodeBase.Logic.Obstacles
{
    public class ObstacleBase : Obstacle
    {
        protected override void Connect(ActorElimination other)
        {
            FreeActor freeActor = other.GetComponent<FreeActor>();
            
            if (freeActor != null)
                return;

            FindObjectOfType<ActorsParent>().ActorsList.Remove(other.GetComponent<Actor>());
            other.Elimination();
        }
    }
}
