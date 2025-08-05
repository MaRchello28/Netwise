using Netwise.Interfaces;
using Netwise.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netwise.Services
{
    public class TxtFileHandler : IFileHandler
    {
        private readonly string filePath;

        public TxtFileHandler(string filePath)
        {
            this.filePath = filePath;
        }

        public void SaveFact(CatFact catFact)
        {
            StreamWriter writer = new StreamWriter(filePath, true);

            try
            {
                writer.WriteLine(catFact.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
            finally
            {
                writer.Close();
            }
        }

        public void DisplayFile()
        {
            StreamReader reader = new StreamReader(filePath);

            try
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading from file: {ex.Message}");
            }
            finally
            {
                reader.Close();
            }
        }
    }
}
