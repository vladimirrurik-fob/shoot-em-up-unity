using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShootEmUp
{
    public sealed class GameLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private int _playerHitPoints = 5;

        protected override void Configure(IContainerBuilder builder)
        {
            // --- plain C# systems (no MonoBehaviour), resolved/injected by the container ---
            builder.Register(_ => new Health(this._playerHitPoints), Lifetime.Singleton);
            builder.Register<GameManager>(Lifetime.Singleton);

            // VContainer drives these from the Unity PlayerLoop (no MonoBehaviour needed).
            // RegisterEntryPoint registers the concrete type AND all implemented interfaces
            // (IGameLoop/ICountdown/ITickable/IFixedTickable), so GameManager can inject them
            // by interface — see https://github.com/hadashiA/VContainer/issues/624
            builder.RegisterEntryPoint<GameLoop>(Lifetime.Singleton);
            builder.RegisterEntryPoint<CountdownManager>(Lifetime.Singleton);

            // --- every active scene MonoBehaviour adapter: enable [Inject] + expose listener interfaces ---
            foreach (MonoBehaviour behaviour in this.FindSceneMonoBehaviours())
            {
                builder.RegisterComponent(behaviour).AsImplementedInterfaces().AsSelf();
            }

            // Scene MonoBehaviours are never "resolved" by anyone, so [Inject] would never fire.
            // Force-inject them once the container is built.
            builder.RegisterBuildCallback(container =>
            {
                foreach (MonoBehaviour behaviour in this.FindSceneMonoBehaviours())
                {
                    container.Inject(behaviour);
                }
            });
        }

        private List<MonoBehaviour> FindSceneMonoBehaviours()
        {
            var result = new List<MonoBehaviour>();

            foreach (GameObject root in this.gameObject.scene.GetRootGameObjects())
            {
                foreach (MonoBehaviour behaviour in root.GetComponentsInChildren<MonoBehaviour>(true))
                {
                    if (behaviour == null || !behaviour.gameObject.activeInHierarchy)
                    {
                        continue;
                    }

                    if (behaviour is GameLifetimeScope)
                    {
                        continue;
                    }

                    result.Add(behaviour);
                }
            }

            return result;
        }
    }
}
