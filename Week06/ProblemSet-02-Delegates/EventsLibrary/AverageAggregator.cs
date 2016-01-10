namespace EventsLibrary
{
    public delegate void AverageChange(object sender, decimal oldAverage, decimal newAverage);

    public class AverageAggregator
    {
        private decimal sum;
        private int numbersCount;
        private decimal average;
        public decimal Average { get { return average; } }
        public static event AverageChange AverageChanged;

        public AverageAggregator(AverageChange changeDelegate)
        {
            sum = 0;
            average = 0;
            AverageChanged += changeDelegate;
        }

        public void AddNumber(int number)
        {
            decimal lastAverage = average;
            sum += number;
            average = sum / ++numbersCount;
            if (lastAverage != average) AverageChanged(this, lastAverage, average);
        }
    }
}
