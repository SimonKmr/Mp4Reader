using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mp4Reader
{
    internal class Mp4File
    {
        public string Path { get; set; }

        public Mp4File(string path)
        {
            Path = path;
        }

        public void Read()
        {
            var bytes = File.ReadAllBytes(Path);
            ulong index = 0;

            var rootAtoms = ReadAtoms(index, (ulong)bytes.Length, bytes);
            var recAtoms = ReadAtomsRecursive(index, (ulong)bytes.Length, bytes,rootAtoms);
            Atom.MovieHeaderAtomData mvhd = new Atom.MovieHeaderAtomData(bytes[(int)rootAtoms[2].Children[0].DataStart..(int)rootAtoms[2].Children[0].DataEnd]);
        }
        public void Write()
        {

        }
        
        private Atom[] ReadAtomsRecursive(ulong start, ulong end, byte[] bytes, Atom[] atoms)
        {
            for(int i = 0; i < atoms.Length; i++)
            {
                if (!atoms[i].IsContainer)
                {
                    atoms[i].Children = null;
                    continue;
                }
                atoms[i].Children = ReadAtoms(atoms[i].DataStart, atoms[i].DataEnd, bytes);
                
                for(int j = 0; j < atoms[i].Children.Length; j++)
                {
                    if(atoms[i].Children[j].IsContainer)
                        ReadAtomsRecursive(atoms[i].Children[j].DataStart, atoms[i].Children[j].DataEnd, bytes, atoms[i].Children);
                }
            }
            return atoms;
        }
        private Atom[] ReadAtoms(ulong start, ulong end, byte[] bytes)
        {
            List<Atom> atoms = new List<Atom>();
            ulong index = start;
            while (index < end)
            {
                var atom = ReadAtomHead(index, bytes);
                atoms.Add(atom);
                index += atom.Length;
            }
            return atoms.ToArray();
        }
        private Atom ReadAtomHead(ulong start, byte[] original)
        {
            byte[] size = new byte[8];
            for (ulong i = 0; i < 4; i++) size[i + 4] = original[i + start];
            Array.Reverse(size);
            ulong length = BitConverter.ToUInt64(size);

            byte[] typeB = new byte[4];
            for(ulong i = 0; i < 4; i++) typeB[i] = original[i + start + 4];
            string type = Encoding.UTF8.GetString(typeB, 0, 4);

            if(length == 1)
            {
                size = new byte[8];
                for (ulong i = 0; i < 8; i++) size[i] = original[i + start + 8];
                Array.Reverse(size);
                length = BitConverter.ToUInt64(size);   
            }

            return new Atom(start,length,type);
        }

    }
}
