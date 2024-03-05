using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DialogueText
{
    public static string[] TUTORIAL_CHECKPOINT_INTRODUCTION = new string[]
    {
        "The yellow line on the ground marked by the arrow is the checkpoint.",
        "There are multiple checkpoints marked throughout the map.",
        "You can use them to respawn on the checkpoint mark after passing through it."
    };

    public static string[] TUTORIAL_JUMP_HELPER = new string[]
    {
        "You can jump through holes and obstacles with Space."
    };

    public static string[] TUTORIAL_DOUBLE_JUMP_HELPER = new string[]
    {
        "Some holes are too big to go through a single jump.",
        "For bigger jumps, press two times the Space.",
    };

    public static string[] TUTORIAL_JUMP_WALRUN_HELPER = new string[]
    {
        "You can run on walls if you jump onto them laterally.",
        "Be careful, if you run straight against the wall, you will climb it."
    };

    public static string[] TUTORIAL_DOUBLE_JUMP_WALRUN_HELPER = new string[]
    {
        "Some walls are bigger then others, perform a double jump before running on the wall."
    };

    public static string[] TUTORIAL_DOUBLE_JUMP_WALRUN_FAIL_HELPER = new string[]
    {
        "When running on walls, if you run away from the wall, the wallrun stops."
    };

    public static string[] TUTORIAL_WALLCLIMB_HELPER = new string[]
    {
        "Run straight against a wall to climb it."
    };

    public static string[] TUTORIAL_JUMP_WALLCLIMB_HELPER = new string[]
    {
        "Some walls are bigger then others.",
        "Try and jump straight against a wall to climb higher walls."
    };

    public static string[] TUTORIAL_MULTIPLE_WALLCLIMB_HELPER = new string[]
    {
        "One way to gain altitude is to use multiple walls in succession to climb.",
        "Try and find spots where this is possible."
    };

    public static string[] TUTORIAL_MULTIPLE_WALLRUN_HELPER = new string[]
    {
        "Similar to climbing multiple walls in succession, you can also wall run on multiple walls."
    };
}
