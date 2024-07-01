using System.Collections.Generic;

namespace FlashcardReminder
{
    public class FlashCardsDeck
    {
        public string DeckName;
        public string LanguageCode;
        public bool DeckEnabled;
        public List<FlashCard> Cards;
    }
    public struct FlashCard
    {
        public string Question { get; set; }
        public string Narration { get; set; }
        public string Answer { get; set; }
        public string Examples { get; set; }
    }
}
