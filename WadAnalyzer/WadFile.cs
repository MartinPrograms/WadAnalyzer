namespace WadAnalyzer;

/// <summary>
/// A WAD file. 
/// </summary>
public class WadFile
{
    private byte[] _data;
    private string _name;

    public WadHeader Header { get; set; }
    public string Name => _name;
    
    public List<WadLump> Lumps { get; set; }

    public Dictionary<string, WadLevel> Levels; // Map of lumps per level
    public List<string> LevelNames => Levels.Keys.ToList();
    
    // Textures:
    public Dictionary<string, WadTexture> Textures = new Dictionary<string, WadTexture>();
    public List<string> TextureNames => Textures.Keys.ToList();
    
    // Patches:
    public Dictionary<string, WadPatch> Patches = new Dictionary<string, WadPatch>();
    public List<string> PatchNames => Patches.Keys.ToList();
    
    // Palettes:
    public Dictionary<string, WadPalette> Palettes = new Dictionary<string, WadPalette>();
    public List<string> PaletteNames => Palettes.Keys.ToList();

    public WadFile(byte[] data, string name)
    {
        _data = data;
        _name = name;

        Header = WadHeader.FromBytes(data);
        Lumps = new List<WadLump>();
        Levels = new Dictionary<string, WadLevel>();
        
        LoadLumps();
        SortLevels();
        LoadTextures();
        LoadPalettes();
    }

    private void LoadPalettes()
    {
        foreach (var lump in Lumps)
        {
            if (lump.IsPalette())
            {
                var palette = WadPalette.FromWadLump(lump);
                Palettes[palette.Name.Trim().ToUpper()] = palette;
            }
        }
    }

    private void LoadTextures()
    {
        WadPNames pnames = null;
        HashSet<string> patchNames = new HashSet<string>();
        
        var pnamesLump = Lumps.FirstOrDefault(l => l.Name == "PNAMES");
        if (pnamesLump != null)
        {
            pnames = WadPNames.FromWadLump(pnamesLump);
            patchNames = new HashSet<string>(pnames.PatchNames);
        }
        
        foreach (var lump in Lumps)
        {
            // Check if this lump is a patch (name exists in PNAMES)
            if (patchNames.Contains(lump.Name.Trim().ToUpper()))
            {
                var patch = WadPatch.FromWadLump(lump);
                Patches[lump.Name.Trim().ToUpper()] = patch; // Store by uppercase name
            }
        }
        
        foreach (var lump in Lumps)
        {
            if (lump.IsTexture())
            {
                var textures = WadTexture.FromWadLump(lump);
                foreach (var texture in textures)
                {
                    // Resolve patches using PNAMES and loaded patch data
                    foreach (var patchRef in texture.Patches)
                    {
                        if (pnames != null && patchRef.PatchId < pnames.PatchNames.Count)
                        {
                            string patchName = pnames.PatchNames[patchRef.PatchId].Trim().ToUpper();
                            if (Patches.TryGetValue(patchName, out var patch))
                            {
                                if (!texture.ResolvedPatches.Contains(patch))
                                    texture.ResolvedPatches.Add(patch);
                            }
                        }
                    }
                    Textures[texture.Name.Trim().ToUpper()] = texture;
                }
            }
        }
    }

    private void SortLevels()
    {
        string currentLevel = null;
        foreach (var lump in Lumps)
        {
            if (lump.IsLevelMarker())
            {
                currentLevel = lump.Name;
                continue;
            }
            
            if (!lump.IsLevelProperty())
            {
                currentLevel = null;
                continue;
            }

            if (currentLevel != null)
            {
                if (!Levels.ContainsKey(currentLevel))
                {
                    Levels[currentLevel] = new WadLevel();
                }

                Levels[currentLevel].Lumps.Add(lump);
            }
        }
    }

    private void LoadLumps()
    {
        for (int i = 0; i < Header.NumLumps; i++)
        {
            WadLump lump = WadLump.FromBytes(_data, Header.InfoTableOffset + i * 16);
            Lumps.Add(lump);
        }
        
        foreach (var entry in Lumps)
        {
            Array.Copy(_data, entry.Offset, entry.Data, 0, entry.Size);
        }
    }

    public static WadFile FromFile(string path)
    {
        return new WadFile(File.ReadAllBytes(path), Path.GetFileNameWithoutExtension(path));
    }
}