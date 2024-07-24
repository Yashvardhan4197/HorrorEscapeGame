using System.Runtime.CompilerServices;
using UnityEngine;

public class LightsOffByGhostEvent : MonoBehaviour
{
    [SerializeField] private int keysRequired;
    [SerializeField] private SoundType soundType;


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerView>() != null && keysRequired == GameService.Instance.GetPlayerController().KeysEquipped)
        {
            EventService.Instance.OnLightsOffByGhostEvent.InvokeEvent();
            this.gameObject.SetActive(false);
        }
    }
}