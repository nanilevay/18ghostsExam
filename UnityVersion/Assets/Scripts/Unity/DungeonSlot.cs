using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _18ghostsExam;

/// <summary>
/// This class allows us to define a dungeon slot and determine whether it's
/// Empty or not
/// </summary>
namespace _18ghostsExam
{
    public class DungeonSlot : MonoBehaviour
    {
        public bool empty = true;

        public char Character = (char)Characters.map;

        public IGhostBase GhostInSlot;
    }
}