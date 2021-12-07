
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class Installer : MonoInstaller
{
    private readonly List<GameObject> _objects = new List<GameObject>();
    private GameObject _objFromScene;
    public override void InstallBindings()
    {
        DeclareSignals();
        AsyncLoadResources();
    }
    private void DeclareSignals()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<UserChangeObjSignal>();
        Container
            .BindSignal<UserChangeObjSignal>()
            .ToMethod((SpawnObj));
        Container.DeclareSignal<UserChangeSceneSignal>();
        Container
            .BindSignal<UserChangeSceneSignal>()
            .ToMethod(AsyncLoadScene);
    }
    private void AsyncLoadResources()
    {
        Observable.WhenAll(
            Resources
                .LoadAsync<GameObject>("Plane")
                .AsAsyncOperationObservable(),
            Resources
                .LoadAsync<GameObject>("Canvas")
                .AsAsyncOperationObservable(),
            Resources
                .LoadAsync<GameObject>("Controllers")
                .AsAsyncOperationObservable(),
            Resources
                .LoadAsync<GameObject>("Objects/Capsule")
                .AsAsyncOperationObservable(),
            Resources
                .LoadAsync<GameObject>("Objects/Cube")
                .AsAsyncOperationObservable(),
            Resources
                .LoadAsync<GameObject>("Objects/Sphere")
                .AsAsyncOperationObservable()
        ).Subscribe((x =>
        {
            var view = Container.InstantiatePrefabForComponent<View>(x[1].asset as GameObject);
            Container.Bind<View>().FromInstance(view).AsSingle();
            Container.InstantiatePrefab(x[2].asset as GameObject);
            if (SceneManager.GetActiveScene().buildIndex == 0) return;
            for (int i = 3; i < x.Length; i++)
            {
                _objects.Add(x[i].asset as GameObject);
            }
            Container.InstantiatePrefab(x[0].asset as GameObject);
            _objFromScene = Container.InstantiatePrefab(_objects[Random.Range(0, _objects.Count)]);
        })).AddTo(this);
    }
    private void SpawnObj()
    {
        Destroy(_objFromScene);
        _objFromScene = Container.InstantiatePrefab(_objects[Random.Range(0, _objects.Count)]);
    }
    private void AsyncLoadScene(UserChangeSceneSignal userChangeSceneSignal)
    {
        SceneManager
            .LoadSceneAsync(userChangeSceneSignal._numberScene) 
            .AsAsyncOperationObservable () 
            .Do (x => {})
            .Subscribe (_ => {})
            .AddTo(this);
    }
}
public class UserChangeObjSignal
{
    
}
public class UserChangeSceneSignal
{
    public int _numberScene;
}
