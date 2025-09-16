using Essence;
using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class UiNetcodeTest : NetworkBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    NetworkVariable<int> ScoreValue = new NetworkVariable<int>(10,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server);

    public Action<int> OnScoreCall; // 함수를 저장할 수 있는 변수.  
    //public Func<int, int> MyFunc;   // 첫번째 타입부터 매개변수이고 , 마지막 매개변수가 리턴값이다. 
    //public Func<int, string> MyFunc2;
    //public void Func() { }
    //public void Func2(int Val) { }
    //public int Func3(int Val2) { return 3; }
    //public int Func4(int Val2, int Val3) { return 3; }
    //public string Func5(int Val) { return ""; }

    // 네트워크 코드는 서버에서 실행하는지, 클라이언트에서 실행해야 하는지 구분을 해야한다.
    // 조건문을 사용해서 서버인지, 서버가 아닌지 구분하고 각 상태에 맞는 함수를 연결하는 식으로 코드를 구현한다.

    public override void OnNetworkSpawn()
    {
        if(IsServer)
        {
            OnScoreCall += HandleAddPoint;
            //MyFunc2 + ScoreValue.Valueue += 10; // 특별한 시점에서만 실행하고 싶다.
        }

        ScoreValue.OnValueChanged += OnScoreValueChanged;       
    }

    private void HandleAddPoint(int value)
    {
        ScoreValue.Value += value;
    }

    public override void OnNetworkDespawn()
    {
        if (IsServer) 
        {
            OnScoreCall -= HandleAddPoint; 
        }

        ScoreValue.OnValueChanged -= OnScoreValueChanged;
    }

    private void OnScoreValueChanged(int previousValue, int newValue)
    {
        // 모든 Client가 실행해야하는 코드.

        scoreText.SetText($"score : {newValue}");
    }



}
