
namespace Bundler.BusinessLogic
{
    public class Range : IRange
    {
        public int From { get; set; }
        public int To { get; set; }

        public bool WithinLowerBounds(int value)
        {
            return this.From <= value && this.To >= value;
        }

        public bool WithinUpperBounds(int value)
        {
            return this.To >= value;
        }
    }
}
