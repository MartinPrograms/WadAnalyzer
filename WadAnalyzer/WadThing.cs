namespace WadAnalyzer;

public class WadThing
{
    public WadTransform WadTransform { get; set; }
    public ushort Type { get; set; }
    public ushort Flags { get; set; }
    
    public static WadThing[] FromWadLump(WadLump wadLump)
    {
        List<WadThing> things = new List<WadThing>();
        
        // Create a new Thing
        for (int i = 0; i < wadLump.Size / 10; i++)
        {
            var thing = new WadThing();
            var data = wadLump.Data;
            
            thing.WadTransform = WadAnalyzer.WadTransform.FromBytes(data[..6]);
            thing.Type = BitConverter.ToUInt16(data[6..8]);
            thing.Flags = BitConverter.ToUInt16(data[8..10]);
            
            things.Add(thing);
        }
        
        return things.ToArray();
    }
}

