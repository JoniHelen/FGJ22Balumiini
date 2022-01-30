using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;
public class OhaseObserver : MonoBehaviour
{
    TextMeshProUGUI ui;
    ReactiveProperty<Phase.Phases> PhaseObserver;
    [SerializeField] Phase phase;
    [SerializeField] Animation followUpBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        ui = GetComponent<TextMeshProUGUI>();
        PhaseObserver = new ReactiveProperty<Phase.Phases>(phase.current);

        PhaseObserver
            .ObserveEveryValueChanged(x => phase.current)
            .TakeUntilDestroy(gameObject)
            .Subscribe(y => 
            { 
                ui.text = $"{phase.current.ToString()} Phase";
                if (followUpBehaviour != null) {
                    followUpBehaviour.Play();
                }
            });
    }


}
