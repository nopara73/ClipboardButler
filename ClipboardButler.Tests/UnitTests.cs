using System;
using Xunit;

namespace ClipboardButler.Tests
{
    public class UnitTests
    {
        [Theory]
        [InlineData("https://youtu.be/7aQ2VdV_S_Y?si=gx0Hcg3hF9fWKcKh", "https://youtu.be/7aQ2VdV_S_Y")]
        [InlineData("https://www.youtube.com/watch?v=7aQ2VdV_S_Y&ab_channel=nopara73", "https://www.youtube.com/watch?v=7aQ2VdV_S_Y")]
        [InlineData("https://www.google.com/url?q=https://fast.com/&sa=D&source=calendar&usd=2&usg=AOvVaw2-43fyjEok_J83Gbx6W6Xw", "https://fast.com/")]
        [InlineData("https://x.com/nopara73?t=XL6mz6zGWAjMvByoVLXHgA&s=09", "https://x.com/nopara73")]
        [InlineData("https://twitter.com/nopara73?t=XL6mz6zGWAjMvByoVLXHgA&s=09", "https://twitter.com/nopara73")]
        [InlineData("https://youtube.com/clip/UgkxqiiZXWjZ0UecWh70gsdZT4vr91uEhl_q?si=4AaWzv636s38XYpy", "https://youtube.com/clip/UgkxqiiZXWjZ0UecWh70gsdZT4vr91uEhl_q")]
        [InlineData("https://www.youtube.com/watch?v=XCT1WCYZOpM&feature=youtu.be", "https://www.youtube.com/watch?v=XCT1WCYZOpM")]
        [InlineData("https://www.amazon.com/gp/product/B0C15QMSHH/ref=ox_sc_act_title_3?smid=A30IGBX08D2XOT&psc=1", "https://www.amazon.com/gp/product/B0C15QMSHH")]
        [InlineData("https://youtu.be/84gIeFO6ipE?feature=shared", "https://youtu.be/84gIeFO6ipE")]
        [InlineData("https://l.facebook.com/l.php?u=https%3A%2F%2Fx.com%2Fnopara73%3Ffbclid%3DIwZXh0bgNhZW0CMTAAAR0OYOUskmn7ar7wAkaH2cN2QvPiFsuVnSyHsto-KXbGLUFvau-n4LSYT-k_aem_PQMJxkEQHetzw1u3ITfwRA&h=AT1Rv7XogRbqmfGnTfnPkl-XEjwUTT40WD8cZeOlwQSvBAY1OYMYVzT45Ynx-8tj-TJ4OXtgu6JtWttePoyMFTS4Q3ng92BWc3AuuzlCMaa9a-j0dNjOD3QeHGcyNbsX3WI", "https://x.com/nopara73")]
        [InlineData("https://www.instagram.com/reel/DEJGTf8OvRk/?igsh=NnkzMTlwZjJycnA3", "https://www.instagram.com/reel/DEJGTf8OvRk/")]
        [InlineData("https://www.instagram.com/p/DAmBaUHSkJ4/?igsh=bDRtb3BmM3h1YWYz&fbclid=IwY2xjawI-drtleHRuA2FlbQIxMAABHQWZ0RKKldPBild-aonIA-7MgdYWMdehVyz_FJLjqPK4-cOIpAGaJYpkkg_aem_Qka1WMN4A_9CFt9ShvJXog&img_index=1", "https://www.instagram.com/p/DAmBaUHSkJ4/")]
        public void TrackingUrls(string input, string expected)
        {
            // Act
            bool result = TextCleaner.TryClean(input, out string actual);

            // Assert
            Assert.True(result);
            Assert.Equal(expected, actual);
        }
    }
}