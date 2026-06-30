namespace ArtCollectionManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int INITIAL_CAPACITY = 10;
            string[][] artworkArray = new string[INITIAL_CAPACITY][];
            HashSet<string> artistNames = new HashSet<string>();
            HashSet<string> mediums = new HashSet<string>();
            int arrayCount = 0;

            Start(artworkArray, artistNames, mediums, ref arrayCount);
        }

        static void Start(string[][] artworkArray, HashSet<string> artistNames, HashSet<string> mediums, ref int arrayCount)
        {
            LoadArtWorks(artworkArray, artistNames, mediums, ref arrayCount);
            SortByArtist(artworkArray, ref arrayCount);
            MainMenu(artworkArray, artistNames, mediums, ref arrayCount);
        }

        // Display main menu to the user
        static void MainMenu(string[][] artworkArray, HashSet<string> artistNames, HashSet<string> mediums, ref int arrayCount)
        {
            bool programRunning = true;

            while (programRunning)
            {
                Console.WriteLine("\n--- Art Collection Database ---");
                Console.WriteLine("1. View All Artwork");
                Console.WriteLine("2. Add New Artwork");
                Console.WriteLine("3. Search For an Artwork by Artist");
                Console.WriteLine("4. Save and Exit");
                Console.WriteLine("\nSelect an option (1 - 4):");

                bool correct = int.TryParse(Console.ReadLine().Trim(), out int choice);

                switch (choice)
                {
                    case 1:
                        PrintHeader("Viewing All Artwork");
                        DisplayArtwork(artworkArray, ref arrayCount);
                        break;
                    case 2:
                        PrintHeader("Add New Artwork");
                        AddNewArtwork(ref artworkArray, mediums, artistNames, ref arrayCount);
                        break;
                    case 3:
                        PrintHeader("Search Artwork");
                        SearchForArtwork(artworkArray, artistNames, ref arrayCount);
                        break;
                    case 4:
                        PrintHeader("Saving Data");
                        SaveArtworks(artworkArray, ref arrayCount);
                        programRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }

        // Load Artworks
        static void LoadArtWorks(string[][] artworkArray, HashSet<string> artistNames, HashSet<string> mediums, ref int arrayCount)
        {
            try
            {
                using (StreamReader reader = new StreamReader("../../../art_collection_basic.txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] splitLine = line.Split(" | ");

                        CreateBiggerArray(ref artworkArray, arrayCount);

                        artworkArray[arrayCount] = splitLine;

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

        // Save Artworks to new file
        static void SaveArtworks(string[][] artworkArray, ref int arrayCount)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("../../../sorted_art_collection.txt"))
                {
                    for (int i = 0; i < arrayCount; i++)
                    {
                        string line = string.Join(" | ", artworkArray[i]);
                        writer.WriteLine(line);
                    }
                }
                Console.WriteLine("File Saved Successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be saved.");
                Console.WriteLine(e.Message);
            }
        }

        static void CreateBiggerArray(ref string[][] arr, int count)
        {
            if (count == arr.Length)
            {
                string[][] biggerArray = new string[arr.Length * 2][];
                for (int i = 0; i < arr.Length; i++)
                {
                    biggerArray[i] = arr[i];
                }
                arr = biggerArray;
            }
        }

        // Sort Artworks By Artist (Insertion Sort)
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

        // Add New Artwork (Ordered Insertion)
        static void AddNewArtwork(ref string[][] artworkArray, HashSet<string> mediums, HashSet<string> artistNames, ref int arrayCount)
        {
            string[] newArtwork = GetUserInput(mediums, artistNames);
            int index = arrayCount - 1;
            CreateBiggerArray(ref artworkArray, arrayCount);

            // Shift existing elements to the right to maintain alphabetical order by artist
            while (index >= 0 && artworkArray[index][1].CompareTo(newArtwork[1]) > 0)
            {
                artworkArray[index + 1] = artworkArray[index];
                index--;
            }
            artworkArray[index + 1] = newArtwork;
            arrayCount++;
        }

        // Search For Artwork By Artist
        static void SearchForArtwork(string[][] artworkArray, HashSet<string> artistNames, ref int arrayCount)
        {
            List<string> artistList = artistNames.ToList();
            bool correct;
            int chosenArtist;

            do
            {
                Console.WriteLine("\nWhat artists work are you searching for?");
                for (int i = 0; i < artistList.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {artistList[i]}");
                }

                correct = int.TryParse(Console.ReadLine(), out chosenArtist)
                    && chosenArtist >= 1
                    && chosenArtist <= artistList.Count;

                if (!correct)
                {
                    Console.WriteLine($"Error: Please enter a valid number (1 - {arrayCount}).");
                }
            }
            while (!correct);

            string searchArtist = artistList[chosenArtist - 1];
            int targetIndex = BinarySearch(searchArtist, artworkArray, arrayCount);

            if (targetIndex == -1)
            {
                Console.WriteLine("No records found");
                return;
            }

            // Perform a linear scan backward from the binary search hit to guarantee we display the very first record by this artist
            while (targetIndex > 0 && artworkArray[targetIndex - 1][1].CompareTo(searchArtist) == 0)
            {
                targetIndex -= 1;
            }

            // Display the artworks until its last ocurrence
            while (targetIndex < arrayCount && artworkArray[targetIndex][1].CompareTo(searchArtist) == 0)
            {
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine($"Artwork: {artworkArray[targetIndex][0]}");
                Console.WriteLine($"Artist Name: {artworkArray[targetIndex][1]}");
                Console.WriteLine($"Art Year: {artworkArray[targetIndex][2]}");
                Console.WriteLine($"Art Medium: {artworkArray[targetIndex][3]}");
                Console.WriteLine("-----------------------------------------------");

                targetIndex += 1;
            }
        }

        // Binary Search operation to search for an artists work
        static int BinarySearch(string searchArtist, string[][] artworkArray, int arrayCount)
        {
            int low = 0;
            int high = arrayCount - 1;

            while (low <= high)
            {
                int mid = low + (high - low) / 2;

                if (artworkArray[mid][1].CompareTo(searchArtist) == 0)
                {
                    return mid;
                }

                if (artworkArray[mid][1].CompareTo(searchArtist) < 0)
                {
                    low = mid + 1;
                }
                else
                {
                    high = mid - 1;
                }
            }
            return -1;
        }

        // Get User Input
        static string[] GetUserInput(HashSet<string> mediums, HashSet<string> artistNames)
        {
            string[] artDetails = new string[4];

            artDetails[0] = GetValidInput("Please enter the title of the artwork:");
            artDetails[1] = GetArtistInput(artistNames);
            artistNames.Add(artDetails[1]); // Add to the artist names HashSet

            artDetails[2] = GetValidYear("Enter the art year (e.g., 1995):");

            List<string> mediumList = mediums.ToList();
            bool correct;
            int chosenMedium;

            do
            {
                Console.WriteLine("\nPlease choose a artwork medium:");
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

        // Allow user to select or input an artist
        static string GetArtistInput(HashSet<string> artistNames)
        {
            List<string> artistList = artistNames.ToList();
            int choice;
            bool correct;

            do
            {
                Console.WriteLine("\nChoose an Artist:");
                Console.WriteLine($"0. Enter New Artist");
                for (int i = 0; i < artistList.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {artistList[i]}");
                }

                correct = int.TryParse(Console.ReadLine(), out choice)
                    && choice >= 0
                    && choice <= artistList.Count;
            }
            while (!correct);

            if (choice > 0 && choice <= artistList.Count)
            {
                return artistList[choice - 1];
            }
            else
            {
                string newArtist = GetValidInput("Enter the new artist's name:");
                artistNames.Add(newArtist); // Update the artist names HashSet
                return newArtist;
            }
        }

        // Validate the users input
        static string GetValidInput(string prompt)
        {
            string input;
            do
            {
                Console.WriteLine($"\n{prompt}");
                Console.Write("> ");
                input = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Error: Input cannot be empty. Please try again.");
                }
            }
            while (string.IsNullOrWhiteSpace(input));

            return input;
        }

        static string GetValidYear(string prompt)
        {
            string input;
            bool isValid;

            do
            {
                Console.WriteLine($"\n{prompt}");
                Console.Write("> ");
                input = Console.ReadLine()?.Trim();

                isValid = int.TryParse(input, out int year)
                    && year >= 1000
                    && year <= 2026;

                if (!isValid)
                {
                    Console.WriteLine("Error: Please enter a valid 4-digit year (e.g., 2020).");
                }
            }
            while (!isValid);

            return input;
        }

        // Display All Artwork
        static void DisplayArtwork(string[][] artworkArray, ref int arrayCount)
        {
            for (int i = 0; i < arrayCount; i++)
            {

                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine($"Artwork: {artworkArray[i][0]}");
                Console.WriteLine($"Artist Name: {artworkArray[i][1]}");
                Console.WriteLine($"Art Year: {artworkArray[i][2]}");
                Console.WriteLine($"Art Medium: {artworkArray[i][3]}");
                Console.WriteLine("-----------------------------------------------");
            }
        }

        static void PrintHeader(string title)
        {
            Console.WriteLine("\n=========================================");
            Console.WriteLine($"\t{title}");
            Console.WriteLine("=========================================\n");
        }
    }
}
