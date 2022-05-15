using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DropDownPicker : MonoBehaviour
{
    [SerializeField]
    private DropDownTraductible DropdownPicker;
    public Localization.TypeTrad TypeTrad;

    void Awake()
    {
        Localization.Start();
        Localization.Substribe(TypeTrad, this.SetupTraduction);
    }

    void Destroy()
    {
        Localization.Unsubstribe(TypeTrad, this.SetupTraduction);
    }

    public void SetupTraduction(Dictionary<string, string> trads)
    {
        this.SetTradCaption();
        foreach (var option in this.DropdownPicker.OptionsTraductible)
        {
            SetTextTrad(trads, option);
        }
    }

    public string GetValue()
    {
        return this.DropdownPicker.options[this.DropdownPicker.value].text;
    }

    private void SetTradCaption()
    {
        // Traduire le nom de la note affichée. (la partie option ne change pas, mais la partie que l'on sélectionne oui, donc il faut l'update)
        var chosenOption = this.DropdownPicker.options[this.DropdownPicker.value];
        var chosenOptionTraductible = this.DropdownPicker.OptionsTraductible.FirstOrDefault(o => o.Option == chosenOption);
        string nameTraduit = Localization.GetTrad(TypeTrad, chosenOptionTraductible.InitalValue);
        this.DropdownPicker.captionText.text = nameTraduit;
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
