namespace Mp4Reader
{
    public partial class Atom
    {
        public class SoundMediaInformationHeader
        {
            public const string Type = "smhd";
            //smhd
            public Byte Version { get; set; }
            public UInt16 Balance { get; set; }

            public SoundMediaInformationHeader(byte[] bytes)
            {
                Version = bytes[0];
                Balance = BitConverter.ToUInt16(Utility.ReverseRange(bytes[4..6]));
            }
        }
    }
}
