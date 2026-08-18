using UnityEngine;

public class CasualLevelGenerator : MonoBehaviour
{
    private const string ConfigResourcePath = "PlayModeLevels/PlayModeLevels";

    [Tooltip("Valore >= 0 rende la generazione riproducibile. Lasciare -1 in produzione.")]
    [SerializeField] private int fixedSeed = -1;

    private static CasualLevelGenerator _instance;
    public static CasualLevelGenerator Instance => _instance;

    private CasualLevelConfigJson config;
}
