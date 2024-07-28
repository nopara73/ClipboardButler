# ClipboardButler

Non-invasive clipboard cleaner. Removes tracking links and other dark patterns.

![image](https://github.com/nopara73/ClipboardButler/assets/9156103/84c1ff59-4ddf-4e1c-9199-815405962ecc)

- This: `https://youtu.be/7aQ2VdV_S_Y?si=gx0Hcg3hF9fWKcKh` becomes this: `https://youtu.be/7aQ2VdV_S_Y`
- This: `https://www.youtube.com/watch?v=7aQ2VdV_S_Y&ab_channel=nopara73` becomes this: `https://www.youtube.com/watch?v=7aQ2VdV_S_Y`
- This: `https://youtube.com/clip/UgkxqiiZXWjZ0UecWh70gsdZT4vr91uEhl_q?si=4AaWzv636s38XYpy` becomes this: `https://youtube.com/clip/UgkxqiiZXWjZ0UecWh70gsdZT4vr91uEhl_q`
- This: `https://youtu.be/84gIeFO6ipE?feature=shared` becomes this: `https://youtu.be/84gIeFO6ipE`
- This: `https://www.google.com/url?q=https://fast.com/&sa=D&source=calendar&usd=2&usg=AOvVaw2-43fyjEok_J83Gbx6W6Xw` becomes this: `https://fast.com`
- This: `https://x.com/nopara73?t=XL6mz6zGWAjMvByoVLXHgA&s=09` becomes this: `https://x.com/nopara73`
- This: `https://twitter.com/nopara73?t=XL6mz6zGWAjMvByoVLXHgA&s=09` becomes this: `https://twitter.com/nopara73`
- This: `https://www.youtube.com/watch?v=XCT1WCYZOpM&feature=youtu.be` becomes this: `https://www.youtube.com/watch?v=XCT1WCYZOpM`
- This: `https://www.amazon.com/gp/product/B0C15QMSHH/ref=ox_sc_act_title_3?smid=A30IGBX08D2XOT&psc=1` becomes this: `https://www.amazon.com/gp/product/B0C15QMSHH`



## How To Use

I did not spend much time on packaging and other stuff, but I made it work for myself. For now it only works with Windows.

### Get The Requirements

1. Get Git: https://git-scm.com/downloads
2. Get .NET 8.0 SDK: https://dotnet.microsoft.com/download

### Get ClipboardButler

Clone & Restore & Build

```sh
git clone https://github.com/nopara73/ClipboardButler.git
cd ClipboardButler/ClipboardButler
dotnet build
```

### Run ClipboardButler

Run ClipboardButler with `dotnet run` from the `ClipboardButler` folder.  
You can verify it's running by looking at the start menu.  
![image](https://github.com/nopara73/ClipboardButler/assets/9156103/8d62ebcd-06b0-423e-8b56-a80954a715f2)  
I have also created a shortcut and put that in the Windows startup folder so it always starts with Windows.  

### Update ClipboardButler

```sh
git pull
```
