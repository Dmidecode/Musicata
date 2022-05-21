using System;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class UIOptionsPicker : MonoBehaviour
{
    [SerializeField]
    private Toggle HandPicker;

    [SerializeField]
    private DropDownPicker CadencePicker;

    [SerializeField]
    private DropDownPicker NotePicker;

    [SerializeField]
    private DropDownPicker GammePicker;

    [SerializeField]
    private DropDownPicker AlterationPicker;

    [SerializeField]
    private Toggle PointerPicker;

    [SerializeField]
    private Compositeur Compositeur;

    public void AddNote()
    {
        bool isMainDroite = HandPicker.isOn;
        bool isPointer = PointerPicker.isOn;
        TypeNote note = this.GetEnumType<TypeNote>(this.NotePicker.GetValue());
        TypeCadenceNote cadence = this.GetEnumType<TypeCadenceNote>(this.CadencePicker.GetValue());
        TypeGamme gamme = this.GetEnumType<TypeGamme>(this.GammePicker.GetValue());
        TypeAlteration alteration = this.GetEnumType<TypeAlteration>(this.AlterationPicker.GetValue());
        this.Compositeur.AddNote(note, cadence, gamme, alteration, isPointer, isMainDroite);
    }

    private E GetEnumType<E>(OptionDataTraductible option) where E : Enum
    {
        foreach (E note in Enum.GetValues(typeof(E)))
        {
            if (note.ToString().ToLower() == option.InitalValue.ToLower())
            {
                return note;
            }
        }

        throw new NotImplementedException();
    }
}
