using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAsset : MonoBehaviour
{
    public static ItemAsset Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Sprite s_Wood;
    public Sprite s_Log;
    public Sprite s_WoodStick;
    public Sprite s_CobbleStone;
    public Sprite s_Web;
    public Sprite s_Carrot;
    public Sprite s_FishingRod;
    public Sprite s_CarrotFishingRod;
    public Sprite s_StonePickax;
    public Sprite s_WoodPickax;
}
