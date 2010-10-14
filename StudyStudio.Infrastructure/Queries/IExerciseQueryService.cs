using System.Collections.Generic;

namespace StudyStudio.Infrastructure.Queries
{
    public interface IExerciseQueryService
    {
        IList<ExerciseDetails> BrowseExercises();
    }
}