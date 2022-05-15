using System.Collections.Generic;
using UnityEngine.UI;
using static UnityEngine.UI.Dropdown;

public class DropDownTraductible : Dropdown
{
    public List<OptionDataTraductible> OptionsTraductible;

    protected override void Start()
    {
        base.Start();
        this.OptionsTraductible = new List<OptionDataTraductible>();
        foreach (var option in this.options)
        {
            this.OptionsTraductible.Add(new OptionDataTraductible(option));
        }
    }
}

public class OptionDataTraductible
{
    public OptionData Option { get; private set; }
    public string InitalValue { get; private set; }

    public OptionDataTraductible(OptionData option)
    {
        this.Option = option;
        this.InitalValue = option.text;
    }
}