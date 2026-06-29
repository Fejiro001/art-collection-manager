namespace ArtCollectionManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[][] artworkArray = new string[10][];
            int arrayCount = 0;

            Start(artworkArray, ref arrayCount);
        }

        static void Start(string[][] artworkArray, ref int arrayCount)
        {
            LoadArtWorks(artworkArray, ref arrayCount);
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
        static void LoadArtWorks(string[][] artworkArray, ref int arrayCount)
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
        // Sort Artworks By Year
        static void SortByYear()
        {

        }
        // Add New Artwork
        static void AddNewArtwork()
        {

        }
        // Search For Artwork By Year
        static void SearchForArtwork()
        {

        }
        // Save Artworks to new file
        static void SaveArtworks()
        {

        }
    }
}
