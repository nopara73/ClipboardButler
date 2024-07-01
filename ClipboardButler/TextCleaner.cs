﻿using System;
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
            string youtuBePrefix = "https://youtu.be/";
            string youtubePrefix = "https://www.youtube.com/watch?v=";

            if (dirty.StartsWith(youtuBePrefix))
            {
                // Find the position of the '?' character
                int questionMarkIndex = dirty.IndexOf('?');
                if (questionMarkIndex > -1)
                {
                    // Return the URL without the query parameters
                    clean = dirty.Substring(0, questionMarkIndex);
                    return true;
                }
                // If no query parameters are found, return the original URL
                clean = dirty;
                return true;
            }
            else if (dirty.StartsWith(youtubePrefix))
            {
                // Find the position of the '&' character
                int ampersandIndex = dirty.IndexOf('&');
                if (ampersandIndex > -1)
                {
                    // Return the URL without the query parameters after the video ID
                    clean = dirty.Substring(0, ampersandIndex);
                    return true;
                }
                // If no query parameters are found, return the original URL
                clean = dirty;
                return true;
            }

            // Return the original URL if it doesn't match the known patterns
            clean = dirty;
            return false;
        }
    }
}
