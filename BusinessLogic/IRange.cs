
namespace Bundler.BusinessLogic
{
    public interface IRange
    {
        int From { get; set; }
        int To { get; set; }

        bool WithinLowerBounds(int value);
        bool WithinUpperBounds(int value);
    }
}
