using FlashcardReminder.Forms;
using FlashcardReminder.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WindowsFormsApp1.Forms;

namespace FlashcardReminder
{
    public static class Globals
    {
        public static int CardIntervalTimerMilliseconds = Settings.Default.CardIntervalTimerMilliseconds;
        public static int AnswerPrivewTimerMilliseconds = Settings.Default.AnswerPrivewTimerMilliseconds;
        public static int speechRate = Settings.Default.SpeechRate;

        public static string DecksPath => Path.Combine(Application.StartupPath, "Decks");

        public static Timer timer = new Timer() { Interval = CardIntervalTimerMilliseconds };
        public static SettingsWindow settingsForm;
        public static CardsView cardViewForm;
        public static List<FlashCardsDeck> flashCardsDecks;
        static Random random = new Random();

        static Dictionary<FlashCardsDeck, List<FlashCard>> selectedCards = new Dictionary<FlashCardsDeck, List<FlashCard>>();


        public static Dictionary<LanguageCodes, string> Languages = new Dictionary<LanguageCodes, string>()
        {
            { LanguageCodes.Japanese , "ja-JP"},
            { LanguageCodes.Arabic , "ar-AR"},
            { LanguageCodes.English , "en-EN"},
        };
        public static void LoadDecks()
        {
            flashCardsDecks = new List<FlashCardsDeck>();
            try
            {
                var dirs = Directory.EnumerateFiles(DecksPath, "*.json");
                foreach (string file in dirs)
                {
                    string fileContent = File.ReadAllText(file);
                    flashCardsDecks.Add(JsonConvert.DeserializeObject<FlashCardsDeck>(fileContent));
                }
            }
            catch (Exception e)
            {
                Directory.CreateDirectory(DecksPath);
            }

        }
        public static void SaveDecks()
        {
            for (int i = 0; i < flashCardsDecks.Count; i++)
            {
                string dir = Path.Combine(DecksPath, flashCardsDecks[i].DeckName + ".json");
                string content = JsonConvert.SerializeObject(flashCardsDecks[i], Formatting.Indented);
                File.WriteAllText(dir, content);
            }
        }
        public static FlashCard SelectRandomCard(out string languageCode)
        {
            List<FlashCardsDeck> activeDecks = flashCardsDecks.Where((d) => d.DeckEnabled).ToList();
            FlashCardsDeck selectedDeck = activeDecks[random.Next(activeDecks.Count)];

            if (!selectedCards.ContainsKey(selectedDeck)) selectedCards.Add(selectedDeck, new List<FlashCard>());
            if (selectedCards[selectedDeck].Count == selectedDeck.Cards.Count) selectedCards[selectedDeck].Clear();

            languageCode = selectedDeck.LanguageCode;
            var selectableCards = selectedDeck.Cards.Where((sc) => !selectedCards[selectedDeck].Contains(sc)).ToArray();
            var selectedCard = selectableCards[random.Next(selectableCards.Length)];
            selectedCards[selectedDeck].Add(selectedCard);
            return selectedCard;
        }
        public static void PlaySound(string text, string languageCode)
        {

            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(languageCode)) return;

            SpeechSynthesizer synthesizer = new SpeechSynthesizer();

            synthesizer.SpeakCompleted += (sender, e) => {
                synthesizer.Dispose();
            };

            synthesizer.Rate = speechRate;
            
            // Set the language
            synthesizer.SelectVoiceByHints(VoiceGender.NotSet, VoiceAge.NotSet, 0, new CultureInfo(languageCode));

            // Configure the audio output
            synthesizer.SetOutputToDefaultAudioDevice();

            // Speak the text asynchronously
            synthesizer.SpeakAsync(text);
        }

        public static string GetFile(string title, string searchFilter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = searchFilter;
            openFileDialog.Title = title;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
                return openFileDialog.FileName;
            
            return null;
        }
        public static void OpenJsonFile(string inputFile)
        {
            string fileContent;
            try
            {
                fileContent = File.ReadAllText(inputFile);
            }
            catch (Exception e)
            {
                throw e;
            }

            FlashCardsDeck deck;
            try
            {
                deck = JsonConvert.DeserializeObject<FlashCardsDeck>(fileContent);
            }
            catch (Exception e)
            {
                throw e;
            }

            try
            {
                File.WriteAllText(Path.Combine(DecksPath, $"{deck.DeckName}.json"), JsonConvert.SerializeObject(deck, Formatting.Indented));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void OpenTxtFile(string inputFile, string LanguageCode)
        {
            string fileContent;
            try
            {
                fileContent = File.ReadAllText(inputFile);
            }
            catch (Exception e)
            {
                throw e;
            }

            string[] pathSegs = inputFile.Split('\\');
            string fileName = pathSegs[pathSegs.Length - 1].Split('.')[0];
            FlashCardsDeck deck = new FlashCardsDeck()
            {
                DeckName = fileName,
                DeckEnabled = false,
                LanguageCode = LanguageCode,
            };

            List<FlashCard> cards = new List<FlashCard>();

            string[] allLines = fileContent.Split(Environment.NewLine.ToCharArray());

            string[] lines = allLines.Where((l) => !l.StartsWith("#") && !string.IsNullOrEmpty(l) && !string.IsNullOrWhiteSpace(l)).ToArray();
            foreach (string line in lines)
            {
                string[] segments = @line.Split('\t');
                FlashCard flashCard = new FlashCard()
                {
                    Question = segments[0],
                    Narration = segments[1],
                    Answer = string.Join(" | ", GetWordsFromLine(segments[2])),
                    Examples = segments[3],
                };
                cards.Add(flashCard);
            }
            deck.Cards = cards;

            try
            {
                File.WriteAllText(Path.Combine(DecksPath, $"{deck.DeckName}.json"), JsonConvert.SerializeObject(deck, Formatting.Indented));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        static string[] GetWordsFromLine(string input)
        {
            List<string> parts = input.Split(';').ToList();

            for (int i = 0; i < parts.Count; i++)
            {
                string pattern = @"\([^)]*\)";
                string cleanedString = Regex.Replace(parts[i], pattern, "");

                // Remove numbers, Japanese letters, and non-alphabetic characters except spaces
                cleanedString = Regex.Replace(cleanedString, @"[^\x00-\x7F]", "");  // Remove non-ASCII characters
                cleanedString = Regex.Replace(cleanedString, @"[^a-zA-Z\s]", "");  // Remove non-alphabetic characters except spaces

                while (cleanedString.StartsWith(" ")) cleanedString = cleanedString.Remove(0, 1);

                parts[i] = cleanedString;
            }

            return parts.Where((p) => !string.IsNullOrWhiteSpace(p)).ToArray();
        }
    }

    public enum LanguageCodes
    {
        Japanese,
        English,
        Arabic
    }
}
