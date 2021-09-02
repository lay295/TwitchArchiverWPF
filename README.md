<p align="center">
  <a href="https://github.com/lay295/TwitchArchiverWPF">
    <img src="TwitchArchiverWPF/Images/logo.png" alt="Logo" width="80" height="80">
    
  </a>

  <h3 align="center">Twitch Archiver</h3>

  <p align="center">
    A program for Windows that let's you save and archive streams from Twitch.TV
    <br />
    <br />
    <a href="https://github.com/lay295/TwitchArchiverWPF/issues">Report Bug</a>
  </p>
</p>

![Screenshot](https://user-images.githubusercontent.com/1060681/131883633-4f9cb815-8398-4788-b3c7-afc528a12d15.png)

## About The Project
I have a similar project called TwitchArchives.com where I archive some of my favorite streamers. A few people have asked if they could setup something similar, some with no programming knowledge. So I tried to make a simple desktop program that has one goal, to download livestreams from Twitch.

This also has the option to render the chat file after the stream is finished, so this could be used by video editors in case their streamer deletes their VOD.

## Usage
1. Go to the [releases](https://github.com/lay295/TwitchArchiverWPF/releases) section and download the latest Setup.msi, run and install the program.
2. Add each streamer you want to archive by hitting "Add Streamer" button and select which download options you want, enter their name and hit add.

![image](https://user-images.githubusercontent.com/1060681/131884968-6c198db7-849e-4773-b374-da0678a58121.png)

3. That should be it! When your streamer is live it should start downloading the selected options. When the stream is done and finished processing, it should show up in the respective folder (Click on the folder icon next to the streamer)

## Configuration
There are some additional settings you can change, so I will explain them here. For the Live Recording and Post Processing settings, you're able to change them on a per streamer basis to override the global settings.

<img src="https://user-images.githubusercontent.com/1060681/131885761-43d91c59-f495-4e15-adaf-2171ceca4135.png" alt="settings" height="500">

#### Download Folder
Where the final stream downloads are placed after processing. They are separated into streamer folders then a folder per stream. Folder/file names are not configurable.
#### Temp Folder
Where stream downloads in progress are placed. Because of how the program works, a 20GB stream would need about 40GB of free space to be processed.
#### Use TTV.LOL
[TTV.LOL](https://ttv.lol/) is an adblocking extension for Twitch. If this option is selected it will use TTV.LOL servers which proxy the stream playlist request to a region where ads aren't served. This breaks occasionally and I cannot control it, use at your own risk. If you're subbed to the streamer you want to archive, use a Live OAuth instead. This option overrides Live OAuth.
#### Use Custom OAuth Token (Live)
When requesting a live stream from Twitch, the archiver will use this OAuth token for authentication. With no OAuth, there will be ads on the Live Broadcast but not on the VOD Broadcast. OAuth token will only stop ads if you have [Twitch Turbo](https://www.twitch.tv/turbo) or are subscribed to the streamer you're archiving.
#### Use Custom OAuth Token (VOD)
When requesting a VOD from Twitch, the archiver will use this OAuth token for authentication. Authentication is only required for a VOD if the streamer has subscriber only VODs enabled.
#### Check Live Every x Seconds
Pretty self explanatory, how often the archiver tries to check if the streamer is live.
#### Render Chat
If selected, will attempt to render the chat into a video format using the selected Render Options. Mainly useful for video editors.
#### Start Program On Startup
Again pretty self explanatory, on Windows Startup the archiver will also start.

## How Do I Get My OAuth Token?
I have an unrelated video [here](https://www.youtube.com/watch?v=1MBsUoFGuls) explaining how to get your Twitch OAuth token.

TLDW: It's the "auth-token" cookie from Twitch.tv

Full Disclaimer, this OAuth token basically gives **FULL ACCESS** to the Twitch account, if you don't trust my program don't put it in and don't give it to anybody. If you don't trust my program, you can just archive VODs which don't have ads, deal with ads on Live Broadcasts, or maybe make a separate Twitch account you don't care about that has Twitch Turbo to give to the program.
