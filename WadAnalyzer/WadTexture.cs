using System.Text;
using SkiaSharp;
using WadAnalyzer;

/// <summary>
/// Represents a composite wall texture from TEXTURE1/TEXTURE2.
/// </summary>
public class WadTexture
{
    public string Name { get; set; } = string.Empty;
    public ushort Masked { get; set; }  // Unused in vanilla Doom
    public ushort Width { get; set; }
    public ushort Height { get; set; }
    public ushort ColumnDirectory { get; set; }  // Unused
    public List<WadTexturePatch> Patches { get; set; } = new();
    public List<WadPatch> ResolvedPatches { get; set; } = new();
    public static WadTexture[] FromWadLump(WadLump wadLump)
    {
        byte[] data = wadLump.Data;
        int numTextures = BitConverter.ToInt32(data, 0);
        int[] offsets = new int[numTextures];

        // Read texture offsets (4 bytes each starting at offset 4)
        for (int i = 0; i < numTextures; i++)
        {
            offsets[i] = BitConverter.ToInt32(data, 4 + i * 4);
        }

        WadTexture[] textures = new WadTexture[numTextures];
    
        for (int i = 0; i < numTextures; i++)
        {
            int offset = offsets[i];
            textures[i] = new WadTexture
            {
                Name = Encoding.ASCII.GetString(data, offset, 8).TrimEnd('\0'),
                Masked = BitConverter.ToUInt16(data, offset + 8),
                Width = BitConverter.ToUInt16(data, offset + 12), // Was offset +10
                Height = BitConverter.ToUInt16(data, offset + 14), // Was offset +12
                ColumnDirectory = BitConverter.ToUInt16(data, offset + 16) // Was offset +14
            };

            // CORRECTED: Read number of patches from offset +20
            ushort numPatches = BitConverter.ToUInt16(data, offset + 20);
            int patchStart = offset + 22; // Patches start immediately after numPatches
        
            for (int j = 0; j < numPatches; j++)
            {
                int patchOffset = patchStart + (j * 10); // 10 bytes per patch
                textures[i].Patches.Add(new WadTexturePatch
                {
                    OriginX = BitConverter.ToInt16(data, patchOffset),
                    OriginY = BitConverter.ToInt16(data, patchOffset + 2),
                    PatchId = BitConverter.ToUInt16(data, patchOffset + 4),
                    StepDir = BitConverter.ToUInt16(data, patchOffset + 6),
                    ColorMap = BitConverter.ToUInt16(data, patchOffset + 8)
                });
            }
        }

        return textures;
    }
}

public class WadTexturePatch
{
    public short OriginX { get; set; }
    public short OriginY { get; set; }
    public ushort PatchId { get; set; }  // Index into PNAMES list
    public ushort StepDir { get; set; }   // Unused (always 8)
    public ushort ColorMap { get; set; }  // Unused (always 0)
}