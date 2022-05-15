using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

public class Localization : MonoBehaviour
{
    public enum TypeTrad
    {
        CadenceName,
        NoteName,
        GammeName,
        AlternationName,
        QuelleMain,

        BoutonText
    }

    private static Traduction TraductionNoteName = new Traduction();
    private static Traduction TraductionCadenceName = new Traduction();
    private static Traduction TraductionGammeName = new Traduction();
    private static Traduction TraductionAlternationName = new Traduction();
    private static Traduction TraductionQuelleMain = new Traduction();
    private static Traduction TraductionBoutonText = new Traduction();

    private static Locale Locale;

    private static Localization instance;

    public static Localization Instance => instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public static void Start()
    {
        LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
        LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
        Locale locale = LocalizationSettings.SelectedLocale;
        foreach (TypeTrad trad in Enum.GetValues(typeof(TypeTrad)))
        {
            Traduction traduction = GetTraduction(trad);
            LoadTrad(trad, locale);
        }
    }

    public static void Substribe(TypeTrad trad, Action<Dictionary<string, string>> funcOnLocalChange)
    {
        GetTraduction(trad).CallBack.Add(funcOnLocalChange);
    }

    public static void Unsubstribe(TypeTrad trad, Action<Dictionary<string, string>> funcOnLocalChange)
    {
        GetTraduction(trad).CallBack.Remove(funcOnLocalChange);
    }

    public static string GetTrad(TypeTrad trad, string name)
    {
        Traduction traduction = GetTraduction(trad);
        if (Locale == null)
        {
            Locale = LocalizationSettings.SelectedLocale;
        }

        if (!traduction.Tards.ContainsKey(Locale))
        {
            LoadTrad(trad, Locale);
        }
        else if (traduction.Tards[Locale].ContainsKey(name))
        {
            return traduction.Tards[Locale][name];
        }

        return name;
    }

    private static Traduction GetTraduction(TypeTrad trad)
    {
        switch (trad)
        {
            case TypeTrad.GammeName:
                return TraductionGammeName;
            case TypeTrad.AlternationName:
                return TraductionAlternationName;
            case TypeTrad.BoutonText:
                return TraductionBoutonText;
            case TypeTrad.CadenceName:
                return TraductionCadenceName;
            case TypeTrad.NoteName:
                return TraductionNoteName;
            case TypeTrad.QuelleMain:
                return TraductionQuelleMain;
            default:
                return new Traduction();
        }
    }

    private static void OnLocaleChanged(Locale locale)
    {
        Locale = locale;
        foreach (TypeTrad trad in Enum.GetValues(typeof(TypeTrad)))
        {
            Traduction traduction = GetTraduction(trad);
            LoadTrad(trad, locale);
            UpdateUsedTrads(locale, traduction.Tards, traduction.CallBack);
        }
    }

    private static void LoadTrad(TypeTrad trad, Locale locale)
    {
        Traduction traduction = GetTraduction(trad);
        if (!traduction.Tards.ContainsKey(locale))
        {
            traduction.Tards[locale] = new Dictionary<string, string>();
        }
        else
        {
            return;
        }

        var getAsync = LocalizationSettings.StringDatabase.GetTableAsync(trad.ToString(), locale);
        if (getAsync.IsDone)
        {
            UpdateTrad(locale, getAsync.Result.Values, traduction.Tards, traduction.CallBack);
        }
        else
        {
            getAsync.Completed += (op) =>
            {
                UpdateTrad(locale, op.Result.Values, traduction.Tards, traduction.CallBack);
            };
        }
    }

    private static void UpdateTrad(Locale locale, ICollection<StringTableEntry> localizedStringTable, Dictionary<Locale, Dictionary<string, string>> tards, HashSet<Action<Dictionary<string, string>>> actions)
    {
        foreach (var line in localizedStringTable)
        {
            tards[locale][line.Key] = line.Value;
        }

        UpdateUsedTrads(locale, tards, actions);
    }

    private static void UpdateUsedTrads(Locale locale, Dictionary<Locale, Dictionary<string, string>> tards, HashSet<Action<Dictionary<string, string>>> actions)
    {
        foreach (Action<Dictionary<string, string>> callBack in actions)
        {
            callBack(tards[locale]);
        }
    }
}

public class Traduction
{
    public Dictionary<Locale, Dictionary<string, string>> Tards = new Dictionary<Locale, Dictionary<string, string>>();

    public HashSet<Action<Dictionary<string, string>>> CallBack = new HashSet<Action<Dictionary<string, string>>>();
}
