namespace WadAnalyzer;

/// <summary>
/// Stores the position and angle of a WAD object.
/// </summary>
public class WadTransform
{
    public short X { get; set; }
    public short Y { get; set; }
    public short Angle { get; set; }

    public static WadTransform FromBytes(byte[] bytes)
    {
        return new WadTransform
        {
            X = BitConverter.ToInt16(bytes, 0),
            Y = BitConverter.ToInt16(bytes, 2),
            Angle = BitConverter.ToInt16(bytes, 4)
        };
    }
}