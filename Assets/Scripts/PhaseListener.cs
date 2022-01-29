using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PhaseListener : MonoBehaviour
{
    [SerializeField] Phase phase;
    ReactiveProperty<Phase.Phases> phaseObserver;
    [SerializeField] Phase.Phases myPhase;
    [SerializeField] MonoBehaviour myHandler;

    // Start is called before the first frame update
    void Start()
    {
        phaseObserver = new ReactiveProperty<Phase.Phases>(phase.current);
        phaseObserver
            .ObserveEveryValueChanged(x => phase.current)
            .TakeUntilDestroy(gameObject)
            .Subscribe(y =>{ myHandler.enabled = (phase.current == myPhase) ? true : false; });
    }

    public void ChangePhase()
    {
        phase.Change();
    }
}
