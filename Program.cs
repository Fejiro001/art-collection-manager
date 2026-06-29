namespace ArtCollectionManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[][] artworkArray = new string[10][];
            HashSet<string> artistNames = new HashSet<string>();
            int arrayCount = 0;

            Start(artworkArray, artistNames, ref arrayCount);
        }

        static void Start(string[][] artworkArray, HashSet<string> artistNames, ref int arrayCount)
        {
            LoadArtWorks(artworkArray, artistNames, ref arrayCount);
            SortByArtist(artworkArray, ref arrayCount);
        }

        static string[][] CreateBiggerArray(string[][] arr, int count)
        {
            if (count == arr.Length)
            {
                string[][] biggerArray = new string[arr.Length * 2][];
                for (int i = 0; i < arr.Length; i++)
                {
                    biggerArray[i] = arr[i];
                }
                return biggerArray;
            }
            return arr;
        }

        // Load Artworks
        static void LoadArtWorks(string[][] artworkArray, HashSet<string> artistNames, ref int arrayCount)
        {
            try
            {
                using (StreamReader reader = new StreamReader("C:/Users/abere/Desktop/source/repos/assignments/ArtCollectionManager/art_collection_basic.txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] splitLine = line.Split(" | ");

                        artworkArray = CreateBiggerArray(artworkArray, arrayCount);

                        artworkArray[arrayCount] = splitLine;
                        artistNames.Add(splitLine[1]);
                        arrayCount++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read.");
                Console.WriteLine(e.Message);
            }
        }

        // Sort Artworks By Artist
        static void SortByArtist(string[][] artworkArray, ref int arrayCount)
        {
            for (int i = 1; i < arrayCount; i++)
            {
                string[] keyRow = artworkArray[i];
                int j = i - 1;

                while (j >= 0 && artworkArray[j][1].CompareTo(keyRow[1]) > 0)
                {
                    artworkArray[j + 1] = artworkArray[j];
                    j--;
                }
                artworkArray[j + 1] = keyRow;
            }
        }

        // Add New Artwork
        static void AddNewArtwork()
        {

        }
        // Search For Artwork By Artist
        static void SearchForArtwork()
        {

        }
        // Save Artworks to new file
        static void SaveArtworks()
        {

        }
    }
}
