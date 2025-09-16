using Unity.Netcode.Components;
using UnityEngine;

// server가 변경 사항이 생겼을 때 수정을 합니다.

public class ClientNetworkAnimator : NetworkAnimator
{
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }
}
