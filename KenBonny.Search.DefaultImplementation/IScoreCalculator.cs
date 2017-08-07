using KenBonny.Search.Core.Queries;
using KenBonny.Search.Core.ReadModel;

namespace KenBonny.Search.DefaultImplementation
{
    public interface IScoreCalculator
    {
        int CalculateScore(Seat seat, SearchQuery query);
    }
}