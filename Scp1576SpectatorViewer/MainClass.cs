using System;
using Exiled.API.Features;
using HarmonyLib;

namespace Scp1576SpectatorViewer;

public class MainClass : Plugin<Config>
{
    public override string Author => "xNexusACS";
    public override string Name => "Scp1576SpectatorViewer";
    public override string Prefix => "Scp1576SpectatorViewer";
    public override Version Version => new(1, 0, 0);
    public override Version RequiredExiledVersion => new(7, 0, 0);
    
    public Harmony Harmony { get; private set; }
    
    public override void OnEnabled()
    {
        Harmony = new Harmony($"xnexusacs.scp1576spectatorviewer");
        Harmony.PatchAll();
        
        base.OnEnabled();
    }
    
    public override void OnDisabled()
    {
        Harmony.UnpatchAll(Harmony.Id);
        Harmony = null;
        
        base.OnDisabled();
    }
}