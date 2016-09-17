namespace WordPatForm
{
    public class CharacterFrequency
    {
        public CharacterFrequency() {}

        public CharacterFrequency(char character, double frequency)
        {
            Character = character;
            Frequency = frequency;
        }

        public char Character { get; set; }
        public double Frequency { get; set; }
    }
}