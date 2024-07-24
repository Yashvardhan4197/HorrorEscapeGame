using System;
using System.Collections.Generic;
using UnityEditor.MPE;
using UnityEngine;

public class LightSwitchView : MonoBehaviour, IInteractable
{
    [SerializeField] private List<Light> lightsources = new List<Light>();
    private SwitchState currentState;

    private void OnEnable()
    {
        EventService.Instance.OnLightsOffByGhostEvent.AddListener(OnLightSwitchToggledByGhost);
        EventService.Instance.OnLightSwitchToggled.AddListener(onLightSwitch);
    }

    private void OnDisable()
    {
        EventService.Instance.OnLightsOffByGhostEvent.RemoveListener(OnLightSwitchToggledByGhost);
        EventService.Instance.OnLightSwitchToggled.RemoveListener(onLightSwitch);
    }

    private void Start() => currentState = SwitchState.Off;

    public void Interact() => EventService.Instance.OnLightSwitchToggled.InvokeEvent();

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

    private void SetToggle(bool lights)
    {
        foreach (Light lightSource in lightsources)
        {
            lightSource.enabled = lights;
        }

        if (lights)
        {
           currentState= SwitchState.On;
        }
        else
        {
            currentState = SwitchState.Off;
        }
    }

    private void OnLightSwitchToggledByGhost()
    {
        SetToggle(false);
        GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.SpookyGiggle);
        GameService.Instance.GetInstructionView().ShowInstruction(InstructionType.LightsOff);
    }

    private void onLightSwitch()
    {
        toggleLights();
        GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.SwitchSound);
        GameService.Instance.GetInstructionView().HideInstruction();
    }
}
