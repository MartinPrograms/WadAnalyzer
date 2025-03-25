using System.Text;

namespace WadAnalyzer;

/// <summary>
/// Also named "Lump" in some games.
/// </summary>
public class WadLump(string name, int offset, int size)
{
    public string Name { get; set; } = name;
    public int Offset { get; set; } = offset;
    public int Size { get; set; } = size;
    
    public byte[] Data { get; set; } = new byte[size];

    public static WadLump FromBytes(byte[] data, int arrayOffset)
    {
        var offset = BitConverter.ToInt32(data, arrayOffset);
        var size = BitConverter.ToInt32(data, arrayOffset + 4);
        var name = Encoding.ASCII.GetString(data, arrayOffset + 8, 8).TrimEnd('\0');
        
        return new WadLump(name, offset, size);
    }
}