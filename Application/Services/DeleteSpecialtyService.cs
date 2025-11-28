using Application.Interfaces;

namespace Application.Services
{
    public class DeleteSpecialtyService : IDeleteSpecialtyService
    {
        private readonly ISpecialtyCommand _specialtyCommand;

        public DeleteSpecialtyService(ISpecialtyCommand specialtyCommand)
        {
            _specialtyCommand = specialtyCommand;
        }

        public async Task<bool> DeleteSpecialtyAsync(long id)
        {
            return await _specialtyCommand.DeleteAsync(id);
        }
    }
}




