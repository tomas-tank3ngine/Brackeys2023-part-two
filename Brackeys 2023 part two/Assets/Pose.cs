using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Pose", menuName = "Pose")]
public class Pose : ScriptableObject
{
    public new string name;
    public string keyInput;
    public Sprite sprite;
}
