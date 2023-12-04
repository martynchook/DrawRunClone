using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class AnySceneLoader : MonoBehaviour
    { 
        [SerializeField] public GameBootstrapper _gameBootstrapperPrefab;
        private void Awake()
        {
            GameBootstrapper gameBootstrapper = FindObjectOfType<GameBootstrapper>();
            if (gameBootstrapper == null) 
                Instantiate(_gameBootstrapperPrefab);
        }
    }
}