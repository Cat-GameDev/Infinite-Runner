using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cache
{

    private static Dictionary<Collider, Coin> coins = new Dictionary<Collider, Coin>();

    public static Coin GetCoin(Collider collider)
    {
        if (!coins.ContainsKey(collider))
        {
            coins.Add(collider, collider.GetComponent<Coin>());
        }

        return coins[collider];
    }

    private static Dictionary<Collider, Object> objects = new Dictionary<Collider, Object>();

    public static Object GetObject(Collider collider)
    {
        if (!objects.ContainsKey(collider))
        {
            objects.Add(collider, collider.GetComponent<Object>());
        }

        return objects[collider];
    }




}


