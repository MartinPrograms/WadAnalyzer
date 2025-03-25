using System.Text;

namespace WadAnalyzer;

public class WadPNames
{
    public List<string> PatchNames { get; } = new List<string>();

    public static WadPNames FromWadLump(WadLump lump)
    {
        var pnames = new WadPNames();
        byte[] data = lump.Data;

        // First 4 bytes = number of patches
        int numPatches = BitConverter.ToInt32(data, 0);

        // Each patch name is 8 bytes (ASCII)
        for (int i = 0; i < numPatches; i++)
        {
            int offset = 4 + (i * 8);
            string name = Encoding.ASCII.GetString(data, offset, 8).TrimEnd('\0');
            pnames.PatchNames.Add(name);
        }

        return pnames;
    }

    public string GetPatchName(int index)
    {
        if (index < 0 || index >= PatchNames.Count)
            throw new IndexOutOfRangeException($"Patch index {index} is out of range");

        return PatchNames[index];
    }

    public int? FindPatchIndex(string name)
    {
        string upperName = name.ToUpperInvariant();
        for (int i = 0; i < PatchNames.Count; i++)
        {
            if (PatchNames[i].Equals(upperName, StringComparison.OrdinalIgnoreCase))
                return i;
        }

        return null;
    }
}