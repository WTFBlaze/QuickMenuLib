using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using static MelonLoader.MelonLogger;

namespace PepsiLibTestMod
{
    public class PepsiLibTestModMod : MelonMod
    {
        public override void OnApplicationStart()
        {
            PepsiLib.PepsiLibMod.RegisterModMenu(new MyModMenu());
        }
    }
}