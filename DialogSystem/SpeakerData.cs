using System.Collections.Generic;
using Newtonsoft.Json;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DialogSystem;

public enum SpeakerPosition
{
    Center,
    Left,
    Right,
    Offscreen
}

/// <summary>
/// Data class that stores speaker info that is loaded from config files
/// </summary>
public class SpeakerData
{
    /// <summary>
    /// This name will be displayed as current speaker name
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// Name of the texture, from TextureStateNames list, that will be loaded by default
    /// </summary>
    public string DefaultTextureName { get; set; } = string.Empty;

    /// <summary>
    /// Textures that need to be loaded by the speaker
    /// </summary>
    public Dictionary<string, string> TextureStateNames { get; set; } = new Dictionary<string, string>();
}