using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private List<GameObject> ShouldBeInactiveOnSilence;

    public void WhenValueChanged()
    {
        OptionDataTraductible chosendCadence = this.DropDownPickerCadence.GetValue();
        foreach (CadenceNameSilence silence in Enum.GetValues(typeof(CadenceNameSilence)))
        {
            if (chosendCadence.InitalValue == silence.ToString())
            {
                foreach (GameObject shouldBeInactive in ShouldBeInactiveOnSilence)
                {
                    shouldBeInactive.SetActive(false);
                }

                return;
            }
        }

        foreach (GameObject shouldBeInactive in ShouldBeInactiveOnSilence)
        {
            shouldBeInactive.SetActive(true);
        }
    }
}
