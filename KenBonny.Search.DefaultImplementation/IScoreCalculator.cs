using KenBonny.Search.Core.Queries;
using KenBonny.Search.DefaultImplementation.ReadModel;

namespace KenBonny.Search.DefaultImplementation
{
    public interface IScoreCalculator
    {
        int CalculateScore(ISeat seat, SearchQuery query);
    }
}