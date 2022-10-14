using Sanitize.Models;

namespace Sanitize.Interface
{
    public interface IWordRepository
    {
        public List<WordModel> GetWords();
        public ResultsModel CreateUpdateNewWord(WordModel model);
        public ResultsModel DeleteWord(int id);
        public string SanitizeWord(string word);
    }
}
