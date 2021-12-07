using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BaseConstruct : MonoBehaviour
{
    protected View _view;
    protected SignalBus _signalBus;
    [Inject]
    private void Construct(View view)
    {
        _view = view;
    }
    [Inject]
    public void Construct(SignalBus _signalBus)
    {
        this._signalBus = _signalBus;
    }
}
