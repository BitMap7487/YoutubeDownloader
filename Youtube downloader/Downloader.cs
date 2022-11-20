using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace Youtube_downloader
{
    class Downloader
    {
        public static void start(string link, string choice)
        {

            Task.Run(async () =>
            {
                await download(link, choice);
            }).GetAwaiter().GetResult();

        }

        private static async Task download(string link, string option)
        {

            var youtube = new YoutubeClient();

            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(link);

            var video = await youtube.Videos.GetAsync(link);

            var title = video.Title;

            Console.WriteLine($"Starting downloading the {option} of {title}, You can find this in the video's folder.");

            if (option == "Audio")
            {
                var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();

                // Get the actual stream
                var stream = await youtube.Videos.Streams.GetAsync(streamInfo);

                // Download the stream to a file
                await youtube.Videos.Streams.DownloadAsync(streamInfo, $"video's/{string.Join("_", title.Split(Path.GetInvalidFileNameChars()))} Audio.{streamInfo.Container}");

                Console.WriteLine($"Done Downloading the audio of {title}, You can find this in the video's folder.");

            }
            if (option == "Video")
            {
                var streamInfo = streamManifest
                .GetVideoOnlyStreams().Where(s => s.Container == Container.Mp4).GetWithHighestVideoQuality();

                // Get the actual stream
                var stream = await youtube.Videos.Streams.GetAsync(streamInfo);
                              
                // Download the stream to a file
                await youtube.Videos.Streams.DownloadAsync(streamInfo, $"video's/{string.Join("_", title.Split(Path.GetInvalidFileNameChars()))} Video.{streamInfo.Container}");

                Console.WriteLine($"Done Downloading the video of {title}, You can find this in the video's folder.");

            }
            if (option == "Both")
            {

                await download(link, "Audio");
                await download(link, "Video");

            }
        }

    }
}
