using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using CodeBase.Logic.Actors;
using CodeBase.Logic.Drawing;
using Dreamteck.Splines;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
        }
        
        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            InitGameWorld();
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld()
        {
            GameObject actors = _gameFactory.CreateActors();
            actors.GetComponent<ActorsFollower>().Construct(Object.FindObjectOfType<SplineComputer>()); //!TODO:fix
            InitHud(actors);
        }

        private void InitHud(GameObject actors)
        {
            GameObject hud = _gameFactory.CreateHud();
            hud.GetComponentInChildren<DrawingArea>().Construct(
                actors.GetComponent<ActorsPositioner>(), 
                actors.GetComponent<ActorsFollower>(), 
                actors.GetComponent<ActorsParent>());
        }
    }
}