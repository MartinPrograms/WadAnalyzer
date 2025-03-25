using SkiaSharp;
using WadAnalyzer;

public class WadPatch
{
    public short Width { get; set; }
    public short Height { get; set; }
    public short LeftOffset { get; set; }
    public short TopOffset { get; set; }
    public byte[][] Pixels { get; set; } // Column-major 8-bit palette indices

    public static WadPatch FromWadLump(WadLump patchLump)
    {
        byte[] data = patchLump.Data;
        var patch = new WadPatch
        {
            Width = BitConverter.ToInt16(data, 0),
            Height = BitConverter.ToInt16(data, 2),
            LeftOffset = BitConverter.ToInt16(data, 4),
            TopOffset = BitConverter.ToInt16(data, 6)
        };

        // Read column offsets
        int[] columnOffsets = new int[patch.Width];
        for (int i = 0; i < patch.Width; i++)
        {
            columnOffsets[i] = BitConverter.ToInt32(data, 8 + i * 4);
        }

        // Read column data
        patch.Pixels = new byte[patch.Width][];
        for (int x = 0; x < patch.Width; x++)
        {
            int offset = columnOffsets[x];
            List<byte> column = new List<byte>();

            while (true)
            {
                byte rowStart = data[offset++];
                if (rowStart == 0xFF) break; // End of column

                byte pixelCount = data[offset++];
                offset++; // Skip unused byte

                for (int i = 0; i < pixelCount; i++)
                {
                    column.Add(data[offset++]);
                }

                offset++; // Skip unused byte
            }

            patch.Pixels[x] = column.ToArray();
        }

        return patch;
    }

    public bool Export(string path, SKColor[] toSkiaSharpPalette)
    {
        using SKBitmap bitmap = new SKBitmap(Width, Height);
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                byte pixel = Pixels[x][y];
                SKColor color = toSkiaSharpPalette[pixel];
                bitmap.SetPixel(x, y, color);
            }
        }

        using SKImage image = SKImage.FromBitmap(bitmap);
        using SKData data = image.Encode();
        using FileStream file = File.Create(path);
        data.SaveTo(file);

        return true;
    }
}