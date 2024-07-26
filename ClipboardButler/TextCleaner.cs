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
            string youtubePrefix1 = "https://www.youtube.com/";
            string youtubePrefix2 = "https://youtu.be/";
            string youtubeClipPrefix = "https://youtube.com/clip/";
            string googleRedirectPrefix = "https://www.google.com/url?q=";
            string xPrefix = "https://x.com/";
            string twitterPrefix = "https://twitter.com/";
            string amazonProductPrefix = "https://www.amazon.com/gp/product/";

            if (dirty.StartsWith(youtubePrefix1)
                || dirty.StartsWith(youtubePrefix2)
                || dirty.StartsWith(youtubeClipPrefix))
            {
                // Find the position of the '?' character
                int questionMarkIndex = dirty.IndexOf('?');
                int qsIndex = dirty.IndexOf("?si=");
                if (questionMarkIndex > -1 && qsIndex > -1)
                {
                    // Return the URL without the query parameters
                    clean = dirty.Substring(0, questionMarkIndex);
                    return true;
                }


                // Find the position of the '&' character
                int ampersandIndex = dirty.IndexOf('&');
                int wIndex = dirty.IndexOf("/watch?v=");
                int abchannelIndex = dirty.IndexOf("&ab_channel");
                int featureIndex = dirty.IndexOf("&feature=youtu.be");
                if (ampersandIndex > -1 && wIndex > -1 && abchannelIndex > -1)
                {
                    // Return the URL without the query parameters after the video ID
                    clean = dirty.Substring(0, ampersandIndex);
                    return true;
                }
                if (featureIndex > -1 && wIndex > -1 && featureIndex > -1)
                {
                    // Return the URL without the query parameters after the video ID
                    clean = dirty.Substring(0, featureIndex);
                    return true;
                }
            }
            else if (dirty.StartsWith(googleRedirectPrefix))
            {
                // Extract the actual URL from the Google redirect URL
                int startIndex = googleRedirectPrefix.Length;
                int endIndex = dirty.IndexOf('&', startIndex);
                if (endIndex > -1)
                {
                    clean = dirty.Substring(startIndex, endIndex - startIndex);
                }
                else
                {
                    clean = dirty.Substring(startIndex);
                }
                clean = Uri.UnescapeDataString(clean);
                return true;
            }
            else if (dirty.StartsWith(xPrefix)
                || dirty.StartsWith(twitterPrefix))
            {
                // Find the position of the '?' character
                int questionMarkIndex = dirty.IndexOf('?');
                int qteIndex = dirty.IndexOf("?t=");
                int aseIndex = dirty.IndexOf("&s=");
                if (questionMarkIndex > -1 && qteIndex > -1 && aseIndex > -1)
                {
                    // Return the URL without the query parameters
                    clean = dirty.Substring(0, questionMarkIndex);
                    return true;
                }
            }
            else if (dirty.StartsWith(amazonProductPrefix))
            {
                int refIndex = dirty.IndexOf("/ref=");
                if (refIndex > -1)
                {
                    // Return the URL without the query parameters
                    clean = dirty.Substring(0, refIndex);
                    return true;
                }
            }

            // Return the original URL if it doesn't match the known patterns
            clean = dirty;
            return false;
        }
    }
}
