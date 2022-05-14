using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DropDownNotePicker : MonoBehaviour
{
    [SerializeField]
    private DropDownTraductible DropdownNotePicker;
    public Localization.TypeTrad TypeTrad;

    void Awake()
    {
        Localization.Start();
        Localization.SubstribeToNoteName(this.SetupTraduction);
    }

    void Destroy()
    {
        Localization.UnsubstribeToNoteName(this.SetupTraduction);
    }

    public void SetupTraduction(Dictionary<string, string> trads)
    {
        this.SetTradCaption();
        foreach (var option in this.DropdownNotePicker.OptionsTraductible)
        {
            SetTextTrad(trads, option);
        }
    }

    private void SetTradCaption()
    {
        // Traduire le nom de la note affichée. (la partie option ne change pas, mais la partie que l'on sélectionne oui, donc il faut l'update)
        var chosenOption = this.DropdownNotePicker.options[this.DropdownNotePicker.value];
        var chosenOptionTraductible = this.DropdownNotePicker.OptionsTraductible.FirstOrDefault(o => o.Option == chosenOption);
        string noteNameTraduit = Localization.GetTradNoteName(chosenOptionTraductible.InitalValue);
        this.DropdownNotePicker.captionText.text = noteNameTraduit;
    }

    private void SetTextTrad(Dictionary<string, string> trads, OptionDataTraductible option)
    {
        string text = option.InitalValue;
        if (trads.ContainsKey(text))
        {
            option.Option.text = trads[text];
        }
    }
}
