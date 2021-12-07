
using UniRx;
using UnityEngine.SceneManagement;

public class SceneController : BaseConstruct
{
    private void Start()
    {
        _view._backMenu
            .OnClickAsObservable()
            .Subscribe(x =>
            {   
                _signalBus.Fire(new UserChangeSceneSignal {_numberScene = SceneManager.GetActiveScene().buildIndex != 0 ? 0 : 1});
            })
            .AddTo(this);
    }
}
