using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        private const string BootstrapScene = "Bootstrap";
        
        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            RegisterServices();

            //!TODO Возможно тут надо регистрацию намутить
        }

        public void Enter()
        {
            
            _sceneLoader.Load(name: BootstrapScene, onLoaded: EnterLoadLevel);
        }

        public void Exit() { }

        private void RegisterServices()
        {
            IAssets assets = RegisterAssetProvider();
            IGameFactory gameFactory = RegisterGameFactory(assets);
        }

        private IAssets RegisterAssetProvider()
        { 
            _services.RegisterSingle<IAssets>(new AssetProvider());
            return _services.Single<IAssets>();
        }
        
        private IGameFactory RegisterGameFactory(IAssets assets)
        {
            _services.RegisterSingle<IGameFactory>(new GameFactory(assets));
            return _services.Single<IGameFactory>();
        }
        
        private void EnterLoadLevel() =>
            _gameStateMachine.Enter<LoadLevelState, string>("Main"); // TODO: Take a loadable level from the progress service
        
    }
}