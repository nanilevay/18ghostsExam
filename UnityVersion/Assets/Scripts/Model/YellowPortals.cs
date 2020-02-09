using System.Collections;
using System.Collections.Generic;
using _18ghostsExam;
using UnityEngine;

/// <summary>
/// this class allows us to define a yellow portal with its initial rotation
/// and colour
/// </summary>
public class YellowPortals : PortalBase
{
    /// <summary>
    /// sets the portal with the initial direction facing right
    /// </summary>
    public YellowPortals()
    {
        // current starting rotation of the portal
        CurrentRot = PortalDir.right;
    }
}
