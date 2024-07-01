using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipboardButler
{
    public static class TextCleaner
    {
        public static bool TryClean(string dirty, out string clean) 
        {
            // Check if the URL starts with "https://youtu.be/"
            string prefix = "https://youtu.be/";
            if (dirty.StartsWith(prefix))
            {
                // Find the position of the '?' character
                int questionMarkIndex = dirty.IndexOf('?');
                if (questionMarkIndex > -1)
                {
                    // Return the URL without the query parameters
                    clean = dirty.Substring(0, questionMarkIndex);
                    return true;
                }
            }
            // Return the original URL if no query parameters were found
            clean = dirty;
            return false;
        }
    }
}
