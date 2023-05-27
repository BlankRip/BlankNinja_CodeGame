using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class DeligateTest : MonoBehaviour
{
    public delegate int AddEvent(float value1, int value2);
    public AddEvent myAddEvent;
    public delegate int AddEventAnother(float value1, int value2);
    public AddEventAnother anotherEvent;
    
    Action<float, int> AddAction;

    bool another;

    // Start is called before the first frame update
    void Start()
    {
        myAddEvent += ToAdd;

        AddAction += AnotherAdd;

    }

    private void OnDestroy()
    {
        myAddEvent -= ToAdd;

        AddAction -= AnotherAdd;
    }

    int ToAdd(float a, int b)
    {
        return (int)(a + b);
    }

    void AnotherAdd(float a, int b)
    {
        a = (int) (a-b);
    }

    public void CallAdd(Action<float, int> eventPassed)
    {
        int a = 1 + 4; //stats
        float b = 25.333f; //eney git stats

        eventPassed.Invoke(b, a);
    }

    void Update()
    {
        CallAdd(AddAction);

        //CallAdd(ToAdd);
        CallAdd(AnotherAdd);
        // myAddEvent.Invoke(1, 3);
        // AddAction.Invoke(1, 3);
    }
}
