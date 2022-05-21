using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CadenceSilence : MonoBehaviour
{
    public enum CadenceNameSilence
    {
        Soupir,
        DemiSoupir,
        DemiPause,
        Pause
    }

    [SerializeField]
    private DropDownPicker DropDownPickerCadence;

    [SerializeField]
    private List<Selectable> ShouldBeInactiveOnSilence;

    public void WhenValueChanged()
    {
        OptionDataTraductible chosendCadence = this.DropDownPickerCadence.GetValue();
        foreach (CadenceNameSilence silence in Enum.GetValues(typeof(CadenceNameSilence)))
        {
            if (chosendCadence.InitalValue == silence.ToString())
            {
                foreach (Selectable shouldBeInactive in ShouldBeInactiveOnSilence)
                {
                    shouldBeInactive.interactable = false;
                }

                return;
            }
        }

        foreach (Selectable shouldBeInactive in ShouldBeInactiveOnSilence)
        {
            shouldBeInactive.interactable = true;
        }
    }
}
