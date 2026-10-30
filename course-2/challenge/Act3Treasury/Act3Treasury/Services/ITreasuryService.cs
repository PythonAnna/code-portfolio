using Act3Treasury.Dtos;

namespace Act3Treasury.Services
{
    public interface ITreasuryService
    {
        RestoreResponse Restore(RestoreRequest request);
    }
}
