
using Sanitize.Interface;
using Sanitize.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Sanitize.Repository
{
    public class WordRepository : IWordRepository
    {

        private SystemDB systemDB = SystemDB.getInstance();

        public ResultsModel CreateUpdateNewWord(WordModel model)
        {
            //update
            if(model.Id != 0)
            {
                var data = systemDB.Data.Where(x => x.Id == model.Id).Select(x=>x).FirstOrDefault();
                data.Word = model.Word;

                return new ResultsModel
                {
                    Id = model.Id,
                    Word = model.Word, 
                    Response = "Word Updated"
                };
            }
            else//Create new
            {
                var data = systemDB.getData().Where(x => x.Word.ToLower() == model.Word).FirstOrDefault(); 
                var newModel = new ResultsModel();

                if (data != null)
                {
                    newModel = new ResultsModel
                    {
                        Id = data.Id,
                        Word = data.Word,
                        Response = "Word already in db"
                    };

                    return newModel;
                }
                else
                {

                    var test = systemDB.Data.Last().Id;

                    newModel = new ResultsModel
                    {
                        Id = systemDB.Data.Last().Id + 1,
                        Word = model.Word,
                        Response = model.Word + " Added to db"
                    };

                    systemDB.Data.Add(newModel);

                    return newModel;
                }
            }




        }

        public ResultsModel DeleteWord(int Id)
        {
            
            var record = systemDB.Data.Where(x => x.Id == Id).FirstOrDefault();


            if (record != null)
            {
                var resultsModel = new ResultsModel
                {
                    Id = record.Id,
                    Word = record.Word,
                    Response = "Record Deleted"
                };

                systemDB.Data.Remove(record);

                return resultsModel;
            }
            return new ResultsModel
            {
                Id = record.Id,
                Word = record.Word,
                Response = "Record not Deleted"
            }; ;
        }

        public List<WordModel> GetWords()
        {
            return systemDB.getData();
        }

        public string SanitizeWord(string word)
        {

            var dbWords = systemDB.Data;

            var searchWords = new HashSet<string>(word.Split(" "));
            var newStr = new StringBuilder();
            foreach (var i in searchWords)
            {
                if (dbWords.Select(x => x.Word.ToLower()).Contains(i.ToLower()))
                {
                    var sensitiveWord = dbWords.Where(x => x.Word.ToLower() == i.ToLower()).FirstOrDefault();

                    foreach (char c in sensitiveWord.Word)
                    {
                        newStr.Append("*");
                    }
                    newStr.Append(" ");
                }
                else
                {
                    newStr.Append(i);
                    newStr.Append(" ");
                }
            }

            return newStr.ToString();

        }

    }
}
