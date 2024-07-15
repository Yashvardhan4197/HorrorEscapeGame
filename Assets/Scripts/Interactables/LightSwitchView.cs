using System.Collections.Generic;
using UnityEngine;

public class LightSwitchView : MonoBehaviour, IInteractable
{
    [SerializeField] private List<Light> lightsources = new List<Light>();
    private SwitchState currentState;

    private delegate void LightSwitchDelegate(); //Signature

    private LightSwitchDelegate lightToggled;

    private void Start() => currentState = SwitchState.Off;

    private void OnEnable()
    {
        lightToggled = onSwitchToggle;
        lightToggled += PlaySoundOnSwitch;
    }

    public void Interact()
    {
        //Todo - Implement Interaction
        lightToggled();
    }
    private void toggleLights()
    {
        bool lights = false;

        switch (currentState)
        {
            case SwitchState.On:
                currentState = SwitchState.Off;
                lights = false;
                break;
            case SwitchState.Off:
                currentState = SwitchState.On;
                lights = true;
                break;
            case SwitchState.Unresponsive:
                break;
        }
        foreach (Light lightSource in lightsources)
        {
            lightSource.enabled = lights;
        }
    }
    private void onSwitchToggle()
    {
        toggleLights();
        GameService.Instance.GetInstructionView().HideInstruction();
    }

    private void PlaySoundOnSwitch()
    {
        GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.SwitchSound);
    }
}
