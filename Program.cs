namespace ArtCollectionManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] artworkArray = new int[10];
            int arrayCount = 0;

            LoadArtWorks();
        }
        // Load Artworks
        static void LoadArtWorks()
        {
            try
            {
                using (StreamReader reader = new StreamReader("C:/Users/abere/Desktop/source/repos/assignments/ArtCollectionManager/art_collection_basic.txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            catch (Exception e)
            {
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
