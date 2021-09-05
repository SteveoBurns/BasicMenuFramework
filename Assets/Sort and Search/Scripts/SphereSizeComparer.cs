using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Compares the size value of the Sphere class.
/// </summary>
public class SphereSizeComparer : Comparer<Sphere>
{
    
    public override int Compare(Sphere _x, Sphere _y)
    {
        if(_x.size > _y.size)
            return 1;
        if(_x.size < _y.size)
            return -1;
        return 0;
        
    }
}
