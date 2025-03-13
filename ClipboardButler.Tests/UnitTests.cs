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
        [InlineData("https://oaziscomputer.hu/termek/28426/samsung-odyssey-neo-g9-ls57cg952nuxen?gad_source=1&gclid=Cj0KCQiAuou6BhDhARIsAIfgrn459GPank9top8kr0-NbkUXZNSW121K2khfuoEBw_MCcBc1fWsUsUIaAm1JEALw_wcB", "https://oaziscomputer.hu/termek/28426/samsung-odyssey-neo-g9-ls57cg952nuxen")]
        [InlineData("https://www.notebook.hu/samsung-g95nc-57-dual-uhd-va-ivelt-gaming-monitor-ls57cg952nuxen-353004?utm_campaign=SH-TOF-monitor-All&gad_source=1&gclid=Cj0KCQiAuou6BhDhARIsAIfgrn7azMbuOVW4rB0YeN04FOQatXqoRlWM96Ey1hdgV2b60PpKi6phdvcaAo55EALw_wcB", "https://www.notebook.hu/samsung-g95nc-57-dual-uhd-va-ivelt-gaming-monitor-ls57cg952nuxen-353004")]
        [InlineData("https://www.samsung.com/hu/monitors/gaming/odyssey-oled-g9-g95sc-49-inch-240hz-curved-dual-qhd-ls49cg950suxdu/?cid=hu_pd_ppc_google_monitor_hot_ecommerce_pla_shopping-feed-based_bau&gad_source=1&gclid=Cj0KCQiAuou6BhDhARIsAIfgrn5EtZB7hsRutY5vtHcUcyBTCdTA7a_ArH5uivpkOER9DuZxwHNUEdUaAisMEALw_wcB", "https://www.samsung.com/hu/monitors/gaming/odyssey-oled-g9-g95sc-49-inch-240hz-curved-dual-qhd-ls49cg950suxdu/")]
        [InlineData("https://euronics.hu/gamer-monitor/samsung-ls57cg952nuxen-odyssey-neo-g9-57-uhd-4k-gaming-monitor-p292932?utm_source=google&utm_medium=cpc&utm_campaign=PMax_Notebook_Monitor_Nyomtat%C3%B3_[ROI]&gad_source=1&gclid=Cj0KCQiAuou6BhDhARIsAIfgrn62bizHrnl19ze-ADttqciuD3wOdvcaWETxbxe-mtB1Pr8GuSiydcAaAvZ2EALw_wcB", "https://euronics.hu/gamer-monitor/samsung-ls57cg952nuxen-odyssey-neo-g9-57-uhd-4k-gaming-monitor-p292932")]
        [InlineData("https://www.emag.hu/monitor-samsung-odyssey-neo-g9-57-duhd-240-hz-ls57cg952nuxen/pd/DFXH3YYBM/?cmpid=105284&gad_source=1&gclid=Cj0KCQiAuou6BhDhARIsAIfgrn4vBE1aWSE05nhKV0_fOEwuWqhBZGqTDopNRbukk-_wYSm-O8UB4U0aAp-MEALw_wcB", "https://www.emag.hu/monitor-samsung-odyssey-neo-g9-57-duhd-240-hz-ls57cg952nuxen/pd/DFXH3YYBM/")]
        [InlineData("https://ipon.hu/shop/termek/samsung-odyssey-neo-g9-g95nc-57-ls57cg952nuxen/2223310?utm_source=google&utm_medium=cpc&utm_campaign=HU|IPON|PMX|:%20C06%20-%20TV,%20Monitor,%20Projektor,%20VR&gad_source=1&gclid=Cj0KCQiAuou6BhDhARIsAIfgrn4fbgS5lLmjBfVXhvmIs-NLBDYSB_U44UVQ4vc0l0UQxt3ZXJ-1RhAaApo2EALw_wcB", "https://ipon.hu/shop/termek/samsung-odyssey-neo-g9-g95nc-57-ls57cg952nuxen/2223310")]
        [InlineData("https://www.alza.hu/EN/57-samsung-odyssey-neo-g9-d7915024.htm?gclid=Cj0KCQiAuou6BhDhARIsAIfgrn7gR3c-1lau8-S0ZISEXegCB9MeTzymiQu3_H-bH4T-rTUtbAlIKWIaAtj1EALw_wcB&kampan=adw1_alza_pla_all_bfr_monitory_c_9106144_WH605Q4", "https://www.alza.hu/EN/57-samsung-odyssey-neo-g9-d7915024.htm")]
        [InlineData("https://www.google.com/url?q=https://cantunsee.space/&sa=D&source=calendar&usd=2&usg=AOvVaw3A4OinmjefbrfMab0QbCRY", "https://cantunsee.space/")]
        [InlineData("https://calendly.com/asdasdasd/dsgwefwef?back=1&month=2025-03", "https://calendly.com/asdasdasd/dsgwefwef")]
        public void TrackingUrls(string input, string expected)
        {
            // Act
            bool result = TextCleaner.TryClean(input, out string actual);

            // Assert
            Assert.True(result);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("https://example.com/somepath?param=value")]
        [InlineData("https://randomsite.com/?a=b&c=d")]
        [InlineData("https://notarelevant.com/path?another=thing")]
        [InlineData("https://github.com/user/repo?query=param")]
        public void NonTrackingUrls_ShouldRemainUnchanged(string input)
        {
            // Act
            bool result = TextCleaner.TryClean(input, out string actual);

            // Assert - the URL should remain unchanged (result is false)
            Assert.False(result);
            Assert.Equal(input, actual);
        }
    }
}