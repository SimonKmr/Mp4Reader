namespace Mp4Reader
{
    public partial class Atom
    {
        public ulong Index { get; set; }
        public ulong Length { get; set; }
        public string Type { get; set; }
        
        
        public bool IsContainer { get => IsContainerAtom(Type); }
        public ulong DataStart { get => Index + 8; }
        public ulong DataEnd { get => Index + Length; }
        
        
        public Atom(ulong index, ulong length, string type)
        {
            Index = index;
            Length = length;
            Type = type;
        }

        public Atom[] Children { get; set; }

        private static bool IsContainerAtom(string type)
        {
            switch (type)
            {
                case "moov": return true;
                case "clip": return true;
                case "udta": return true;
                case "trak": return true;
                case "matt": return true;
                case "edts": return true;
                case "mdia": return true;
                case "minf": return true;
                case "dinf": return true;
                case "stbl": return true;
            }
            return false;
        }

        public class TrackHeaderAtom
        {

        }

    }
}
