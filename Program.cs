using Mp4Reader;


for(int i = 0; i < 50; i++)
{
    Mp4File mp4 = new Mp4File($@"C:\Users\Simon\Desktop\MontageBotDownloadFolderTest\{i}.mp4");
    var data = mp4.Read();
    var vidInfo = data[1].DataRaw;
    var leafs = Mp4File.GetLeafAtoms(data);

    string duration = String.Empty;
    string ratio = String.Empty;
    foreach (var a in leafs)
    {
        if (a.Type == Atom.MovieHeader.Type)
        {
            var mvhd = a.Data as Atom.MovieHeader;
            duration = $"{mvhd.Duration / mvhd.TimeScale}";
        }
        if (a.Type == Atom.TrackHeader.Type)
        {
            var tkhd = a.Data as Atom.TrackHeader;
            if (tkhd.TrackWidth + tkhd.TrackHeight == 0) continue;
            ratio = $"{tkhd.TrackWidth} : {tkhd.TrackHeight}";
        }
    }
    Console.WriteLine($"{i}.mp4\t\t{duration}\t\t{ratio}");
}
//File.WriteAllBytes("vidInfo.txt", vidInfo);
Console.WriteLine("done");