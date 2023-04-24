using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThunderCloud : MonoBehaviour
{

    public float safeTime = 3.0f;
    public float chargeTime = 3.0f;
    public float discargeTime = 3.0f;
    public float stateTime;
    public Collider discargeCollider;

    public int state = 0;
    //0 - Safe
    //1 - Charging
    //2 - Discharging

    float [] stateTimes = new float[3];
    public UnityEvent<int> StateChange;

    public UnityEvent BecomeSafe;
    public UnityEvent BecomeCharging;
    public UnityEvent BecomeDischarging;

    UnityEvent[] stateEvents = new UnityEvent[3];

    // Start is called before the first frame update
    void Start()
    {
        stateTimes[0] = safeTime;
        stateTimes[1] = chargeTime;
        stateTimes[2] = discargeTime;

        stateEvents[0] = BecomeSafe;
        stateEvents[1] = BecomeCharging;
        stateEvents[2] = BecomeDischarging;
    }

    // Update is called once per frame
    void Update()
    {
        stateTime += Time.deltaTime;
        if (stateTime > stateTimes[state])
        {
            CycleState();
        }
    }


    public void SetState(int i)
    {
        state = i;
        stateTime = 0;
        StateChange.Invoke(i);
        stateEvents[i].Invoke();
    }

    public void CycleState(){
        SetState((state + 1) % 3);
    }

    


}
