
using UniRx;

public class ChangeObjectController : BaseConstruct
{
    private void Start()
    {
        _view._changeObject
            .OnClickAsObservable()
            .Subscribe(x =>
            {
                _signalBus.Fire(new UserChangeObjSignal());
            })
            .AddTo(this);
    }
    
}
