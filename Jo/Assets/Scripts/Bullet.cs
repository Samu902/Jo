using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    Normal, Sniper, Area, Hit //eccetera: normale, raggio più ampio, danno ad area, danno + spostamento per il target
}

public class Bullet : MonoBehaviour
{
    public BulletType type;
    public float damage;
    //---in tiles-----
    [Range(-2, 2)]
    public int additionalRange;
    [Range(0, 2)]
    public int damageSize;
    [Range(0, 2)]
    public int recoilSize;
}
