namespace WordPatForm
{
    public class SortedResults {

        public SortedResults(string solution, double frequency)
        {
            Solution = solution;
            Frequency = frequency;
        }
        public string Solution { get; set; }
        public double Frequency { get; set; }
        
    }
}