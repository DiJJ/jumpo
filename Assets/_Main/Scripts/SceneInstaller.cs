using Main.Player;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Main {
    public class SceneInstaller : MonoInstaller {
        [SerializeField, AssetsOnly] private InputService m_inputService;
        [SerializeField, SceneObjectsOnly] private GameUI m_gameUI;
    
        public override void InstallBindings() {
            Container.Bind<IInputService>().FromComponentInNewPrefab(m_inputService).AsSingle();
            Container.Bind<IGameUI>().FromInstance(m_gameUI).AsSingle();
        }
    }
}