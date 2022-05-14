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
        Cadence,
        NoteName,
    }

    private static Locale Locale;

    private static Dictionary<Locale, Dictionary<string, string>> TardsNoteName = new Dictionary<Locale, Dictionary<string, string>>();

    private static HashSet<Action<Dictionary<string, string>>> NoteNameCallBack = new HashSet<Action<Dictionary<string, string>>>();

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
        LoadNoteName(locale);
    }

    public static void SubstribeToNoteName(Action<Dictionary<string, string>> funcOnLocalChange)
    {
        NoteNameCallBack.Add(funcOnLocalChange);
    }

    public static void UnsubstribeToNoteName(Action<Dictionary<string, string>> funcOnLocalChange)
    {
        NoteNameCallBack.Remove(funcOnLocalChange);
    }

    public static string GetTradNoteName(string noteName)
    {
        if (Locale == null)
        {
            Locale = LocalizationSettings.SelectedLocale;
        }

        if (!TardsNoteName.ContainsKey(Locale))
        {
            LoadNoteName(Locale);
        }
        else if (TardsNoteName[Locale].ContainsKey(noteName))
        {
            return TardsNoteName[Locale][noteName];
        }

        return noteName;
    }

    private static void OnLocaleChanged(Locale locale)
    {
        Locale = locale;
        LoadNoteName(locale);
        UpdateUsedTrads(locale, TardsNoteName, NoteNameCallBack);
    }

    private static void LoadNoteName(Locale locale)
    {
        if (!TardsNoteName.ContainsKey(locale))
        {
            TardsNoteName[locale] = new Dictionary<string, string>();
        }
        else
        {
            return;
        }

        var getAsync = LocalizationSettings.StringDatabase.GetTableAsync(TypeTrad.NoteName.ToString(), locale);
        if (getAsync.IsDone)
        {
            UpdateTrad(locale, getAsync.Result.Values, TardsNoteName, NoteNameCallBack);
        }
        else
        {
            getAsync.Completed += (op) =>
            {
                UpdateTrad(locale, op.Result.Values, TardsNoteName, NoteNameCallBack);
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
