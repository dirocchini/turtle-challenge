using System;
using System.Collections.Generic;
using System.IO;
using TurtleChallenge.Application.Interfaces;
using TurtleChallenge.Model;
using TurtleChallenge.Model.Enum;

namespace TurtleChallenge.Application
{
    public class ConfigService : IConfigService
    {
        private readonly string _defaultGameSettingsFile = "initial_settings\\game-settings";
        private readonly string _defaultMovesFile = "initial_settings\\moves";

        public ConfigService()
        {

        }

        public Position Size { get; set; }
        public Position StartPosition { get; set; }
        public Position ExitPosition { get; set; }
        public List<Position> Mines { get; set; } = new List<Position>();
        public Direction StartDirection { get; set; }
        public List<Movement> Movements { get; set; } = new List<Movement>();

        public void Configure(string gameSettingsFile, string movesFile)
        {
            var gameSettings = File.ReadAllLines(gameSettingsFile ?? Path.Combine(Directory.GetCurrentDirectory(), _defaultGameSettingsFile));

            ValidateConfigFile(gameSettings);

            Size = GetPoint(gameSettings, "Boarding Size:", 0);
            StartPosition = GetPoint(gameSettings, "Starting Point:", 1);
            ExitPosition = GetPoint(gameSettings, "Exit Point:", 3);
            StartDirection = GetDirection(gameSettings[2]);

            for (int i = 4; i < gameSettings.Length; i++)
                Mines.Add(GetPoint(gameSettings, "Mine:", i));



            var movements = File.ReadAllLines(movesFile ?? Path.Combine(Directory.GetCurrentDirectory(), _defaultMovesFile));
            for (int i = 0; i < movements.Length; i++)
            {
                if (string.IsNullOrEmpty(movements[i]))
                    continue;

                if (movements[i].ToLower().Trim() == "m")
                    Movements.Add(Movement.Move);
                else if (movements[i].ToLower().Trim() == "r")
                    Movements.Add(Movement.Rotate);
            }
        }

        private void ValidateConfigFile(string[] gameSettings)
        {
            string file = string.Join(" ", gameSettings);

            if(
                !file.ToLower().Contains("boarding size") ||
                !file.ToLower().Contains("starting point") ||
                !file.ToLower().Contains("exit point") ||
                !file.ToLower().Contains("direction")
                )
            {
                throw new Exception($"Your game-settings file hasn't the correct format. It should has Boarding Size, Starting Point, Exit Point and Direction configurations");
            }
        }

        private Direction GetDirection(string line)
        {
            try
            {
                var sDirection = line.Split('=')[1].Trim();
                System.Enum.TryParse<Direction>(sDirection, out var direction);
                return direction;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting direction info from line: {line}. Details: {ex.ToString()}");
            }
        }

        private Position GetPoint(string[] gameSettings, string lowerPrefix, int line)
        {
            if (gameSettings.Length > line)
                if (gameSettings[line].ToLower().Trim().StartsWith(lowerPrefix.ToLower()))
                    return GetXYPoint(gameSettings[line]);

            throw new Exception($"{System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lowerPrefix)} Not Informed");
        }

        private Position GetXYPoint(string line)
        {
            try
            {
                string[] halfs = line.Split(':');
                if (halfs.Length > 1)
                {
                    string[] values = halfs[1].Split(',');

                    int.TryParse(values[0].Split('=')[1].Trim(), out var x);
                    int.TryParse(values[1].Split('=')[1].Trim(), out var y);
                    Position p = new Position(x, y);

                    return p;
                }

                throw new Exception($"Boarding Size Bad Format: {line}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting info from line: {line}. Details: {ex.ToString()}");
            }
        }
    }
}
