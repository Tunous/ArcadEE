using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using ArcadeBot;
using BotCake;
using BotBits;
using BotBits.Events;
using System.Text.RegularExpressions;

namespace ArcadEE
{
    public class BotManager : BotBase
    {
        [ImportMany]
        public IEnumerable<Lazy<Bot, IBotMetadata>> Bots { get; set; }

        private List<PlaygroundBase> playgrounds = new List<PlaygroundBase>();

        public BotManager()
        {
            Console.WriteLine("Loading bots...");
            var catalog = new DirectoryCatalog("Bots");
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);

            foreach (var bot in Bots)
            {
                Console.WriteLine($" - {bot.Metadata.Id} ({bot.Metadata.Version})");
            }
        }

        private void ScanForPlaygrounds()
        {
            foreach (var block in Blocks)
            {
                if (block.Foreground.Block.Type == ForegroundType.Text)
                {
                    CreatePlayground(new Point(block.X, block.Y), block.Foreground.Block.Text);
                }
            }
        }

        [EventListener]
        private void OnJoinComplete(JoinCompleteEvent e)
        {
            Console.WriteLine("Join complete");
            ScanForPlaygrounds();
        }

        [EventListener]
        private void OnForegroundPlace(ForegroundPlaceEvent e)
        {
            if (e.New.Block.Type == ForegroundType.Team)
            {
                CreatePlayground(new Point(e.X, e.Y), e.New.Block.Text);
            }
        }

        [EventListener]
        private void OnLoadLevel(LoadLevelEvent e)
        {
            ScanForPlaygrounds();
        }

        private void CreatePlayground(Point point, string blockText)
        {
            foreach (var p in playgrounds)
            {
                if (p.Contains(point))
                {
                    return;
                }
            }

            var match = Regex.Match(blockText, @"^\[(.*)\]$");
            var botId = match.Success ? match.Groups[1].Value : "";
            if (botId == "")
                return;
            
            var bot = Bots.FirstOrDefault(it => it.Metadata.Id == botId)?.Value;
            if (bot == null)
            {
                Console.WriteLine($"ERROR: No valid bot found for [{botId}].");
                return;
            }

            PlaygroundBase playground;
            if (bot.TryCreatePlaygroundAt(point, out playground))
            {
                Console.WriteLine($"Created playground for {botId}.");
                playgrounds.Add(playground);
            }
            else
            {
                Console.WriteLine($"ERROR: Failed to create playground for [{botId}] at {point.X}x{point.Y}.");
            }
        }
    }
}

