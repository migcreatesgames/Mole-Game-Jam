using System;
using UnityEngine;

/// <summary>
/// static class that store generic UI action events. 
/// </summary>
public static class UIEvents
{
    static public Action<CanvasGroup> OnHUDDisplay;
    static public Action<CanvasGroup> OnHUDHide;

    static public Action<CanvasGroup> OnMenuOpened;
    static public Action<CanvasGroup> OnMenuClosed;
}