using System;
using NLog.Web;
using System.IO;
using System.Linq;

namespace MediaLibrary
{
    class Program
    {
        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {

            logger.Info("Program started");

            string scrubbedFile = FileScrubber.ScrubMovies("movies.csv");
            MovieFile movieFile = new MovieFile(scrubbedFile);
             Console.WriteLine("Enter 1 to list movie collection.");
            Console.WriteLine("Enter 2 to add movie to collection.");
            Console.WriteLine("Enter 3 to search for a movie");
            Console.WriteLine("Enter anything else to quit.");

            String resp = Console.ReadLine();

            if (resp == "1")
            {
                 // Display All Movies
                    foreach(Movie m in movieFile.Movies)
                    {
                        Console.WriteLine(m.Display());
                    }
            }
            
            if (resp == "2")
            {
                   // Add movie
                    Movie movie = new Movie();
                    // ask user to input movie title
                    Console.WriteLine("Enter movie title");
                    // input title
                    movie.title = Console.ReadLine();
                    // verify title is unique
                    if (movieFile.isUniqueTitle(movie.title)){
                        // input genres
                        string input;
                        do
                        {
                            // ask user to enter genre
                            Console.WriteLine("Enter genre (or done to quit)");
                            // input genre
                            input = Console.ReadLine();
                            // if user enters "done"
                            // or does not enter a genre do not add it to list
                            if (input != "done" && input.Length > 0)
                            {
                                movie.genres.Add(input);
                            }
                        } while (input != "done");
                        // specify if no genres are entered
                        if (movie.genres.Count == 0)
                        {
                            movie.genres.Add("(no genres listed)");
                        }
                        // add movie
                        movieFile.AddMovie(movie);
            }

            Console.ForegroundColor = ConsoleColor.Green;

            // LINQ - Where filter operator & Contains quantifier operator
            var Movies = movieFile.Movies.Where(m => m.title.Contains("(1990)"));
            // LINQ - Count aggregation method
            Console.WriteLine($"There are {Movies.Count()} movies from 1990");


            logger.Info("Program ended");
        }
    }
}
