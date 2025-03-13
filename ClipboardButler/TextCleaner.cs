using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ClipboardButler
{
    public static class TextCleaner
    {
        public static bool TryClean(string dirty, out string clean)
        {
            dirty = dirty.Trim();
            string youtubePrefix1 = "https://www.youtube.com/";
            string youtubePrefix2 = "https://youtu.be/";
            string youtubeClipPrefix = "https://youtube.com/clip/";
            string googleRedirectPrefix = "https://www.google.com/url?q=";
            string xPrefix = "https://x.com/";
            string twitterPrefix = "https://twitter.com/";
            string amazonProductPrefix = "https://www.amazon.com/gp/product/";
            string facebookLinkPrefix = "https://l.facebook.com/l.php?u=";
            string instagramReelPrefix = "https://www.instagram.com/reel/";
            string instagramPostPrefix = "https://www.instagram.com/p/";
            string oazisComputerPrefix = "https://oaziscomputer.hu/termek/";
            string notebookHuPrefix = "https://www.notebook.hu/";
            string samsungPrefix = "https://www.samsung.com/hu/monitors/gaming/";
            string euronicsPrefix = "https://euronics.hu/gamer-monitor/";
            string emagPrefix = "https://www.emag.hu/";
            string iponPrefix = "https://ipon.hu/shop/termek/";
            string alzaPrefix = "https://www.alza.hu/";
            string calendlyPrefix = "https://calendly.com/";

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
                    clean = dirty[..questionMarkIndex];
                    return true;
                }

                // Find the position of the '&' character
                int ampersandIndex = dirty.IndexOf('&');
                int wIndex = dirty.IndexOf("/watch?v=");
                int abchannelIndex = dirty.IndexOf("&ab_channel");
                int feature1Index = dirty.IndexOf("&feature=youtu.be");
                int feature2Index = dirty.IndexOf("?feature=shared");
                if (ampersandIndex > -1 && wIndex > -1 && abchannelIndex > -1)
                {
                    // Return the URL without the query parameters after the video ID
                    clean = dirty[..ampersandIndex];
                    return true;
                }
                if (feature1Index > -1 && wIndex > -1 && feature1Index > -1)
                {
                    // Return the URL without the query parameters after the video ID
                    clean = dirty[..feature1Index];
                    return true;
                }
                if (feature2Index > -1 && feature2Index > -1)
                {
                    // Return the URL without the query parameters after the video ID
                    clean = dirty[..feature2Index];
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
                    clean = dirty[startIndex..endIndex];
                }
                else
                {
                    clean = dirty[startIndex..];
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
                    clean = dirty[..questionMarkIndex];
                    return true;
                }
            }
            else if (dirty.StartsWith(amazonProductPrefix))
            {
                int refIndex = dirty.IndexOf("/ref=");
                if (refIndex > -1)
                {
                    // Return the URL without the query parameters
                    clean = dirty[..refIndex];
                    return true;
                }
            }
            else if (dirty.StartsWith(facebookLinkPrefix))
            {
                int startIndex = facebookLinkPrefix.Length;
                int endIndex = dirty.IndexOf("%3Ffbclid", startIndex);
                if (endIndex > -1)
                {
                    clean = dirty[startIndex..endIndex];
                }
                else
                {
                    clean = dirty[startIndex..];
                }
                clean = Uri.UnescapeDataString(HttpUtility.UrlDecode(clean));
                return true;
            }
            else if (dirty.StartsWith(instagramReelPrefix))
            {
                // Remove query parameters from Instagram reel URLs
                int questionMarkIndex = dirty.IndexOf('?');
                if (questionMarkIndex > -1)
                {
                    clean = dirty[..questionMarkIndex];
                    return true;
                }
            }
            else if (dirty.StartsWith(instagramPostPrefix))
            {
                // Remove query parameters from Instagram post URLs
                int questionMarkIndex = dirty.IndexOf('?');
                if (questionMarkIndex > -1)
                {
                    clean = dirty[..questionMarkIndex];
                    return true;
                }
            }
            else if (dirty.StartsWith(oazisComputerPrefix))
            {
                // Remove query parameters from OazisComputer URLs
                int questionMarkIndex = dirty.IndexOf('?');
                if (questionMarkIndex > -1)
                {
                    clean = dirty[..questionMarkIndex];
                    return true;
                }
            }
            else if (dirty.StartsWith(notebookHuPrefix))
            {
                // Remove query parameters from Notebook.hu URLs
                int questionMarkIndex = dirty.IndexOf('?');
                if (questionMarkIndex > -1)
                {
                    clean = dirty[..questionMarkIndex];
                    return true;
                }
            }
            else if (dirty.StartsWith(samsungPrefix))
            {
                // Remove query parameters from Samsung URLs
                int questionMarkIndex = dirty.IndexOf('?');
                if (questionMarkIndex > -1)
                {
                    clean = dirty[..questionMarkIndex];
                    return true;
                }
            }
            else if (dirty.StartsWith(euronicsPrefix))
            {
                // Remove query parameters from Euronics.hu URLs
                int questionMarkIndex = dirty.IndexOf('?');
                if (questionMarkIndex > -1)
                {
                    clean = dirty[..questionMarkIndex];
                    return true;
                }
            }
            else if (dirty.StartsWith(emagPrefix))
            {
                // Remove query parameters from eMag.hu URLs
                int questionMarkIndex = dirty.IndexOf('?');
                if (questionMarkIndex > -1)
                {
                    clean = dirty[..questionMarkIndex];
                    return true;
                }
            }
            else if (dirty.StartsWith(iponPrefix))
            {
                // Remove query parameters from Ipon.hu URLs
                int questionMarkIndex = dirty.IndexOf('?');
                if (questionMarkIndex > -1)
                {
                    clean = dirty[..questionMarkIndex];
                    return true;
                }
            }
            else if (dirty.StartsWith(alzaPrefix))
            {
                // Remove query parameters from Alza.hu URLs
                int questionMarkIndex = dirty.IndexOf('?');
                if (questionMarkIndex > -1)
                {
                    clean = dirty[..questionMarkIndex];
                    return true;
                }
            }
            else if (dirty.StartsWith(calendlyPrefix))
            {
                // Remove query parameters from Calendly URLs
                int questionMarkIndex = dirty.IndexOf('?');
                if (questionMarkIndex > -1)
                {
                    clean = dirty[..questionMarkIndex];
                    return true;
                }
            }

            // Return the original URL if it doesn't match the known patterns
            clean = dirty;
            return false;
        }
    }
}