using CodeBase.Infrastructure.AssetManagement;
using Dreamteck.Splines;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        
        public GameFactory(IAssets assets)
        {
            _assets = assets;
        }

        public GameObject CreateActors()
        {
            GameObject actors = _assets.Instantiate(AssetPaths.Actors);
            return actors;
        }

        public GameObject CreateActors(SplineComputer spline)
        {
            GameObject actors = _assets.Instantiate(AssetPaths.Actors);
            return actors;
        }

        public GameObject CreateHud()
        {
            GameObject hud = _assets.Instantiate(AssetPaths.Hud);
            return hud;
        }
        
    }
}