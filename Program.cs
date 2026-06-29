namespace ArtCollectionManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[][] artworkArray = new string[10][];
            HashSet<string> artistNames = new HashSet<string>();
            HashSet<string> mediums = new HashSet<string>();
            int arrayCount = 0;

            Start(artworkArray, artistNames, mediums, ref arrayCount);
        }

        static void Start(string[][] artworkArray, HashSet<string> artistNames, HashSet<string> mediums, ref int arrayCount)
        {
            LoadArtWorks(artworkArray, artistNames, mediums, ref arrayCount);
            SortByArtist(artworkArray, ref arrayCount);
            MainMenu();
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
        static void LoadArtWorks(string[][] artworkArray, HashSet<string> artistNames, HashSet<string> mediums, ref int arrayCount)
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
                        artistNames.Add(splitLine[1]);
                        mediums.Add(splitLine[3]);

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

        static void MainMenu()
        {
            Console.WriteLine("Art Collection Database");
            Console.WriteLine();

            bool programRunning = true;

            while (programRunning)
            {
                Console.WriteLine("Get Started:");
                Console.WriteLine("1. View All Artwork");
                Console.WriteLine("2. Add New Artwork");
                Console.WriteLine("3. Search For an Artwork by Artist");
                Console.WriteLine("4. Exit");
            }
        }

        // Get User Input
        static string[] GetUserInput(HashSet<string> mediums)
        {
            string[] artDetails = new string[4];

            Console.WriteLine("Enter art name");
            string artName = Console.ReadLine().Trim();
            artDetails[0] = artName;
            Console.WriteLine();

            Console.WriteLine("Enter artist name");
            string artistName = Console.ReadLine().Trim();
            artDetails[1] = artistName;
            Console.WriteLine();

            Console.WriteLine("Enter the art year");
            string artYear = Console.ReadLine().Trim();
            artDetails[2] = artYear;
            Console.WriteLine();

            List<string> mediumList = mediums.ToList();
            bool correct;
            int chosenMedium;

            do
            {
                Console.WriteLine("Choose a Medium:");
                for (int i = 0; i < mediumList.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {mediumList[i]}");
                }
                Console.WriteLine($"{mediumList.Count + 1}. Other");

                correct = int.TryParse(Console.ReadLine(), out chosenMedium)
                    && chosenMedium >= 1
                    && chosenMedium <= mediumList.Count + 1;
            }
            while (!correct);
            if (chosenMedium <= mediumList.Count)
            {
                artDetails[3] = mediumList[chosenMedium - 1];
            }
            else
            {
                artDetails[3] = "Other";
            }

            return artDetails;
        }

        // Add New Artwork
        static void AddNewArtwork(string[][] artworkArray, HashSet<string> mediums, ref int arrayCount)
        {
            string[] newArtwork = GetUserInput(mediums);
            int index = arrayCount - 1;

            while (index >= 0 && artworkArray[index][1].CompareTo(newArtwork[1]) > 0)
            {
                artworkArray[index + 1] = artworkArray[index];
                index--;
            }
            artworkArray[index + 1] = newArtwork;
            arrayCount++;
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
