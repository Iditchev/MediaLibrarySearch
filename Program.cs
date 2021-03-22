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
                string choice = "";
                 do
                {
                    // display choices to user
                    Console.WriteLine("1) Display All Movies");
                     Console.WriteLine("2) Add Movie");
                    Console.WriteLine("3) Search for Movie");
                    Console.WriteLine("Enter to quit");
                     // input selection
                    choice = Console.ReadLine();
                    logger.Info("User choice: {Choice}", choice);

                        if (choice == "1")
                        {
                                // Display All Movies
                                 foreach(Movie m in movieFile.Movies)
                                {
                                    Console.WriteLine(m.Display());
                                 }
                         }               
            
                        else if (choice == "2")
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
        }

                        else if (choice == "3")
                        {
                            Console.WriteLine("Enter movie you are searching for.");
                            string search = Console.ReadLine().ToLower();
                            
                            // LINQ - Where filter operator & Select projection operator & Contains quantifier operator
                             var titles = movieFile.Movies.Where(m => m.title.ToLower().Contains((search))).Select(m => m.title);
                            // LINQ - Count aggregation method
                            Console.WriteLine($"There are {titles.Count()} movies with \"{search}\" in the title:");
                            foreach(string t in titles)
                            {
                                Console.WriteLine($"  {t}");
                            }     
                        }  
         


                        logger.Info("Program ended");
            
                } while (choice == "1" || choice == "2"|| choice == "3");
        }   
                  
    } 
}