using Exiled.API.Interfaces;

namespace Scp1576SpectatorViewer;

public class Config : IConfig
{
    public bool IsEnabled { get; set; } = true;
    public bool Debug { get; set; } = false;
}