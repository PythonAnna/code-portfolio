using Act3Treasury.Dtos;

namespace Act3Treasury.Services
{
    public class TreasuryService : ITreasuryService
    {
        public RestoreResponse Restore(RestoreRequest request)
        {
            for (int i = 0; i < request.Reference.Length; i++)
            {
                if (request.Reference[i] < 'A' || request.Reference[i] > 'Z')
                    throw new Exception("Код должен содеражать только заглавные латинские буквы!");
            }

            for (int i = 0; i < request.Damaged.Length; i++)
            {
                bool found = false;
                for (int j = 0; j < request.Reference.Length; j++)
                {
                    if (request.Reference[j] == request.Damaged[i])
                    {
                        found = true;
                        break;
                    }
                }
                if (found == false)
                    throw new Exception("Поврежденный код содержит символы, которых нет в эталонном!");
            }

            List<char> missingList = new List<char>();

            for(int i = 0; i < request.Reference.Length; i++)
            {
                bool found = false;
                for(int j = 0; j < request.Damaged.Length; j++)
                {
                    if (request.Reference[i] == request.Damaged[j])
                    {
                        found = true;
                        break;
                    }
                }
                if(found == false)
                {
                    missingList.Add(request.Reference[i]);
                }
            }

            missingList.Sort();
            string missing = string.Concat(missingList);
            string message = "";
            if (missing.Length == 0)
                message = "Созвездия синхронизированы. Дверь Сокровищницы открыта!";
            else
                message = "Узор повреждён. Не хватает звёзд для открытия.";


                return new RestoreResponse
                {
                    MissingSymbols = missing,
                    IsComplete = missing.Length == 0,
                    Message = message
                };
        }
    }
}
