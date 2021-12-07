
using UniRx;
using UnityEngine;
using Zenject;
[RequireComponent(typeof(Rigidbody))]
public class ForceController : MonoBehaviour
{
    private View _view;
    [Inject]
    private void Construct(View view)
    {
        _view = view;
    }
    private Rigidbody _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        _view._addForce
            .OnClickAsObservable()
            .Subscribe(x =>
            {
                _rigidbody.AddForce(Vector3.up * 500, ForceMode.Acceleration);
            })
            .AddTo(this);
    }
}  


