﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToTwitter;

namespace LinqToTwitterDemo
{
    public class StreamingDemo
    {
        public static void Run(LinqToTwitter.TwitterContext twitterCtx)
        {
            //FilterDemo(twitterCtx);
            //SamplesDemo(twitterCtx);
            UserStreamDemo(twitterCtx);
        }

        private static void FilterDemo(TwitterContext twitterCtx)
        {
            twitterCtx.StreamingUserName = "";
            twitterCtx.StreamingPassword = "";

            if (twitterCtx.StreamingUserName == string.Empty ||
                twitterCtx.StreamingPassword == string.Empty)
            {
                Console.WriteLine("\n*** This won't work until you set the StreamingUserName and StreamingPassword on TwitterContext to valid values.\n");
                return;
            }

            Console.WriteLine("\nStreamed Content: \n");
            int count = 0;

            var streaming =
                (from strm in twitterCtx.Streaming
                 where strm.Type == StreamingType.Filter &&
                       strm.Track == "LINQ to Twitter"
                 select strm)
                .StreamingCallback(strm =>
                    {
                        Console.WriteLine(strm.Content + "\n");

                        if (count++ >= 2)
                        {
                            strm.CloseStream();
                        }
                    })
                .SingleOrDefault();
        }

        private static void SamplesDemo(LinqToTwitter.TwitterContext twitterCtx)
        {
            twitterCtx.StreamingUserName = "";
            twitterCtx.StreamingPassword = "";

            if (twitterCtx.StreamingUserName == string.Empty ||
                twitterCtx.StreamingPassword == string.Empty)
            {
                Console.WriteLine("\n*** This won't work until you set the StreamingUserName and StreamingPassword on TwitterContext to valid values.\n");
                return;
            }

            Console.WriteLine("\nStreamed Content: \n");
            int count = 0;

            var streaming =
                (from strm in twitterCtx.Streaming
                 where strm.Type == StreamingType.Sample
                 select strm)
                .StreamingCallback(strm =>
                {
                    Console.WriteLine(strm.Content + "\n");

                    if (count++ >= 10)
                    {
                        strm.CloseStream();
                    }
                })
                .SingleOrDefault();
        }

        private static void UserStreamDemo(TwitterContext twitterCtx)
        {
            Console.WriteLine("\nStreamed Content: \n");
            int count = 0;

            var streaming =
                (from strm in twitterCtx.UserStream
                 where strm.Type == UserStreamType.User
                 select strm)
                .StreamingCallback(strm =>
                {
                    Console.WriteLine(strm.Content + "\n");

                    if (count++ >= 10)
                    {
                        strm.CloseStream();
                    }
                })
                .SingleOrDefault();
        }
    }
}